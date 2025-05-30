using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class TiposLicenciumServices : ITiposLicencium
    {
        private TiposLicenciumRepository _dataRepository = new TiposLicenciumRepository();

        public IEnumerable<TiposLicencium> TiposLicenciumInfoAll
        {
            get { return _dataRepository.TiposLicenciumInfoAll(); }
        }

        public TiposLicencium GetTiposLicenciumById(int idTiposLicencium)
        {
            return _dataRepository.GetTiposLicenciumById(idTiposLicencium);
        }

        public void InsertTiposLicencium(TiposLicencium New)
        {
            _dataRepository.InsertTiposLicencium(New);
        }

        public void UpdateTiposLicencium(TiposLicencium UpdItem)
        {
            _dataRepository.UpdateTiposLicencium(UpdItem);
        }

        public void DeleteTiposLicencium(TiposLicencium DelItem)
        {
            _dataRepository.DeleteTiposLicencium(DelItem);
        }

        public void DeleteTiposLicenciumById(int idTiposLicencium)
        {
            _dataRepository.DeleteTiposLicenciumById(idTiposLicencium);
        }
    }
}
