using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class MovimientosInventarioRepository
    {


        // ------------------ primero actualiza el stock de la bodega  luego registra el movimiento--------------------------------//
        public bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error)
        {
            error = null;

            var movimientosProcesados = new List<MovimientosInventarioDTO>();

            foreach (var movimiento in movimientos)
            {
                var tipo = movimiento.TipoMovimiento?.ToUpperInvariant();
                movimiento.FechaMovimiento ??= DateTime.Now;

                switch (tipo)
                {
                    case "TRANSFERENCIA":
                        if (movimiento.IdBodegaOrigen == null || movimiento.IdBodegaDestino == null)
                        {
                            error = "Para una transferencia, debe especificar la bodega origen y destino.";
                            return false;
                        }

                        // SALIDA desde la bodega origen
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

                        // ENTRADA hacia la bodega destino
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
                        if (movimiento.IdBodega == null || movimiento.IdBodega <= 0)
                        {
                            error = "Debe indicar la bodega de entrada (IdBodega).";
                            return false;
                        }
                        if (movimiento.IdDocumentoOrigen == null)
                        {
                            error = "Debe indicar el documento origen para la entrada.";
                            return false;
                        }

                        movimientosProcesados.Add(movimiento);
                        break;

                    case "SALIDA":
                        if (movimiento.IdBodega == null || movimiento.IdBodega <= 0)
                        {
                            error = "Debe indicar la bodega de salida (IdBodega).";
                            return false;
                        }

                        movimientosProcesados.Add(movimiento);
                        break;

                    case "AJUSTE":
                        if (movimiento.IdBodega == null || movimiento.IdBodega <= 0)
                        {
                            error = "Debe indicar la bodega para el ajuste.";
                            return false;
                        }

                        movimientosProcesados.Add(movimiento);
                        break;

                    default:
                        error = "Tipo de movimiento no reconocido.";
                        return false;
                }
            }

            // Procesar stock según la lógica establecida
            var stockRepo = new StockBodegaDataRepository();
            if (!stockRepo.ProcesarMovimientoStock(movimientosProcesados, out error))
            {
                return false;
            }

            // Guardar en base de datos
            using var context = new InvensisContext();
            foreach (var movimiento in movimientosProcesados)
            {
                context.MovimientosInventarios.Add(new MovimientosInventario
                {
                    IdBodega = movimiento.IdBodega,
                    IdProducto = movimiento.IdProducto,
                    TipoMovimiento = movimiento.TipoMovimiento,
                    Cantidad = movimiento.Cantidad,
                    PrecioUnitario = movimiento.PrecioUnitario,
                    NumeroSerie = movimiento.NumeroSerie,
                    StockAnterior = movimiento.StockAnterior,
                    StockActual = movimiento.StockActual,
                    Observaciones = movimiento.Observaciones,
                    UsuarioRegistro = movimiento.UsuarioRegistro,
                    FechaMovimiento = movimiento.FechaMovimiento,
                    Origen = movimiento.Origen,
                    IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
                    IdBodegaOrigen = movimiento.IdBodegaOrigen,
                    IdBodegaDestino = movimiento.IdBodegaDestino
                });
            }

            context.SaveChanges();
            return true;
        }




        public MovimientosInventario? ObtenerPorId(int id)
        {
            using var context = new InvensisContext();
            return context.MovimientosInventarios.FirstOrDefault(m => m.IdMovimiento == id);
        }

        //paginado que te busco por bodega, tipo de movimiento, nomnbre producto, fechas 
        public PagedResult<MovimientosInventarioDTO> GetPaginados(
                int pagina,
                int pageSize,
                string? tipoMovimiento,
                int? idBodega,
                string? nombreProducto,
                DateTime? desde,
                DateTime? hasta)
        {
            using var context = new InvensisContext();
            var query = context.MovimientosInventarios
                .Include(x => x.IdProductoNavigation)
                .Include(x => x.IdBodegaNavigation)
                .AsQueryable();

            // Validación de rango de fechas
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

            if (!string.IsNullOrWhiteSpace(tipoMovimiento))
                query = query.Where(x => x.TipoMovimiento.ToUpper() == tipoMovimiento.ToUpper());

            if (idBodega.HasValue)
                query = query.Where(x =>
                    x.IdBodega == idBodega.Value ||
                    x.IdBodegaOrigen == idBodega.Value ||
                    x.IdBodegaDestino == idBodega.Value);

            if (!string.IsNullOrWhiteSpace(nombreProducto))
            {
                var nombre = nombreProducto.Trim().ToUpper();
                query = query.Where(x => x.IdProductoNavigation.Nombre.ToUpper().Contains(nombre));
            }

            if (desde.HasValue)
                query = query.Where(x => x.FechaMovimiento >= desde.Value);

            if (hasta.HasValue)
                query = query.Where(x => x.FechaMovimiento <= hasta.Value);

            var totalItems = query.Count();

            var items = query
                .OrderByDescending(x => x.FechaMovimiento)
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

                    // NUEVOS CAMPOS
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



        //traer todas las solicitudes que no hayan sido ingresadas en MovimientosInventario
        public async Task<List<SolicitudesCompraDTO>> ObtenerSolicitudesNoUsadasAsync()
        {
            // Obtener lista de solicitudes que NO han sido usadas como documento origen
            using var context = new InvensisContext();
            var usadas = await context.MovimientosInventarios
                .Where(m => m.IdDocumentoOrigen != null)
                .Select(m => m.IdDocumentoOrigen!.ToString())
                .Distinct()
                .ToListAsync();

            return await context.SolicitudesCompras
                .Where(f => !usadas.Contains(f.NumeroSolicitud))
                .Select(f => new SolicitudesCompraDTO
                {
                    IdSolicitud = f.IdSolicitud,
                    NumeroSolicitud = f.NumeroSolicitud,
                    IdUsuarioAutoriza = f.IdUsuarioAutoriza,
                    IdUsuarioDestino = f.IdUsuarioDestino,
                    IdDepartamento = f.IdDepartamento
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
            // Obtener lista de facturas que NO han sido usadas como documento origen
            using var context = new InvensisContext();
            var usadas = await context.MovimientosInventarios
                .Where(m => m.IdDocumentoOrigen != null)
                .Select(m => m.IdDocumentoOrigen!.ToString())
                .Distinct()
                .ToListAsync();

            return await context.FacturasCompras
                .Where(f => !usadas.Contains(f.NumeroFactura))
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
