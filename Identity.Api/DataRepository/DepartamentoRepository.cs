using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class DepartamentoRepository
    {
        public List<DepartamentoDTO> GetAllDepartamento()
        {
            using var context = new InvensisContext();

            return context.Departamentos

                .Include(s => s.IdSucursalNavigation)
                .Select(s => new DepartamentoDTO
                {
                    IdDepartamento = s.IdDepartamento,
                    IdSucursal = s.IdSucursal,
                    CodigoDepartamento = s.CodigoDepartamento,
                    NombreDepartamento = s.NombreDepartamento,
                    Descripcion = s.Descripcion,
                    Responsable = s.Responsable,
                    Extension = s.Extension,
                    Estado = s.Estado,

                    // Campos relacionados:
                    NombreSucursal = s.IdSucursalNavigation.NombreSucursal
                })
                .ToList();
        }

        public DepartamentoDTO? GetDepartamentoById(int idDepartamento)
        {
            using var context = new InvensisContext();

            return context.Departamentos
                .Include(s => s.IdSucursalNavigation)

                .Where(s => s.IdDepartamento == idDepartamento)
                .Select(s => new DepartamentoDTO
                {
                    IdDepartamento = s.IdDepartamento,
                    IdSucursal = s.IdSucursal,
                    CodigoDepartamento = s.CodigoDepartamento,
                    NombreDepartamento = s.NombreDepartamento,
                    Descripcion = s.Descripcion,
                    Responsable = s.Responsable,
                    Extension = s.Extension,
                    Estado = s.Estado,

                    // Campos relacionados:
                    NombreSucursal = s.IdSucursalNavigation.NombreSucursal
                })
                .FirstOrDefault();
        }

        public void InsertDepartamento(DepartamentoDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                var sucursal = context.Sucursales.Find(dto.IdSucursal);
                

                if (sucursal == null )
                {
                    throw new Exception("no existe el dato en la tabla relacionada");
                }

                var nueva = new Departamento
                {

                    IdDepartamento = dto.IdDepartamento,
                    IdSucursal = dto.IdSucursal,
                    CodigoDepartamento = dto.CodigoDepartamento,
                    NombreDepartamento = dto.NombreDepartamento,
                    Descripcion = dto.Descripcion,
                    Responsable = dto.Responsable,
                    Extension = dto.Extension,
                    Estado = dto.Estado
                };

                context.Departamentos.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Error al insertar departamento: {innerMessage}");
            }
        }



        public void UpdateDepartamento(DepartamentoDTO dto)
        {
            using var context = new InvensisContext();

            var departamento = context.Departamentos
                .FirstOrDefault(s => s.IdDepartamento == dto.IdDepartamento);

            if (departamento != null)
            {
                departamento.IdSucursal = dto.IdSucursal;
                departamento.CodigoDepartamento = dto.CodigoDepartamento;
                departamento.NombreDepartamento = dto.NombreDepartamento;
                departamento.Descripcion = dto.Descripcion;
                departamento.Responsable = dto.Responsable;
                departamento.Extension = dto.Extension;
                departamento.Estado = dto.Estado;

                // Actualizar navegación (opcional)
                departamento.IdSucursalNavigation = context.Sucursales
                    .FirstOrDefault(e => e.IdSucursal == dto.IdSucursal);

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
        public void DeleteDepartamentoById(int idDepartamento)
        {
            using (var context = new InvensisContext())
            {
                var suscripcion = context.Departamentos
                    .FirstOrDefault(s => s.IdDepartamento == idDepartamento);

                if (suscripcion != null)
                {
                    context.Departamentos.Remove(suscripcion);
                    context.SaveChanges();
                }
            }
        }
    }
}
