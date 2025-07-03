using Identity.Api.DTO;
using Identity.Api.Paginado;
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


        //PAGINADA 
        public PagedResult<Proveedore> GetProveedorePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Proveedores
                
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NombreComercial.ToLower().Contains(filtro) ||
                    u.RazonSocial.ToLower().Contains(filtro) ||
                    u.Ruc.ToLower().Contains(filtro));
            }

            // Aplicar filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            // Total de registros filtrados
            var totalItems = query.Count();

            // Obtener página solicitada con paginado
            var usuarios = query
                .OrderBy(u => u.IdProveedor) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new Proveedore
                {
                    IdProveedor = s.IdProveedor,
                    Ruc = s.Ruc,
                    RazonSocial = s.RazonSocial,
                    NombreComercial = s.NombreComercial,
                    DireccionMatriz = s.DireccionMatriz,
                    Telefono = s.Telefono,
                    CorreoElectronico = s.CorreoElectronico,
                    ObligadoContabilidad = s.ObligadoContabilidad,
                    ContribuyenteEspecial = s.ContribuyenteEspecial,
                    AgenteRetencion = s.AgenteRetencion,
                    Estado = s.Estado,

                })
                .ToList();

            return new PagedResult<Proveedore>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
