using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Identity.Api.DataRepository
{
    public class CategoriasProductoRepository
    {
        public List<CategoriasProducto> CategoriasProductoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.CategoriasProductos.ToList();
            }
        }

        public CategoriasProducto GetCategoriasProductoById(int IdCategoriasProducto)
        {
            using (var context = new InvensisContext())
            {
                return context.CategoriasProductos.FirstOrDefault(p => p.IdCategoria == IdCategoriasProducto); ;
            }

        }

        public void InsertCategoriasProducto(CategoriasProducto NewItem)
        {
            using (var context = new InvensisContext())
            {
                var nuevo = new CategoriasProducto
                {
                    Nombre = NewItem.Nombre?.ToUpper(),
                    Descripcion = NewItem.Descripcion?.ToUpper(),
                    RequiereSerial = NewItem.RequiereSerial,
                    VidaUtilMeses = NewItem.VidaUtilMeses,
                    Estado = NewItem.Estado,
                };
                context.CategoriasProductos.Add(nuevo);
                context.SaveChanges();
            }
           
        }
        

        public void UpdateCategoriasProducto(CategoriasProducto UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.CategoriasProductos
                                         .Where(a => a.IdCategoria == UpdItem.IdCategoria)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.Nombre = UpdItem.Nombre?.ToUpper();
                    registrado.Descripcion = UpdItem.Descripcion?.ToUpper();
                    registrado.RequiereSerial = UpdItem.RequiereSerial;
                    registrado.VidaUtilMeses = UpdItem.VidaUtilMeses;
                    registrado.Estado = UpdItem.Estado;
                    

                    context.SaveChanges();
                }
            }
        }

        public void DeleteCategoriasProducto(CategoriasProducto NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.CategoriasProductos.Remove(NewItem);
                context.SaveChanges();
            }
        }

        public void DeleteCategoriasProductoById(int IdCategoriaProducto)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.CategoriasProductos
                                         .Where(a => a.IdCategoria == IdCategoriaProducto)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.CategoriasProductos.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }


        //PAGINADA 
        public PagedResult<CategoriasProducto> GetCategoriasProductoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.CategoriasProductos
               
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Nombre.ToLower().Contains(filtro) );
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
                .OrderBy(u => u.IdCategoria) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new CategoriasProducto
                {
                    IdCategoria = s.IdCategoria,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    RequiereSerial = s.RequiereSerial,
                    VidaUtilMeses = s.VidaUtilMeses,
                    Estado = s.Estado
                })
                .ToList();

            return new PagedResult<CategoriasProducto>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }

    }
}
