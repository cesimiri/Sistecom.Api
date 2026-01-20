using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
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

        public async Task<List<SpResponseDTO>> InsertActivos(List<ActivoDTO> activos)
        {
            return await _dataRepository.InsertarActivos(activos);
        }

        public void UpdateActivo(Activo UpdItem)
        {
            _dataRepository.UpdateActivo(UpdItem);
        }

        //update 1 a 1 
        public void Update1a1(ActivoDTO updActivo)
        {
            _dataRepository.Update1a1(updActivo);
        }

        public void DeleteActivoById(int IdActivo)
        {
            _dataRepository.DeleteActivoById(IdActivo);
        }


        //paginado
        public PagedResult<ActivoDTO> GetActivoPaginados(
        int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null)
        {
            return _dataRepository.GetActivoPaginados(
                pagina, pageSize,
                filtro, estadoActivo
            );
        }

    }
}
