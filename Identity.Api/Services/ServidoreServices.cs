using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ServidoreServices : IServidore
    {
        private ServidoreRepository _dataRepository = new ServidoreRepository();

        public IEnumerable<Servidore> ServidoreInfoAll
        {
            get { return _dataRepository.ServidoreInfoAll(); }
        }

        //Para trawer todos los activos donde sea el nombre del producto servidores o servidor en mayuscula 
        public IEnumerable<ActivoDTO> GetActivosServidores
        {
            get { return _dataRepository.GetActivosServidores(); }
        }

        public Servidore GetServidoreById(int IdServidore)
        {
            return _dataRepository.GetServidoreById(IdServidore);
        }

        public void InsertServidore(ServidoreDTO New)
        {
            _dataRepository.InsertServidore(New);
        }

        public void UpdateServidore(Servidore UpdItem)
        {
            _dataRepository.UpdateServidore(UpdItem);
        }

        public void DeleteServidoreById(int IdServidore)
        {
            _dataRepository.DeleteServidoreById(IdServidore);
        }

        //paginado
        public PagedResult<ServidoreDTO> GetServidorePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetServidorePaginados(pagina, pageSize, filtro, estado);
        }
    }
}
