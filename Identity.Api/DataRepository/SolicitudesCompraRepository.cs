using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System;
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

        //traer los usuarios destino por departamento ya que es identificador unico 
        public List<UsuarioDTO> ObtenerUsuarioDestino(int idDepartamento)
        {
            using var context = new InvensisContext(); // No se pasa idDepartamento aquí

            return context.Usuarios
                .Include(u => u.IdCargoNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Where(u => u.Estado == "ACTIVO" && u.IdDepartamento == idDepartamento)
                .Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    IdDepartamento = u.IdDepartamento,
                    IdCargo = u.IdCargo,
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Email = u.Email,
                    Telefono = u.Telefono,
                    Extension = u.Extension,
                    PuedeSolicitar = u.PuedeSolicitar,
                    Estado = u.Estado
                })
                .ToList();
        }

        //traer los usuarios quienes pueden autorizar solo por cargo jefe subjefe y gerencia
        public List<UsuarioDTO> ObtenerUsuariosAutorizaAsync()
        {
            using var context = new InvensisContext();

            return context.Usuarios
                .Include(u => u.IdCargoNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Where(u => u.Estado == "ACTIVO"

                 && new[] { 1, 2, 3 }.Contains(u.IdCargoNavigation.NivelJerarquico.GetValueOrDefault()))
                .Select(u => new UsuarioDTO
                {
                    
                    IdUsuario = u.IdUsuario,
                    IdDepartamento = u.IdDepartamento,
                    IdCargo = u.IdCargo,
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Email = u.Email,
                    Telefono = u.Telefono,
                    Extension = u.Extension,
                    PuedeSolicitar = u.PuedeSolicitar,
                    Estado = u.Estado

                })
                .ToList();

        }

        //traer los usuarios q solicita dependiendo cargo 4,3,2 o tecnico jefe subjefe
        public List<UsuarioDTO> ObtenerUsuarioSolicitaAsync()
        {
            using var context = new InvensisContext();

            return context.Usuarios
                .Include(u => u.IdCargoNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Where(u => u.Estado == "ACTIVO"

                 && new[] { 2, 3, 4 }.Contains(u.IdCargoNavigation.NivelJerarquico.GetValueOrDefault()))
                .Select(u => new UsuarioDTO
                {
                    //IdUsuario = u.IdUsuario,
                    //Cedula = u.Cedula,
                    //Nombres = u.Nombres,
                    //Apellidos = u.Apellidos,
                    //Email = u.Email,
                    //Telefono = u.Telefono,
                    //Extension = u.Extension,
                    //Estado = u.Estado,
                    //IdCargo = u.IdCargo,
                    //IdDepartamento = u.IdDepartamento,
                    //NombreCargo = u.IdCargoNavigation.Descripcion,
                    //NombreDepartamento = u.IdDepartamentoNavigation.NombreDepartamento
                    IdUsuario = u.IdUsuario,
                    IdDepartamento = u.IdDepartamento,
                    IdCargo = u.IdCargo,
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Email = u.Email,
                    Telefono = u.Telefono,
                    Extension = u.Extension,
                    PuedeSolicitar = u.PuedeSolicitar,
                    Estado = u.Estado,

                })
                .ToList();
        }

        //obtener las sucursales despues de seleccionar la empresa
        public List<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        {
            using var context = new InvensisContext();

            return context.Sucursales
                .Where(m => m.RucEmpresa == RucEmpresa)
        .Select(m => new SucursaleDTO
        {
            IdSucursal = m.IdSucursal,
            RucEmpresa = m.RucEmpresa,
            CodigoSucursal = m.CodigoSucursal!,
            NombreSucursal = m.NombreSucursal,
            Direccion = m.Direccion,
            Ciudad = m.Ciudad,
            Telefono = m.Telefono,
            Email = m.Email,
            //FechaDescontinuacion = m.FechaDescontinuacion.HasValue
            //    ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
            //: null,
            Responsable = m.Responsable,
            TelefonoResponsable = m.TelefonoResponsable,
            EsMatriz = m.EsMatriz,
            Estado = m.Estado
        })
        .ToList();
        }

        //obtener los departamentos despues de seleccionar la sucursal
        public List<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        {
            using var context = new InvensisContext();

            return context.Departamentos
                .Where(m => m.IdSucursal == idSucursal)
        .Select(m => new DepartamentoDTO
        {
            IdDepartamento = m.IdDepartamento,
            IdSucursal = m.IdSucursal,
            CodigoDepartamento = m.CodigoDepartamento,
            NombreDepartamento = m.NombreDepartamento!,
            Descripcion = m.Descripcion,
            Responsable = m.Responsable,
            EmailDepartamento = m.EmailDepartamento,
            Extension = m.Extension,
            CentroCosto = m.CentroCosto,
            //FechaDescontinuacion = m.FechaDescontinuacion.HasValue
            //    ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
            //: null,
            Estado = m.Estado
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
                //ver los datos(dto.RucEmpresa) que se ingresa existen
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
                    IdUsuarioDestino = dto.IdUsuarioDestino,
                    FechaSolicitud = dto.FechaSolicitud,
                    FechaAprobacion = dto.FechaAprobacion,
                    FechaRequerida = DateOnly.FromDateTime(dto.FechaRequerida),
                    SubtotalSinImpuestos = 0,
                    DescuentoTotal = 0,
                    Iva = 0,
                    ValorTotal = 0,
                    Justificacion = dto.Justificacion,
                    Prioridad = "NORMAL",
                    Estado = dto.Estado,
                    MotivoRechazo = dto.MotivoRechazo,
                    Observaciones = dto.Observaciones,
                    ArchivoOc = dto.ArchivoOc
                    
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
