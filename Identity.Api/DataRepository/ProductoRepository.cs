using Modelo.Sistecom.Modelo.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Identity.Api.DataRepository
{
    public class ProductoRepository
    {
        public List<Producto> ProductoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.Productos.ToList();
            }
        }

        public Producto GetProductoById(int IdProducto)
        {
            using (var context = new InvensisContext())
            {
                return context.Productos.FirstOrDefault(p => p.IdProducto == IdProducto); ;
            }

        }

        public void InsertProducto(Producto NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.Productos.Add(NewItem);
                context.SaveChanges();
            }
        }

        public void UpdateProducto(Producto updItem)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Productos
                                         .Where(a => a.IdProducto == updItem.IdProducto)
                                         .FirstOrDefault();

                if (existente != null)
                {
                    existente.CodigoPrincipal = updItem.CodigoPrincipal;
                    existente.CodigoAuxiliar = updItem.CodigoAuxiliar;
                    existente.Nombre = updItem.Nombre;
                    existente.Descripcion = updItem.Descripcion;
                    existente.IdCategoria = updItem.IdCategoria;
                    existente.TipoProducto = updItem.TipoProducto;
                    existente.EsComponente = updItem.EsComponente;
                    existente.EsEnsamblable = updItem.EsEnsamblable;
                    existente.RequiereSerial = updItem.RequiereSerial;
                    existente.Marca = updItem.Marca;
                    existente.Modelo = updItem.Modelo;
                    existente.UnidadMedida = updItem.UnidadMedida;
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
                    existente.FechaRegistro = updItem.FechaRegistro;


                    context.SaveChanges();
                }
            }
        }

        public void DeleteProducto(Producto NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.Productos.Remove(NewItem);
                context.SaveChanges();
            }
        }

        public void DeleteProductoById(int IdProducto)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Productos
                                         .Where(a => a.IdProducto == IdProducto)
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
