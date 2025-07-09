using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class MovimientosInventarioRepository
    {
        //public List<MovimientosInventario> MovimientosInventarioInfoAll()
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        return context.MovimientosInventarios.ToList();
        //    }
        //}

        //public MovimientosInventario GetMovimientosInventarioById(int IdMovimientosInventario)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        return context.MovimientosInventarios.FirstOrDefault(m => m.IdMovimiento == IdMovimientosInventario);
        //    }
        //}

        //public void InsertMovimientosInventario(MovimientosInventario nuevoMovimiento)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.MovimientosInventarios.Add(nuevoMovimiento);
        //        context.SaveChanges();
        //    }
        //}

        ////ingreso masivo
        ////public void InsertarMovimientoInventarioMasivo(List<MovimientosInventarioDTO> lista)
        ////{
        ////    using var context = new InvensisContext();

        ////    if (lista == null || lista.Count == 0)
        ////        throw new Exception("La lista de productos está vacía.");

        ////    int idFactura = lista[0].IdFactura; // ❗ Usa IdFactura, no IdDetalle

        ////    foreach (var item in lista)
        ////    {
        ////        var facturaEntity = context.FacturasCompras.Find(item.IdFactura);
        ////        var producto = context.Productos.Find(item.IdProducto);

        ////        if (facturaEntity == null || producto == null)
        ////        {
        ////            throw new Exception("Error: Factura o Producto no válido.");
        ////        }

        ////        var nuevoDetalle = new MovimientosInventarioDTO
        ////        {
        ////            IdBodega = item.IdBodega,
        ////            IdProducto = item.IdProducto,
        ////            TipoMovimiento = item.TipoMovimiento,
        ////            Origen = item.Origen,
        ////            IdDocumentoOrigen = item.IdDocumentoOrigen,
        ////            IdBodegaOrigen = item.IdBodegaOrigen,
        ////            IdBodegaDestino = item.IdBodegaDestino,
        ////            Cantidad = item.Cantidad,
        ////            PrecioUnitario = item.PrecioUnitario,
        ////            NumeroSerie = item.NumeroSerie,
        ////            StockAnterior = item.StockAnterior,
        ////            StockActual = item.StockActual,
        ////            Observaciones = item.Observaciones,
        ////            UsuarioRegistro = item.UsuarioRegistro,
        ////            FechaMovimiento = item.FechaMovimiento,

        ////        };


        ////        context.MovimientosInventarios.Add(nuevoDetalle);
        ////    }

        ////    // Guardar los detalles nuevos primero
        ////    context.SaveChanges();

        ////}


        //public void UpdateMovimientosInventario(MovimientosInventario movimientoActualizado)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        var existente = context.MovimientosInventarios.FirstOrDefault(a => a.IdMovimiento == movimientoActualizado.IdMovimiento);
        //        if (existente != null)
        //        {
        //            existente.IdBodega = movimientoActualizado.IdBodega;
        //            existente.IdProducto = movimientoActualizado.IdProducto;
        //            existente.TipoMovimiento = movimientoActualizado.TipoMovimiento;
        //            existente.Origen = movimientoActualizado.Origen;
        //            existente.IdDocumentoOrigen = movimientoActualizado.IdDocumentoOrigen;
        //            existente.IdBodegaOrigen = movimientoActualizado.IdBodegaOrigen;
        //            existente.IdBodegaDestino = movimientoActualizado.IdBodegaDestino;
        //            existente.Cantidad = movimientoActualizado.Cantidad;
        //            existente.PrecioUnitario = movimientoActualizado.PrecioUnitario;
        //            existente.NumeroSerie = movimientoActualizado.NumeroSerie;
        //            existente.StockAnterior = movimientoActualizado.StockAnterior;
        //            existente.StockActual = movimientoActualizado.StockActual;
        //            existente.Observaciones = movimientoActualizado.Observaciones;
        //            existente.UsuarioRegistro = movimientoActualizado.UsuarioRegistro;
        //            existente.FechaMovimiento = movimientoActualizado.FechaMovimiento;

        //            context.SaveChanges();
        //        }
        //    }
        //}

        //public void DeleteMovimientosInventario(MovimientosInventario activoToDelete)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.MovimientosInventarios.Remove(activoToDelete);
        //        context.SaveChanges();
        //    }
        //}

        //public void DeleteMovimientosInventarioById(int idMovimientosInventario)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        var existente = context.MovimientosInventarios.FirstOrDefault(a => a.IdMovimiento == idMovimientosInventario);
        //        if (existente != null)
        //        {
        //            context.MovimientosInventarios.Remove(existente);
        //            context.SaveChanges();
        //        }
        //    }
        //}

        // ------------------ primero actualiza el stock de la bodega  luego registra el movimiento--------------------------------//
        public bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error)
        {
            error = null;

            var stockRepo = new StockBodegaDataRepository();
            if (!stockRepo.ProcesarMovimientoStock(movimientos, out error))
            {
                return false;
            }

            using var context = new InvensisContext();

            foreach (var movimiento in movimientos)
            {
                var entidad = new MovimientosInventario
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
                    FechaMovimiento = movimiento.FechaMovimiento ?? DateTime.Now,
                    Origen = movimiento.Origen,
                    IdDocumentoOrigen = movimiento.IdDocumentoOrigen,
                    IdBodegaOrigen = movimiento.IdBodegaOrigen,
                    IdBodegaDestino = movimiento.IdBodegaDestino
                };

                context.MovimientosInventarios.Add(entidad);
            }

            context.SaveChanges();

            return true;
        }





        public MovimientosInventario? ObtenerPorId(int id)
        {
            using var context = new InvensisContext();
            return context.MovimientosInventarios.FirstOrDefault(m => m.IdMovimiento == id);
        }

        //paginado
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

            if (!string.IsNullOrWhiteSpace(tipoMovimiento))
                query = query.Where(x => x.TipoMovimiento.ToUpper() == tipoMovimiento.ToUpper());

            if (idBodega.HasValue)
                query = query.Where(x => x.IdBodega == idBodega.Value || x.IdBodegaOrigen == idBodega || x.IdBodegaDestino == idBodega);

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
                    Observaciones = x.Observaciones
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

    }
}
