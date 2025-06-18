using Identity.Api.DTO;
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
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
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
                    existente.Nombre = dto.Nombre;
                    existente.Descripcion = dto.Descripcion;
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
    }
}
