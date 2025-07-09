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
                    u.IdProductoNavigation.Nombre.ToLower().Contains(filtro));
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
                // Usar EF.Functions.Like para un filtro más eficiente (SQL LIKE)
                var filtroFormateado = $"%{filtro.Trim()}%";
                query = query.Where(x => EF.Functions.Like(x.IdProductoNavigation.Nombre, filtroFormateado));
            }

            var totalItems = query.Count();

            var items = query
                .OrderBy(x => x.IdStock)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(sb => new stockBodegaDTO
                {
                    IdStock = sb.IdStock,
                    IdBodega = sb.IdBodega,
                    IdProducto = sb.IdProducto,
                    NombreBodega = sb.IdBodegaNavigation.Nombre,
                    NombreProducto = sb.IdProductoNavigation.Nombre,
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
                        case "TRANSFERENCIA":
                            if (stock.CantidadDisponible < movimiento.Cantidad)
                            {
                                error = $"Stock insuficiente para el producto {movimiento.IdProducto}.";
                                return false;
                            }
                            stock.CantidadDisponible -= movimiento.Cantidad;
                            stock.UltimaSalida = DateOnly.FromDateTime(movimiento.FechaMovimiento ?? DateTime.Now);
                            break;

                        case "AJUSTE":
                            stock.CantidadDisponible += movimiento.Cantidad;
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
