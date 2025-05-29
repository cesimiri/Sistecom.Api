using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class CategoriasProductoRepository
    {
        public List<CategoriasProducto> CategoriasProductoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.CategoriasProductos.ToList();
            }
        }

        public CategoriasProducto GetCategoriasProductoById(int IdCategoriasProducto)
        {
            using (var context = new InvensisContext())
            {
                return context.CategoriasProductos.FirstOrDefault(p => p.IdCategoria == IdCategoriasProducto); ;
            }

        }

        public void InsertCategoriasProducto(CategoriasProducto NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.CategoriasProductos.Add(NewItem);
                context.SaveChanges();
            }
        }

        public void UpdateCategoriasProducto(CategoriasProducto UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.CategoriasProductos
                                         .Where(a => a.IdCategoria == UpdItem.IdCategoria)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.Nombre = UpdItem.Nombre;
                    registrado.Descripcion = UpdItem.Descripcion;
                    registrado.RequiereSerial = UpdItem.RequiereSerial;
                    registrado.VidaUtilMeses = UpdItem.VidaUtilMeses;
                    registrado.Estado = UpdItem.Estado;
                    

                    context.SaveChanges();
                }
            }
        }

        public void DeleteCategoriasProducto(CategoriasProducto NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.CategoriasProductos.Remove(NewItem);
                context.SaveChanges();
            }
        }

        public void DeleteCategoriasProductoById(int IdCategoriaProducto)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.CategoriasProductos
                                         .Where(a => a.IdCategoria == IdCategoriaProducto)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.CategoriasProductos.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}
