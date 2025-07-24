using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class AsignacionesActivoRepository
    {
        public List<AsignacionesActivo> AsignacionesActivoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.AsignacionesActivos.ToList();
            }
        }

        public AsignacionesActivo GetAsignacionesActivoById(int IdAsignacionesActivo)
        {
            using (var context = new InvensisContext())
            {
                return context.AsignacionesActivos.FirstOrDefault(a => a.IdAsignacion == IdAsignacionesActivo);
            }
        }

        public void InsertAsignacionesActivo(AsignacionesActivo newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.AsignacionesActivos.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateAsignacionesActivo(AsignacionesActivo historial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.AsignacionesActivos.FirstOrDefault(a => a.IdAsignacion == historial.IdAsignacion);
                if (existente != null)
                {
                    existente.IdActivo = historial.IdActivo;
                    //existente.IdUsuario = historial.IdUsuario;
                    existente.IdOrdenEntrega = historial.IdOrdenEntrega;
                    existente.FechaAsignacion = historial.FechaAsignacion;
                    existente.FechaDevolucion = historial.FechaDevolucion;
                    existente.ActaEntrega = historial.ActaEntrega;
                    existente.ActaDevolucion = historial.ActaDevolucion;
                    existente.EstadoAsignacion = historial.EstadoAsignacion;
                    existente.CondicionEntrega = historial.CondicionEntrega;
                    existente.CondicionDevolucion = historial.CondicionDevolucion;
                    existente.ObservacionesEntrega = historial.ObservacionesEntrega;
                    existente.ObservacionesDevolucion = historial.ObservacionesDevolucion;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteAsignacionesActivo(AsignacionesActivo activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.AsignacionesActivos.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteAsignacionesActivoById(int idAsignacionesActivo)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.AsignacionesActivos.FirstOrDefault(a => a.IdAsignacion == idAsignacionesActivo);
                if (existente != null)
                {
                    context.AsignacionesActivos.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
