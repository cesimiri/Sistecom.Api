using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class ProveedorDataRepository
    {
        private readonly InvensisContext _context;

        // Constructor que inicializa el contexto manualmente
        public ProveedorDataRepository()
        {
            _context = new InvensisContext(); // Creación manual del contexto
        }

        // Obtener todos los proveedores
        public List<Proveedore> GetAllProveedores()
        {
            return _context.Proveedores.ToList();
        }

        // Obtener un proveedor por su ID
        public Proveedore GetProveedorById(int idProveedor)
        {
            return _context.Proveedores.FirstOrDefault(p => p.IdProveedor == idProveedor);
        }

        // Insertar un nuevo proveedor
        public void InsertProveedor(Proveedore newProveedor)
        {
            _context.Proveedores.Add(newProveedor);
            _context.SaveChanges();
        }

        // Actualizar un proveedor
        public void UpdateProveedor(Proveedore updatedProveedor)
        {
            var proveedor = _context.Proveedores
                                     .FirstOrDefault(p => p.IdProveedor == updatedProveedor.IdProveedor);

            if (proveedor != null)
            {
                proveedor.Ruc = updatedProveedor.Ruc;
                proveedor.RazonSocial = updatedProveedor.RazonSocial;
                proveedor.NombreComercial = updatedProveedor.NombreComercial;
                proveedor.DireccionMatriz = updatedProveedor.DireccionMatriz;
                proveedor.Telefono = updatedProveedor.Telefono;
                proveedor.CorreoElectronico = updatedProveedor.CorreoElectronico;
                proveedor.ObligadoContabilidad = updatedProveedor.ObligadoContabilidad;
                proveedor.ContribuyenteEspecial = updatedProveedor.ContribuyenteEspecial;
                proveedor.AgenteRetencion = updatedProveedor.AgenteRetencion;
                proveedor.Estado = updatedProveedor.Estado;
                proveedor.FechaRegistro = updatedProveedor.FechaRegistro;

                _context.SaveChanges();
            }
        }

        // Eliminar un proveedor
        public void DeleteProveedor(Proveedore proveedorToDelete)
        {
            _context.Proveedores.Remove(proveedorToDelete);
            _context.SaveChanges();
        }

        // Eliminar un proveedor por su ID
        public void DeleteProveedorById(int idProveedor)
        {
            var proveedor = _context.Proveedores
                                     .FirstOrDefault(p => p.IdProveedor == idProveedor);

            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                _context.SaveChanges();
            }
        }
    }
}
