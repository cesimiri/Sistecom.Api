using Microsoft.EntityFrameworkCore;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
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
                .AsNoTracking()
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
                .AsNoTracking()
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
<<<<<<< HEAD
            if (newSuscripcion.FechaRegistro == null)
                newSuscripcion.FechaRegistro = DateTime.UtcNow;

            await _context.Suscripciones.AddAsync(newSuscripcion);
            await _context.SaveChangesAsync();
=======
            using (var context = new InvensisContext())
            {
                context.Suscripciones.Add(newSuscripcion);
                context.SaveChanges();
            }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
        }

        public void UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
<<<<<<< HEAD
            var existing = await _context.Suscripciones
                .FirstOrDefaultAsync(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);
=======
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Suscripciones
                    .FirstOrDefault(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)

            if (existing == null)
                throw new ArgumentException($"Suscripción con ID {updatedSuscripcion.IdSuscripcion} no encontrada");

<<<<<<< HEAD
            _context.Entry(existing).CurrentValues.SetValues(updatedSuscripcion);
            await _context.SaveChangesAsync();
=======
                    context.SaveChanges();
                }
            }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
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
                throw new ArgumentException($"Suscripción con ID {idSuscripcion} no encontrada");

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
            }
        }
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
    }
}
