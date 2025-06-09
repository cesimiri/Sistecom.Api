using Identity.Api.Model;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;
using Identity.Api.DTO;
namespace Identity.Api.DataRepository
{
    public class SuscripcioneDataRepository
    {
        public List<SuscripcionDto> GetAllSuscripciones()
        {
            using var context = new InvensisContext();

            return context.Suscripciones
                .Include(s => s.IdEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .Select(s => new SuscripcionDto
                {
                    IdSuscripcion = s.IdSuscripcion,
                    IdEmpresa = s.IdEmpresa,
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
                    RazonSocialEmpresa = s.IdEmpresaNavigation.RazonSocial,
                    RazonSocialProveedor = s.IdProveedorNavigation.RazonSocial
                })
                .ToList();
        }

        public SuscripcionDto? GetSuscripcionById(int idSuscripcion)
        {
            using var context = new InvensisContext();

            return context.Suscripciones
                .Include(s => s.IdEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .Where(s => s.IdSuscripcion == idSuscripcion)
                .Select(s => new SuscripcionDto
                {
                    IdSuscripcion = s.IdSuscripcion,
                    IdEmpresa = s.IdEmpresa,
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
                    RazonSocialEmpresa = s.IdEmpresaNavigation.RazonSocial,
                    RazonSocialProveedor = s.IdProveedorNavigation.RazonSocial
                })
                .FirstOrDefault();
        }


        public void InsertSuscripcion(SuscripcionDto dto)
        {
            using var context = new InvensisContext();

            var empresa = context.EmpresasClientes.Find(dto.IdEmpresa);
            var proveedor = context.Proveedores.Find(dto.IdProveedor);

            if (empresa == null || proveedor == null)
            {
                throw new Exception("IdEmpresa o IdProveedor no existen en la base de datos.");
            }

            var nueva = new Suscripcione
            {
                IdEmpresa = dto.IdEmpresa,
                IdProveedor = dto.IdProveedor,
                NombreServicio = dto.NombreServicio,
                TipoSuscripcion = dto.TipoSuscripcion,
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
                Observaciones = dto.Observaciones,
                FechaRegistro = dto.FechaRegistro
            };

            context.Suscripciones.Add(nueva);
            context.SaveChanges();
        }




        public void UpdateSuscripcion(SuscripcionDto dto)
        {
            using var context = new InvensisContext();

            var suscripcion = context.Suscripciones
                .FirstOrDefault(s => s.IdSuscripcion == dto.IdSuscripcion);

            if (suscripcion != null)
            {
                suscripcion.IdProveedor = dto.IdProveedor;
                suscripcion.IdEmpresa = dto.IdEmpresa;
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
                suscripcion.FechaRegistro = dto.FechaRegistro;

                // Actualizar navegación (opcional)
                suscripcion.IdEmpresaNavigation = context.EmpresasClientes
                    .FirstOrDefault(e => e.IdEmpresa == dto.IdEmpresa);
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
    }
}
