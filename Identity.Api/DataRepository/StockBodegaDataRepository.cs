using Identity.Api.DTO;
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
    }
}
