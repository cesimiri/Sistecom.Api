using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface ISuscripcione
    {
<<<<<<< HEAD
        Task<IEnumerable<Suscripcione>> SuscripcionesAll();
        Task<Suscripcione?> GetSuscripcionById(int idSuscripcion);
        Task InsertSuscripcion(Suscripcione newSuscripcion);
        Task UpdateSuscripcion(Suscripcione updatedSuscripcion);
        Task DeleteSuscripcion(Suscripcione suscripcionToDelete);
        Task DeleteSuscripcionById(int idSuscripcion);

        // Métodos agregados para obtener un proveedor y una empresa por su ID
        Task<Proveedore?> GetProveedorByIdAsync(int idProveedor);
        Task<EmpresasCliente?> GetEmpresaByIdAsync(int idEmpresa);
        Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync();
        Task<IEnumerable<Proveedore>> GetProveedoreAsync();
=======
        IEnumerable<Suscripcione> SuscripcionesAll { get; }
        Suscripcione GetSuscripcionById(int idSuscripcion);
        void InsertSuscripcion(Suscripcione newSuscripcion);
        void UpdateSuscripcion(Suscripcione updatedSuscripcion);
        void DeleteSuscripcion(Suscripcione suscripcionToDelete);
        void DeleteSuscripcionById(int idSuscripcion);
>>>>>>> parent of dfa63f3 (5-6-25 16:13)
    }
}
