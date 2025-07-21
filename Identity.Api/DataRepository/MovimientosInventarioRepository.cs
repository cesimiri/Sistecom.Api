using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;


namespace Identity.Api.DataRepository
{
    public class MovimientosInventarioRepository
    {


        // ------------------ primero actualiza el stock de la bodega  luego registra el movimiento--------------------------------//
        private int contadorInvocacionesRegistrarMovimientos = 0;

        public bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error)
        {
            contadorInvocacionesRegistrarMovimientos++;
            error = null;
            var movimientosProcesados = new List<MovimientosInventarioDTO>();

            // 🟡 Mostrar los movimientos recibidos
            Console.WriteLine($"\n🟡 [INICIO] Recibidos desde el frontend - Invocación #{contadorInvocacionesRegistrarMovimientos}:");
            Console.WriteLine($"Cantidad de movimientos recibidos: {movimientos.Count}");
            foreach (var m in movimientos)
            {
                Console.WriteLine($"  - Producto: {m.IdProducto}, Cantidad: {m.Cantidad}, Tipo: {m.TipoMovimiento}, Bodega: {m.IdBodega}");
            }

            // 🔄 Procesamiento de movimientos: desglosar transferencia si aplica
            foreach (var movimiento in movimientos)
            {
                var tipo = movimiento.TipoMovimiento?.ToUpperInvariant();
                movimiento.FechaMovimiento ??= DateTime.Now;

                Console.WriteLine($"\n🔄 Procesando movimiento: Producto {movimiento.IdProducto}, Tipo {tipo}, Cantidad {movimiento.Cantidad}, Bodega {movimiento.IdBodega}");

                switch (tipo)
                {
                    case "TRANSFERENCIA":
                        if (movimiento.IdBodegaOrigen == null || movimiento.IdBodegaDestino == null)
                        {
                            error = "Para una transferencia, debe especificar la bodega origen y destino.";
                            Console.WriteLine($"❌ Error: {error}");
                            return false;
                        }

                        Console.WriteLine("🔁 Generando movimientos de transferencia...");

                        movimientosProcesados.Add(new MovimientosInventarioDTO
                        {
                            IdBodega = movimiento.IdBodegaOrigen.Value,
                            IdProducto = movimiento.IdProducto,
                            TipoMovimiento = "SALIDA",
                            Cantidad = movimiento.Cantidad,
                            PrecioUnitario = movimiento.PrecioUnitario,
                            NumeroSerie = movimiento.NumeroSerie,
                            Observaciones = movimiento.Observaciones,
                            UsuarioRegistro = movimiento.UsuarioRegistro,
                            FechaMovimiento = movimiento.FechaMovimiento.Value,
                            Origen = "TRANSFERENCIA",
                            IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
                            IdBodegaOrigen = movimiento.IdBodegaOrigen,
                            IdBodegaDestino = movimiento.IdBodegaDestino
                        });

                        movimientosProcesados.Add(new MovimientosInventarioDTO
                        {
                            IdBodega = movimiento.IdBodegaDestino.Value,
                            IdProducto = movimiento.IdProducto,
                            TipoMovimiento = "ENTRADA",
                            Cantidad = movimiento.Cantidad,
                            PrecioUnitario = movimiento.PrecioUnitario,
                            NumeroSerie = movimiento.NumeroSerie,
                            Observaciones = movimiento.Observaciones,
                            UsuarioRegistro = movimiento.UsuarioRegistro,
                            FechaMovimiento = movimiento.FechaMovimiento.Value,
                            Origen = "TRANSFERENCIA",
                            IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
                            IdBodegaOrigen = movimiento.IdBodegaOrigen,
                            IdBodegaDestino = movimiento.IdBodegaDestino
                        });
                        break;

                    case "ENTRADA":
                    case "SALIDA":
                    case "AJUSTE":
                        if (movimiento.IdBodega == null || movimiento.IdBodega <= 0)
                        {
                            error = $"Debe indicar la bodega para el movimiento tipo {tipo}.";
                            Console.WriteLine($"❌ Error: {error}");
                            return false;
                        }

                        if (tipo == "ENTRADA" && movimiento.IdDocumentoOrigen == null)
                        {
                            error = "Debe indicar el documento origen para la entrada.";
                            Console.WriteLine($"❌ Error: {error}");
                            return false;
                        }

                        Console.WriteLine($"✅ Movimiento de tipo {tipo} procesado.");
                        movimientosProcesados.Add(movimiento);
                        break;

                    default:
                        error = "Tipo de movimiento no reconocido.";
                        Console.WriteLine($"❌ Error: {error}");
                        return false;
                }
            }

            // ✅ Mostrar resumen de movimientos a guardar
            Console.WriteLine($"\n✅ Movimientos listos para guardar en BD. Total: {movimientosProcesados.Count}");
            foreach (var m in movimientosProcesados)
            {
                Console.WriteLine($"  -> Producto: {m.IdProducto}, Cantidad: {m.Cantidad}, Tipo: {m.TipoMovimiento}, Bodega: {m.IdBodega}");
            }

            using var context = new InvensisContext();
            var movimientosFiltrados = new List<MovimientosInventarioDTO>();

            // 🔎 Verificar duplicados en base de datos
            Console.WriteLine("\n🔎 Verificando duplicados en BD...");
            foreach (var m in movimientosProcesados)
            {
                bool yaExiste = context.MovimientosInventarios.Any(mi =>
                    mi.IdDocumentoOrigen == m.IdDocumentoOrigen &&
                    mi.TipoMovimiento == m.TipoMovimiento &&
                    mi.IdProducto == m.IdProducto &&
                    mi.IdBodega == m.IdBodega &&
                    mi.Cantidad == m.Cantidad
                );

                if (yaExiste)
                {
                    Console.WriteLine($"⚠️ Movimiento duplicado detectado y omitido: Producto {m.IdProducto}, Doc {m.IdDocumentoOrigen}, Bodega {m.IdBodega}, Cantidad {m.Cantidad}");
                }
                else
                {
                    movimientosFiltrados.Add(m);
                }
            }

            if (!movimientosFiltrados.Any())
            {
                error = "Todos los movimientos ya fueron registrados anteriormente. No se realizaron cambios.";
                Console.WriteLine($"⚠️ {error}");
                return false;
            }

            // 💾 Insertar movimientos con cálculo de stock anterior y actual
            Console.WriteLine("\n💾 Guardando movimientos válidos en base de datos...");
            foreach (var movimiento in movimientosFiltrados)
            {
                var stockActualBodega = context.StockBodegas
                    .AsNoTracking()
                    .FirstOrDefault(s => s.IdBodega == movimiento.IdBodega && s.IdProducto == movimiento.IdProducto);

                // 🧮 Calcular stock anterior y nuevo
                decimal stockAnterior = stockActualBodega?.CantidadDisponible ?? 0;
                decimal stockActual = movimiento.TipoMovimiento.ToUpper() switch
                {
                    "ENTRADA" => stockAnterior + movimiento.Cantidad,
                    "SALIDA" => stockAnterior - movimiento.Cantidad,
                    "AJUSTE" => movimiento.Cantidad, // el ajuste se asume como nuevo valor
                    _ => stockAnterior
                };

                // Guardar en DTO (opcional, por si lo necesitas después)
                movimiento.StockAnterior = stockAnterior;
                movimiento.StockActual = stockActual;

                Console.WriteLine($"  Guardando: Producto {movimiento.IdProducto}, Tipo {movimiento.TipoMovimiento}, Cantidad {movimiento.Cantidad}, Bodega: {movimiento.IdBodega}, StockAnterior: {stockAnterior}, StockActual: {stockActual}");

                // Guardar en BD
                context.MovimientosInventarios.Add(new MovimientosInventario
                {
                    IdBodega = movimiento.IdBodega,
                    IdProducto = movimiento.IdProducto,
                    TipoMovimiento = movimiento.TipoMovimiento,
                    Cantidad = movimiento.Cantidad,
                    PrecioUnitario = movimiento.PrecioUnitario,
                    NumeroSerie = movimiento.NumeroSerie,
                    Observaciones = movimiento.Observaciones,
                    UsuarioRegistro = movimiento.UsuarioRegistro,
                    FechaMovimiento = movimiento.FechaMovimiento,
                    Origen = movimiento.Origen,
                    IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
                    IdBodegaOrigen = movimiento.IdBodegaOrigen,
                    IdBodegaDestino = movimiento.IdBodegaDestino,
                    StockAnterior = stockAnterior,
                    StockActual = stockActual
                });
            }

            context.SaveChanges();
            Console.WriteLine("✅ Guardado finalizado sin duplicados.");

            // 📋 Mostrar resumen final de stock actualizado
            Console.WriteLine("\n📋 RESUMEN FINAL DE STOCK EN BD:");
            using var contextoResumen = new InvensisContext();
            foreach (var m in movimientosFiltrados)
            {
                var stock = contextoResumen.StockBodegas
                    .AsNoTracking()
                    .FirstOrDefault(s => s.IdBodega == m.IdBodega && s.IdProducto == m.IdProducto);

                if (stock != null)
                {
                    Console.WriteLine($"  🧾 Producto {m.IdProducto}, Bodega {m.IdBodega}, CantidadDisponible: {stock.CantidadDisponible}");
                }
                else
                {
                    Console.WriteLine($"  ⚠️ Producto {m.IdProducto}, Bodega {m.IdBodega} aún no tiene stock registrado.");
                }
            }

            Console.WriteLine($"🟡 [FIN] RegistrarMovimientos - Invocación #{contadorInvocacionesRegistrarMovimientos}\n");
            return true;
        }








