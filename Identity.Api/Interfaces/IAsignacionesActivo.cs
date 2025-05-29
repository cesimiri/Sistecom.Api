using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IAsignacionesActivo
    {
        IEnumerable<AsignacionesActivo> AsignacionesActivoInfoAll { get; }
        AsignacionesActivo GetAsignacionesActivoById(int IdAsignacionesActivo);
        void InsertAsignacionesActivo(AsignacionesActivo New);
        void UpdateAsignacionesActivo(AsignacionesActivo UpdItem);
        void DeleteAsignacionesActivo(AsignacionesActivo DelItem);
        void DeleteAsignacionesActivoById(int IdAsignacionesActivo);
    }
}
