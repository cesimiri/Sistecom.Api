using Modelo.Sistecom.Modelo.Database;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Interfaces;

namespace Identity.Api.Repositories
{
    public class SuscripcionRepository : ISuscripcione
    {
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
        }

        public async Task<Suscripcione?> GetSuscripcionById(int idSuscripcion)
        {
            return await _context.Suscripciones
                .Include(s => s.IdEmpresaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .FirstOrDefaultAsync(s => s.IdSuscripcion == idSuscripcion);
        }

        public async Task InsertSuscripcion(Suscripcione newSuscripcion)
        {
            // Establecer fecha de registro si no está establecida
            if (newSuscripcion.FechaRegistro == null)
            {
                newSuscripcion.FechaRegistro = DateTime.UtcNow;
            }

            await _context.Suscripciones.AddAsync(newSuscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            var existingSuscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);

            if (existingSuscripcion == null)
            {
                throw new ArgumentException($"Suscripción con ID {updatedSuscripcion.IdSuscripcion} no encontrada");
            }

            // Usar AutoMapper o método de extensión para mapear propiedades
            _context.Entry(existingSuscripcion).CurrentValues.SetValues(updatedSuscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            _context.Suscripciones.Remove(suscripcionToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSuscripcionById(int idSuscripcion)
        {
            var suscripcion = await _context.Suscripciones
                .FirstOrDefaultAsync(s => s.IdSuscripcion == idSuscripcion);

            if (suscripcion == null)
            {
                throw new ArgumentException($"Suscripción con ID {idSuscripcion} no encontrada");
            }

            _context.Suscripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
        }

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
    }
}