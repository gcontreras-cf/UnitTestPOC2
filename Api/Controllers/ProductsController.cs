using Domain.DTO.Requests;
using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.Services.Transactional;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductServiceTransactional _productServiceTransactional;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IProductServiceTransactional productServiceTransactional, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _productServiceTransactional = productServiceTransactional;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByName([FromQuery] string name)
        {
            try
            {
                var products = await _productService.GetByNameAsync(name);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering products by name: {Name}", name);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{productSeq}")]
        public async Task<ActionResult<Product>> GetByProductSeq(int productSeq)
        {
            try
            {
                var product = await _productService.GetByProductSeqAsync(productSeq);
                if (product == null)
                {
                    _logger.LogWarning("Product with ProductSeq {ProductSeq} not found.", productSeq);
                    return NotFound($"Product with ProductSeq {productSeq} not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product with ProductSeq: {ProductSeq}", productSeq);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Insert([FromBody] ProductInsertRequest request)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    Active = true
                };

                var insertedProduct = await _productServiceTransactional.InsertAsync(product);
                return CreatedAtAction(nameof(GetByProductSeq), new { productSeq = insertedProduct.ProductSeq }, insertedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting a new product.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Update([FromBody] ProductUpdateRequest request)
        {
            try
            {
                var product = new Product
                {
                    ProductSeq = request.ProductSeq,
                    Name = request.Name,
                    Price = request.Price,
                    Active = true
                };

                var updatedProduct = await _productServiceTransactional.UpdateAsync(product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ProductSeq: {ProductSeq}", request.ProductSeq);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{productSeq}")]
        public async Task<ActionResult> Inactivate(int productSeq)
        {
            try
            {
                var result = await _productServiceTransactional.InactivateAsync(productSeq);
                if (!result)
                {
                    _logger.LogWarning("Product with ProductSeq {ProductSeq} not found for inactivation.", productSeq);
                    return NotFound($"Product with ProductSeq {productSeq} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inactivating product with ProductSeq: {ProductSeq}", productSeq);
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
