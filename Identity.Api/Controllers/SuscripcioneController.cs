using Identity.Api.Interfaces;
using Identity.Api.Repositories;
using Microsoft.Extensions.Logging;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class SuscripcionService : ISuscripcione
    {
        private readonly SuscripcionRepository _suscripcionRepository;
        private readonly ILogger<SuscripcionService> _logger;

        public SuscripcionService(SuscripcionRepository suscripcionRepository, ILogger<SuscripcionService> logger)
        {
            _suscripcionRepository = suscripcionRepository ?? throw new ArgumentNullException(nameof(suscripcionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Métodos CRUD

        public async Task<IEnumerable<Suscripcione>> SuscripcionesAll()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las suscripciones");
                return await _suscripcionRepository.SuscripcionesAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las suscripciones");
                throw;
            }
        }

        public async Task<Suscripcione> GetSuscripcionById(int idSuscripcion)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener suscripción por ID");
                throw;
            }
        }

        public async Task InsertSuscripcion(Suscripcione n
