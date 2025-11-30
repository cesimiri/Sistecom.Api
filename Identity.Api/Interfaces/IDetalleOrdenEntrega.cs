using Identity.Api.DTO;

namespace Identity.Api.Interfaces
{
    public interface IDetalleOrdenEntrega
    {

        //ingreso detalle masivo
        void InsertDetalleOrdenEntregaMasivo(List<DetalleOrdenEntregaDTO> lista);


        void DeleteDetalleOrdenEntregaByIdOrden(int IdDetalleOrdenEntrega);

        //nos trae toda las lineas por el número de solicitud para poder editarla 
        IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud);

        IEnumerable<DetalleOrdenEntregaDTO> GetDetallesOrdenEntregaById(int idOrden);

    }
}
