using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.DataRepository
{
    public class SolicitudesCompraDataRepository
    {
        public List<SolicitudesCompraDTO> GetAllSolicitudesCompra()
        {
            using var context = new InvensisContext();
            return context.SolicitudesCompras
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.IdUsuarioSolicitaNavigation)
                .Include(s => s.IdDepartamentoNavigation)


                .Select(s => new SolicitudesCompraDTO
                {
                    NumeroSolicitud = s.NumeroSolicitud,
                    IdSolicitud = s.IdSolicitud,
                    RucEmpresa = s.RucEmpresa,
                    IdDepartamento = s.IdDepartamento,
                    IdUsuarioSolicita = s.IdUsuarioSolicita,
                    IdUsuarioAutoriza = s.IdUsuarioAutoriza,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaAprobacion = s.FechaAprobacion,
                    FechaRequerida = s.FechaRequerida.HasValue
                    ? new DateTime(s.FechaRequerida.Value.Year, s.FechaRequerida.Value.Month, s.FechaRequerida.Value.Day)
                    : DateTime.MinValue,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    Justificacion = s.Justificacion,
                    Prioridad = s.Prioridad,
                    MotivoRechazo = s.MotivoRechazo,
                    Observaciones = s.Observaciones,
                    ArchivoOc = s.ArchivoOc,
                    Estado = s.Estado,

                    // campos relacionados:
                    RazonSocial = s.RucEmpresaNavigation.RazonSocial,
                    NombreSolicitanteCompleto = s.IdUsuarioSolicitaNavigation.Nombres + " " + s.IdUsuarioSolicitaNavigation.Apellidos,
                    NombreAutorizadorCompleto = s.IdUsuarioAutorizaNavigation.Nombres + " " + s.IdUsuarioAutorizaNavigation.Apellidos,
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento
                })
                .ToList();
        }

        public SolicitudesCompraDTO GetSolicitudById(int idSolicitud)
        {
            using var context = new InvensisContext();

            return context.SolicitudesCompras
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.IdUsuarioSolicitaNavigation)
                .Include(s => s.IdUsuarioAutorizaNavigation)
                .Include(s => s.IdDepartamentoNavigation)


                .Where(s => s.IdSolicitud == idSolicitud)
                .Select(s => new SolicitudesCompraDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    NumeroSolicitud =s.NumeroSolicitud,
                    RucEmpresa = s.RucEmpresa,
                    IdDepartamento = s.IdDepartamento,
                    IdUsuarioSolicita = s.IdUsuarioSolicita,
                    IdUsuarioAutoriza = s.IdUsuarioAutoriza,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaAprobacion = s.FechaAprobacion,
                    //FechaRequerida = s.FechaRequerida,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    Justificacion = s.Justificacion,
                    Prioridad = s.Prioridad,
                    MotivoRechazo = s.MotivoRechazo,
                    Observaciones = s.Observaciones,
                    ArchivoOc = s.ArchivoOc,
                    Estado = s.Estado,

                    // campos relacionados:
                    RazonSocial = s.RucEmpresaNavigation.RazonSocial,
                    NombreSolicitanteCompleto = s.IdUsuarioSolicitaNavigation.Nombres + " " + s.IdUsuarioSolicitaNavigation.Apellidos,
                    NombreAutorizadorCompleto = s.IdUsuarioAutorizaNavigation.Nombres + " " + s.IdUsuarioAutorizaNavigation.Apellidos,
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento
                })
                .FirstOrDefault();
        }

  

        public void InsertSolicitud(SolicitudesCompraDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                var empresa = context.EmpresasClientes.Find(dto.RucEmpresa);
                var usuarioSolicita = context.Usuarios.Find(dto.IdUsuarioSolicita);
                var usuarioAutoriza = context.Usuarios.Find(dto.IdUsuarioAutoriza);
                var departamento = context.Departamentos.Find(dto.IdDepartamento);

                if (empresa == null || usuarioSolicita == null || usuarioAutoriza == null || departamento == null)
                {
                    throw new Exception("Esa categoria no existe en la base de datos.");
                }

                // Generar el NumeroSolicitud automático
                var year = DateTime.Now.Year;
                // Obtener la última solicitud para este año para sacar el siguiente número
                var lastNumero = context.SolicitudesCompras
                    .Where(s => s.NumeroSolicitud.StartsWith($"SC-{year}"))
                    .OrderByDescending(s => s.NumeroSolicitud)
                    .Select(s => s.NumeroSolicitud)
                    .FirstOrDefault();

                int nextNumber = 1;
                if (lastNumero != null)
                {
                    // Ejemplo: SC-2025-0005
                    var lastNumberStr = lastNumero.Split('-').Last();
                    if (int.TryParse(lastNumberStr, out var lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
                var nuevoNumeroSolicitud = $"SC-{year}-{nextNumber:D4}";

                var nueva = new SolicitudesCompra
                {
                    NumeroSolicitud = nuevoNumeroSolicitud, // Aquí asignas el nuevo número generado
                    RucEmpresa = dto.RucEmpresa,
                    IdDepartamento = dto.IdDepartamento,
                    IdUsuarioSolicita = dto.IdUsuarioSolicita,
                    IdUsuarioAutoriza = dto.IdUsuarioAutoriza,
                    FechaSolicitud = dto.FechaSolicitud,
                    FechaAprobacion = dto.FechaAprobacion,
                    FechaRequerida = DateOnly.FromDateTime(dto.FechaRequerida),
                    SubtotalSinImpuestos = dto.SubtotalSinImpuestos,
                    DescuentoTotal = dto.DescuentoTotal,
                    Iva = dto.Iva,
                    ValorTotal = dto.ValorTotal,
                    Justificacion = dto.Justificacion,
                    Prioridad = dto.Prioridad,
                    MotivoRechazo = dto.MotivoRechazo,
                    Observaciones = dto.Observaciones,
                    ArchivoOc = dto.ArchivoOc,
                    Estado = dto.Estado,
                };

                context.SolicitudesCompras.Add(nueva);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException?.Message ?? ex.Message;
                throw new Exception("Error al insertar la solicitud: " + mensajeError, ex);
            }
        }

        public void UpdateSolicitud(SolicitudesCompraDTO updatedSolicitud)
        {
            using (var context = new InvensisContext())
            {
                var solicitud = context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == updatedSolicitud.IdSolicitud);

                if (solicitud != null)
                {
                    solicitud.IdSolicitud = updatedSolicitud.IdSolicitud;
                    solicitud.RucEmpresa = updatedSolicitud.RucEmpresa;
                    solicitud.IdDepartamento = updatedSolicitud.IdDepartamento;
                    solicitud.IdUsuarioSolicita = updatedSolicitud.IdUsuarioSolicita;
                    solicitud.IdUsuarioAutoriza = updatedSolicitud.IdUsuarioAutoriza;
                    solicitud.FechaSolicitud = updatedSolicitud.FechaSolicitud;
                    solicitud.FechaAprobacion = updatedSolicitud.FechaAprobacion;
                    solicitud.FechaRequerida = updatedSolicitud.FechaRequerida == DateTime.MinValue? null
                    :DateOnly.FromDateTime(updatedSolicitud.FechaRequerida);
                    solicitud.SubtotalSinImpuestos = updatedSolicitud.SubtotalSinImpuestos;
                    solicitud.DescuentoTotal = updatedSolicitud.DescuentoTotal;
                    solicitud.Iva = updatedSolicitud.Iva;
                    solicitud.ValorTotal = updatedSolicitud.ValorTotal;
                    solicitud.Justificacion = updatedSolicitud.Justificacion;
                    solicitud.Prioridad = updatedSolicitud.Prioridad;
                    solicitud.Estado = updatedSolicitud.Estado;
                    solicitud.MotivoRechazo = updatedSolicitud.MotivoRechazo;
                    solicitud.Observaciones = updatedSolicitud.Observaciones;
                    solicitud.ArchivoOc = updatedSolicitud.ArchivoOc;

                    context.SaveChanges();
                }
            }
        }

        //public void DeleteSolicitud(SolicitudesCompra solicitudToDelete)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.SolicitudesCompras.Remove(solicitudToDelete);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteSolicitudById(int idSolicitud)
        {
            using (var context = new InvensisContext())
            {
                var solicitud = context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == idSolicitud);

                if (solicitud != null)
                {
                    context.SolicitudesCompras.Remove(solicitud);
                    context.SaveChanges();
                }
            }
        }
    }
}
