using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface ISuscripcione
    {
        //Task<IEnumerable<Suscripcione>> SuscripcionesAll { get; }
        Task<IEnumerable<Suscripcione>> SuscripcionesAll();
        Task<Suscripcione?> GetSuscripcionById(int idSuscripcion);
        Task InsertSuscripcion(Suscripcione newSuscripcion);
        Task UpdateSuscripcion(Suscripcione updatedSuscripcion);
        Task DeleteSuscripcion(Suscripcione suscripcionToDelete);
        Task DeleteSuscripcionById(int idSuscripcion);
        //Agregado
        Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync();
        Task<IEnumerable<Proveedore>> GetProveedoreAsync();

    }
}
