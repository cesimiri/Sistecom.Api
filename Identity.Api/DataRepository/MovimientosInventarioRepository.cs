using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class MovimientosInventarioRepository
    {
        public List<MovimientosInventario> MovimientosInventarioInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.MovimientosInventarios.ToList();
            }
        }

        public MovimientosInventario GetMovimientosInventarioById(int IdMovimientosInventario)
        {
            using (var context = new InvensisContext())
            {
                return context.MovimientosInventarios.FirstOrDefault(m => m.IdMovimiento == IdMovimientosInventario);
            }
        }

        public void InsertMovimientosInventario(MovimientosInventario nuevoMovimiento)
        {
            using (var context = new InvensisContext())
            {
                context.MovimientosInventarios.Add(nuevoMovimiento);
                context.SaveChanges();
            }
        }


        public void UpdateMovimientosInventario(MovimientosInventario movimientoActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.MovimientosInventarios.FirstOrDefault(a => a.IdMovimiento == movimientoActualizado.IdMovimiento);
                if (existente != null)
                {
                    existente.IdBodega = movimientoActualizado.IdBodega;
                    existente.IdProducto = movimientoActualizado.IdProducto;
                    existente.TipoMovimiento = movimientoActualizado.TipoMovimiento;
                    existente.Origen = movimientoActualizado.Origen;
                    existente.IdDocumentoOrigen = movimientoActualizado.IdDocumentoOrigen;
                    existente.IdBodegaOrigen = movimientoActualizado.IdBodegaOrigen;
                    existente.IdBodegaDestino = movimientoActualizado.IdBodegaDestino;
                    existente.Cantidad = movimientoActualizado.Cantidad;
                    existente.PrecioUnitario = movimientoActualizado.PrecioUnitario;
                    existente.NumeroSerie = movimientoActualizado.NumeroSerie;
                    existente.StockAnterior = movimientoActualizado.StockAnterior;
                    existente.StockActual = movimientoActualizado.StockActual;
                    existente.Observaciones = movimientoActualizado.Observaciones;
                    existente.UsuarioRegistro = movimientoActualizado.UsuarioRegistro;
                    existente.FechaMovimiento = movimientoActualizado.FechaMovimiento;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteMovimientosInventario(MovimientosInventario activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.MovimientosInventarios.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteMovimientosInventarioById(int idMovimientosInventario)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.MovimientosInventarios.FirstOrDefault(a => a.IdMovimiento == idMovimientosInventario);
                if (existente != null)
                {
                    context.MovimientosInventarios.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
