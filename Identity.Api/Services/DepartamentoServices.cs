using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;

namespace Identity.Api.Services
{
    public class DepartamentoServices : IDepartamento
    {
        private readonly DepartamentoRepository _dataRepository = new DepartamentoRepository();

        public IEnumerable<DepartamentoDTO> GetAllDepartamento()
        {
            return _dataRepository.GetAllDepartamento();
        }

        public DepartamentoDTO? GetDepartamentoById(int idDepartamento)
        {
            return _dataRepository.GetDepartamentoById(idDepartamento);
        }

        public void InsertDepartamento(DepartamentoDTO dto)
        {
            _dataRepository.InsertDepartamento(dto);
        }

        public void UpdateDepartamento(DepartamentoDTO dto)
        {
            _dataRepository.UpdateDepartamento(dto);
        }

        //public void DeleteSuscripcion(SuscripcionDto dto)
        //{
        //    _dataRepository.DeleteSuscripcion(dto);
        //}

        public void DeleteDepartamentoById(int idDepartamento)
        {
            _dataRepository.DeleteDepartamentoById(idDepartamento);
        }

        //paginado
        public PagedResult<DepartamentoDTO> GetDepartamentosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetDepartamentosPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
