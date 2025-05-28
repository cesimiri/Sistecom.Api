using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Interfaces
{
    public interface IEmpresaCliente
    {
        IEnumerable<EmpresasCliente> EmpresasClientesInfoAll { get; }
        EmpresasCliente GetEmpresaClienteById(int idEmpresaCliente);
        void InsertEmpresaCliente(EmpresasCliente New);
        void UpdateEmpresaCliente(EmpresasCliente UpdItem);
        void DeleteEmpresaCliente(EmpresasCliente DelItem);
        void DeleteEmpresaClienteById(int idregistrado);
    }
}

