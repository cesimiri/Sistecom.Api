using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
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

        //trae todos los serviodores    
        public IEnumerable<ServidoreDTO> GetSetvidores
        {
            get { return _dataRepository.GetSetvidores(); }
        }

        public ServiciosServidor GetServiciosServidorById(int IdServiciosServidor)
        {
            return _dataRepository.GetServiciosServidorById(IdServiciosServidor);
        }

        public void InsertServiciosServidor(ServiciosServidorDTO New)
        {
            _dataRepository.InsertServiciosServidor(New);
        }

        public void UpdateServiciosServidor(ServiciosServidor UpdItem)
        {
            _dataRepository.UpdateServiciosServidor(UpdItem);
        }

        public void DeleteServiciosServidorById(int IdServiciosServidor)
        {
            _dataRepository.DeleteServiciosServidorById(IdServiciosServidor);
        }

        //paginado
        public PagedResult<ServiciosServidorDTO> GetServiciosServidorPaginados(
        int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null)
        {
            return _dataRepository.GetServiciosServidorPaginados(
                pagina, pageSize,
                filtro, estadoActivo
            );
        }
    }
}
