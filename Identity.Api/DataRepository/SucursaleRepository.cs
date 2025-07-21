using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class SucursaleRepository
    {
        public List<SucursaleDTO> GetAllSucursale()
        {
            using var context = new InvensisContext();

            return context.Sucursales
                .Where(s => s.Estado == "ACTIVO") // ✅ Filtrar solo las sucursales activas
                .Include(s => s.RucEmpresaNavigation)
                .OrderBy(s => s.NombreSucursal) // ✅ Ordenar por NombreSucursal
                .Select(s => new SucursaleDTO
                {
                    IdSucursal = s.IdSucursal,
                    RucEmpresa = s.RucEmpresa,
                    CodigoSucursal = s.CodigoSucursal,
                    NombreSucursal = s.NombreSucursal,
                    Direccion = s.Direccion,
                    Ciudad = s.Ciudad,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Responsable = s.Responsable,
                    TelefonoResponsable = s.TelefonoResponsable,
                    EsMatriz = s.EsMatriz,
                    Estado = s.Estado,

                    // Campos relacionados:
                    RazonSocialEmpresa = s.RucEmpresaNavigation.RazonSocial,

                })
                .ToList();
        }

        public SucursaleDTO? GetSucursaleById(int idSucursal)
        {
            using var context = new InvensisContext();

            return context.Sucursales
                .Include(s => s.RucEmpresaNavigation)
                .Where(s => s.IdSucursal == idSucursal)
                .Select(s => new SucursaleDTO
                {
                    IdSucursal = s.IdSucursal,
                    RucEmpresa = s.RucEmpresa,
                    CodigoSucursal = s.CodigoSucursal,
                    NombreSucursal = s.NombreSucursal,
                    Direccion = s.Direccion,
                    Ciudad = s.Ciudad,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Responsable = s.Responsable,
                    TelefonoResponsable = s.TelefonoResponsable,
                    EsMatriz = s.EsMatriz,
                    Estado = s.Estado,
                    // Campos relacionados:
                    RazonSocialEmpresa = s.RucEmpresaNavigation.RazonSocial,
                })
                .FirstOrDefault();
        }

        public void InsertSucursale(SucursaleDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                var empresa = context.EmpresasClientes.Find(dto.RucEmpresa);


                if (empresa == null)
                {
                    throw new Exception("La empresa no existen en la base de datos.");
                }

                var nueva = new Sucursale
                {

                    RucEmpresa = dto.RucEmpresa,
                    CodigoSucursal = dto.CodigoSucursal?.ToUpper(),
                    NombreSucursal = dto.NombreSucursal?.ToUpper(),
                    Direccion = dto.Direccion?.ToUpper(),
                    Ciudad = dto.Ciudad?.ToUpper(),
                    Telefono = dto.Telefono,
                    Email = dto.Email?.Trim().ToLower(),
                    Responsable = dto.Responsable?.ToUpper(),
                    TelefonoResponsable = dto.TelefonoResponsable,
                    EsMatriz = dto.EsMatriz,
                    Estado = dto.Estado,
                };

                context.Sucursales.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la suscripción: " + ex.InnerException?.Message ?? ex.Message);
            }
        }



        public void UpdateSucursale(SucursaleDTO dto)
        {
            using var context = new InvensisContext();

            var suscripcion = context.Sucursales
                .FirstOrDefault(s => s.IdSucursal == dto.IdSucursal);

            if (suscripcion != null)
            {

                suscripcion.RucEmpresa = dto.RucEmpresa;
                suscripcion.CodigoSucursal = dto.CodigoSucursal?.ToUpper();
                suscripcion.NombreSucursal = dto.NombreSucursal?.ToUpper();
                //conversion de DATETIME a DATEONLY
                suscripcion.Direccion = dto.Direccion?.ToUpper();
                suscripcion.Ciudad = dto.Ciudad?.ToUpper();
                suscripcion.Telefono = dto.Telefono;
                suscripcion.Email = dto.Email?.Trim().ToLower();
                suscripcion.Responsable = dto.Responsable;
                suscripcion.TelefonoResponsable = dto.TelefonoResponsable;
                suscripcion.EsMatriz = dto.EsMatriz;
                suscripcion.Estado = dto.Estado;


                // Actualizar navegación (opcional)
                suscripcion.RucEmpresaNavigation = context.EmpresasClientes
                    .FirstOrDefault(e => e.Ruc == dto.RucEmpresa);


                context.SaveChanges();
            }
        }

        //public void DeleteSuscripcion(SuscripcionDto dto)
        //{
        //    using var context = new InvensisContext();

        //    var suscripcion = context.Suscripciones
        //        .FirstOrDefault(s => s.IdSuscripcion == dto.IdSuscripcion);

        //    if (suscripcion != null)
        //    {
        //        context.Suscripciones.Remove(suscripcion);
        //        context.SaveChanges();
        //    }
        //}
        public void DeleteSucursaleById(int idSucursal)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Sucursales
                    .FirstOrDefault(s => s.IdSucursal == idSucursal);

                if (suscripcion != null)
                {
                    context.Sucursales.Remove(suscripcion);
                    context.SaveChanges();
                }
            }
        }



        //PAGINADA 
        public PagedResult<SucursaleDTO> GetSucursalePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Sucursales
                .Include(s => s.RucEmpresaNavigation)

                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NombreSucursal.ToLower().Contains(filtro));
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
                .OrderBy(u => u.NombreSucursal) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SucursaleDTO
                {
                    IdSucursal = s.IdSucursal,
                    RucEmpresa = s.RucEmpresa,
                    CodigoSucursal = s.CodigoSucursal,
                    NombreSucursal = s.NombreSucursal,
                    Direccion = s.Direccion,
                    Ciudad = s.Ciudad,
                    Email = s.Email,
                    Telefono = s.Telefono,
                    Responsable = s.Responsable,
                    TelefonoResponsable = s.TelefonoResponsable,
                    EsMatriz = s.EsMatriz,
                    Estado = s.Estado,
                    RazonSocialEmpresa = s.RucEmpresaNavigation.RazonSocial
                })
                .ToList();

            return new PagedResult<SucursaleDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }




        //EXPORTAR
        public List<SucursaleDTO> ObtenerSucursalesFiltradas(string? filtro, string? estado)
        {
            using var context = new InvensisContext();

            var query = context.Sucursales.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var lowerFiltro = filtro.ToLower();
                query = query.Where(e =>
                    e.NombreSucursal.ToLower().Contains(lowerFiltro) ||
                    e.RucEmpresa.ToLower().Contains(lowerFiltro));
            }

            if (!string.IsNullOrWhiteSpace(estado))
            {
                query = query.Where(e => e.Estado == estado);
            }

            return query
                .Select(e => new SucursaleDTO
                {
                    RucEmpresa = e.RucEmpresa,
                    CodigoSucursal = e.CodigoSucursal,
                    NombreSucursal = e.NombreSucursal,
                    Direccion = e.Direccion,
                    Ciudad = e.Ciudad,
                    Telefono = e.Telefono,
                    Email = e.Email,
                    Responsable = e.Responsable,
                    TelefonoResponsable = e.TelefonoResponsable,
                    EsMatriz = e.EsMatriz,
                    Estado = e.Estado,
                })
                .ToList();
        }
    }
}