        //paginado
        public PagedResult<MovimientosInventarioDTO> GetPaginados(
            int pagina,
            int pageSize,
            string? tipoMovimiento,
            int? idBodega,
            string? nombreProducto,
            DateTime? desde,
            DateTime? hasta,
            string? ordenColumna = null,
            bool ordenAscendente = true,
            int? idProducto = null // nuevo parámetro
        )
        {
            using var context = new InvensisContext();
            var query = context.MovimientosInventarios
                .Include(x => x.IdProductoNavigation)
                .Include(x => x.IdBodegaNavigation)
                .AsQueryable();

            // Validación de fechas inválidas
            if (desde.HasValue && hasta.HasValue && desde > hasta)
            {
                return new PagedResult<MovimientosInventarioDTO>
                {
                    Items = new List<MovimientosInventarioDTO>(),
                    TotalItems = 0,
                    Page = pagina,
                    PageSize = pageSize
                };
            }

            // Aplicar filtros
            if (!string.IsNullOrWhiteSpace(tipoMovimiento))
                query = query.Where(x => x.TipoMovimiento.ToUpper() == tipoMovimiento.ToUpper());

            if (idBodega.HasValue)
                query = query.Where(x =>
                    x.IdBodega == idBodega.Value ||
                    x.IdBodegaOrigen == idBodega.Value ||
                    x.IdBodegaDestino == idBodega.Value);

            // Priorizar filtro por IdProducto si se envía
            if (idProducto.HasValue)
            {
                query = query.Where(x => x.IdProducto == idProducto.Value);
            }
            else if (!string.IsNullOrWhiteSpace(nombreProducto))
            {
                var nombre = nombreProducto.Trim().ToUpper();
                query = query.Where(x => x.IdProductoNavigation.Nombre.ToUpper().Contains(nombre));
            }

            if (desde.HasValue)
                query = query.Where(x => x.FechaMovimiento >= desde.Value);

            if (hasta.HasValue)
                query = query.Where(x => x.FechaMovimiento <= hasta.Value);

            var totalItems = query.Count();

            // Ordenamiento
            if (!string.IsNullOrEmpty(ordenColumna))
            {
                query = ApplyOrdering(query, ordenColumna, ordenAscendente);
            }
            else
            {
                query = query.OrderByDescending(x => x.FechaMovimiento);
            }

            // Proyección DTO
            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new MovimientosInventarioDTO
                {
                    IdMovimiento = x.IdMovimiento,
                    IdBodega = x.IdBodega,
                    IdProducto = x.IdProducto,
                    TipoMovimiento = x.TipoMovimiento,
                    Cantidad = x.Cantidad,
                    PrecioUnitario = x.PrecioUnitario,
                    StockAnterior = x.StockAnterior,
                    StockActual = x.StockActual,
                    FechaMovimiento = x.FechaMovimiento,
                    Origen = x.Origen,
                    Observaciones = x.Observaciones,
                    NombreProducto = x.IdProductoNavigation.Nombre,
                    NombreBodega = x.IdBodegaNavigation.Nombre
                })
                .ToList();

