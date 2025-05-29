using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.DataRepository
{
    public class OrdenesEntregaRepository
    {
        public List<OrdenesEntrega> OrdenesEntregaInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.OrdenesEntregas.ToList();
            }
        }

        public OrdenesEntrega GetOrdenesEntregaById(int IdOrdenesEntrega)
        {
            using (var context = new InvensisContext())
            {
                return context.OrdenesEntregas.FirstOrDefault(p => p.IdOrden == IdOrdenesEntrega); ;
            }

        }

        public void InsertOrdenesEntrega(OrdenesEntrega NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.OrdenesEntregas.Add(NewItem);
                context.SaveChanges();
            }
        }

        public void UpdateOrdenesEntrega(OrdenesEntrega UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.OrdenesEntregas
                                         .Where(a => a.IdOrden == UpdItem.IdOrden)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.NumeroOrden = UpdItem.NumeroOrden;
                    registrado.IdSolicitud = UpdItem.IdSolicitud;
                    registrado.IdUsuarioRecibe = UpdItem.IdUsuarioRecibe;
                    registrado.FechaProgramada = UpdItem.FechaProgramada;
                    registrado.HoraProgramada = UpdItem.HoraProgramada;
                    registrado.FechaEntrega = UpdItem.FechaEntrega;
                    registrado.DireccionEntrega = UpdItem.DireccionEntrega;
                    registrado.ContactoRecepcion = UpdItem.ContactoRecepcion;
                    registrado.TelefonoContacto = UpdItem.TelefonoContacto;
                    registrado.Estado = UpdItem.Estado;
                    registrado.GuiaRemision = UpdItem.GuiaRemision;
                    registrado.Transportista = UpdItem.Transportista;
                    registrado.FirmaRecepcion = UpdItem.FirmaRecepcion;
                    registrado.FotoEntrega = UpdItem.FotoEntrega;
                    registrado.IncluyeLicencias = UpdItem.IncluyeLicencias;
                    registrado.ObservacionesEntrega = UpdItem.ObservacionesEntrega;
                    registrado.FechaRegistro = UpdItem.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteOrdenesEntrega(OrdenesEntrega NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.OrdenesEntregas.Remove(NewItem);
                context.SaveChanges();
            }
        }

        public void DeleteOrdenesEntregaById(int IdOrdenesEntega)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.OrdenesEntregas
                                         .Where(a => a.IdOrden == IdOrdenesEntega)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.OrdenesEntregas.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}
