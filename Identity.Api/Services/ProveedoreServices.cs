using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ProveedorService : IProveedor
    {
        private readonly ProveedorDataRepository _dataRepository;

        public ProveedorService()
        {
            _dataRepository = new ProveedorDataRepository();
        }

        public IEnumerable<Proveedore> GetAllProveedores()
        {
            return _dataRepository.GetAllProveedores();
        }

        public Proveedore GetProveedorById(int idProveedor)
        {
            return _dataRepository.GetProveedorById(idProveedor);
        }

        public void InsertProveedor(Proveedore newProveedor)
        {
            _dataRepository.InsertProveedor(newProveedor);
        }

        public void UpdateProveedor(Proveedore updatedProveedor)
        {
            _dataRepository.UpdateProveedor(updatedProveedor);
        }

        public void DeleteProveedor(Proveedore proveedorToDelete)
        {
            _dataRepository.DeleteProveedor(proveedorToDelete);
        }

        public void DeleteProveedorById(int idProveedor)
        {
            _dataRepository.DeleteProveedorById(idProveedor);
        }
    }
}
