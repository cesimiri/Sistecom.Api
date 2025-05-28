using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
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

        public EmpresasCliente GetEmpresaClienteById(int idEmpresaCliente)
        {
            return _dataRepository.GetEmpresaClienteById(idEmpresaCliente);
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

        public void DeleteEmpresaClienteById(int idregistrado)
        {
            _dataRepository.DeleteEmpresaClienteById(idregistrado);
        }
    }
}
