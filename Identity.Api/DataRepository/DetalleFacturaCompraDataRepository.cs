using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
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

        // borra todos los registros que tengan IdFactura el valor que le paso en idRegistrado
        public void DeleteDetalleFacturaCompraById(int idRegistrado)
        {
            using (var context = new InvensisContext())
            {
                var existentes = context.DetalleFacturaCompras
                    .Where(d => d.IdFactura == idRegistrado)
                    .ToList();

                if (existentes.Any())
                {
                    context.DetalleFacturaCompras.RemoveRange(existentes);
                    context.SaveChanges();
                }
            }
        }




        //detalle masivo y que calcule en iva y actualice

        public void InsertarDetallesMasivos(List<DetalleFacturaCompraDTO> lista)
        {
            using var context = new InvensisContext();

            if (lista == null || lista.Count == 0)
                throw new Exception("La lista de detalles está vacía.");

            int idFactura = lista[0].IdFactura;

            foreach (var item in lista)
            {
                var facturaEntity = context.FacturasCompras.Find(item.IdFactura);
                var producto = context.Productos.Find(item.IdProducto);

                if (facturaEntity == null || producto == null)
                    throw new Exception("Error: Factura o Producto no válido.");

                var nuevoDetalle = new DetalleFacturaCompra
                {
                    IdFactura = item.IdFactura,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    Descuento = item.Descuento,
                    Subtotal = (item.Cantidad * item.PrecioUnitario),
                    NumerosSerie = item.NumerosSerie,
                    DetallesAdicionales = item.DetallesAdicionales,
                };

                context.DetalleFacturaCompras.Add(nuevoDetalle);
            }

            context.SaveChanges();

            // === Calcular subtotal total ===
            var subtotalTotal = context.DetalleFacturaCompras
                .Where(d => d.IdFactura == idFactura)
                .Sum(d => d.Subtotal);

            // === Obtener porcentaje enviado desde el front ===
            decimal porcentajeIva = lista[0].PorcentajeIva;

            // Validar que sea uno de los permitidos
            var valoresPermitidos = new decimal[] { 0m, 12m, 15m, 16m };
            if (!valoresPermitidos.Contains(porcentajeIva))
                porcentajeIva = 15m;  // fallback seguro

            // === Solo usamos el porcentaje para calcular el total ===
            decimal ivaCalculado = subtotalTotal * (porcentajeIva / 100m);
            decimal total = subtotalTotal + ivaCalculado;

            // === Actualizar factura ===
            var facturaParaActualizar = context.FacturasCompras.Find(idFactura);
            if (facturaParaActualizar != null)
            {
                facturaParaActualizar.SubtotalSinImpuestos = subtotalTotal;

                // ❌ Ya NO se guarda el valor monetario del IVA
                // facturaParaActualizar.Iva = ivaCalculado;  (ELIMINADO)

                // ✔ Solo guardamos el porcentaje del IVA
                facturaParaActualizar.Iva = porcentajeIva;

                facturaParaActualizar.ValorTotal = total;

                context.SaveChanges();
            }
        }


        public IEnumerable<DetalleFacturaCompraDTO> GetDetalleFacturaCompraByIdFactura(int idFactura)
        {
            using var context = new InvensisContext();

            return context.DetalleFacturaCompras
                .Include(s => s.IdProductoNavigation)
                .Where(s => s.IdFactura == idFactura)
                .Select(s => new DetalleFacturaCompraDTO
                {
                    IdDetalle = s.IdDetalle,
                    IdFactura = s.IdFactura,
                    IdProducto = s.IdProducto,
                    Cantidad = s.Cantidad,
                    PrecioUnitario = s.PrecioUnitario,
                    Descuento = s.Descuento,
                    Subtotal = s.Subtotal,
                    NumerosSerie = s.NumerosSerie,
                    DetallesAdicionales = s.DetallesAdicionales
                })
                .ToList();
        }

    }
}
