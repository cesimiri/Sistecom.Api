using Identity.Api.DTO;

using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface ISolicitudesCompra
    {
        IEnumerable<SolicitudesCompraDTO> GetAllSolicitudesCompra { get; }
        SolicitudesCompraDTO GetSolicitudById(int idSolicitud);
        void InsertSolicitud(SolicitudesCompraDTO newSolicitud);
        void UpdateSolicitud(SolicitudesCompraDTO updatedSolicitud);
        //void DeleteSolicitud(SoliciudesCompraDTO solicitudToDelete);
        void DeleteSolicitudById(int idSolicitud);
    }
}
