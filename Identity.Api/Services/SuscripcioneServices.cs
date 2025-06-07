using Identity.Api.Interfaces;
using Identity.Api.Repositories;
using Microsoft.Extensions.Logging;
using Modelo.Sistecom.Modelo.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Services
{
    public class SuscripcionService : ISuscripcione
    {
        private readonly SuscripcionRepository _suscripcionRepository;
        private readonly ILogger<SuscripcionService> _logger;

        public SuscripcionService(SuscripcionRepository suscripcionRepository, ILogger<SuscripcionService> logger)
        {
            _suscripcionRepository = suscripcionRepository;
            _logger = logger;
        }

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

        public async Task<Suscripcione?> GetSuscripcionById(int idSuscripcion)
        {
            if (idSuscripcion <= 0)
                throw new ArgumentException("El ID de suscripción debe ser mayor a cero", nameof(idSuscripcion));

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
        }

        public async Task InsertSuscripcion(Suscripcione newSuscripcion)
        {
            try
            {
                await ValidateSuscripcionBusinessRules(newSuscripcion);

                var proveedor = await _suscripcionRepository.GetProveedorByIdAsync(newSuscripcion.IdProveedor);
                var empresa = await _suscripcionRepository.GetEmpresaByIdAsync(newSuscripcion.IdEmpresa);

                if (proveedor == null)
                    throw new InvalidOperationException($"El proveedor con ID {newSuscripcion.IdProveedor} no existe");

                if (empresa == null)
                    throw new InvalidOperationException($"La empresa con ID {newSuscripcion.IdEmpresa} no existe");

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
        }

        public async Task UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            try
            {
                await ValidateSuscripcionBusinessRules(updatedSuscripcion);

                var existingSuscripcion = await _suscripcionRepository.GetSuscripcionById(updatedSuscripcion.IdSuscripcion);
                if (existingSuscripcion == null)
                    throw new InvalidOperationException($"La suscripción con ID {updatedSuscripcion.IdSuscripcion} no existe");

                var proveedor = await _suscripcionRepository.GetProveedorByIdAsync(updatedSuscripcion.IdProveedor);
                var empresa = await _suscripcionRepository.GetEmpresaByIdAsync(updatedSuscripcion.IdEmpresa);

                if (proveedor == null)
                    throw new InvalidOperationException($"El proveedor con ID {updatedSuscripcion.IdProveedor} no existe");

                if (empresa == null)
                    throw new InvalidOperationException($"La empresa con ID {updatedSuscripcion.IdEmpresa} no existe");

                _logger.LogInformation("Actualizando suscripción con ID: {IdSuscripcion}", updatedSuscripcion.IdSuscripcion);
                await _suscripcionRepository.UpdateSuscripcion(updatedSuscripcion);
                _logger.LogInformation("Suscripción actualizada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar suscripción con ID: {IdSuscripcion}", updatedSuscripcion.IdSuscripcion);
                throw;
            }
        }

        public async Task DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            if (suscripcionToDelete?.IdSuscripcion <= 0)
                throw new ArgumentException("Suscripción inválida para eliminar");

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
        }

        public async Task DeleteSuscripcionById(int idSuscripcion)
        {
            if (idSuscripcion <= 0)
                throw new ArgumentException("El ID de suscripción debe ser mayor a cero", nameof(idSuscripcion));

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

        #region Consultas entidades relacionadas

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
        }

        public async Task<Proveedore?> GetProveedorByIdAsync(int idProveedor)
        {
            if (idProveedor <= 0)
                throw new ArgumentException("El ID del proveedor debe ser mayor a cero", nameof(idProveedor));

            return await _suscripcionRepository.GetProveedorByIdAsync(idProveedor);
        }

        public async Task<EmpresasCliente?> GetEmpresaByIdAsync(int idEmpresa)
        {
            if (idEmpresa <= 0)
                throw new ArgumentException("El ID de la empresa debe ser mayor a cero", nameof(idEmpresa));

            return await _suscripcionRepository.GetEmpresaByIdAsync(idEmpresa);
        }

        #endregion

        #region Validaciones de Negocio

        private async Task ValidateSuscripcionBusinessRules(Suscripcione suscripcion)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(suscripcion.NombreServicio))
                errors.Add("El nombre del servicio es requerido");

            if (suscripcion.IdProveedor <= 0)
                errors.Add("Debe especificar un proveedor válido");

            if (suscripcion.IdEmpresa <= 0)
                errors.Add("Debe especificar una empresa válida");

            if (suscripcion.CostoPeriodo < 0)
                errors.Add("El costo del período no puede ser negativo");

            if (suscripcion.FechaRenovacion <= suscripcion.FechaInicio)
                errors.Add("La fecha de renovación debe ser posterior a la fecha de inicio");

            if (suscripcion.UsuariosIncluidos.HasValue && suscripcion.UsuariosIncluidos <= 0)
                errors.Add("El número de usuarios incluidos debe ser mayor a cero");

            if (suscripcion.AlmacenamientoGb.HasValue && suscripcion.AlmacenamientoGb <= 0)
                errors.Add("El almacenamiento debe ser mayor a cero");

            if (!string.IsNullOrWhiteSpace(suscripcion.UrlAcceso) &&
                !Uri.TryCreate(suscripcion.UrlAcceso, UriKind.Absolute, out _))
            {
                errors.Add("La URL de acceso no tiene un formato válido");
            }

            if (errors.Count > 0)
                throw new ArgumentException($"Errores de validación: {string.Join(", ", errors)}");

            await Task.CompletedTask;
        }

        #endregion
    }
}
