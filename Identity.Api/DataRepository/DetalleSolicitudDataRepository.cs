﻿using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.DataRepository
{
    public class DetalleSolicitudDataRepository
    {
        public List<DetalleSolicitud> GetAllDetalleSolicitudes()
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleSolicituds.ToList();
            }
        }

        public DetalleSolicitud GetDetalleSolicitudById(int idDetalle)
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleSolicituds.FirstOrDefault(d => d.IdDetalle == idDetalle);
            }
        }

        public void InsertDetalleSolicitud(DetalleSolicitud newItem)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleSolicituds.Add(newItem);
                context.SaveChanges();
            }
        }

        public void UpdateDetalleSolicitud(DetalleSolicitud updItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.DetalleSolicituds.FirstOrDefault(d => d.IdDetalle == updItem.IdDetalle);

                if (registrado != null)
                {
                    registrado.IdSolicitud = updItem.IdSolicitud;
                    registrado.IdProducto = updItem.IdProducto;
                    registrado.Cantidad = updItem.Cantidad;
                    registrado.PrecioUnitario = updItem.PrecioUnitario;
                    registrado.Descuento = updItem.Descuento;
                    registrado.Subtotal = updItem.Subtotal;
                    registrado.IdUsuarioDestino = updItem.IdUsuarioDestino;
                    registrado.Observaciones = updItem.Observaciones;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteDetalleSolicitud(DetalleSolicitud delItem)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleSolicituds.Remove(delItem);
                context.SaveChanges();
            }
        }

        public void DeleteDetalleSolicitudById(int idDetalle)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.DetalleSolicituds.FirstOrDefault(d => d.IdDetalle == idDetalle);

                if (registrado != null)
                {
                    context.DetalleSolicituds.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}
