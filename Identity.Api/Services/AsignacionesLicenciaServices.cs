using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class AsignacionesLicenciaServices : IAsignacionesLicencia
    {
        private AsignacionesLicenciaRepository _dataRepository = new AsignacionesLicenciaRepository();

        public IEnumerable<AsignacionesLicencia> AsignacionesLicenciaInfoAll
        {
            get { return _dataRepository.AsignacionesLicenciaInfoAll(); }
        }

        public AsignacionesLicencia GetAsignacionesLicenciaById(int idAsignacionesLicencia)
        {
            return _dataRepository.GetAsignacionesLicenciaById(idAsignacionesLicencia);
        }

        public void InsertAsignacionesLicencia(AsignacionesLicencia New)
        {
            _dataRepository.InsertAsignacionesLicencia(New);
        }

        public void UpdateAsignacionesLicencia(AsignacionesLicencia UpdItem)
        {
            _dataRepository.UpdateAsignacionesLicencia(UpdItem);
        }

        public void DeleteAsignacionesLicencia(AsignacionesLicencia DelItem)
        {
            _dataRepository.DeleteAsignacionesLicencia(DelItem);
        }

        public void DeleteAsignacionesLicenciaById(int idAsignacionesLicencia)
        {
            _dataRepository.DeleteAsignacionesLicenciaById(idAsignacionesLicencia);
        }
    }
}
