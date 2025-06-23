using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface IDetalleSolicitud
    {
        IEnumerable<DetalleSolicitudDTO> DetalleSolicitudesAll { get; }
        DetalleSolicitudDTO GetDetalleSolicitudById(int idDetalle);
        void InsertDetalleSolicitud(DetalleSolicitudDTO newItem);
        void UpdateDetalleSolicitud(DetalleSolicitudDTO updItem);
        //void DeleteDetalleSolicitud(DetalleSolicitudDTO delItem);
        void DeleteDetalleSolicitudById(int idDetalle);

        void InsertarDetallesMasivos(List<DetalleSolicitudDTO> lista);

        IEnumerable<SolicitudesCompraDTO> SolicitudesDeCompraPorEstadoAsync();

        //nos trae toda las lineas por el número de solicitud para poder editarla 
        IEnumerable<DetalleSolicitudDTO> GetDetallesBySolicitudId(int idSolicitud);
    }
}