            return new PagedResult<MovimientosInventarioDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }


        private IQueryable<MovimientosInventario> ApplyOrdering(
            IQueryable<MovimientosInventario> source,
            string columnName,
            bool ascending)
        {
            switch (columnName)
            {
                case "FechaMovimiento":
                    return ascending ?
                        source.OrderBy(x => x.FechaMovimiento) :
                        source.OrderByDescending(x => x.FechaMovimiento);
                case "TipoMovimiento":
                    return ascending ?
                        source.OrderBy(x => x.TipoMovimiento) :
                        source.OrderByDescending(x => x.TipoMovimiento);
                case "NombreProducto":
                    return ascending ?
                        source.OrderBy(x => x.IdProductoNavigation.Nombre) :
                        source.OrderByDescending(x => x.IdProductoNavigation.Nombre);
                case "NombreBodega":
                    return ascending ?
                        source.OrderBy(x => x.IdBodegaNavigation.Nombre) :
                        source.OrderByDescending(x => x.IdBodegaNavigation.Nombre);
                case "Cantidad":
                    return ascending ?
                        source.OrderBy(x => x.Cantidad) :
                        source.OrderByDescending(x => x.Cantidad);
                case "PrecioUnitario":
                    return ascending ?
                        source.OrderBy(x => x.PrecioUnitario) :
                        source.OrderByDescending(x => x.PrecioUnitario);
                // Agrega otros casos si tienes más columnas para ordenar
                default:
                    return source.OrderByDescending(x => x.FechaMovimiento);
            }
        }



        //traer todas las solicitudes que no hayan sido ingresadas en MovimientosInventario
        public async Task<List<SolicitudesCompraDTO>> ObtenerSolicitudesNoUsadasAsync()
        {
            using var context = new InvensisContext();

            // 1. Obtener los IdSolicitud que ya han sido usados como documento origen
            var usadas = await context.MovimientosInventarios
                .Where(m => m.IdDocumentoOrigen != null)
                .Select(m => m.IdDocumentoOrigen.Value) // <- int
                .Distinct()
                .ToListAsync();

            // 2. Devolver las solicitudes que NO estén en esa lista
            return await context.SolicitudesCompras
                .Where(s => !usadas.Contains(s.IdSolicitud)) // <- correcto
                .Select(s => new SolicitudesCompraDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    NumeroSolicitud = s.NumeroSolicitud,
                    IdUsuarioAutoriza = s.IdUsuarioAutoriza,
                    IdUsuarioDestino = s.IdUsuarioDestino,
                    IdDepartamento = s.IdDepartamento
                })
                .ToListAsync();
        }


        //trae todos los productos por el idSolicitud que se le pase 
        public async Task<List<DetalleSolicitudDTO>> ObtenerDetalleSolicitudAsync(int idSolicitud)
        {
            using var context = new InvensisContext();
            return await context.DetalleSolicituds
                .Where(d => d.IdSolicitud == idSolicitud)
                .Select(d => new DetalleSolicitudDTO
                {

                    IdDetalle = d.IdDetalle,
                    IdSolicitud = d.IdSolicitud,
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal,

                })
                .ToListAsync();
        }



        //traer todas las facturas que no hayan sido ingresadas en MovimientosInventario
        public async Task<List<FacturasCompraDTO>> ObtenerFacturasNoUsadasAsync()
        {
            using var context = new InvensisContext();

            // 1. Obtener los IdFactura que ya han sido usados en movimientos
            var usados = await context.MovimientosInventarios
                .Where(m => m.IdDocumentoOrigen != null)
                .Select(m => m.IdDocumentoOrigen.Value) // <- como int
                .Distinct()
                .ToListAsync();

            // 2. Devolver las facturas que NO están en la lista de usados
            return await context.FacturasCompras
                .Where(f => !usados.Contains(f.IdFactura)) // <- correcto
                .Select(f => new FacturasCompraDTO
                {
                    IdFactura = f.IdFactura,
                    NumeroFactura = f.NumeroFactura,
                    IdProveedor = f.IdProveedor,
                    IdBodega = f.IdBodega
                })
                .ToListAsync();
        }


        //trae todos los productos por el idfactura que se le pase 
        public async Task<List<DetalleFacturaCompraDTO>> ObtenerDetalleFacturaAsync(int idFactura)
        {
            using var context = new InvensisContext();
            return await context.DetalleFacturaCompras
                .Where(d => d.IdFactura == idFactura)
                .Select(d => new DetalleFacturaCompraDTO
                {
                    IdDetalle = d.IdDetalle,
                    IdFactura = d.IdFactura,
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                })
                .ToListAsync();
        }

    }
}
