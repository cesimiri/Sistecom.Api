using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class LicenciaServices : ILicencia
    {
        private LicenciaRepository _dataRepository = new LicenciaRepository();

        public IEnumerable<Licencia> LicenciaInfoAll
        {
            get { return _dataRepository.LicenciaInfoAll(); }
        }

        public Licencia GetLicenciaById(int idLicencia)
        {
            return _dataRepository.GetLicenciaById(idLicencia);
        }

        public void InsertLicencia(Licencia New)
        {
            _dataRepository.InsertLicencia(New);
        }

        public void UpdateLicencia(Licencia UpdItem)
        {
            _dataRepository.UpdateLicencia(UpdItem);
        }

        public void DeleteLicencia(Licencia DelItem)
        {
            _dataRepository.DeleteLicencia(DelItem);
        }

        public void DeleteLicenciaById(int idLicencia)
        {
            _dataRepository.DeleteLicenciaById(idLicencia);
        }
    }
}
