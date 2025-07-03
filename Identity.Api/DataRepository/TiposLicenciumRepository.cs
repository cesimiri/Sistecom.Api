using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Paginado;

namespace identity.api.datarepository
{
    public class TiposLicenciumRepository
    {
        public List<TiposLicencium> TiposLicenciumInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.TiposLicencia.ToList();
            }
        }

        public TiposLicencium GetTiposLicenciumById(int IdTiposLicencium)
        {
            using (var context = new InvensisContext())
            {
                return context.TiposLicencia.FirstOrDefault(a => a.IdTipoLicencia == IdTiposLicencium);
            }
        }

        public void InsertTiposLicencium(TiposLicencium newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.TiposLicencia.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateTiposLicencium(TiposLicencium tipoActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.TiposLicencia.FirstOrDefault(a => a.IdTipoLicencia == tipoActualizado.IdTipoLicencia);
                if (existente != null)
                {
                    existente.Nombre = tipoActualizado.Nombre;
                    existente.Fabricante = tipoActualizado.Fabricante;
                    existente.Categoria = tipoActualizado.Categoria;
                    existente.TipoLicenciamiento = tipoActualizado.TipoLicenciamiento;
                    existente.PermiteMultipleUso = tipoActualizado.PermiteMultipleUso;
                    existente.Descripcion = tipoActualizado.Descripcion;
                    existente.Estado = tipoActualizado.Estado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteTiposLicencium(TiposLicencium activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.TiposLicencia.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteTiposLicenciumById(int idTiposLicencium)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.TiposLicencia.FirstOrDefault(a => a.IdTipoLicencia == idTiposLicencium);
                if (existente != null)
                {
                    context.TiposLicencia.Remove(existente);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<TiposLicencium> GetTiposLicenciumPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.TiposLicencia
                
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Nombre.ToLower().Contains(filtro));
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
                .OrderBy(u => u.IdTipoLicencia) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new TiposLicencium
                {
                    IdTipoLicencia = s.IdTipoLicencia,
                    Nombre = s.Nombre,
                    Fabricante = s.Fabricante,
                    Categoria = s.Categoria,
                    TipoLicenciamiento = s.TipoLicenciamiento,
                    PermiteMultipleUso = s.PermiteMultipleUso,
                    Descripcion = s.Descripcion,
                    Estado = s.Estado
                    
                })
                .ToList();

            return new PagedResult<TiposLicencium>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
