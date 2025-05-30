using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IMovimientosInventario
    {
        IEnumerable<MovimientosInventario> MovimientosInventarioInfoAll { get; }
        MovimientosInventario GetMovimientosInventarioById(int IdMovimientosInventario);
        void InsertMovimientosInventario(MovimientosInventario New);
        void UpdateMovimientosInventario(MovimientosInventario UpdItem);
        void DeleteMovimientosInventario(MovimientosInventario DelItem);
        void DeleteMovimientosInventarioById(int IdMovimientosInventario);
    }
}
