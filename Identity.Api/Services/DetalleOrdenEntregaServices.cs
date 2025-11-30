using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;

namespace Identity.Api.Services
{
    public class DetalleOrdenEntregaServices : IDetalleOrdenEntrega
    {
        private DetalleOrdenEntregaRepository _dataRepository = new DetalleOrdenEntregaRepository();



        public void InsertDetalleOrdenEntregaMasivo(List<DetalleOrdenEntregaDTO> lista)
        {
            _dataRepository.InsertDetalleOrdenEntregaMasivo(lista);
        }



        public void DeleteDetalleOrdenEntregaByIdOrden(int idDetalleOrdenEntrega)
        {
            _dataRepository.DeleteDetalleOrdenEntregaByIdOrden(idDetalleOrdenEntrega);
        }

        //nos trae toda las lineas por el número de solicitud para poder editarla 

        public IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud)
        {
            return _dataRepository.GetDetallesBySolicitudId(idSolicitud);
        }

        public IEnumerable<DetalleOrdenEntregaDTO> GetDetallesOrdenEntregaById(int idOrden)
        {
            return _dataRepository.GetDetallesOrdenEntregaById(idOrden);
        }
    }
}

