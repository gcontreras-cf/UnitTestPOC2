using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.Services.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;

namespace Core.Service.Transactional
{
    public class OrderServiceTransactional : IOrderServiceTransactional
    {
        private readonly IOrderUnitOfWorkTransactional _unitOfWork;
        private readonly IClientService _clientService;
        private readonly IProductService _productService;

        public OrderServiceTransactional(
            IOrderUnitOfWorkTransactional unitOfWork,
            IClientService clientService,
            IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _clientService = clientService;
            _productService = productService;
        }

        public async Task<OrderCreateResponse> CreateOrderAsync(OrderCreateRequest request)
        {
            // Validate client
            var client = await _clientService.GetByClientSeqAsync(request.ClientSeq);
            if (client == null)
            {
                return new OrderCreateResponse { Success = false, Message = "Client not found." };
            }

            // Validate products and calculate total amount
            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();
            foreach (var detail in request.OrderDetails)
            {
                var product = await _productService.GetByProductSeqAsync(detail.ProductSeq);
                if (product == null)
                {
                    return new OrderCreateResponse { Success = false, Message = $"Product with ID {detail.ProductSeq} not found." };
                }

                var orderDetail = new OrderDetail
                {
                    ProductSeq = product.ProductSeq,
                    Quantity = detail.Quantity,
                    UnitPrice = product.Price,
                    Active = true
                };
                totalAmount += detail.Quantity * product.Price;
                orderDetails.Add(orderDetail);
            }

            // Create order
            var order = new Order
            {
                ClientSeq = request.ClientSeq,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Active = true,
                OrderDetails = orderDetails
            };

            // Save order and details
            try
            {
                await _unitOfWork.OrderRepositoryTransactional.InsertAsync(order);
                await _unitOfWork.SaveChangesAsync();
                return new OrderCreateResponse { Success = true, Message = "Order created successfully." };
            }
            catch
            {
                return new OrderCreateResponse { Success = false, Message = "Failed to create order." };
            }
        }

        public async Task<IEnumerable<TodayOrderResponse>> GetTodayOrdersAsync()
        {
            var today = DateTime.UtcNow.Date;
            var orders = await _unitOfWork.OrderRepositoryTransactional.GetOrdersByDateAsync(today);

            return orders.Select(order => new TodayOrderResponse
            {
                ClientName = order.Client.Name,
                OrderDetails = order.OrderDetails.Select(detail => new TodayOrderDetailResponse
                {
                    ProductName = detail.Product.Name,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                }).ToList()
            });
        }
    }
}