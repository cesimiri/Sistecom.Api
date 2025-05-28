using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface ISolicitudesCompra
    {
        IEnumerable<SolicitudesCompra> SolicitudesCompraAll { get; }
        SolicitudesCompra GetSolicitudById(int idSolicitud);
        void InsertSolicitud(SolicitudesCompra newSolicitud);
        void UpdateSolicitud(SolicitudesCompra updatedSolicitud);
        void DeleteSolicitud(SolicitudesCompra solicitudToDelete);
        void DeleteSolicitudById(int idSolicitud);
    }
}
