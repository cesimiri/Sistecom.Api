using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class LicenciaRepository
    {
        public List<LicenciaDTO> LicenciaInfoAll()
        {
            using var context = new InvensisContext();
            return context.Licencias
                .Include(s => s.IdTipoLicenciaNavigation)
                .Include(s => s.IdProductoNavigation)
                .Include(s => s.IdFacturaCompraNavigation)



                .Select(s => new LicenciaDTO
                {

                    IdLicencia = s.IdLicencia,
                    IdTipoLicencia = s.IdTipoLicencia,
                    IdProducto = s.IdProducto,
                    IdFacturaCompra = s.IdFacturaCompra,
                    NumeroLicencia = s.NumeroLicencia,
                    ClaveProducto = s.ClaveProducto,
                    FechaAdquisicion = s.FechaAdquisicion,
                    FechaInicioVigencia = s.FechaInicioVigencia,
                    FechaFinVigencia = s.FechaFinVigencia,
                    TipoSuscripcion = s.TipoSuscripcion,
                    CantidadUsuarios = s.CantidadUsuarios,
                    CostoLicencia = s.CostoLicencia,
                    RenovacionAutomatica = s.RenovacionAutomatica,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado,
                    

                    // campos relacionados:
                    nombreLicencia = s.IdTipoLicenciaNavigation.Nombre,
                    nombreProducto = s.IdProductoNavigation.Nombre,
                    numeroFactura = s.IdFacturaCompraNavigation.NumeroFactura

                })
                .ToList();
        }


        public Licencia GetLicenciaById(int IdLicencia)
        {
            using (var context = new InvensisContext())
            {
                return context.Licencias.FirstOrDefault(m => m.IdLicencia == IdLicencia);
            }
        }

        public void InsertLicencia(LicenciaDTO dto)
        {
            try 
            {
                using var context = new InvensisContext();
                //validación para el ingreso de los id relacionados.
                var idTiposLicencia = context.TiposLicencia.Find(dto.IdTipoLicencia);
                var idProducto = context.Productos.Find(dto.IdProducto);
                var idFactura = context.FacturasCompras.Find(dto.IdFacturaCompra);

                if (idTiposLicencia == null || idProducto == null || idFactura == null )
                {
                    throw new Exception("Esa idTiposLicencia, idProducto, idFactura no existe en la base de datos.");
                }

                // Generar el Código Licenica automático
                var lastCodigo = context.Productos
                    .Where(s => s.CodigoPrincipal.StartsWith("LIC-"))
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

                var NuevoCodigoPrincipal = $"LIC-{nextNumber:D4}";

                var nueva = new Licencia
                {
                    IdTipoLicencia = dto.IdTipoLicencia,
                    IdProducto = dto.IdProducto,
                    IdFacturaCompra = dto.IdFacturaCompra,
                    NumeroLicencia = dto.NumeroLicencia,
                    ClaveProducto = dto.ClaveProducto,
                    FechaAdquisicion = dto.FechaAdquisicion,
                    FechaInicioVigencia = dto.FechaInicioVigencia,
                    FechaFinVigencia = dto.FechaFinVigencia,
                    TipoSuscripcion = dto.TipoSuscripcion,
                    CantidadUsuarios = dto.CantidadUsuarios,
                    CostoLicencia = dto.CostoLicencia,
                    RenovacionAutomatica = dto.RenovacionAutomatica,
                    Observaciones = dto.Observaciones,
                    Estado = dto.Estado,


                };
                context.Licencias.Add(nueva);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Producto: " + ex.InnerException?.Message ?? ex.Message);
            }
            
        }


        public void UpdateLicencia(Licencia licenciaActualizada)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Licencias.FirstOrDefault(a => a.IdLicencia == licenciaActualizada.IdLicencia);
                if (existente != null)
                {
                    existente.IdTipoLicencia = licenciaActualizada.IdTipoLicencia;
                    existente.IdProducto = licenciaActualizada.IdProducto;
                    existente.IdFacturaCompra = licenciaActualizada.IdFacturaCompra;
                    existente.NumeroLicencia = licenciaActualizada.NumeroLicencia;
                    existente.ClaveProducto = licenciaActualizada.ClaveProducto;
                    existente.FechaAdquisicion = licenciaActualizada.FechaAdquisicion;
                    existente.FechaInicioVigencia = licenciaActualizada.FechaInicioVigencia;
                    existente.FechaFinVigencia = licenciaActualizada.FechaFinVigencia;
                    existente.TipoSuscripcion = licenciaActualizada.TipoSuscripcion;
                    existente.CantidadUsuarios = licenciaActualizada.CantidadUsuarios;
                    existente.CostoLicencia = licenciaActualizada.CostoLicencia;
                    existente.RenovacionAutomatica = licenciaActualizada.RenovacionAutomatica;
                    existente.Observaciones = licenciaActualizada.Observaciones;
                    existente.Estado = licenciaActualizada.Estado;
                    existente.FechaRegistro = licenciaActualizada.FechaRegistro;


                    context.SaveChanges();
                }
            }
        }

        //public void DeleteLicencia(Licencia activoToDelete)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.Licencias.Remove(activoToDelete);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteLicenciaById(int idLicencia)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Licencias.FirstOrDefault(a => a.IdLicencia == idLicencia);
                if (existente != null)
                {
                    context.Licencias.Remove(existente);
                    context.SaveChanges();
                }
            }
        }


        //public List<FacturasCompraDTO> GetFacturasConCategoria6()
        //{
        //    using var context = new InvensisContext();

        //    var facturas = context.FacturasCompras
        //        .Include(f => f.IdProveedorNavigation)
        //        .Include(f => f.IdBodegaNavigation)
        //        .Include(f => f.DetalleFacturaCompras!)
        //            .ThenInclude(d => d.IdProductoNavigation)
        //        .Where(f => f.DetalleFacturaCompras!
        //            .Any(d => d.IdProductoNavigation != null && d.IdProductoNavigation.IdCategoria == 6))
        //        .Select(s => new FacturasCompraDTO
        //        {
        //            IdFactura = s.IdFactura,
        //            NumeroFactura = s.NumeroFactura,
        //            NumeroAutorizacion = s.NumeroAutorizacion,
        //            ClaveAcceso = s.ClaveAcceso,
        //            IdProveedor = s.IdProveedor,
        //            IdBodega = s.IdBodega,
        //            FechaEmision = s.FechaEmision,
        //            SubtotalSinImpuestos = s.SubtotalSinImpuestos,
        //            DescuentoTotal = s.DescuentoTotal,
        //            Ice = s.Ice,
        //            Iva = s.Iva,
        //            ValorTotal = s.ValorTotal,
        //            FormaPago = s.FormaPago,
        //            Estado = s.Estado,
        //            Observaciones = s.Observaciones,

        //            // Datos relacionados
        //            RazonSocial = s.IdProveedorNavigation.RazonSocial,
        //            NombreBodega = s.IdBodegaNavigation.Nombre
        //        })
        //        .ToList();

        //    return facturas;
        //}

        public List<FacturasCompraDTO> GetFacturasConCategoria6()
        {
            using var context = new InvensisContext();

            // Paso 1: Agrupar detalle de facturas por producto (solo productos categoría 6)
            var detalleFacturas = context.DetalleFacturaCompras
                .Where(d => d.IdProductoNavigation.IdCategoria == 6)
                .GroupBy(d => new { d.IdFactura, d.IdProducto })
                .Select(g => new
                {
                    IdFactura = g.Key.IdFactura,
                    IdProducto = g.Key.IdProducto,
                    CantidadComprada = g.Sum(x => x.Cantidad)
                }).ToList();

            // Paso 2: Agrupar licencias ya registradas por factura y producto
            var licencias = context.Licencias
                .Where(l => l.IdProducto != null && l.IdFacturaCompra != null)
                .GroupBy(l => new { l.IdFacturaCompra, l.IdProducto })
                .Select(g => new
                {
                    IdFactura = g.Key.IdFacturaCompra.Value,
                    IdProducto = g.Key.IdProducto.Value,
                    LicenciasUsadas = g.Count()
                }).ToList();

            // Paso 3: Ver facturas que aún tienen productos disponibles
            var facturasDisponibles = detalleFacturas
                .GroupJoin(licencias,
                    df => new { df.IdFactura, df.IdProducto },
                    l => new { l.IdFactura, l.IdProducto },
                    (df, lic) => new
                    {
                        df.IdFactura,
                        df.IdProducto,
                        CantidadComprada = df.CantidadComprada,
                        LicenciasUsadas = lic.FirstOrDefault()?.LicenciasUsadas ?? 0
                    })
                .Where(x => x.LicenciasUsadas < x.CantidadComprada)
                .Select(x => x.IdFactura)
                .Distinct()
                .ToList();

            // Paso 4: Traer los datos completos solo de esas facturas
            var facturas = context.FacturasCompras
                .Include(f => f.IdProveedorNavigation)
                .Include(f => f.IdBodegaNavigation)
                .Where(f => facturasDisponibles.Contains(f.IdFactura))
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
                    RazonSocial = s.IdProveedorNavigation.RazonSocial,
                    NombreBodega = s.IdBodegaNavigation.Nombre
                })
                .ToList();

            return facturas;
        }



        public List<ProductoDTO> GetProductoConCategoria6()
        {
            using var context = new InvensisContext();
            var productos = context.Productos
                .Where(s=> s.IdCategoria ==6)
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
                    Estado = s.Estado

                })
                .ToList();
            return productos;
        }
    }
}
