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




        ////actualizar stocks recordar que un trigger 
        //private int contadorInvocacionesRegistrarMovimientos = 0;

        //public bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error)
        //{
        //    contadorInvocacionesRegistrarMovimientos++;
        //    error = null;
        //    var movimientosProcesados = new List<MovimientosInventarioDTO>();

        //    Console.WriteLine($"\n🟡 [INICIO] Recibidos desde el frontend - Invocación #{contadorInvocacionesRegistrarMovimientos}:");
        //    Console.WriteLine($"Cantidad de movimientos recibidos: {movimientos.Count}");
        //    foreach (var m in movimientos)
        //    {
        //        Console.WriteLine($"  - Producto: {m.IdProducto}, Cantidad: {m.Cantidad}, Tipo: {m.TipoMovimiento}, Bodega: {m.IdBodega}");
        //    }

        //    // Procesamiento de movimientos (desglosar transferencia si aplica)
        //    foreach (var movimiento in movimientos)
        //    {
        //        var tipo = movimiento.TipoMovimiento?.ToUpperInvariant();
        //        movimiento.FechaMovimiento ??= DateTime.Now;

        //        Console.WriteLine($"\n🔄 Procesando movimiento: Producto {movimiento.IdProducto}, Tipo {tipo}, Cantidad {movimiento.Cantidad}, Bodega {movimiento.IdBodega}");

        //        switch (tipo)
        //        {
        //            case "TRANSFERENCIA":
        //                if (movimiento.IdBodegaOrigen == null || movimiento.IdBodegaDestino == null)
        //                {
        //                    error = "Para una transferencia, debe especificar la bodega origen y destino.";
        //                    Console.WriteLine($"❌ Error: {error}");
        //                    return false;
        //                }

        //                Console.WriteLine("🔁 Generando movimientos de transferencia...");

        //                movimientosProcesados.Add(new MovimientosInventarioDTO
        //                {
        //                    IdBodega = movimiento.IdBodegaOrigen.Value,
        //                    IdProducto = movimiento.IdProducto,
        //                    TipoMovimiento = "SALIDA",
        //                    Cantidad = movimiento.Cantidad,
        //                    PrecioUnitario = movimiento.PrecioUnitario,
        //                    NumeroSerie = movimiento.NumeroSerie,
        //                    Observaciones = movimiento.Observaciones,
        //                    UsuarioRegistro = movimiento.UsuarioRegistro,
        //                    FechaMovimiento = movimiento.FechaMovimiento.Value,
        //                    Origen = "TRANSFERENCIA",
        //                    IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
        //                    IdBodegaOrigen = movimiento.IdBodegaOrigen,
        //                    IdBodegaDestino = movimiento.IdBodegaDestino
        //                });

        //                movimientosProcesados.Add(new MovimientosInventarioDTO
        //                {
        //                    IdBodega = movimiento.IdBodegaDestino.Value,
        //                    IdProducto = movimiento.IdProducto,
        //                    TipoMovimiento = "ENTRADA",
        //                    Cantidad = movimiento.Cantidad,
        //                    PrecioUnitario = movimiento.PrecioUnitario,
        //                    NumeroSerie = movimiento.NumeroSerie,
        //                    Observaciones = movimiento.Observaciones,
        //                    UsuarioRegistro = movimiento.UsuarioRegistro,
        //                    FechaMovimiento = movimiento.FechaMovimiento.Value,
        //                    Origen = "TRANSFERENCIA",
        //                    IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
        //                    IdBodegaOrigen = movimiento.IdBodegaOrigen,
        //                    IdBodegaDestino = movimiento.IdBodegaDestino
        //                });
        //                break;

        //            case "ENTRADA":
        //            case "SALIDA":
        //            case "AJUSTE":
        //                if (movimiento.IdBodega == null || movimiento.IdBodega <= 0)
        //                {
        //                    error = $"Debe indicar la bodega para el movimiento tipo {tipo}.";
        //                    Console.WriteLine($"❌ Error: {error}");
        //                    return false;
        //                }

        //                if (tipo == "ENTRADA" && movimiento.IdDocumentoOrigen == null)
        //                {
        //                    error = "Debe indicar el documento origen para la entrada.";
        //                    Console.WriteLine($"❌ Error: {error}");
        //                    return false;
        //                }

        //                Console.WriteLine($"✅ Movimiento de tipo {tipo} procesado.");
        //                movimientosProcesados.Add(movimiento);
        //                break;

        //            default:
        //                error = "Tipo de movimiento no reconocido.";
        //                Console.WriteLine($"❌ Error: {error}");
        //                return false;
        //        }
        //    }

        //    Console.WriteLine($"\n✅ Movimientos listos para guardar en BD. Total: {movimientosProcesados.Count}");
        //    foreach (var m in movimientosProcesados)
        //    {
        //        Console.WriteLine($"  -> Producto: {m.IdProducto}, Cantidad: {m.Cantidad}, Tipo: {m.TipoMovimiento}, Bodega: {m.IdBodega}");
        //    }

        //    using var context = new InvensisContext();
        //    var movimientosFiltrados = new List<MovimientosInventarioDTO>();

        //    Console.WriteLine("\n🔎 Verificando duplicados en BD...");
        //    foreach (var m in movimientosProcesados)
        //    {
        //        bool yaExiste = context.MovimientosInventarios.Any(mi =>
        //            mi.IdDocumentoOrigen == m.IdDocumentoOrigen &&
        //            mi.TipoMovimiento == m.TipoMovimiento &&
        //            mi.IdProducto == m.IdProducto &&
        //            mi.IdBodega == m.IdBodega &&
        //            mi.Cantidad == m.Cantidad
        //        );

        //        if (yaExiste)
        //        {
        //            Console.WriteLine($"⚠️ Movimiento duplicado detectado y omitido: Producto {m.IdProducto}, Doc {m.IdDocumentoOrigen}, Bodega {m.IdBodega}, Cantidad {m.Cantidad}");
        //        }
        //        else
        //        {
        //            movimientosFiltrados.Add(m);
        //        }
        //    }

        //    if (!movimientosFiltrados.Any())
        //    {
        //        error = "Todos los movimientos ya fueron registrados anteriormente. No se realizaron cambios.";
        //        Console.WriteLine($"⚠️ {error}");
        //        return false;
        //    }

        //    // Ya no llamamos a ProcesarMovimientoStock porque el trigger actualiza stock automáticamente
        //    Console.WriteLine("\n💾 Guardando movimientos válidos en base de datos...");
        //    foreach (var movimiento in movimientosFiltrados)
        //    {
        //        Console.WriteLine($"  Guardando: Producto {movimiento.IdProducto}, Tipo {movimiento.TipoMovimiento}, Cantidad {movimiento.Cantidad}, Bodega: {movimiento.IdBodega}");
        //        context.MovimientosInventarios.Add(new MovimientosInventario
        //        {
        //            IdBodega = movimiento.IdBodega,
        //            IdProducto = movimiento.IdProducto,
        //            TipoMovimiento = movimiento.TipoMovimiento,
        //            Cantidad = movimiento.Cantidad,
        //            PrecioUnitario = movimiento.PrecioUnitario,
        //            NumeroSerie = movimiento.NumeroSerie,
        //            Observaciones = movimiento.Observaciones,
        //            UsuarioRegistro = movimiento.UsuarioRegistro,
        //            FechaMovimiento = movimiento.FechaMovimiento,
        //            Origen = movimiento.Origen,
        //            IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
        //            IdBodegaOrigen = movimiento.IdBodegaOrigen,
        //            IdBodegaDestino = movimiento.IdBodegaDestino
        //        });
        //    }

        //    context.SaveChanges();
        //    Console.WriteLine("✅ Guardado finalizado sin duplicados.");

        //    // Mostrar resumen final de stock (solo lectura)
        //    Console.WriteLine("\n📋 RESUMEN FINAL DE STOCK EN BD:");
        //    using var contextoResumen = new InvensisContext();
        //    foreach (var m in movimientosFiltrados)
        //    {
        //        var stock = contextoResumen.StockBodegas
        //            .AsNoTracking()
        //            .FirstOrDefault(s => s.IdBodega == m.IdBodega && s.IdProducto == m.IdProducto);
        //        if (stock != null)
        //        {
        //            Console.WriteLine($"  🧾 Producto {m.IdProducto}, Bodega {m.IdBodega}, CantidadDisponible: {stock.CantidadDisponible}");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"  ⚠️ Producto {m.IdProducto}, Bodega {m.IdBodega} aún no tiene stock registrado.");
        //        }
        //    }

        //    Console.WriteLine($"🟡 [FIN] RegistrarMovimientos - Invocación #{contadorInvocacionesRegistrarMovimientos}\n");
        //    return true;
        //}








    }
}
