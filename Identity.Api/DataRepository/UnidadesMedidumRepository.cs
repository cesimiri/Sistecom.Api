using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class UnidadesMedidumRepository
    {
        public List<UnidadesMedidum> GetAllUnidades()
        {
            using (var context = new InvensisContext())
            {
                return context.UnidadesMedida.ToList();
            }
        }

        public UnidadesMedidum GetUnidadesById(int idUnidades)
        {
            using (var context = new InvensisContext())
            {
                return context.UnidadesMedida.FirstOrDefault(p => p.IdUnidadMedida == idUnidades); ;
            }

        }

        public void InsertUnidades(UnidadesMedidumDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                // Generar el NumeroSolicitud automático
                // Obtener la última solicitud para este año para sacar el siguiente número
                var lastNumero = context.UnidadesMedida
                    .Where(s => s.Codigo.StartsWith($"UM-"))
                    .OrderByDescending(s => s.Codigo)
                    .Select(s => s.Codigo)
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
                var NuevoCodigoPrincipal = $"UM-{nextNumber:D4}";


                var nueva = new UnidadesMedidum
                {

                    Codigo = NuevoCodigoPrincipal,
                    Nombre = dto.Nombre?.ToUpper(),
                    Descripcion = dto.Descripcion?.ToUpper(),
                    PermiteDecimales = dto.PermiteDecimales,
                    EsUnidadBase = dto.EsUnidadBase,
                    FactorConversion = dto.FactorConversion,
                    IdUnidadBase = dto.IdUnidadBase,
                    
                    Estado = dto.Estado,

                };

                context.UnidadesMedida.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la unidad de medida : " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void UpdateUnidades(UnidadesMedidum dto)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.UnidadesMedida
                                         .Where(a => a.IdUnidadMedida == dto.IdUnidadMedida)
                                         .FirstOrDefault();

                if (existente != null)
                {
                    existente.IdUnidadMedida = dto.IdUnidadMedida;
                    existente.Codigo = dto.Codigo;
                    existente.Nombre = dto.Nombre?.ToUpper();
                    existente.Descripcion = dto.Descripcion?.ToUpper();
                    existente.PermiteDecimales = dto.PermiteDecimales;
                    existente.EsUnidadBase = dto.EsUnidadBase;
                    existente.FactorConversion = dto.FactorConversion;
                    existente.IdUnidadBase = dto.IdUnidadBase;
                   
                    existente.Estado = dto.Estado;
                    context.SaveChanges();
                }
            }
        }

        //public void DeleteProducto(Producto NewItem)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.Productos.Remove(NewItem);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteUnidadesById(int idUnidades)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.UnidadesMedida
                                         .Where(a => a.IdUnidadMedida == idUnidades)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.UnidadesMedida.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }



        //PAGINADA 
        public PagedResult<UnidadesMedidumDTO> GetUnidadesMedidumPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.UnidadesMedida
                
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Nombre.ToLower().Contains(filtro) );
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
                .OrderBy(u => u.IdUnidadMedida) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new UnidadesMedidumDTO
                {
                    IdUnidadMedida = s.IdUnidadMedida,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    PermiteDecimales = s.PermiteDecimales ?? false,
                    EsUnidadBase = s.EsUnidadBase ?? false,
                    FactorConversion = s.FactorConversion,
                    IdUnidadBase = s.IdUnidadBase,
                    Estado = s.Estado
                })
                .ToList();

            return new PagedResult<UnidadesMedidumDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
