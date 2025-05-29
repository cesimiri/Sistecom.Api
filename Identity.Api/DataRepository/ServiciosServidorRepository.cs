using Modelo.Sistecom.Modelo.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Identity.Api.DataRepository
{
    public class ServiciosServidorRepository
    {
        public List<ServiciosServidor> ServiciosServidorInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.ServiciosServidors.ToList();
            }
        }

        public ServiciosServidor GetServiciosServidorById(int IdServiciosServidor)
        {
            using (var context = new InvensisContext())
            {
                return context.ServiciosServidors.FirstOrDefault(a => a.IdServicio == IdServiciosServidor);
            }
        }

        public void InsertServiciosServidor(ServiciosServidor newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.ServiciosServidors.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateServiciosServidor(ServiciosServidor historial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.ServiciosServidors.FirstOrDefault(a => a.IdServicio == historial.IdServicio);
                if (existente != null)
                {
                    existente.IdServidor = historial.IdServidor;
                    existente.NombreServicio = historial.NombreServicio;
                    existente.TipoServicio = historial.TipoServicio;
                    existente.Puerto = historial.Puerto;
                    existente.Version = historial.Version;
                    existente.Estado = historial.Estado;
                    existente.FechaInstalacion = historial.FechaInstalacion;
                    existente.Observaciones = historial.Observaciones;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteServiciosServidor(ServiciosServidor activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.ServiciosServidors.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteServiciosServidorById(int idServiciosServidor)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.ServiciosServidors.FirstOrDefault(a => a.IdServicio == idServiciosServidor);
                if (existente != null)
                {
                    context.ServiciosServidors.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
