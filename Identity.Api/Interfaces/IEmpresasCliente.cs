using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Interfaces
{
    public interface IEmpresaCliente
    {
        IEnumerable<EmpresasCliente> EmpresasClientesInfoAll { get; }
        EmpresasCliente GetEmpresaClienteById(string ruc);
        void InsertEmpresaCliente(EmpresasCliente New);
        void UpdateEmpresaCliente(EmpresasCliente UpdItem);
        void DeleteEmpresaCliente(EmpresasCliente DelItem);
        void DeleteEmpresaClienteById(string ruc);

        PagedResult<EmpresasCliente> GetEmpresasPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}

