using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IHistorialActivo
    {
        IEnumerable<HistorialActivo> HistorialActivoInfoAll { get; }
        HistorialActivo GetHistorialActivoById(int IdHistorialActivo);
        void InsertHistorialActivo(HistorialActivo New);
        void UpdateHistorialActivo(HistorialActivo UpdItem);
        void DeleteHistorialActivo(HistorialActivo DelItem);
        void DeleteHistorialActivoById(int IdHistorialActivo);
    }
}
