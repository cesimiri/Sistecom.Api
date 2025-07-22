using Identity.Api.Interfaces;
using Identity.Api.Repositories;
using Microsoft.Extensions.Logging;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
<<<<<<< HEAD
    public class SuscripcionService : ISuscripcione
=======
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuscripcioneController : Controller
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
    {
        private readonly SuscripcionRepository _suscripcionRepository;
        private readonly ILogger<SuscripcionService> _logger;

        public SuscripcionService(SuscripcionRepository suscripcionRepository, ILogger<SuscripcionService> logger)
        {
            _suscripcionRepository = suscripcionRepository ?? throw new ArgumentNullException(nameof(suscripcionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

<<<<<<< HEAD
        #region Métodos CRUD

        public async Task<IEnumerable<Suscripcione>> SuscripcionesAll()
        {
            try
=======
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("SuscripcionesAll")]
        public IActionResult GetAll()
        {
            return Ok(_suscripcioneService.SuscripcionesAll);
        }

        [HttpGet("GetSuscripcionById/{idSuscripcion}")]
        public IActionResult GetById(int idSuscripcion)
        {
            var suscripcion = _suscripcioneService.GetSuscripcionById(idSuscripcion);

            if (suscripcion == null)
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
            {
                _logger.LogInformation("Obteniendo todas las suscripciones");
                return await _suscripcionRepository.SuscripcionesAll();
            }
<<<<<<< HEAD
            catch (Exception ex)
=======

            return Ok(suscripcion);
        }

        [HttpPost("InsertSuscripcion")]
        public IActionResult Insert([FromBody] Suscripcione newSuscripcion)
        {
            if (newSuscripcion == null || !ModelState.IsValid)
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
            {
                _logger.LogError(ex, "Error al obtener todas las suscripciones");
                throw;
            }
<<<<<<< HEAD
        }

        public async Task<Suscripcione> GetSuscripcionById(int idSuscripcion)
=======

            _suscripcioneService.InsertSuscripcion(newSuscripcion);
            return Ok(newSuscripcion);
        }

        [HttpPut("UpdateSuscripcion")]
        public IActionResult Update([FromBody] Suscripcione updatedSuscripcion)
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        {
            if (idSuscripcion <= 0)
                throw new ArgumentException("El ID de suscripción debe ser mayor a cero", nameof(idSuscripcion));

            try
            {
                _logger.LogInformation("Obteniendo suscripción con ID: {Id}", idSuscripcion);
                var suscripcion = await _suscripcionRepository.GetSuscripcionById(idSuscripcion);
                if (suscripcion == null)
                    throw new InvalidOperationException($"No se encontró la suscripción con ID {idSuscripcion}");

                return suscripcion;
            }
<<<<<<< HEAD
            catch (Exception ex)
=======

            _suscripcioneService.UpdateSuscripcion(updatedSuscripcion);
            return NoContent();
        }

        [HttpDelete("DeleteSuscripcion")]
        public IActionResult Delete([FromBody] Suscripcione suscripcionToDelete)
        {
            if (suscripcionToDelete == null || !ModelState.IsValid)
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
            {
                _logger.LogError(ex, "Error al obtener suscripción por ID");
                throw;
            }
<<<<<<< HEAD
        }

        public async Task InsertSuscripcion(Suscripcione n
=======

            _suscripcioneService.DeleteSuscripcion(suscripcionToDelete);
            return NoContent();
        }

        [HttpDelete("DeleteSuscripcion/{idSuscripcion}")]
        public IActionResult DeleteById(int idSuscripcion)
        {
            _suscripcioneService.DeleteSuscripcionById(idSuscripcion);
            return NoContent();
        }
    }
}
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
