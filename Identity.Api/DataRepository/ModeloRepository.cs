using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Modelo.Sistecom.Modelo.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Identity.Api.DataRepository
{
    public class ModeloRepository
    {
        public List<ModeloDTO> GetAllModelo()
        {
            using var context = new InvensisContext();
            return context.Modelos
                .Include(s => s.IdMarcaNavigation)


                .Select(s => new ModeloDTO
                {

                    IdModelo = s.IdModelo,
                    IdMarca = s.IdMarca,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    AñoLanzamiento = s.AñoLanzamiento,
                    Descontinuado = s.Descontinuado,
                    // covertir de dateonly a datetime 
                    FechaDescontinuacion = s.FechaDescontinuacion.HasValue
                    ? s.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
                    : null,
                    EspecificacionesGenerales = s.EspecificacionesGenerales,
                    ImagenUrl = s.ImagenUrl,
                    Estado = s.Estado,


                    // campos relacionados:
                    nombreMarca = s.IdMarcaNavigation.Nombre

                })
                .ToList();
        }

        public ModeloDTO GetModeloById(int idModelo)
        {
            using var context = new InvensisContext();

            return context.Modelos
                .Include(s => s.IdMarcaNavigation)

                .Where(s => s.IdModelo == idModelo)
                .Select(s => new ModeloDTO
                {
                    IdModelo = s.IdModelo,
                    IdMarca = s.IdMarca,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    AñoLanzamiento = s.AñoLanzamiento,
                    Descontinuado = s.Descontinuado,
                    // covertir de dateonly a datetime 
                    FechaDescontinuacion = s.FechaDescontinuacion.HasValue
    ? s.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
    : null,
                    EspecificacionesGenerales = s.EspecificacionesGenerales,
                    ImagenUrl = s.ImagenUrl,
                    Estado = s.Estado,


                    // campos relacionados:
                    nombreMarca = s.IdMarcaNavigation.Nombre
                })
                .FirstOrDefault();

        }

        public void InsertModelo(ModeloDTO dto)
        {
            try
            {
                using var context = new InvensisContext();


                var marca = context.Marcas.Find(dto.IdMarca);

                if (marca == null)
                {
                    throw new Exception("Esa marca no existe en la base de datos.");
                }

                // Generar el Código Principal automático
                var lastCodigo = context.Modelos
                    .Where(s => s.Codigo.StartsWith("CODM-"))
                    .OrderByDescending(s => s.Codigo)
                    .Select(s => s.Codigo)
                    .FirstOrDefault();

                int nextNumber = 1;
                if (lastCodigo != null)
                {
                    var lastNumberStr = lastCodigo.Split('-').Last();
                    if (int.TryParse(lastNumberStr, out var parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                var NuevoCodigoPrincipal = $"CODM-{nextNumber:D4}";

                var nueva = new Modelo.Sistecom.Modelo.Database.Modelo
                {

                    IdModelo = dto.IdModelo,
                    IdMarca = dto.IdMarca,
                    Codigo = NuevoCodigoPrincipal,
                    Nombre = dto.Nombre?.ToUpper(),
                    Descripcion = dto.Descripcion?.ToUpper(),
                    AñoLanzamiento = dto.AñoLanzamiento,
                    Descontinuado = dto.Descontinuado,
                    //covertir de datetime a dateonly 
                    FechaDescontinuacion = dto.FechaDescontinuacion.HasValue
                ? DateOnly.FromDateTime(dto.FechaDescontinuacion.Value)
                : null,
                    EspecificacionesGenerales = dto.EspecificacionesGenerales?.ToUpper(),
                    ImagenUrl = dto.ImagenUrl,      
                    Estado = dto.Estado,

                };

                context.Modelos.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Modelo: " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void UpdateModelo(ModeloDTO updItem)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Modelos
                                         .Where(a => a.IdModelo == updItem.IdModelo)
                                         .FirstOrDefault();

                if (existente != null)
                {
                    existente.IdMarca = updItem.IdMarca;
                    existente.Codigo = updItem.Codigo;
                    existente.Descripcion = updItem.Descripcion?.ToUpper();
                    existente.Nombre = updItem.Nombre?.ToUpper();
                    existente.AñoLanzamiento = updItem.AñoLanzamiento;
                    existente.Descontinuado = updItem.Descontinuado;
                    //covertir de datetime a dateonly 
                    existente.FechaDescontinuacion = updItem.FechaDescontinuacion.HasValue
                    ? DateOnly.FromDateTime(updItem.FechaDescontinuacion.Value): null;
                    existente.EspecificacionesGenerales = updItem.EspecificacionesGenerales?.ToUpper();
                    existente.ImagenUrl = updItem.ImagenUrl;
                    existente.Estado = updItem.Estado;
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

        public void DeleteModeloById(int idModelo)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Modelos
                                         .Where(a => a.IdModelo == idModelo)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.Modelos.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<ModeloDTO> GetModeloPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Modelos
                .Include(s => s.IdMarcaNavigation)

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
                .OrderBy(u => u.IdModelo) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ModeloDTO
                {
                    IdModelo = s.IdModelo,
                    IdMarca = s.IdMarca,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    AñoLanzamiento = s.AñoLanzamiento,
                    Descontinuado = s.Descontinuado,
                    FechaDescontinuacion = s.FechaDescontinuacion.HasValue
                    ? s.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
                    : (DateTime?)null,
                    EspecificacionesGenerales = s.EspecificacionesGenerales,
                    ImagenUrl = s.ImagenUrl,
                    Estado = s.Estado,
                    nombreMarca = s.IdMarcaNavigation.Nombre
                })
                .ToList();

            return new PagedResult<ModeloDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
