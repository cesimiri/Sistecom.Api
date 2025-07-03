using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Interfaces
{
    public interface IProveedor
    {
        IEnumerable<Proveedore> GetAllProveedores();
        Proveedore GetProveedorById(int idProveedor);
        void InsertProveedor(Proveedore newProveedor);
        void UpdateProveedor(Proveedore updatedProveedor);
        void DeleteProveedor(Proveedore proveedorToDelete);
        void DeleteProveedorById(int idProveedor);

        // Nuevo método para paginado:
        PagedResult<Proveedore> GetProveedorePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
