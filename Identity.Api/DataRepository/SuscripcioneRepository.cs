using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.DataRepository
{
    public class SuscripcioneDataRepository
    {
        public List<Suscripcione> GetAllSuscripciones()
        {
            using (var context = new InvensisContext())
            {
                return context.Suscripciones.ToList();
            }
        }

        public Suscripcione GetSuscripcionById(int idSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                return context.Suscripciones.FirstOrDefault(s => s.IdSuscripcion == idSuscripcion);
            }
        }

        public void InsertSuscripcion(Suscripcione newSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                context.Suscripciones.Add(newSuscripcion);
                context.SaveChanges();
            }
        }

        public void UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Suscripciones
                    .FirstOrDefault(s => s.IdSuscripcion == updatedSuscripcion.IdSuscripcion);

                if (suscripcion != null)
                {
                    suscripcion.IdProveedor = updatedSuscripcion.IdProveedor;
                    suscripcion.IdEmpresa = updatedSuscripcion.IdEmpresa;
                    suscripcion.NombreServicio = updatedSuscripcion.NombreServicio;
                    suscripcion.TipoSuscripcion = updatedSuscripcion.TipoSuscripcion;
                    suscripcion.FechaInicio = updatedSuscripcion.FechaInicio;
                    suscripcion.FechaRenovacion = updatedSuscripcion.FechaRenovacion;
                    suscripcion.PeriodoFacturacion = updatedSuscripcion.PeriodoFacturacion;
                    suscripcion.CostoPeriodo = updatedSuscripcion.CostoPeriodo;
                    suscripcion.UsuariosIncluidos = updatedSuscripcion.UsuariosIncluidos;
                    suscripcion.AlmacenamientoGb = updatedSuscripcion.AlmacenamientoGb;
                    suscripcion.UrlAcceso = updatedSuscripcion.UrlAcceso;
                    suscripcion.Administrador = updatedSuscripcion.Administrador;
                    suscripcion.Estado = updatedSuscripcion.Estado;
                    suscripcion.NotificarDiasAntes = updatedSuscripcion.NotificarDiasAntes;
                    suscripcion.Observaciones = updatedSuscripcion.Observaciones;
                    suscripcion.FechaRegistro = updatedSuscripcion.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Suscripciones.Remove(suscripcionToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteSuscripcionById(int idSuscripcion)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Suscripciones
                    .FirstOrDefault(s => s.IdSuscripcion == idSuscripcion);

                if (suscripcion != null)
                {
                    context.Suscripciones.Remove(suscripcion);
                    context.SaveChanges();
                }
            }
        }
    }
}
