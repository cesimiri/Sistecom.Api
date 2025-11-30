using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class DetalleOrdenEntregaRepository
    {


        // ingreso masivo 
        public void InsertDetalleOrdenEntregaMasivo(List<DetalleOrdenEntregaDTO> lista)
        {
            using var context = new InvensisContext();

            if (lista == null || lista.Count == 0)
                throw new Exception("La lista de detalles está vacía.");

            // Todos los detalles deben pertenecer al mismo IdOrden
            int idOrden = lista[0].IdOrden;

            if (idOrden <= 0)
                throw new Exception("El IdOrden es obligatorio y debe ser mayor que cero.");

            // Validar que la orden exista
            var orden = context.OrdenesEntregas.Find(idOrden);
            if (orden == null)
                throw new Exception("La orden de entrega indicada no existe.");

            foreach (var item in lista)
            {
                // Validar consistencia
                if (item.IdOrden != idOrden)
                    throw new Exception("Todos los detalles deben pertenecer al mismo IdOrden.");

                // Validar producto
                var producto = context.Productos.Find(item.IdProducto);
                if (producto == null)
                    throw new Exception($"El producto con Id {item.IdProducto} no existe.");

                // Mapear DTO → Entidad
                var detalleEntity = new DetalleOrdenEntrega
                {
                    IdOrden = item.IdOrden,
                    IdProducto = item.IdProducto,
                    CantidadProgramada = item.CantidadProgramada,
                    CantidadEntregada = item.CantidadEntregada,
                    IdActivo = item.IdActivo,
                    IdLicencia = item.IdLicencia,
                    Observaciones = item.Observaciones
                };

                context.DetalleOrdenEntregas.Add(detalleEntity);
            }

            // Guardar todos en un solo paso
            context.SaveChanges();
        }


        //borrar todos los quie tenga el idOrden
        public void DeleteDetalleOrdenEntregaByIdOrden(int idOrden)
        {
            using var context = new InvensisContext();

            // Buscar todos los detalles asociados al IdOrden
            var detalles = context.DetalleOrdenEntregas
                .Where(d => d.IdOrden == idOrden)
                .ToList();

            if (detalles.Count == 0)
                return; // No hay nada que borrar

            context.DetalleOrdenEntregas.RemoveRange(detalles);
            context.SaveChanges();
        }

        //nos trae toda las lineas por el número de solicitud  GetDetallesBySolicitudId
        public IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud)
        {
            using var context = new InvensisContext();

            return context.DetalleSolicituds
                .Where(s => s.IdSolicitud == idSolicitud)
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
                    // relación
                    NombreProducto = s.IdProductoNavigation.Nombre
                })
                .ToList();
        }

        public IEnumerable<DetalleOrdenEntregaDTO> GetDetallesOrdenEntregaById(int idOrden)
        {
            using var context = new InvensisContext();

            return context.DetalleOrdenEntregas
                .Where(s => s.IdOrden == idOrden)
                .Include(s => s.IdProductoNavigation)
                .Select(s => new DetalleOrdenEntregaDTO
                {
                    IdDetalle = s.IdDetalle,
                    IdOrden = s.IdOrden,
                    IdProducto = s.IdProducto,
                    CantidadProgramada = s.CantidadProgramada,
                    CantidadEntregada = s.CantidadEntregada,
                    IdActivo = s.IdActivo,
                    IdLicencia = s.IdLicencia,
                    Observaciones = s.Observaciones,
                    // relación
                    NombreProducto = s.IdProductoNavigation.Nombre
                })
                .ToList();
        }
    }
}
