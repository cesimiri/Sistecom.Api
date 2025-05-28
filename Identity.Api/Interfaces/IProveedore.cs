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
    }
}
