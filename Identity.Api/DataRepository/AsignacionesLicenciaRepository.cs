using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class AsignacionesLicenciaRepository
    {
        public List<AsignacionesLicencia> AsignacionesLicenciaInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.AsignacionesLicencias.ToList();
            }
        }

        public AsignacionesLicencia GetAsignacionesLicenciaById(int IdAsignacionesLicencia)
        {
            using (var context = new InvensisContext())
            {
                return context.AsignacionesLicencias.FirstOrDefault(a => a.IdAsignacionLicencia == IdAsignacionesLicencia);
            }
        }

        public void InsertAsignacionesLicencia(AsignacionesLicencia newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.AsignacionesLicencias.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateAsignacionesLicencia(AsignacionesLicencia asignacionActualizada)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.AsignacionesLicencias.FirstOrDefault(a => a.IdAsignacionLicencia == asignacionActualizada.IdAsignacionLicencia);
                if (existente != null)
                {
                    existente.IdLicencia = asignacionActualizada.IdLicencia;
                    existente.IdActivo = asignacionActualizada.IdActivo;
                    //existente.IdUsuario = asignacionActualizada.IdUsuario;
                    existente.IdServidor = asignacionActualizada.IdServidor;
                    existente.FechaAsignacion = asignacionActualizada.FechaAsignacion;
                    existente.FechaDesasignacion = asignacionActualizada.FechaDesasignacion;
                    existente.TipoAsignacion = asignacionActualizada.TipoAsignacion;
                    existente.Estado = asignacionActualizada.Estado;
                    existente.Observaciones = asignacionActualizada.Observaciones;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteAsignacionesLicencia(AsignacionesLicencia activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.AsignacionesLicencias.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteAsignacionesLicenciaById(int idAsignacionesLicencia)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.AsignacionesLicencias.FirstOrDefault(a => a.IdAsignacionLicencia == idAsignacionesLicencia);
                if (existente != null)
                {
                    context.AsignacionesLicencias.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
