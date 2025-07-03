using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.DataRepository
{
    public class CargoRepository
    {
        public List<Cargo> GetAllCargo()
        {
            using (var context = new InvensisContext())
            {
                return context.Cargos.ToList();
            }
        }

        public Cargo GetCargoById(int  id)
        {
            using (var context = new InvensisContext())
            {
                return context.Cargos.FirstOrDefault(p => p.IdCargo == id); ;
            }

        }

        public void InsertCargo(Cargo NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.Cargos.Add(NewItem);
                context.SaveChanges();
            }
        }


        public void UpdateCargo(Cargo UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Cargos
                                         .Where(a => a.IdCargo == UpdItem.IdCargo)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.CodigoCargo = UpdItem.CodigoCargo;
                    registrado.NombreCargo = UpdItem.NombreCargo;
                    registrado.Descripcion = UpdItem.Descripcion;
                    registrado.NivelJerarquico = UpdItem.NivelJerarquico;
                    registrado.PuedeAutorizarCompras = UpdItem.PuedeAutorizarCompras;
                    registrado.LimiteAutorizacion = UpdItem.LimiteAutorizacion;
                    
                    registrado.Estado = UpdItem.Estado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteCargo(Cargo NewItem)
        {

            using (var context = new InvensisContext())
            {
                context.Cargos.Remove(NewItem);
                context.SaveChanges();
            }
        }


        public void DeleteCargoById(int id)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Cargos
                                         .Where(a => a.IdCargo == id)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.Cargos.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<Cargo> GetCargoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Cargos
                
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NombreCargo.ToLower().Contains(filtro) 
                   );
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
                .OrderBy(u => u.IdCargo) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new Cargo
                {
                    IdCargo = s.IdCargo,
                    CodigoCargo = s.CodigoCargo,
                    NombreCargo = s.NombreCargo,
                    Descripcion = s.Descripcion,
                    NivelJerarquico = s.NivelJerarquico,
                    PuedeAutorizarCompras = s.PuedeAutorizarCompras,
                    LimiteAutorizacion = s.LimiteAutorizacion,

                    Estado = s.Estado
                   
                })
                .ToList();

            return new PagedResult<Cargo>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
