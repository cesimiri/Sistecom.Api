using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class LicenciaRepository
    {
        public List<Licencia> LicenciaInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.Licencias.ToList();
            }
        }

        public Licencia GetLicenciaById(int IdLicencia)
        {
            using (var context = new InvensisContext())
            {
                return context.Licencias.FirstOrDefault(m => m.IdLicencia == IdLicencia);
            }
        }

        public void InsertLicencia(Licencia nuevoMovimiento)
        {
            using (var context = new InvensisContext())
            {
                context.Licencias.Add(nuevoMovimiento);
                context.SaveChanges();
            }
        }


        public void UpdateLicencia(Licencia licenciaActualizada)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Licencias.FirstOrDefault(a => a.IdLicencia == licenciaActualizada.IdLicencia);
                if (existente != null)
                {
                    existente.IdTipoLicencia = licenciaActualizada.IdTipoLicencia;
                    existente.IdProducto = licenciaActualizada.IdProducto;
                    existente.IdFacturaCompra = licenciaActualizada.IdFacturaCompra;
                    existente.NumeroLicencia = licenciaActualizada.NumeroLicencia;
                    existente.ClaveProducto = licenciaActualizada.ClaveProducto;
                    existente.FechaAdquisicion = licenciaActualizada.FechaAdquisicion;
                    existente.FechaInicioVigencia = licenciaActualizada.FechaInicioVigencia;
                    existente.FechaFinVigencia = licenciaActualizada.FechaFinVigencia;
                    existente.TipoSuscripcion = licenciaActualizada.TipoSuscripcion;
                    existente.CantidadUsuarios = licenciaActualizada.CantidadUsuarios;
                    existente.CostoLicencia = licenciaActualizada.CostoLicencia;
                    existente.RenovacionAutomatica = licenciaActualizada.RenovacionAutomatica;
                    existente.Observaciones = licenciaActualizada.Observaciones;
                    existente.Estado = licenciaActualizada.Estado;
                    existente.FechaRegistro = licenciaActualizada.FechaRegistro;


                    context.SaveChanges();
                }
            }
        }

        public void DeleteLicencia(Licencia activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Licencias.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteLicenciaById(int idLicencia)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Licencias.FirstOrDefault(a => a.IdLicencia == idLicencia);
                if (existente != null)
                {
                    context.Licencias.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
