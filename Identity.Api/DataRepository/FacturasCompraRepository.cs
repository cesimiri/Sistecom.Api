using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class FacturasCompraRepository
    {
        public List<FacturasCompraDTO> GetAllFacturasCompra()
        {
            using var context = new InvensisContext();
            return context.FacturasCompras
                .Include(s => s.IdBodegaNavigation)
                .Include(s => s.IdProveedorNavigation)

                .Select(s => new FacturasCompraDTO
                {

                    IdFactura = s.IdFactura,
                    NumeroFactura = s.NumeroFactura,
                    NumeroAutorizacion = s.NumeroAutorizacion,
                    ClaveAcceso = s.ClaveAcceso,
                    IdProveedor = s.IdProveedor,
                    IdBodega = s.IdBodega,
                    FechaEmision = s.FechaEmision,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Ice = s.Ice,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    FormaPago = s.FormaPago,
                    Estado = s.Estado,
                    Observaciones = s.Observaciones,


                    // campos relacionados:
                    RazonSocial = s.IdProveedorNavigation.RazonSocial,
                    NombreBodega = s.IdBodegaNavigation.Nombre

                })
                .ToList();
        }

        public FacturasCompraDTO GetFacturasCompraById(int idFactura)
        {
            using var context = new InvensisContext();

            var s = context.FacturasCompras
                .Include(f => f.IdProveedorNavigation)
                .Include(f => f.IdBodegaNavigation)
                .FirstOrDefault(f => f.IdFactura == idFactura);

            if (s == null) return null;

            return new FacturasCompraDTO
            {
                IdFactura = s.IdFactura,
                NumeroFactura = s.NumeroFactura,
                NumeroAutorizacion = s.NumeroAutorizacion,
                ClaveAcceso = s.ClaveAcceso,
                IdProveedor = s.IdProveedor,
                IdBodega = s.IdBodega,
                FechaEmision = s.FechaEmision,
                SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                DescuentoTotal = s.DescuentoTotal,
                Ice = s.Ice,
                Iva = s.Iva,
                ValorTotal = s.ValorTotal,
                FormaPago = s.FormaPago,
                Estado = s.Estado,
                Observaciones = s.Observaciones,
                RazonSocial = s.IdProveedorNavigation?.RazonSocial,
                NombreBodega = s.IdBodegaNavigation?.Nombre
            };
        }


        public int InsertFacturasCompra(FacturasCompraDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                //validación para el ingreso de los id relacionados.
                var proveedor = context.Proveedores.Find(dto.IdProveedor);
                var bodega = context.Bodegas.Find(dto.IdBodega);

                if (proveedor == null || bodega == null)
                {
                    throw new Exception("Esa proveedor , bodega no existe en la base de datos.");
                }



                var nueva = new FacturasCompra
                {

                    NumeroFactura = dto.NumeroFactura,
                    NumeroAutorizacion = dto.NumeroAutorizacion,
                    ClaveAcceso = "0",
                    IdProveedor = dto.IdProveedor,
                    IdBodega = dto.IdBodega,
                    FechaEmision = dto.FechaEmision,
                    SubtotalSinImpuestos = dto.SubtotalSinImpuestos,
                    DescuentoTotal = 0,
                    Ice = 0,
                    Iva = 0,
                    ValorTotal = 0,
                    FormaPago = dto.FormaPago,
                    Estado = dto.Estado,
                    Observaciones = dto.Observaciones?.ToUpper()


                };

                context.FacturasCompras.Add(nueva);
                context.SaveChanges();

                //devuelve el valor IdFactura nuevo
                return nueva.IdFactura;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Producto: " + ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task UpdateFacturasCompra(FacturasCompraDTO UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.FacturasCompras
                                         .Where(a => a.IdFactura == UpdItem.IdFactura)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.NumeroFactura = UpdItem.NumeroFactura;
                    registrado.NumeroAutorizacion = UpdItem.NumeroAutorizacion;
                    registrado.ClaveAcceso = UpdItem.ClaveAcceso;
                    registrado.IdProveedor = UpdItem.IdProveedor;
                    registrado.IdBodega = UpdItem.IdBodega;
                    registrado.FechaEmision = UpdItem.FechaEmision;
                    registrado.SubtotalSinImpuestos = UpdItem.SubtotalSinImpuestos;
                    registrado.DescuentoTotal = UpdItem.DescuentoTotal;
                    registrado.Ice = UpdItem.Ice;
                    registrado.Iva = UpdItem.Iva;
                    registrado.ValorTotal = UpdItem.ValorTotal;
                    registrado.FormaPago = UpdItem.FormaPago;
                    registrado.Estado = UpdItem.Estado;
                    registrado.Observaciones = UpdItem.Observaciones?.ToUpper();



                    await context.SaveChangesAsync(); // <- importante
                }
            }
        }


        //public void DeleteFacturasCompra(FacturasCompra NewItem)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.FacturasCompras.Remove(NewItem);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteFacturasCompraById(int IdFacturasCompra)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.FacturasCompras
                                         .Where(a => a.IdFactura == IdFacturasCompra)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.FacturasCompras.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }


        //PAGINADA 
        public PagedResult<FacturasCompraDTO> GetFacturasCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.FacturasCompras
                .Include(s => s.IdBodegaNavigation)
                .Include(s => s.IdProveedorNavigation)
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NumeroFactura.ToLower().Contains(filtro)
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
                .OrderBy(u => u.IdFactura) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new FacturasCompraDTO
                {
                    IdFactura = s.IdFactura,
                    NumeroFactura = s.NumeroFactura,
                    NumeroAutorizacion = s.NumeroAutorizacion,
                    ClaveAcceso = s.ClaveAcceso,
                    IdProveedor = s.IdProveedor,
                    IdBodega = s.IdBodega,
                    FechaEmision = s.FechaEmision,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Ice = s.Ice,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    FormaPago = s.FormaPago,
                    Estado = s.Estado,
                    Observaciones = s.Observaciones,


                    // campos relacionados:
                    RazonSocial = s.IdProveedorNavigation.RazonSocial,
                    NombreBodega = s.IdBodegaNavigation.Nombre
                })
                .ToList();

            return new PagedResult<FacturasCompraDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }


        //exportar
        public List<FacturasCompraDTO> ObtenerFacturaCompraFiltradas(string? filtro, string? estado)
        {
            using var context = new InvensisContext();

            var query = context.FacturasCompras
               .Include(e => e.IdProveedorNavigation)
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var lowerFiltro = filtro.ToLower();
                query = query.Where(e =>
                    e.NumeroFactura.ToLower().Contains(lowerFiltro));
            }

            if (!string.IsNullOrWhiteSpace(estado))
            {
                query = query.Where(e => e.Estado == estado);
            }

            return query
                .Select(s => new FacturasCompraDTO
                {
                    IdFactura = s.IdFactura,
                    NumeroFactura = s.NumeroFactura,
                    NumeroAutorizacion = s.NumeroAutorizacion,
                    ClaveAcceso = s.ClaveAcceso,
                    IdProveedor = s.IdProveedor,
                    IdBodega = s.IdBodega,
                    FechaEmision = s.FechaEmision,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Ice = s.Ice,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    FormaPago = s.FormaPago,
                    Estado = s.Estado,
                    Observaciones = s.Observaciones,


                    // campos relacionados:

                    NombreProveedor = s.IdProveedorNavigation.RazonSocial
                })
                .ToList();
        }

        ////generarFactura atomaticamente

        public async Task<(FacturasCompraDTO factura, List<DetalleFacturaCompraDTO> detalles)> ObtenerFacturaConDetallesAsync(int idFactura)
        {
            using var context = new InvensisContext();

            // Traer la factura con sus relaciones (proveedor y bodega)
            var factura = await context.FacturasCompras
                .Include(f => f.IdProveedorNavigation)
                .Include(f => f.IdBodegaNavigation)
                .Where(f => f.IdFactura == idFactura)
                .Select(f => new FacturasCompraDTO
                {
                    IdFactura = f.IdFactura,
                    NumeroFactura = f.NumeroFactura,
                    NumeroAutorizacion = f.NumeroAutorizacion,
                    ClaveAcceso = f.ClaveAcceso,
                    IdProveedor = f.IdProveedor,
                    IdBodega = f.IdBodega,
                    FechaEmision = f.FechaEmision,
                    SubtotalSinImpuestos = f.SubtotalSinImpuestos,
                    DescuentoTotal = f.DescuentoTotal,
                    Ice = f.Ice,
                    Iva = f.Iva,
                    ValorTotal = f.ValorTotal,
                    FormaPago = f.FormaPago,
                    Estado = f.Estado,
                    Observaciones = f.Observaciones,
                    RazonSocial = f.IdProveedorNavigation.RazonSocial,
                    NombreBodega = f.IdBodegaNavigation.Nombre
                })
                .FirstOrDefaultAsync();

            if (factura == null)
                return (null, null);

            // Traer detalles con async (no usas Include aquí, solo datos directos)
            var detalles = await context.DetalleFacturaCompras
                .Include(f => f.IdProductoNavigation)

                .Where(d => d.IdFactura == idFactura)
                .Select(d => new DetalleFacturaCompraDTO
                {
                    IdDetalle = d.IdDetalle,
                    IdFactura = d.IdFactura,
                    IdProducto = d.IdProducto,
                    NombreProducto = d.IdProductoNavigation.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Descuento = d.Descuento,
                    Subtotal = d.Subtotal,
                    NumerosSerie = d.NumerosSerie,
                    DetallesAdicionales = d.DetallesAdicionales
                })
                .ToListAsync();

            return (factura, detalles);
        }





    }
}
