using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.DataRepository
{
    public class SolicitudesCompraDataRepository
    {
        public List<SolicitudesCompra> GetAllSolicitudesCompra()
        {
            using (var context = new InvensisContext())
            {
                return context.SolicitudesCompras.ToList();
            }
        }

        public SolicitudesCompra GetSolicitudById(int idSolicitud)
        {
            using (var context = new InvensisContext())
            {
                return context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == idSolicitud);
            }
        }

        public void InsertSolicitud(SolicitudesCompra newSolicitud)
        {
            using (var context = new InvensisContext())
            {
                context.SolicitudesCompras.Add(newSolicitud);
                context.SaveChanges();
            }
        }

        public void UpdateSolicitud(SolicitudesCompra updatedSolicitud)
        {
            using (var context = new InvensisContext())
            {
                var solicitud = context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == updatedSolicitud.IdSolicitud);

                if (solicitud != null)
                {
                    solicitud.NumeroSolicitud = updatedSolicitud.NumeroSolicitud;
                    solicitud.RucEmpresa = updatedSolicitud.RucEmpresa;
                    solicitud.IdUsuarioSolicita = updatedSolicitud.IdUsuarioSolicita;
                    //solicitud.IdContrato = updatedSolicitud.IdContrato;
                    solicitud.FechaSolicitud = updatedSolicitud.FechaSolicitud;
                    solicitud.FechaAprobacion = updatedSolicitud.FechaAprobacion;
                    solicitud.FechaRequerida = updatedSolicitud.FechaRequerida;
                    solicitud.SubtotalSinImpuestos = updatedSolicitud.SubtotalSinImpuestos;
                    solicitud.DescuentoTotal = updatedSolicitud.DescuentoTotal;
                    solicitud.Iva = updatedSolicitud.Iva;
                    solicitud.ValorTotal = updatedSolicitud.ValorTotal;
                    solicitud.Estado = updatedSolicitud.Estado;
                    solicitud.MotivoRechazo = updatedSolicitud.MotivoRechazo;
                    solicitud.Observaciones = updatedSolicitud.Observaciones;
                    solicitud.ArchivoOc = updatedSolicitud.ArchivoOc;
                    solicitud.FechaRegistro = updatedSolicitud.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteSolicitud(SolicitudesCompra solicitudToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.SolicitudesCompras.Remove(solicitudToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteSolicitudById(int idSolicitud)
        {
            using (var context = new InvensisContext())
            {
                var solicitud = context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == idSolicitud);

                if (solicitud != null)
                {
                    context.SolicitudesCompras.Remove(solicitud);
                    context.SaveChanges();
                }
            }
        }
    }
}
