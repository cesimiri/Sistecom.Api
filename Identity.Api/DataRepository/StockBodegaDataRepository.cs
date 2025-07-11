using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

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




        //paginado por bodegga
        public PagedResult<stockBodegaDTO> GetPaginadosPorBodega(int idBodega, int pagina, int pageSize, string? filtro = null)
        {
            using var context = new InvensisContext();

            var query = context.StockBodegas
                .Where(sb => sb.IdBodega == idBodega)
                .Include(sb => sb.IdBodegaNavigation)
                .Include(sb => sb.IdProductoNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroFormateado = filtro.Trim().ToLower();
                query = query.Where(sb =>
                    sb.IdProductoNavigation.Nombre.ToLower().Contains(filtroFormateado)
                );
            }

            var totalItems = query.Count();

            var items = query
                .OrderBy(sb => sb.IdStock)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(sb => new stockBodegaDTO
                {
                    IdStock = sb.IdStock,
                    IdBodega = sb.IdBodega,
                    IdProducto = sb.IdProducto,
                    nombreBodega = sb.IdBodegaNavigation.Nombre,
                    nombreProducto = sb.IdProductoNavigation.Nombre,
                    CantidadDisponible = sb.CantidadDisponible,
                    CantidadReservada = sb.CantidadReservada,
                    CantidadEnsamblaje = sb.CantidadEnsamblaje,
                    ValorPromedio = sb.ValorPromedio,
                    UltimaEntrada = sb.UltimaEntrada,
                    UltimaSalida = sb.UltimaSalida,
                    FechaActualizacion = sb.FechaActualizacion
                })
                .ToList();

            return new PagedResult<stockBodegaDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }




        //actualizar stocks
        public bool ProcesarMovimientoStock(List<MovimientosInventarioDTO> movimientos, out string error)
        {
            error = "";
            try
            {
                using var context = new InvensisContext();

                foreach (var movimiento in movimientos)
                {
                    var stock = context.StockBodegas
                        .FirstOrDefault(s => s.IdBodega == movimiento.IdBodega && s.IdProducto == movimiento.IdProducto);

                    if (stock == null)
                    {
                        stock = new StockBodega
                        {
                            IdBodega = movimiento.IdBodega,
                            IdProducto = movimiento.IdProducto,
                            CantidadDisponible = 0,
                            FechaActualizacion = DateTime.Now
                        };
                        context.StockBodegas.Add(stock);
                    }

                    movimiento.StockAnterior = stock.CantidadDisponible;

                    switch (movimiento.TipoMovimiento.ToUpper())
                    {
                        case "ENTRADA":
                            stock.CantidadDisponible += movimiento.Cantidad;
                            stock.UltimaEntrada = DateOnly.FromDateTime(movimiento.FechaMovimiento ?? DateTime.Now);
                            break;

                        case "SALIDA":
                            if (stock.CantidadDisponible < movimiento.Cantidad)
                            {
                                error = $"Stock insuficiente para el producto {movimiento.IdProducto}.";
                                return false;
                            }
                            stock.CantidadDisponible -= movimiento.Cantidad;
                            stock.UltimaSalida = DateOnly.FromDateTime(movimiento.FechaMovimiento ?? DateTime.Now);
                            break;

                        case "TRANSFERENCIA":
                            error = "No debería llegar un movimiento tipo TRANSFERENCIA aquí.";
                            return false;

                        case "AJUSTE":
                            // Validar que no quede stock negativo con ajustes negativos
                            decimal nuevoStock = stock.CantidadDisponible + movimiento.Cantidad;
                            if (nuevoStock < 0)
                            {
                                error = $"El ajuste dejaría stock negativo para el producto {movimiento.IdProducto}.";
                                return false;
                            }
                            stock.CantidadDisponible = nuevoStock;
                            break;

                        default:
                            error = $"Tipo de movimiento '{movimiento.TipoMovimiento}' no reconocido.";
                            return false;
                    }

                    movimiento.StockActual = stock.CantidadDisponible;
                    stock.FechaActualizacion = DateTime.Now;
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = "Error procesando el stock: " + ex.Message;
                return false;
            }
        }




    }
}
