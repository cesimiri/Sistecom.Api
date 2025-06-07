using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Interfaces
{
    public interface ISuscripcione
    {
        Task<IEnumerable<Suscripcione>> SuscripcionesAll();
        Task<Suscripcione?> GetSuscripcionById(int idSuscripcion);
        Task InsertSuscripcion(Suscripcione newSuscripcion);
        Task UpdateSuscripcion(Suscripcione updatedSuscripcion);
        Task DeleteSuscripcion(Suscripcione suscripcionToDelete);
        Task DeleteSuscripcionById(int idSuscripcion);

        Task<Proveedore?> GetProveedorByIdAsync(int idProveedor);
        Task<EmpresasCliente?> GetEmpresaByIdAsync(int idEmpresa);
        Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync();
        Task<IEnumerable<Proveedore>> GetProveedoreAsync();
    }
}
