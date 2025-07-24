using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{

    public class HistorialActivoRepository
    {
        public List<HistorialActivo> HistorialActivoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.HistorialActivos.ToList();
            }
        }

        public HistorialActivo GetHistorialActivoById(int IdHistorialActivo)
        {
            using (var context = new InvensisContext())
            {
                return context.HistorialActivos.FirstOrDefault(a => a.IdHistorial == IdHistorialActivo);
            }
        }

        public void InsertHistorialActivo(HistorialActivo newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.HistorialActivos.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateHistorialActivo(HistorialActivo historial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.HistorialActivos.FirstOrDefault(a => a.IdHistorial == historial.IdHistorial);
                if (existente != null)
                {
                    existente.IdActivo = historial.IdActivo;
                    existente.TipoEvento = historial.TipoEvento;
                    existente.FechaEvento = historial.FechaEvento;
                    existente.Descripcion = historial.Descripcion;
                    //existente.IdUsuarioResponsable = historial.IdUsuarioResponsable;
                    existente.IdDocumentoReferencia = historial.IdDocumentoReferencia;
                    existente.CostoAsociado = historial.CostoAsociado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteHistorialActivo(HistorialActivo activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.HistorialActivos.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteHistorialActivoById(int idHistorial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.HistorialActivos.FirstOrDefault(a => a.IdHistorial == idHistorial);
                if (existente != null)
                {
                    context.HistorialActivos.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
