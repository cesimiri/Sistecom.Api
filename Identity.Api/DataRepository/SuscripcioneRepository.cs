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
        public List<Suscripcione> GetAllSuscripciones()
        {
            using (var context = new InvensisContext())
            {
                return context.Suscripciones
                .Include(s => s.IdEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .ToList();
            }
        }

        public Suscripcione GetSuscripcionById(int idSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                return context.Suscripciones
    .Include(s => s.IdEmpresaNavigation)
    .Include(s => s.IdProveedorNavigation)
    .FirstOrDefault(s => s.IdSuscripcion == idSuscripcion);
            }
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




        public void UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Suscripciones
                    .FirstOrDefault(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);

                if (suscripcion != null)
                {
                    suscripcion.IdProveedor = updatedSuscripcion.IdProveedor;
                    suscripcion.IdEmpresa = updatedSuscripcion.IdEmpresa;
                    suscripcion.NombreServicio = updatedSuscripcion.NombreServicio;
                    suscripcion.TipoSuscripcion = updatedSuscripcion.TipoSuscripcion;
                    suscripcion.FechaInicio = updatedSuscripcion.FechaInicio;
                    suscripcion.FechaRenovacion = updatedSuscripcion.FechaRenovacion;
                    suscripcion.PeriodoFacturacion = updatedSuscripcion.PeriodoFacturacion;
                    suscripcion.CostoPeriodo = updatedSuscripcion.CostoPeriodo;
                    suscripcion.UsuariosIncluidos = updatedSuscripcion.UsuariosIncluidos;
                    suscripcion.AlmacenamientoGb = updatedSuscripcion.AlmacenamientoGb;
                    suscripcion.UrlAcceso = updatedSuscripcion.UrlAcceso;
                    suscripcion.Administrador = updatedSuscripcion.Administrador;
                    suscripcion.Estado = updatedSuscripcion.Estado;
                    suscripcion.NotificarDiasAntes = updatedSuscripcion.NotificarDiasAntes;
                    suscripcion.Observaciones = updatedSuscripcion.Observaciones;
                    suscripcion.FechaRegistro = updatedSuscripcion.FechaRegistro;

                    // Cargar y asignar las relaciones
                    suscripcion.IdEmpresaNavigation = context.EmpresasClientes
                        .FirstOrDefault(e => e.IdEmpresa == updatedSuscripcion.IdEmpresa);

                    suscripcion.IdProveedorNavigation = context.Proveedores
                        .FirstOrDefault(p => p.IdProveedor == updatedSuscripcion.IdProveedor);

                    context.SaveChanges();
                }
            }
        }

        public void DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Suscripciones.Remove(suscripcionToDelete);
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
