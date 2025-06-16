using Identity.Api.DTO;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.DataRepository
{
    public class DetalleSolicitudDataRepository
    {
        public List<DetalleSolicitudDTO> DetalleSolicitudesAll()
        {
            using var context = new InvensisContext();
            return context.DetalleSolicituds
                .Include(s => s.IdUsuarioDestinoNavigation)
                .Include(s => s.IdSolicitudNavigation)
                .Include(s => s.IdProductoNavigation)
                
                .Select(s => new DetalleSolicitudDTO
                {
                    IdDetalle = s.IdDetalle,
                    IdSolicitud = s.IdSolicitud,
                    IdProducto = s.IdProducto,
                    Cantidad = s.Cantidad,
                    PrecioUnitario = s.PrecioUnitario,
                    Descuento = s.Descuento,
                    Subtotal = s.Subtotal,
                    IdUsuarioDestino = s.IdUsuarioDestino,
                    Observaciones = s.Observaciones,

                    // campos relacionados:
                    UsuarioDestino = s.IdUsuarioDestinoNavigation.Nombres + " " + s.IdUsuarioDestinoNavigation.Apellidos,
                    NumeroSolicitud = s.IdSolicitudNavigation.NumeroSolicitud,
                    CodigoPrincipal = s.IdProductoNavigation.Nombre
                })
                .ToList();
        }

        public DetalleSolicitudDTO GetDetalleSolicitudById(int idDetalle)
        {
            using var context = new InvensisContext();

            return context.DetalleSolicituds
                .Include(s => s.IdUsuarioDestinoNavigation)
                .Include(s => s.IdSolicitudNavigation)
                .Include(s => s.IdProductoNavigation)

                .Where(s => s.IdSolicitud == idDetalle)
                .Select(s => new DetalleSolicitudDTO
                {
                   
                    IdDetalle = s.IdDetalle,
                    IdSolicitud = s.IdSolicitud,
                    IdProducto = s.IdProducto,
                    Cantidad = s.Cantidad,
                    PrecioUnitario = s.PrecioUnitario,
                    Descuento = s.Descuento,
                    Subtotal = s.Subtotal,
                    IdUsuarioDestino = s.IdUsuarioDestino,
                    Observaciones = s.Observaciones,
                    // campos relacionados:
                    UsuarioDestino = s.IdUsuarioDestinoNavigation.Nombres + " " + s.IdUsuarioDestinoNavigation.Apellidos,
                    NumeroSolicitud = s.IdSolicitudNavigation.NumeroSolicitud,
                    CodigoPrincipal = s.IdProductoNavigation.Nombre

                })
                .FirstOrDefault();
        }

        public void InsertDetalleSolicitud(DetalleSolicitudDTO newItem)
        {
            try
            {
                using var context = new InvensisContext();

                var idSolicitud = context.SolicitudesCompras.Find(newItem.IdSolicitud);
                var idproducto = context.Productos.Find(newItem.IdProducto);
                var usuarioDestino = context.Usuarios.Find(newItem.IdUsuarioDestino);


                if (idSolicitud == null || idproducto == null || usuarioDestino == null )
                {
                    throw new Exception("Esa categoria no existe en la base de datos.");
                }

                

                var nueva = new DetalleSolicitud
                {

                    IdDetalle = newItem.IdDetalle,
                    IdSolicitud = newItem.IdSolicitud,
                    IdProducto = newItem.IdProducto,
                    Cantidad = newItem.Cantidad,
                    PrecioUnitario = newItem.PrecioUnitario,
                    Descuento = newItem.Descuento,
                    Subtotal = newItem.Subtotal,
                    IdUsuarioDestino = newItem.IdUsuarioDestino,
                    Observaciones = newItem.Observaciones,
                };

                context.DetalleSolicituds.Add(nueva);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                var mensajeError = ex.InnerException?.Message ?? ex.Message;
                throw new Exception("Error al insertar el detalle de solicitud: " + mensajeError, ex);
            }
        }

        public void UpdateDetalleSolicitud(DetalleSolicitudDTO updItem)
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

        //public void DeleteDetalleSolicitud(DetalleSolicitud delItem)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.DetalleSolicituds.Remove(delItem);
        //        context.SaveChanges();
        //    }
        //}

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
