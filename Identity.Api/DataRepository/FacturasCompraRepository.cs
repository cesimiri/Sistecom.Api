using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public void InsertFacturasCompra(FacturasCompra NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.FacturasCompras.Add(NewItem);
                context.SaveChanges();
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


        public void DeleteFacturasCompra(FacturasCompra NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.FacturasCompras.Remove(NewItem);
                context.SaveChanges();
            }
        }

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
