using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ServiciosServidorServices : IServiciosServidor
    {
        private ServiciosServidorRepository _dataRepository = new ServiciosServidorRepository();

        public IEnumerable<ServiciosServidor> ServiciosServidorInfoAll
        {
            get { return _dataRepository.ServiciosServidorInfoAll(); }
        }

        public ServiciosServidor GetServiciosServidorById(int IdServiciosServidor)
        {
            return _dataRepository.GetServiciosServidorById(IdServiciosServidor);
        }

        public void InsertServiciosServidor(ServiciosServidor New)
        {
            _dataRepository.InsertServiciosServidor(New);
        }

        public void UpdateServiciosServidor(ServiciosServidor UpdItem)
        {
            _dataRepository.UpdateServiciosServidor(UpdItem);
        }

        public void DeleteServiciosServidor(ServiciosServidor DelItem)
        {
            _dataRepository.DeleteServiciosServidor(DelItem);
        }

        public void DeleteServiciosServidorById(int IdServiciosServidor)
        {
            _dataRepository.DeleteServiciosServidorById(IdServiciosServidor);
        }
    }
}
