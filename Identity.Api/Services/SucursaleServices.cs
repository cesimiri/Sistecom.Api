using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;

namespace Identity.Api.Services
{
    public class SucursaleServices : ISucursale
    {
        private readonly SucursaleRepository _dataRepository = new SucursaleRepository();

        public IEnumerable<SucursaleDTO> GetAllSucursale()
        {
            return _dataRepository.GetAllSucursale();
        }

        public SucursaleDTO? GetSucursaleById(int idSucursal)
        {
            return _dataRepository.GetSucursaleById(idSucursal);
        }

        public void InsertSucursale(SucursaleDTO dto)
        {
            _dataRepository.InsertSucursale(dto);
        }

        public void UpdateSucursale(SucursaleDTO dto)
        {
            _dataRepository.UpdateSucursale(dto);
        }

        //public void DeleteSuscripcion(SuscripcionDto dto)
        //{
        //    _dataRepository.DeleteSuscripcion(dto);
        //}

        public void DeleteSucursaleById(int idSucursal)
        {
            _dataRepository.DeleteSucursaleById(idSucursal);
        }


        //paginado
        public PagedResult<SucursaleDTO> GetSucursalePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetSucursalePaginados(pagina, pageSize, filtro, estado);
        }
    }
}
