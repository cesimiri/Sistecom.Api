using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class DetalleFacturaCompraDataRepository
    {
        public List<DetalleFacturaCompra> GetAllDetallesFacturaCompra()
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleFacturaCompras.ToList();
            }
        }

        public DetalleFacturaCompra GetDetalleFacturaCompraById(int idDetalle)
        {
            using (var context = new InvensisContext())
            {
                return context.DetalleFacturaCompras.FirstOrDefault(p => p.IdDetalle == idDetalle);
            }
        }

        public void InsertDetalleFacturaCompra(DetalleFacturaCompra NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleFacturaCompras.Add(NewItem);
                context.SaveChanges();
            }
        }

        public void UpdateDetalleFacturaCompra(DetalleFacturaCompra UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.DetalleFacturaCompras.FirstOrDefault(d => d.IdDetalle == UpdItem.IdDetalle);
                if (existente != null)
                {
                    existente.IdFactura = UpdItem.IdFactura;
                    existente.IdProducto = UpdItem.IdProducto;
                    existente.Cantidad = UpdItem.Cantidad;
                    existente.PrecioUnitario = UpdItem.PrecioUnitario;
                    existente.Descuento = UpdItem.Descuento;
                    existente.Subtotal = UpdItem.Subtotal;
                    existente.NumerosSerie = UpdItem.NumerosSerie;
                    existente.DetallesAdicionales = UpdItem.DetallesAdicionales;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteDetalleFacturaCompra(DetalleFacturaCompra DelItem)
        {
            using (var context = new InvensisContext())
            {
                context.DetalleFacturaCompras.Remove(DelItem);
                context.SaveChanges();
            }
        }

        public void DeleteDetalleFacturaCompraById(int idRegistrado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.DetalleFacturaCompras.FirstOrDefault(d => d.IdDetalle == idRegistrado);
                if (existente != null)
                {
                    context.DetalleFacturaCompras.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
