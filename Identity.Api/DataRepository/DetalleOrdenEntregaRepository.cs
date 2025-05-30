using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class DetalleOrdenEntregaRepository
    {
        public List<DetalleOrdenEntrega> DetalleOrdenEntregaInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleOrdenEntregas.ToList();
            }
        }

        public DetalleOrdenEntrega GetDetalleOrdenEntregaById(int IdDetalleOrdenEntrega)
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleOrdenEntregas.FirstOrDefault(a => a.IdDetalle == IdDetalleOrdenEntrega);
            }
        }

        public void InsertDetalleOrdenEntrega(DetalleOrdenEntrega newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleOrdenEntregas.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateDetalleOrdenEntrega(DetalleOrdenEntrega detalleActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.DetalleOrdenEntregas.FirstOrDefault(a => a.IdDetalle == detalleActualizado.IdDetalle);
                if (existente != null)
                {
                    existente.IdOrden = detalleActualizado.IdOrden;
                    existente.IdProducto = detalleActualizado.IdProducto;
                    existente.CantidadProgramada = detalleActualizado.CantidadProgramada;
                    existente.CantidadEntregada = detalleActualizado.CantidadEntregada;
                    existente.IdActivo = detalleActualizado.IdActivo;
                    existente.IdLicencia = detalleActualizado.IdLicencia;
                    existente.Observaciones = detalleActualizado.Observaciones;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteDetalleOrdenEntrega(DetalleOrdenEntrega activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleOrdenEntregas.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteDetalleOrdenEntregaById(int idDetalleOrdenEntrega)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.DetalleOrdenEntregas.FirstOrDefault(a => a.IdDetalle == idDetalleOrdenEntrega);
                if (existente != null)
                {
                    context.DetalleOrdenEntregas.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
