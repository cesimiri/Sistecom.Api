using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class OrdenesEnsamblajeRepository
    {
        public List<OrdenesEnsamblaje> OrdenesEnsamblajeInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.OrdenesEnsamblajes.ToList();
            }
        }

        public OrdenesEnsamblaje GetOrdenesEnsamblajeById(int IdOrdenesEnsamblaje)
        {
            using (var context = new InvensisContext())
            {
                return context.OrdenesEnsamblajes.FirstOrDefault(m => m.IdOrdenEnsamblaje == IdOrdenesEnsamblaje);
            }
        }

        public void InsertOrdenesEnsamblaje(OrdenesEnsamblaje nuevoMovimiento)
        {
            using (var context = new InvensisContext())
            {
                context.OrdenesEnsamblajes.Add(nuevoMovimiento);
                context.SaveChanges();
            }
        }


        public void UpdateOrdenesEnsamblaje(OrdenesEnsamblaje ordenActualizada)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.OrdenesEnsamblajes.FirstOrDefault(a => a.IdOrdenEnsamblaje == ordenActualizada.IdOrdenEnsamblaje);
                if (existente != null)
                {
                    existente.NumeroOrden = ordenActualizada.NumeroOrden;
                    existente.IdProductoFinal = ordenActualizada.IdProductoFinal;
                    existente.Cantidad = ordenActualizada.Cantidad;
                    existente.IdBodegaOrigen = ordenActualizada.IdBodegaOrigen;
                    existente.IdBodegaDestino = ordenActualizada.IdBodegaDestino;
                    existente.FechaOrden = ordenActualizada.FechaOrden;
                    existente.FechaInicio = ordenActualizada.FechaInicio;
                    existente.FechaCompletado = ordenActualizada.FechaCompletado;
                    existente.Estado = ordenActualizada.Estado;
                    existente.TecnicoResponsable = ordenActualizada.TecnicoResponsable;
                    existente.TiempoTotalMinutos = ordenActualizada.TiempoTotalMinutos;
                    existente.CostoTotal = ordenActualizada.CostoTotal;
                    existente.Observaciones = ordenActualizada.Observaciones;


                    context.SaveChanges();
                }
            }
        }

        public void DeleteOrdenesEnsamblaje(OrdenesEnsamblaje activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.OrdenesEnsamblajes.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteOrdenesEnsamblajeById(int idOrdenesEnsamblaje)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.OrdenesEnsamblajes.FirstOrDefault(a => a.IdOrdenEnsamblaje == idOrdenesEnsamblaje);
                if (existente != null)
                {
                    context.OrdenesEnsamblajes.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
