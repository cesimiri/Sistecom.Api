using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class EmpresaClienteService : IEmpresaCliente
    {
        private EmpresaClienteDataRepository _dataRepository = new EmpresaClienteDataRepository();

        public IEnumerable<EmpresasCliente> EmpresasClientesInfoAll
        {
            get { return _dataRepository.GetAllEmpresasClientes(); }
        }

        public EmpresasCliente GetEmpresaClienteById(string ruc)
        {
            return _dataRepository.GetEmpresaClienteById(ruc);
        }

        public void InsertEmpresaCliente(EmpresasCliente New)
        {
            _dataRepository.InsertEmpresaCliente(New);
        }

        public void UpdateEmpresaCliente(EmpresasCliente UpdItem)
        {
            _dataRepository.UpdateEmpresaCliente(UpdItem);
        }

        public void DeleteEmpresaCliente(EmpresasCliente DelItem)
        {
            _dataRepository.DeleteEmpresaCliente(DelItem);
        }

        public void DeleteEmpresaClienteById(string ruc)
        {
            _dataRepository.DeleteEmpresaClienteById(ruc);
        }
        //paginado
        public PagedResult<EmpresasCliente> GetEmpresasPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetEmpresasPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
