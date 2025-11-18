using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IDetalleOrdenEntrega
    {

        DetalleOrdenEntrega GetDetalleOrdenEntregaById(int IdDetalleOrdenEntrega);
        void InsertDetalleOrdenEntrega(DetalleOrdenEntrega New);
        void UpdateDetalleOrdenEntrega(DetalleOrdenEntrega UpdItem);
        void DeleteDetalleOrdenEntrega(DetalleOrdenEntrega DelItem);
        void DeleteDetalleOrdenEntregaById(int IdDetalleOrdenEntrega);
        //nos trae toda las lineas por el número de solicitud para poder editarla 
        IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud);
    }
}
