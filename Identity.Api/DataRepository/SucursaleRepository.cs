using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.DataRepository
{
    public class SucursaleRepository
    {
        public List<SucursaleDTO> GetAllSucursale()
        {
            using var context = new InvensisContext();

            return context.Sucursales
                .Include(s => s.RucEmpresaNavigation)
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
                

                if (empresa == null )
                {
                    throw new Exception("La empresa no existen en la base de datos.");
                }

                var nueva = new Sucursale
                {
                    
                    RucEmpresa = dto.RucEmpresa,
                    CodigoSucursal = dto.CodigoSucursal,
                    NombreSucursal = dto.NombreSucursal,
                    Direccion = dto.Direccion,
                    Ciudad = dto.Ciudad,
                    Telefono = dto.Telefono,
                    Email = dto.Email,
                    Responsable = dto.Responsable,
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
                suscripcion.CodigoSucursal = dto.CodigoSucursal;
                suscripcion.NombreSucursal = dto.NombreSucursal;
                //conversion de DATETIME a DATEONLY
                suscripcion.Direccion = dto.Direccion;
                suscripcion.Ciudad =dto.Ciudad;
                suscripcion.Telefono = dto.Telefono;
                suscripcion.Email = dto.Email;
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
    }
}
