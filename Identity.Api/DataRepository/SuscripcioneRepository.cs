using Identity.Api.DTO;
using Identity.Api.Model;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;


namespace Identity.Api.DataRepository
{
    public class SuscripcioneDataRepository
    {
        public List<SuscripcionDto> GetAllSuscripciones()
        {
            using var context = new InvensisContext();

            return context.Suscripciones
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .Select(s => new SuscripcionDto
                {
                    IdSuscripcion = s.IdSuscripcion,
                    RucEmpresa = s.RucEmpresa,
                    IdProveedor = s.IdProveedor,
                    NombreServicio = s.NombreServicio,
                    TipoSuscripcion = s.TipoSuscripcion,
                    FechaInicio = s.FechaInicio.ToDateTime(new TimeOnly(0, 0)),
                    FechaRenovacion = s.FechaRenovacion.ToDateTime(new TimeOnly(0, 0)),
                    PeriodoFacturacion = s.PeriodoFacturacion,
                    CostoPeriodo = s.CostoPeriodo,
                    UsuariosIncluidos = s.UsuariosIncluidos,
                    AlmacenamientoGb = s.AlmacenamientoGb,
                    UrlAcceso = s.UrlAcceso,
                    Administrador = s.Administrador,
                    Estado = s.Estado,
                    NotificarDiasAntes = s.NotificarDiasAntes,
                    Observaciones = s.Observaciones,
                    // Campos relacionados:
                    RazonSocialEmpresa = s.RucEmpresaNavigation.RazonSocial,
                    RazonSocialProveedor = s.IdProveedorNavigation.RazonSocial
                })
                .ToList();
        }

        public SuscripcionDto? GetSuscripcionById(int idSuscripcion)
        {
            using var context = new InvensisContext();

            return context.Suscripciones
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .Where(s => s.IdSuscripcion == idSuscripcion)
                .Select(s => new SuscripcionDto
                {
                    IdSuscripcion = s.IdSuscripcion,
                    RucEmpresa = s.RucEmpresa,
                    IdProveedor = s.IdProveedor,
                    NombreServicio = s.NombreServicio,
                    TipoSuscripcion = s.TipoSuscripcion,
                    FechaInicio = s.FechaInicio.ToDateTime(new TimeOnly(0, 0)),
                    FechaRenovacion = s.FechaRenovacion.ToDateTime(new TimeOnly(0, 0)),
                    PeriodoFacturacion = s.PeriodoFacturacion,
                    CostoPeriodo = s.CostoPeriodo,
                    UsuariosIncluidos = s.UsuariosIncluidos,
                    AlmacenamientoGb = s.AlmacenamientoGb,
                    UrlAcceso = s.UrlAcceso,
                    Administrador = s.Administrador,
                    Estado = s.Estado,
                    NotificarDiasAntes = s.NotificarDiasAntes,
                    Observaciones = s.Observaciones,
                    // Campos relacionados:
                    RazonSocialEmpresa = s.RucEmpresaNavigation.RazonSocial,
                    RazonSocialProveedor = s.IdProveedorNavigation.RazonSocial
                })
                .FirstOrDefault();
        }

        public void InsertSuscripcion(SuscripcionDto dto)
        {
            try
            {
                using var context = new InvensisContext();

                var empresa = context.EmpresasClientes.Find(dto.RucEmpresa);
                var proveedor = context.Proveedores.Find(dto.IdProveedor);

                if (empresa == null || proveedor == null)
                {
                    throw new Exception("La empresa o el proveedor no existen en la base de datos.");
                }

                var nueva = new Suscripcione
                {
                    RucEmpresa = dto.RucEmpresa,
                    IdProveedor = dto.IdProveedor,
                    NombreServicio = dto.NombreServicio,
                    TipoSuscripcion = dto.TipoSuscripcion,
                    //cambio de datetime a dateonly 
                    FechaInicio = DateOnly.FromDateTime(dto.FechaInicio),
                    FechaRenovacion = DateOnly.FromDateTime(dto.FechaRenovacion),
                    PeriodoFacturacion = dto.PeriodoFacturacion,
                    CostoPeriodo = dto.CostoPeriodo,
                    UsuariosIncluidos = dto.UsuariosIncluidos,
                    AlmacenamientoGb = dto.AlmacenamientoGb,
                    UrlAcceso = dto.UrlAcceso,
                    Administrador = dto.Administrador,
                    Estado = dto.Estado,
                    NotificarDiasAntes = dto.NotificarDiasAntes,
                    Observaciones = dto.Observaciones
                };

                context.Suscripciones.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la suscripción: " + ex.InnerException?.Message ?? ex.Message);
            }
        }



