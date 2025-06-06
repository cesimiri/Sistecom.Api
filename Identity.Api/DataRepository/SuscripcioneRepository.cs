using Modelo.Sistecom.Modelo.Database;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using Identity.Api.Interfaces;
=======
using System.Collections.Generic;
using System.Linq;
>>>>>>> parent of dfa63f3 (5-6-25 16:13)

namespace Identity.Api.Repositories
{
    public class SuscripcionRepository : ISuscripcione
    {
<<<<<<< HEAD
        private readonly InvensisContext _context;

        public SuscripcionRepository(InvensisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Suscripcione>> SuscripcionesAll()
        {
            return await _context.Suscripciones
                .Include(s => s.IdEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .AsNoTracking() // Para consultas de solo lectura
                .ToListAsync();
=======
        public List<Suscripcione> GetAllSuscripciones()
        {
            using (var context = new InvensisContext())
            {
                return context.Suscripciones.ToList();
            }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public Suscripcione GetSuscripcionById(int idSuscripcion)
        {
<<<<<<< HEAD
            return await _context.Suscripciones
                .Include(s => s.IdEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .FirstOrDefaultAsync(s => s.IdSuscripcion == idSuscripcion);
=======
            using (var context = new InvensisContext())
            {
                return context.Suscripciones.FirstOrDefault(s => s.IdSuscripcion == idSuscripcion);
            }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void InsertSuscripcion(Suscripcione newSuscripcion)
        {
            // Establecer fecha de registro si no está establecida
            if (newSuscripcion.FechaRegistro == null)
            {
<<<<<<< HEAD
                newSuscripcion.FechaRegistro = DateTime.UtcNow;
=======
                context.Suscripciones.Add(newSuscripcion);
                context.SaveChanges();
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
            }

            await _context.Suscripciones.AddAsync(newSuscripcion);
            await _context.SaveChangesAsync();
        }

        public void UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            var existingSuscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);

            if (existingSuscripcion == null)
            {
<<<<<<< HEAD
                throw new ArgumentException($"Suscripción con ID {updatedSuscripcion.IdSuscripcion} no encontrada");
=======
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

                    context.SaveChanges();
                }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
            }

            // Usar AutoMapper o método de extensión para mapear propiedades
            _context.Entry(existingSuscripcion).CurrentValues.SetValues(updatedSuscripcion);
            await _context.SaveChangesAsync();
        }

        public void DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
<<<<<<< HEAD
            _context.Suscripciones.Remove(suscripcionToDelete);
            await _context.SaveChangesAsync();
=======
            using (var context = new InvensisContext())
            {
                context.Suscripciones.Remove(suscripcionToDelete);
                context.SaveChanges();
            }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void DeleteSuscripcionById(int idSuscripcion)
        {
<<<<<<< HEAD
            var suscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(s => s.IdSuscripcion == idSuscripcion);

            if (suscripcion == null)
            {
                throw new ArgumentException($"Suscripción con ID {idSuscripcion} no encontrada");
=======
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Suscripciones
                    .FirstOrDefault(s => s.IdSuscripcion == idSuscripcion);

                if (suscripcion != null)
                {
                    context.Suscripciones.Remove(suscripcion);
                    context.SaveChanges();
                }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
            }

            _context.Suscripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
        }
<<<<<<< HEAD

        public async Task<Proveedore?> GetProveedorByIdAsync(int idProveedor)
        {
            return await _context.Proveedores
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdProveedor == idProveedor);
        }

        public async Task<EmpresasCliente?> GetEmpresaByIdAsync(int idEmpresa)
        {
            return await _context.EmpresasClientes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdEmpresa == idEmpresa);
        }

        public async Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync()
        {
            return await _context.EmpresasClientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Proveedore>> GetProveedoreAsync()
        {
            return await _context.Proveedores
                .AsNoTracking()
                .ToListAsync();
        }
=======
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
    }
}