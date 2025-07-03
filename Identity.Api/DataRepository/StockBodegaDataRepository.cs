using Identity.Api.DTO;
using Identity.Api.Paginado;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.DataRepository
{
    public class StockBodegaDataRepository
    {
        public List<StockBodega> GetAllStockBodega()
        {
            using (var context = new InvensisContext())
            {
                return context.StockBodegas
                              .Include(x => x.IdBodegaNavigation)
                              .Include(x => x.IdProductoNavigation)
                              .ToList();
            }
        }

        public StockBodega GetStockBodegaById(int idStock)
        {
            using (var context = new InvensisContext())
            {
                return context.StockBodegas
                              .Include(x => x.IdBodegaNavigation)
                              .Include(x => x.IdProductoNavigation)
                              .FirstOrDefault(p => p.IdStock == idStock);
            }
        }

        public void InsertStockBodega(stockBodegaDTO item)
        {
            try
            {
                using var context = new InvensisContext();


                var bodega = context.Bodegas.Find(item.IdBodega);
                var producto = context.Productos.Find(item.IdProducto);

                if (bodega == null || producto == null)
                {
                    throw new Exception("Esa bodega o producto no existe en la base de datos.");
                }

               

                var nueva = new StockBodega
                {

                    IdBodega = item.IdBodega,
                    IdProducto = item.IdProducto,
                    CantidadDisponible = item.CantidadDisponible,
                    CantidadReservada = item.CantidadReservada,
                    CantidadEnsamblaje = item.CantidadEnsamblaje,
                    ValorPromedio = item.ValorPromedio,
                   

                };

                context.StockBodegas.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el stock Bodegas: " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void UpdateStockBodega(StockBodega item)
        {
            using (var context = new InvensisContext())
            {
                var existing = context.StockBodegas.FirstOrDefault(p => p.IdStock == item.IdStock);

                if (existing != null)
                {
                    existing.IdBodega = item.IdBodega;
                    existing.IdProducto = item.IdProducto;
                    existing.CantidadDisponible = item.CantidadDisponible;
                    existing.CantidadReservada = item.CantidadReservada;
                    existing.CantidadEnsamblaje = item.CantidadEnsamblaje;
                    existing.ValorPromedio = item.ValorPromedio;
                    existing.UltimaEntrada = item.UltimaEntrada;
                    existing.UltimaSalida = item.UltimaSalida;
                    existing.FechaActualizacion = item.FechaActualizacion;

                    context.SaveChanges();
                }
            }
        }

        //public void DeleteStockBodega(StockBodega item)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.StockBodegas.Remove(item);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteStockBodegaById(int idStock)
        {
            using (var context = new InvensisContext())
            {
                var existing = context.StockBodegas.FirstOrDefault(p => p.IdStock == idStock);
                if (existing != null)
                {
                    context.StockBodegas.Remove(existing);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<stockBodegaDTO> GetStockBodegaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.StockBodegas
                .Include(s => s.IdBodegaNavigation)
                .Include(s => s.IdProductoNavigation)
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.IdBodegaNavigation.Nombre.ToLower().Contains(filtro) ||
                    u.IdProductoNavigation.Nombre.ToLower().Contains(filtro) );
            }

            // Aplicar filtro por estado
            //if (!string.IsNullOrEmpty(estado))
            //{
            //    query = query.Where(u => u.Estado == estado);
            //}

            // Total de registros filtrados
            var totalItems = query.Count();

            // Obtener página solicitada con paginado
            var usuarios = query
                .OrderBy(u => u.IdStock) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new stockBodegaDTO
                {
                    IdStock = s.IdStock,
                    IdBodega = s.IdBodega,
                    IdProducto = s.IdProducto,
                    CantidadDisponible = s.CantidadDisponible,
                    CantidadReservada = s.CantidadReservada,
                    CantidadEnsamblaje = s.CantidadEnsamblaje,
                    ValorPromedio = s.ValorPromedio,
                    UltimaEntrada = s.UltimaEntrada,
                    UltimaSalida = s.UltimaSalida,
                    FechaActualizacion = s.FechaActualizacion,


                    nombreBodega = s.IdBodegaNavigation.Nombre,
                    nombreProducto = s.IdProductoNavigation.Nombre
                })
                .ToList();

            return new PagedResult<stockBodegaDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