        public void UpdateSuscripcion(SuscripcionDto dto)
        {
            using var context = new InvensisContext();

            var suscripcion = context.Suscripciones
                .FirstOrDefault(s => s.IdSuscripcion == dto.IdSuscripcion);

            if (suscripcion != null)
            {
                suscripcion.IdProveedor = dto.IdProveedor;
                suscripcion.RucEmpresa = dto.RucEmpresa;
                suscripcion.NombreServicio = dto.NombreServicio;
                suscripcion.TipoSuscripcion = dto.TipoSuscripcion;
                //conversion de DATETIME a DATEONLY
                suscripcion.FechaInicio = DateOnly.FromDateTime(dto.FechaInicio);
                suscripcion.FechaRenovacion = DateOnly.FromDateTime(dto.FechaRenovacion);
                suscripcion.PeriodoFacturacion = dto.PeriodoFacturacion;
                suscripcion.CostoPeriodo = dto.CostoPeriodo;
                suscripcion.UsuariosIncluidos = dto.UsuariosIncluidos;
                suscripcion.AlmacenamientoGb = dto.AlmacenamientoGb;
                suscripcion.UrlAcceso = dto.UrlAcceso;
                suscripcion.Administrador = dto.Administrador;
                suscripcion.Estado = dto.Estado;
                suscripcion.NotificarDiasAntes = dto.NotificarDiasAntes;
                suscripcion.Observaciones = dto.Observaciones;
               

                // Actualizar navegación (opcional)
                suscripcion.RucEmpresaNavigation = context.EmpresasClientes
                    .FirstOrDefault(e => e.Ruc == dto.RucEmpresa);
                suscripcion.IdProveedorNavigation = context.Proveedores
                    .FirstOrDefault(p => p.IdProveedor == dto.IdProveedor);

                context.SaveChanges();
            }
        }

        public void DeleteSuscripcion(SuscripcionDto dto)
        {
            using var context = new InvensisContext();

            var suscripcion = context.Suscripciones
                .FirstOrDefault(s => s.IdSuscripcion == dto.IdSuscripcion);

            if (suscripcion != null)
            {
                context.Suscripciones.Remove(suscripcion);
                context.SaveChanges();
            }
        }
        public void DeleteSuscripcionById(int idSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Suscripciones
                    .FirstOrDefault(s => s.IdSuscripcion == idSuscripcion);

                if (suscripcion != null)
                {
                    context.Suscripciones.Remove(suscripcion);
                    context.SaveChanges();
                }
            }
        }



        //PAGINADA 
        public PagedResult<SuscripcionDto> GetSuscripcionPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Suscripciones
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NombreServicio.ToLower().Contains(filtro) );
            }

            // Aplicar filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            // Total de registros filtrados
            var totalItems = query.Count();

            // Obtener página solicitada con paginado
            var usuarios = query
                .OrderBy(u => u.IdSuscripcion) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SuscripcionDto
                {
                    IdSuscripcion = s.IdSuscripcion,
                    RucEmpresa = s.RucEmpresa,
                    IdProveedor = s.IdProveedor,
                    NombreServicio = s.NombreServicio,
                    TipoSuscripcion = s.TipoSuscripcion,
                    FechaInicio = s.FechaInicio.ToDateTime(new TimeOnly(0, 0)),
                    FechaRenovacion = s.FechaRenovacion.ToDateTime(new TimeOnly(0, 0)),
                    PeriodoFacturacion = s.PeriodoFacturacion,
                    CostoPeriodo = s.CostoPeriodo,
                    UsuariosIncluidos = s.UsuariosIncluidos,
                    AlmacenamientoGb = s.AlmacenamientoGb,
                    UrlAcceso = s.UrlAcceso,
                    Administrador = s.Administrador,
                    Estado = s.Estado,
                    NotificarDiasAntes = s.NotificarDiasAntes,
                    Observaciones = s.Observaciones,
                    // Campos relacionados:
                    RazonSocialEmpresa = s.RucEmpresaNavigation.RazonSocial,
                    RazonSocialProveedor = s.IdProveedorNavigation.RazonSocial
                })
                .ToList();

            return new PagedResult<SuscripcionDto>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }


    }
}
