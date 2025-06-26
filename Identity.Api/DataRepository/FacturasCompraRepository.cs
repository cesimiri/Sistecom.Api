using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Api.DTO;

namespace Identity.Api.DataRepository
{
    public class FacturasCompraRepository
    {
        public List<FacturasCompra> GetAllFacturasCompra()
        {
            using (var context = new InvensisContext())
            {
                return context.FacturasCompras.ToList();
            }
        }

        public FacturasCompra GetFacturasCompraById(int idFacturasCompra)
        {
            using (var context = new InvensisContext())
            {
                return context.FacturasCompras.FirstOrDefault(p => p.IdFactura == idFacturasCompra); ;
            }

        }

        public int InsertFacturasCompra(FacturasCompraDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                //validación para el ingreso de los id relacionados.
                var proveedor = context.Proveedores.Find(dto.IdProveedor);
                var bodega = context.Bodegas.Find(dto.IdBodega);

                if (proveedor == null || bodega == null )
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
                    Observaciones = dto.Observaciones,


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



        public void UpdateFacturasCompra(FacturasCompra UpdItem)
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
                    registrado.Observaciones = UpdItem.Observaciones;
                    registrado.FechaRegistro = UpdItem.FechaRegistro;
                    

                    context.SaveChanges();
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

    }
}
