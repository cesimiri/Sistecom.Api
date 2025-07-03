using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Identity.Api.DataRepository
{
    public class ProductoRepository
    {
        public List<ProductoDTO> GetAllProducto()
        {
            using var context = new InvensisContext();
            return context.Productos
                .Include(s => s.IdCategoriaNavigation)
                .Include(s => s.IdMarcaNavigation)
                .Include(s => s.IdModeloNavigation)
                .Include(s => s.IdUnidadMedidaNavigation)


                .Select(s => new ProductoDTO
                {

                    IdProducto = s.IdProducto,
                    CodigoPrincipal = s.CodigoPrincipal,
                    CodigoAuxiliar = s.CodigoAuxiliar,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    IdCategoria = s.IdCategoria,
                    TipoProducto = s.TipoProducto,
                    EsComponente = s.EsComponente,
                    EsEnsamblable = s.EsEnsamblable,
                    RequiereSerial = s.RequiereSerial,
                    IdMarca = s.IdMarca,
                    IdModelo = s.IdModelo,
                    IdUnidadMedida = s.IdUnidadMedida,
                    PrecioUnitario = s.PrecioUnitario,
                    PrecioVentaSugerido = s.PrecioVentaSugerido,
                    CostoEnsamblaje = s.CostoEnsamblaje,
                    TiempoEnsamblajeMinutos = s.TiempoEnsamblajeMinutos,
                    AplicaIva = s.AplicaIva,
                    PorcentajeIva = s.PorcentajeIva,
                    StockMinimo = s.StockMinimo,
                    StockMaximo = s.StockMaximo,
                    GarantiaMeses = s.GarantiaMeses,
                    EspecificacionesTecnicas = s.EspecificacionesTecnicas,
                    ImagenUrl = s.ImagenUrl,
                    Estado = s.Estado,

                    // campos relacionados:
                    NombreCategoria = s.IdCategoriaNavigation.Nombre,
                    NombreMarca = s.IdMarcaNavigation.Nombre,
                    NombreModelo = s.IdModeloNavigation.Nombre,
                    NombreUnidadesMedidas = s.IdUnidadMedidaNavigation.Nombre

                })
                .ToList();
        }



        // para traer los modelo 
        //public List<ModeloDTO> GetModelosByIdMarca(int idMarca)
        //{
        //    using var context = new InvensisContext();

        //    return context.Modelos
        //        .Where(m => m.IdMarca == idMarca)
        //        .Select(m => new ModeloDTO
        //        {
        //            IdModelo = m.IdModelo,
        //            Nombre = m.Nombre
        //        })
        //        .ToList();
        //}

        //obtener los modelso luego de seleccionar la marca
        public List<ModeloDTO> GetModelosByIdMarca(int idMarca)
        {
            using var context = new InvensisContext();

            return context.Modelos
                .Where(m => m.IdMarca == idMarca)
        .Select(m => new ModeloDTO
        {
            IdModelo = m.IdModelo,
            IdMarca = m.IdMarca,
            Codigo = m.Codigo!,
            Nombre = m.Nombre,
            Descripcion = m.Descripcion,
            AñoLanzamiento = m.AñoLanzamiento,
            Descontinuado = m.Descontinuado,
            FechaDescontinuacion = m.FechaDescontinuacion.HasValue
                ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
            : null,
            EspecificacionesGenerales = m.EspecificacionesGenerales,
            ImagenUrl = m.ImagenUrl,
            Estado = m.Estado
        })
        .ToList();  
        }

        public ProductoDTO GetProductoById(int idProducto)
        {
            using var context = new InvensisContext();

            return context.Productos
                .Include(s => s.IdCategoriaNavigation)
                .Include(s => s.IdMarcaNavigation)
                .Include(s => s.IdModeloNavigation)
                .Include(s => s.IdUnidadMedidaNavigation)

                .Where(s => s.IdProducto == idProducto)
                .Select(s => new ProductoDTO
                {
                    IdProducto = s.IdProducto,
                    CodigoPrincipal = s.CodigoPrincipal,
                    CodigoAuxiliar = s.CodigoAuxiliar,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    IdCategoria = s.IdCategoria,
                    TipoProducto = s.TipoProducto,
                    EsComponente = s.EsComponente,
                    EsEnsamblable = s.EsEnsamblable,
                    RequiereSerial = s.RequiereSerial,
                    IdMarca = s.IdMarca,
                    IdModelo = s.IdModelo,
                    IdUnidadMedida = s.IdUnidadMedida,
                    PrecioUnitario = s.PrecioUnitario,
                    PrecioVentaSugerido = s.PrecioVentaSugerido,
                    CostoEnsamblaje = s.CostoEnsamblaje,
                    TiempoEnsamblajeMinutos = s.TiempoEnsamblajeMinutos,
                    AplicaIva = s.AplicaIva,
                    PorcentajeIva = s.PorcentajeIva,
                    StockMinimo = s.StockMinimo,
                    StockMaximo = s.StockMaximo,
                    GarantiaMeses = s.GarantiaMeses,
                    EspecificacionesTecnicas = s.EspecificacionesTecnicas,
                    ImagenUrl = s.ImagenUrl,
                    Estado = s.Estado,

                    // campos relacionados:
                    NombreCategoria = s.IdCategoriaNavigation.Nombre,
                    NombreMarca = s.IdMarcaNavigation.Nombre,
                    NombreModelo = s.IdModeloNavigation.Nombre,
                    NombreUnidadesMedidas = s.IdUnidadMedidaNavigation.Nombre
                })
                .FirstOrDefault();

        }

        public void InsertProducto(ProductoDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                //validación para el ingreso de los id relacionados.
                var categoria = context.CategoriasProductos.Find(dto.IdCategoria);
                var marca = context.Marcas.Find(dto.IdMarca);
                var modelo = context.Modelos.Find(dto.IdModelo);
                var nombreUnidadesMedidas = context.UnidadesMedida.Find(dto.IdUnidadMedida);

                if (categoria == null|| marca == null || modelo == null || nombreUnidadesMedidas == null)
                {
                    throw new Exception("Esa Categoria , marca, modelo, unidadesmedidas no existe en la base de datos.");
                }

                // Generar el Código Principal automático
                var lastCodigo = context.Productos
                    .Where(s => s.CodigoPrincipal.StartsWith("CODP-"))
                    .OrderByDescending(s => s.CodigoPrincipal)
                    .Select(s => s.CodigoPrincipal)
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

                var NuevoCodigoPrincipal = $"CODP-{nextNumber:D4}";

                var nueva = new Producto
                {

                    CodigoPrincipal = NuevoCodigoPrincipal,
                    CodigoAuxiliar = dto.CodigoAuxiliar,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdCategoria = dto.IdCategoria,
                    TipoProducto = "PRODUCTO_FINAL",
                    EsComponente = dto.EsComponente,
                    EsEnsamblable = dto.EsEnsamblable,
                    RequiereSerial = dto.RequiereSerial,
                    IdMarca = dto.IdMarca,
                    IdModelo = dto.IdModelo,
                    IdUnidadMedida = dto.IdUnidadMedida,
                    PrecioUnitario = dto.PrecioUnitario,
                    PrecioVentaSugerido = dto.PrecioVentaSugerido,
                    CostoEnsamblaje = dto.CostoEnsamblaje,
                    TiempoEnsamblajeMinutos = dto.TiempoEnsamblajeMinutos,
                    AplicaIva = dto.AplicaIva,
                    PorcentajeIva = dto.PorcentajeIva,
                    StockMinimo = dto.StockMinimo,
                    StockMaximo = dto.StockMaximo,
                    GarantiaMeses = dto.GarantiaMeses,
                    EspecificacionesTecnicas = dto.EspecificacionesTecnicas,
                    Estado = dto.Estado,

                };

                context.Productos.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Producto: " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void UpdateProducto(ProductoDTO updItem)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Productos
                                         .Where(a => a.IdProducto == updItem.IdProducto)
                                         .FirstOrDefault();

                if (existente != null)
                {
                    existente.IdProducto = updItem.IdProducto;
                    existente.CodigoPrincipal = updItem.CodigoPrincipal;
                    existente.CodigoAuxiliar = updItem.CodigoAuxiliar;
                    existente.Nombre = updItem.Nombre;
                    existente.Descripcion = updItem.Descripcion;
                    existente.IdCategoria = updItem.IdCategoria;
                    existente.TipoProducto = updItem.TipoProducto;
                    existente.EsComponente = updItem.EsComponente;
                    existente.EsEnsamblable = updItem.EsEnsamblable;
                    existente.RequiereSerial = updItem.RequiereSerial;
                    existente.IdMarca = updItem.IdMarca;
                    existente.IdModelo = updItem.IdModelo;
                    existente.IdUnidadMedida = updItem.IdUnidadMedida;
                    existente.PrecioUnitario = updItem.PrecioUnitario;
                    existente.PrecioVentaSugerido = updItem.PrecioVentaSugerido;
                    existente.CostoEnsamblaje = updItem.CostoEnsamblaje;
                    existente.TiempoEnsamblajeMinutos = updItem.TiempoEnsamblajeMinutos;
                    existente.AplicaIva = updItem.AplicaIva;
                    existente.PorcentajeIva = updItem.PorcentajeIva;
                    existente.StockMinimo = updItem.StockMinimo;
                    existente.StockMaximo = updItem.StockMaximo;
                    existente.GarantiaMeses = updItem.GarantiaMeses;
                    existente.EspecificacionesTecnicas = updItem.EspecificacionesTecnicas;
                    existente.ImagenUrl = updItem.ImagenUrl;
                    existente.Estado = updItem.Estado;
                    context.SaveChanges();
                }
            }
        }

        

        public void DeleteProductoById(int idProducto)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Productos
                                         .Where(a => a.IdProducto == idProducto)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.Productos.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<ProductoDTO> GetProductoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Productos
                .Include(s => s.IdCategoriaNavigation)
                .Include(s => s.IdMarcaNavigation)
                .Include(s => s.IdModeloNavigation)
                .Include(s => s.IdUnidadMedidaNavigation)
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
                .OrderBy(u => u.IdProducto) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ProductoDTO
                {
                    IdProducto = s.IdProducto,
                    CodigoPrincipal = s.CodigoPrincipal,
                    CodigoAuxiliar = s.CodigoAuxiliar,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    IdCategoria = s.IdCategoria,
                    EsComponente = s.EsComponente,
                    EsEnsamblable = s.EsEnsamblable,
                    RequiereSerial = s.RequiereSerial,
                    IdMarca = s.IdMarca,
                    IdModelo = s.IdModelo,
                    IdUnidadMedida = s.IdUnidadMedida,
                    PrecioUnitario = s.PrecioUnitario,
                    PrecioVentaSugerido = s.PrecioVentaSugerido,
                    CostoEnsamblaje = s.CostoEnsamblaje,
                    TiempoEnsamblajeMinutos = s.TiempoEnsamblajeMinutos,
                    AplicaIva = s.AplicaIva,
                    PorcentajeIva = s.PorcentajeIva,
                    StockMinimo = s.StockMinimo,
                    StockMaximo = s.StockMaximo,
                    GarantiaMeses = s.GarantiaMeses,
                    EspecificacionesTecnicas = s.EspecificacionesTecnicas,
                    ImagenUrl = s.ImagenUrl,
                    Estado = s.Estado,
                    // campos relacionados:
                    NombreCategoria = s.IdCategoriaNavigation.Nombre,
                    NombreMarca = s.IdMarcaNavigation.Nombre,
                    NombreModelo = s.IdModeloNavigation.Nombre,
                    NombreUnidadesMedidas = s.IdUnidadMedidaNavigation.Nombre
                })
                .ToList();

            return new PagedResult<ProductoDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
