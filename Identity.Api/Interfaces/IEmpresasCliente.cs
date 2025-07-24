using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

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

        //paginados
        PagedResult<EmpresasCliente> GetEmpresasPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

        //exportar excel
        List<EmpresasCliente> ObtenerEmpresasFiltradas(string? filtro, string? estado);

    }
}

