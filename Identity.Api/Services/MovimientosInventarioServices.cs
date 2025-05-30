using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class MovimientosInventarioServices : IMovimientosInventario
    {
        private MovimientosInventarioRepository _dataRepository = new MovimientosInventarioRepository();

        public IEnumerable<MovimientosInventario> MovimientosInventarioInfoAll
        {
            get { return _dataRepository.MovimientosInventarioInfoAll(); }
        }

        public MovimientosInventario GetMovimientosInventarioById(int idMovimientosInventario)
        {
            return _dataRepository.GetMovimientosInventarioById(idMovimientosInventario);
        }

        public void InsertMovimientosInventario(MovimientosInventario New)
        {
            _dataRepository.InsertMovimientosInventario(New);
        }

        public void UpdateMovimientosInventario(MovimientosInventario UpdItem)
        {
            _dataRepository.UpdateMovimientosInventario(UpdItem);
        }

        public void DeleteMovimientosInventario(MovimientosInventario DelItem)
        {
            _dataRepository.DeleteMovimientosInventario(DelItem);
        }

        public void DeleteMovimientosInventarioById(int idMovimientosInventario)
        {
            _dataRepository.DeleteMovimientosInventarioById(idMovimientosInventario);
        }
    }
}
