using Identity.Api.DTO;
using Identity.Api.Interfaces;
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
                    //Marca = s.Marca,
                    //Modelo = s.Modelo,
                    //UnidadMedida = s.UnidadMedida,
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
                    NombreCategoria = s.IdCategoriaNavigation.Nombre

                })
                .ToList();
        }

        public ProductoDTO GetProductoById(int idProducto)
        {
            using var context = new InvensisContext();

            return context.Productos
                .Include(s => s.IdCategoriaNavigation)

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
                    //Marca = s.Marca,
                    //Modelo = s.Modelo,
                    //UnidadMedida = s.UnidadMedida,
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
                    NombreCategoria = s.IdCategoriaNavigation.Nombre
                })
                .FirstOrDefault();

        }

        public void InsertProducto(ProductoDTO dto)
        {
            try
            {
                using var context = new InvensisContext();


                var categoria = context.CategoriasProductos.Find(dto.IdCategoria);

                if (categoria == null )
                {
                    throw new Exception("Esa Categoria no existe en la base de datos.");
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
                    TipoProducto = dto.TipoProducto,
                    EsComponente = dto.EsComponente,
                    EsEnsamblable = dto.EsEnsamblable,
                    RequiereSerial = dto.RequiereSerial,
                    //Marca = dto.Marca,
                    //Modelo = dto.Modelo,
                    //UnidadMedida = dto.UnidadMedida,
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
                    //existente.Marca = updItem.Marca;
                    //existente.Modelo = updItem.Modelo;
                    //existente.UnidadMedida = updItem.UnidadMedida;
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

        //public void DeleteProducto(Producto NewItem)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.Productos.Remove(NewItem);
        //        context.SaveChanges();
        //    }
        //}

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
    }
}
