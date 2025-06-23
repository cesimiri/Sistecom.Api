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
              
                    Observaciones = s.Observaciones,

                    // campos relacionados:
                    
                    NumeroSolicitud = s.IdSolicitudNavigation.NumeroSolicitud,
                    
                })
                .ToList();
        }

        public DetalleSolicitudDTO GetDetalleSolicitudById(int idDetalle)
        {
            using var context = new InvensisContext();

            return context.DetalleSolicituds
                //.Include(s => s.IdUsuarioDestinoNavigation)
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
               
                    Observaciones = s.Observaciones,
                    // campos relacionados:
                    
                    NumeroSolicitud = s.IdSolicitudNavigation.NumeroSolicitud,
                    

                })
                .FirstOrDefault();
        }

        //Busca las Solicitudes Compra por estado diferente al RECHAZADA , COMPLETADA, CANCELADA
        public List<SolicitudesCompraDTO> SolicitudesDeCompraPorEstadoAsync()
        {
            using var context = new InvensisContext();
            return context.SolicitudesCompras

  
                .Where(s => s.Estado != "RECHAZADA"
                && s.Estado != "COMPLETADA"
                && s.Estado != "CANCELADA")

                .Select(s => new SolicitudesCompraDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    NumeroSolicitud = s.NumeroSolicitud,
                    RucEmpresa = s.RucEmpresa,
                    IdDepartamento = s.IdDepartamento,
                    IdUsuarioSolicita = s.IdUsuarioSolicita,
                    IdUsuarioAutoriza = s.IdUsuarioAutoriza,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaAprobacion = s.FechaAprobacion,
                    //FechaRequerida = s.FechaRequerida,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    ValorTotal = s.ValorTotal,
                    Estado = s.Estado,
                    Observaciones = s.Observaciones,

                    // campos relacionados:

                    

                })
                .ToList();
        }



        public void InsertDetalleSolicitud(DetalleSolicitudDTO newItem)
        {
            try
            {
                using var context = new InvensisContext();

                var idSolicitud = context.SolicitudesCompras.Find(newItem.IdSolicitud);
                var idproducto = context.Productos.Find(newItem.IdProducto);


                if (idSolicitud == null || idproducto == null )
                {
                    throw new Exception("Esa categoria no existe en la base de datos.");
                }

                

                var nueva = new DetalleSolicitud
                {

                    IdSolicitud = newItem.IdSolicitud,
                    IdProducto = newItem.IdProducto,
                    Cantidad = newItem.Cantidad,
                    PrecioUnitario = newItem.PrecioUnitario,
                    Descuento = 0,
                    Subtotal = newItem.Subtotal,

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

        //INSERCION MASIVA
        //public void InsertarDetallesMasivos(List<DetalleSolicitudDTO> lista)
        //{
        //    using var context = new InvensisContext();

        //    foreach (var item in lista)
        //    {
        //        var idSolicitud = context.SolicitudesCompras.Find(item.IdSolicitud);
        //        var idproducto = context.Productos.Find(item.IdProducto);

        //        if (idSolicitud == null || idproducto == null)
        //        {
        //            throw new Exception("Error: Solicitud o Producto no válido.");
        //        }

        //        var nuevo = new DetalleSolicitud
        //        {
        //            IdSolicitud = item.IdSolicitud,
        //            IdProducto = item.IdProducto,
        //            Cantidad = item.Cantidad,
        //            PrecioUnitario = item.PrecioUnitario,
        //            Descuento = item.Descuento,
        //            Subtotal = item.Subtotal,
        //            Observaciones = item.Observaciones
        //        };

        //        context.DetalleSolicituds.Add(nuevo);
        //    }

        //    context.SaveChanges();
        //}

        public void InsertarDetallesMasivos(List<DetalleSolicitudDTO> lista)
        {
            using var context = new InvensisContext();

            if (lista == null || lista.Count == 0)
                throw new Exception("La lista de detalles está vacía.");

            int idSolicitud = lista[0].IdSolicitud;

            foreach (var item in lista)
            {
                var idSolicitudEntity = context.SolicitudesCompras.Find(item.IdSolicitud);
                var idproducto = context.Productos.Find(item.IdProducto);

                if (idSolicitudEntity == null || idproducto == null)
                {
                    throw new Exception("Error: Solicitud o Producto no válido.");
                }

                var nuevo = new DetalleSolicitud
                {
                    IdSolicitud = item.IdSolicitud,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    Descuento = item.Descuento,
                    Subtotal = item.Subtotal,
                    Observaciones = item.Observaciones
                };

                context.DetalleSolicituds.Add(nuevo);
            }

            // Guardar los detalles nuevos primero
            context.SaveChanges();

            // Calcular el subtotal total para la solicitud
            var subtotalTotal = context.DetalleSolicituds
                .Where(d => d.IdSolicitud == idSolicitud)
                .Sum(d => d.Subtotal);

            // Calcular IVA (15%)
            decimal iva = subtotalTotal * 0.15m;

            // Total
            decimal total = subtotalTotal + iva;

            // Actualizar SolicitudCompra con los totales calculados
            var solicitudParaActualizar = context.SolicitudesCompras.Find(idSolicitud);
            if (solicitudParaActualizar != null)
            {
                solicitudParaActualizar.SubtotalSinImpuestos = subtotalTotal;
                solicitudParaActualizar.Iva = iva;
                solicitudParaActualizar.ValorTotal = total;

                context.SaveChanges();
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
    
                    registrado.Observaciones = updItem.Observaciones;

                    context.SaveChanges();
                }
            }
        }
        

        // borra por todas las solicitudes 
        public void DeleteDetalleSolicitudById(int idDetalle)
        {
            using var context = new InvensisContext();

            // 1. Buscar el detalle por idDetalle
            var detalle = context.DetalleSolicituds.FirstOrDefault(d => d.IdDetalle == idDetalle);

            if (detalle != null)
            {
                int idSolicitud = detalle.IdSolicitud;

                // 2. Buscar todos los detalles con ese IdSolicitud
                var detallesRelacionados = context.DetalleSolicituds
                    .Where(d => d.IdSolicitud == idSolicitud)
                    .ToList();

                // 3. Eliminar en conjunto
                context.DetalleSolicituds.RemoveRange(detallesRelacionados);
                context.SaveChanges();
            }
        }

        //nos trae toda las lineas por el número de solicitud para poder editarla 

        public IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud)
        {
            using var context = new InvensisContext();

            return context.DetalleSolicituds
                .Where(s => s.IdSolicitud == idSolicitud)
                .Select(s => new DetalleSolicitudDTO
                {
                    IdDetalle = s.IdDetalle,
                    IdSolicitud = s.IdSolicitud,
                    IdProducto = s.IdProducto,
                    Cantidad = s.Cantidad,
                    PrecioUnitario = s.PrecioUnitario,
                    Descuento = s.Descuento,
                    Subtotal = s.Subtotal,
                    Observaciones = s.Observaciones
                })
                .ToList();
        }
    }
}
