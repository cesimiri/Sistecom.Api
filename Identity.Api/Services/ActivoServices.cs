using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{
    public class ActivoServices : IActivo
    {
        private ActivoRepository _dataRepository = new ActivoRepository();

        public IEnumerable<Activo> ActivoInfoAll
        {
            get { return _dataRepository.ActivoInfoAll(); }
        }

        public Activo GetActivoById(int IdActivo)
        {
            return _dataRepository.GetActivoById(IdActivo);
        }

        public void InsertActivo(Activo New)
        {
            _dataRepository.InsertActivo(New);
        }

        public void UpdateActivo(Activo UpdItem)
        {
            _dataRepository.UpdateActivo(UpdItem);
        }

        public void DeleteActivo(Activo DelItem)
        {
            _dataRepository.DeleteActivo(DelItem);
        }

        public void DeleteActivoById(int IdActivo)
        {
            _dataRepository.DeleteActivoById(IdActivo);
        }
    }
}
