using Identity.Api.DTO;
using Identity.Api.Paginado;
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
                    EmailDepartamento = s.EmailDepartamento,
                    Extension = s.Extension,
                    CentroCosto = s.CentroCosto,
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
                    EmailDepartamento = s.EmailDepartamento,
                    Extension = s.Extension,
                    CentroCosto = s.CentroCosto,
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

                // Generar el NumeroSolicitud automático
                
                // Obtener la última solicitud para este año para sacar el siguiente número
                var lastNumero = context.Departamentos
                    .Where(s => s.CodigoDepartamento.StartsWith($"DP-"))
                    .OrderByDescending(s => s.CodigoDepartamento)
                    .Select(s => s.CodigoDepartamento)
                    .FirstOrDefault();

                int nextNumber = 1;
                if (lastNumero != null)
                {
                    // Ejemplo: SC-2025-0005
                    var lastNumberStr = lastNumero.Split('-').Last();
                    if (int.TryParse(lastNumberStr, out var lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
                var nuevoCodigoDepartamento = $"DP-{nextNumber:D4}";


                var sucursal = context.Sucursales.Find(dto.IdSucursal);
                

                if (sucursal == null )
                {
                    throw new Exception("no existe el dato en la tabla relacionada");
                }

                var nueva = new Departamento
                {

                    IdDepartamento = dto.IdDepartamento,
                    IdSucursal = dto.IdSucursal,
                    CodigoDepartamento = nuevoCodigoDepartamento,
                    NombreDepartamento = dto.NombreDepartamento?.ToUpper(),
                    Descripcion = dto.Descripcion?.ToUpper(),
                    Responsable = dto.Responsable?.ToUpper(),
                    EmailDepartamento = dto.EmailDepartamento?.Trim().ToLower(),
                    Extension = dto.Extension,
                    CentroCosto = dto.CentroCosto,
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
                departamento.NombreDepartamento = dto.NombreDepartamento?.ToUpper();
                departamento.Descripcion = dto.Descripcion?.ToUpper();
                departamento.Responsable = dto.Responsable?.ToUpper();
                departamento.EmailDepartamento = dto.EmailDepartamento?.Trim().ToLower();
                departamento.Extension = dto.Extension;
                departamento.CentroCosto = dto.CentroCosto;
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


        //PAGINADA 
        public PagedResult<DepartamentoDTO> GetDepartamentosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Departamentos
                .Include(s => s.IdSucursalNavigation)
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NombreDepartamento.ToLower().Contains(filtro) ||
                    u.IdSucursalNavigation.NombreSucursal.ToLower().Contains(filtro)


                );
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
                .OrderBy(u => u.IdDepartamento) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new DepartamentoDTO
                {
                    IdDepartamento = s.IdDepartamento,
                    IdSucursal = s.IdSucursal,
                    CodigoDepartamento = s.CodigoDepartamento,
                    NombreDepartamento = s.NombreDepartamento,
                    Descripcion = s.Descripcion,
                    Responsable = s.Responsable,
                    EmailDepartamento = s.EmailDepartamento,
                    Extension = s.Extension,
                    CentroCosto = s.CentroCosto,
                    Estado = s.Estado,
                    NombreSucursal = s.IdSucursalNavigation.NombreSucursal
                })
                .ToList();

            return new PagedResult<DepartamentoDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
