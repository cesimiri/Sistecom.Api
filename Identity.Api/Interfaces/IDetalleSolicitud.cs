using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface IDetalleSolicitud
    {
        IEnumerable<DetalleSolicitud> DetalleSolicitudesInfoAll { get; }
        DetalleSolicitud GetDetalleSolicitudById(int idDetalle);
        void InsertDetalleSolicitud(DetalleSolicitud newItem);
        void UpdateDetalleSolicitud(DetalleSolicitud updItem);
        void DeleteDetalleSolicitud(DetalleSolicitud delItem);
        void DeleteDetalleSolicitudById(int idDetalle);
    }
}
