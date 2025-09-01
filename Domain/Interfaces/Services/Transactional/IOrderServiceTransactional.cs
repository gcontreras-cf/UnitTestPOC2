using Domain.DTO.Requests;
using Domain.DTO.Responses;

namespace Domain.Interfaces.Services.Transactional
{
    public interface IOrderServiceTransactional
    {
        Task<OrderCreateResponse> CreateOrderAsync(OrderCreateRequest request);
        Task<IEnumerable<TodayOrderResponse>> GetTodayOrdersAsync();
    }
}