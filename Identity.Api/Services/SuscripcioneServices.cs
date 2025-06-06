using Identity.Api.Interfaces;
using Identity.Api.Repositories;
using Microsoft.Extensions.Logging;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class SuscripcionService : ISuscripcione
    {
        private readonly SuscripcionRepository _suscripcionRepository; // ✅ CORREGIDO: Ahora usa Repository
        private readonly ILogger<SuscripcionService> _logger;

        public SuscripcionService(
            SuscripcionRepository suscripcionRepository, // ✅ CORREGIDO
            ILogger<SuscripcionService> logger)
        {
            _suscripcionRepository = suscripcionRepository;
            _logger = logger;
        }

<<<<<<< HEAD
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
=======
        public IEnumerable<Suscripcione> SuscripcionesAll
        {
            get { return _dataRepository.GetAllSuscripciones(); }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public Suscripcione GetSuscripcionById(int idSuscripcion)
        {
<<<<<<< HEAD
            if (idSuscripcion <= 0)
            {
                throw new ArgumentException("El ID de suscripción debe ser mayor a cero", nameof(idSuscripcion));
            }

            try
            {
                _logger.LogInformation("Obteniendo suscripción con ID: {IdSuscripcion}", idSuscripcion);
                return await _suscripcionRepository.GetSuscripcionById(idSuscripcion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener suscripción con ID: {IdSuscripcion}", idSuscripcion);
                throw;
            }
=======
            return _dataRepository.GetSuscripcionById(idSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void InsertSuscripcion(Suscripcione newSuscripcion)
        {
<<<<<<< HEAD
            try
            {
                // Validaciones de negocio
                await ValidateSuscripcionBusinessRules(newSuscripcion);

                // Validar que existan el proveedor y la empresa
                var proveedor = await _suscripcionRepository.GetProveedorByIdAsync(newSuscripcion.IdProveedor);
                var empresa = await _suscripcionRepository.GetEmpresaByIdAsync(newSuscripcion.IdEmpresa);

                if (proveedor == null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {newSuscripcion.IdProveedor} no existe");
                }

                if (empresa == null)
                {
                    throw new InvalidOperationException($"La empresa con ID {newSuscripcion.IdEmpresa} no existe");
                }

                // Establecer valores por defecto
                newSuscripcion.FechaRegistro ??= DateTime.UtcNow;
                newSuscripcion.Estado ??= "Activa";

                _logger.LogInformation("Insertando nueva suscripción para empresa {IdEmpresa}", newSuscripcion.IdEmpresa);
                await _suscripcionRepository.InsertSuscripcion(newSuscripcion);

                _logger.LogInformation("Suscripción insertada exitosamente con ID: {IdSuscripcion}", newSuscripcion.IdSuscripcion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar suscripción");
                throw;
            }
=======
            _dataRepository.InsertSuscripcion(newSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
<<<<<<< HEAD
            try
            {
                // Validaciones de negocio
                await ValidateSuscripcionBusinessRules(updatedSuscripcion);

                // Validar que la suscripción existe
                var existingSuscripcion = await _suscripcionRepository.GetSuscripcionById(updatedSuscripcion.IdSuscripcion);
                if (existingSuscripcion == null)
                {
                    throw new InvalidOperationException($"La suscripción con ID {updatedSuscripcion.IdSuscripcion} no existe");
                }

                // Validar que existan el proveedor y la empresa
                var proveedor = await _suscripcionRepository.GetProveedorByIdAsync(updatedSuscripcion.IdProveedor);
                var empresa = await _suscripcionRepository.GetEmpresaByIdAsync(updatedSuscripcion.IdEmpresa);

                if (proveedor == null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {updatedSuscripcion.IdProveedor} no existe");
                }

                if (empresa == null)
                {
                    throw new InvalidOperationException($"La empresa con ID {updatedSuscripcion.IdEmpresa} no existe");
                }

                _logger.LogInformation("Actualizando suscripción con ID: {IdSuscripcion}", updatedSuscripcion.IdSuscripcion);
                await _suscripcionRepository.UpdateSuscripcion(updatedSuscripcion);

                _logger.LogInformation("Suscripción actualizada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar suscripción con ID: {IdSuscripcion}", updatedSuscripcion.IdSuscripcion);
                throw;
            }
=======
            _dataRepository.UpdateSuscripcion(updatedSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
<<<<<<< HEAD
            if (suscripcionToDelete?.IdSuscripcion <= 0)
            {
                throw new ArgumentException("Suscripción inválida para eliminar");
            }

            try
            {
                _logger.LogInformation("Eliminando suscripción con ID: {IdSuscripcion}", suscripcionToDelete.IdSuscripcion);
                await _suscripcionRepository.DeleteSuscripcion(suscripcionToDelete);

                _logger.LogInformation("Suscripción eliminada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar suscripción");
                throw;
            }
=======
            _dataRepository.DeleteSuscripcion(suscripcionToDelete);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void DeleteSuscripcionById(int idSuscripcion)
        {
<<<<<<< HEAD
            if (idSuscripcion <= 0)
            {
                throw new ArgumentException("El ID de suscripción debe ser mayor a cero", nameof(idSuscripcion));
            }

            try
            {
                _logger.LogInformation("Eliminando suscripción con ID: {IdSuscripcion}", idSuscripcion);
                await _suscripcionRepository.DeleteSuscripcionById(idSuscripcion);

                _logger.LogInformation("Suscripción eliminada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar suscripción con ID: {IdSuscripcion}", idSuscripcion);
                throw;
            }
        }

        // Métodos de consulta para entidades relacionadas
        public async Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync()
        {
            try
            {
                return await _suscripcionRepository.GetEmpresaClienteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener empresas cliente");
                throw;
            }
        }

        public async Task<IEnumerable<Proveedore>> GetProveedoreAsync()
        {
            try
            {
                return await _suscripcionRepository.GetProveedoreAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener proveedores");
                throw;
            }
=======
            _dataRepository.DeleteSuscripcionById(idSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public async Task<Proveedore?> GetProveedorByIdAsync(int idProveedor)
        {
            if (idProveedor <= 0)
            {
                throw new ArgumentException("El ID del proveedor debe ser mayor a cero", nameof(idProveedor));
            }

            return await _suscripcionRepository.GetProveedorByIdAsync(idProveedor);
        }

        public async Task<EmpresasCliente?> GetEmpresaByIdAsync(int idEmpresa)
        {
            if (idEmpresa <= 0)
            {
                throw new ArgumentException("El ID de la empresa debe ser mayor a cero", nameof(idEmpresa));
            }

            return await _suscripcionRepository.GetEmpresaByIdAsync(idEmpresa);
        }

        #region Validaciones de Negocio

        private async Task ValidateSuscripcionBusinessRules(Suscripcione suscripcion)
        {
            var errors = new List<string>();

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(suscripcion.NombreServicio))
            {
                errors.Add("El nombre del servicio es requerido");
            }

            if (suscripcion.IdProveedor <= 0)
            {
                errors.Add("Debe especificar un proveedor válido");
            }

            if (suscripcion.IdEmpresa <= 0)
            {
                errors.Add("Debe especificar una empresa válida");
            }

            if (suscripcion.CostoPeriodo < 0)
            {
                errors.Add("El costo del período no puede ser negativo");
            }

            // Validación de fechas
            if (suscripcion.FechaRenovacion <= suscripcion.FechaInicio)
            {
                errors.Add("La fecha de renovación debe ser posterior a la fecha de inicio");
            }

            // Validación de usuarios incluidos
            if (suscripcion.UsuariosIncluidos.HasValue && suscripcion.UsuariosIncluidos <= 0)
            {
                errors.Add("El número de usuarios incluidos debe ser mayor a cero");
            }

            // Validación de almacenamiento
            if (suscripcion.AlmacenamientoGb.HasValue && suscripcion.AlmacenamientoGb <= 0)
            {
                errors.Add("El almacenamiento debe ser mayor a cero");
            }

            // Validación de URL (si se proporciona)
            if (!string.IsNullOrWhiteSpace(suscripcion.UrlAcceso))
            {
                if (!Uri.TryCreate(suscripcion.UrlAcceso, UriKind.Absolute, out _))
                {
                    errors.Add("La URL de acceso no tiene un formato válido");
                }
            }

            if (errors.Any())
            {
                throw new ArgumentException($"Errores de validación: {string.Join(", ", errors)}");
            }

            await Task.CompletedTask; // Para mantener async
        }

        #endregion
    }
}