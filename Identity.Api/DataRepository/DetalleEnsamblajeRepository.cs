using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class DetalleEnsamblajeRepository
    {
        public List<DetalleEnsamblaje> DetalleEnsamblajeInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleEnsamblajes.ToList();
            }
        }

        public DetalleEnsamblaje GetDetalleEnsamblajeById(int IdDetalleEnsamblaje)
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleEnsamblajes.FirstOrDefault(m => m.IdDetalle == IdDetalleEnsamblaje);
            }
        }

        public void InsertDetalleEnsamblaje(DetalleEnsamblaje nuevoMovimiento)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleEnsamblajes.Add(nuevoMovimiento);
                context.SaveChanges();
            }
        }


        public void UpdateDetalleEnsamblaje(DetalleEnsamblaje detalleActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.DetalleEnsamblajes.FirstOrDefault(a => a.IdDetalle == detalleActualizado.IdDetalle);
                if (existente != null)
                {
                    existente.IdOrdenEnsamblaje = detalleActualizado.IdOrdenEnsamblaje;
                    existente.IdComponente = detalleActualizado.IdComponente;
                    existente.CantidadRequerida = detalleActualizado.CantidadRequerida;
                    existente.CantidadUsada = detalleActualizado.CantidadUsada;
                    existente.LoteComponente = detalleActualizado.LoteComponente;
                    existente.Observaciones = detalleActualizado.Observaciones;


                    context.SaveChanges();
                }
            }
        }

        public void DeleteDetalleEnsamblaje(DetalleEnsamblaje activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleEnsamblajes.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteDetalleEnsamblajeById(int idDetalleEnsamblaje)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.DetalleEnsamblajes.FirstOrDefault(a => a.IdDetalle == idDetalleEnsamblaje);
                if (existente != null)
                {
                    context.DetalleEnsamblajes.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
