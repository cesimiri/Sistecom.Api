using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.DataRepository
{
    public class SuscripcioneDataRepository
    {
        public async Task<List<Suscripcione>> SuscripcionesAll()
        {
            using (var context = new InvensisContext())
            {
                return await context.Suscripciones
                    .Include(s => s.IdEmpresaNavigation)
                    .Include(s => s.IdProveedorNavigation)
                    .ToListAsync();
            }
        }

        public async Task<Suscripcione?> GetSuscripcionById(int idSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                return await context.Suscripciones
                    .Include(s => s.IdEmpresaNavigation)
                    .Include(s => s.IdProveedorNavigation)
                    .FirstOrDefaultAsync(s => s.IdSuscripcion == idSuscripcion);
            }
        }

        public async Task InsertSuscripcion(Suscripcione newSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                await context.Suscripciones.AddAsync(newSuscripcion);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = await context.Suscripciones
                    .FirstOrDefaultAsync(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);

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

                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Suscripciones.Remove(suscripcionToDelete);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteSuscripcionById(int idSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = await context.Suscripciones
                    .FirstOrDefaultAsync(s => s.IdSuscripcion == idSuscripcion);

                if (suscripcion != null)
                {
                    context.Suscripciones.Remove(suscripcion);
                    await context.SaveChangesAsync();
                }
            }
        }
        // agregado
        public async Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync()
        {
            using (var context = new InvensisContext())
            {
                return await context.EmpresasClientes.ToListAsync();
            }
        }

        public async Task<IEnumerable<Proveedore>> GetProveedoreAsync()
        {
            using (var context = new InvensisContext())
            {
                return await context.Proveedores.ToListAsync();
            }
        }
    }
}
