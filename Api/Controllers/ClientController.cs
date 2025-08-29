using Domain.DTO.Requests;
using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.Services.Transactional;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IClientServiceTransactional _clientServiceTransactional;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, IClientServiceTransactional clientServiceTransactional, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _clientServiceTransactional = clientServiceTransactional;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAll()
        {
            try
            {
                var clients = await _clientService.GetAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los clientes.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Client>>> GetByName([FromQuery] string name)
        {
            try
            {
                var clients = await _clientService.GetByNameAsync(name);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al filtrar clientes por nombre: {Name}", name);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpGet("{clientSeq}")]
        public async Task<ActionResult<Client>> GetByClientSeq(int clientSeq)
        {
            try
            {
                var client = await _clientService.GetByClientSeqAsync(clientSeq);
                if (client == null)
                {
                    _logger.LogWarning("Cliente con ClientSeq {ClientSeq} no encontrado.", clientSeq);
                    return NotFound($"Cliente con ClientSeq {clientSeq} no encontrado.");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el cliente con ClientSeq: {ClientSeq}", clientSeq);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Insert([FromBody] ClientInsertRequest request)
        {
            try
            {
                var client = new Client
                {
                    Name = request.Name,
                    Email = request.Email,
                    Active = true
                };

                var insertedClient = await _clientServiceTransactional.InsertAsync(client);
                return CreatedAtAction(nameof(GetByClientSeq), new { clientSeq = insertedClient.ClientSeq }, insertedClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar un nuevo cliente.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Client>> Update([FromBody] ClientUpdateRequest request)
        {
            try
            {
                var client = new Client
                {
                    ClientSeq = request.ClientSeq,
                    Name = request.Name,
                    Email = request.Email,
                    Active = true
                };

                var updatedClient = await _clientServiceTransactional.UpdateAsync(client);
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente con ClientSeq: {ClientSeq}", request.ClientSeq);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete("{clientSeq}")]
        public async Task<ActionResult> Inactivate(int clientSeq)
        {
            try
            {
                var result = await _clientServiceTransactional.InactivateAsync(clientSeq);
                if (!result)
                {
                    _logger.LogWarning("Cliente con ClientSeq {ClientSeq} no encontrado para inactivar.", clientSeq);
                    return NotFound($"Cliente con ClientSeq {clientSeq} no encontrado.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al inactivar el cliente con ClientSeq: {ClientSeq}", clientSeq);
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}