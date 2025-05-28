using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Interfaces
{
    public interface IFacturasCompra
    {
        IEnumerable<FacturasCompra> FacturasCompraInfoAll { get; }
        FacturasCompra GetFacturasCompraById(int idFacturasCompra);
        void InsertFacturasCompra(FacturasCompra New);
        void UpdateFacturasCompra(FacturasCompra UpdItem);
        void DeleteFacturasCompra(FacturasCompra DelItem);
        void DeleteFacturasCompraById(int IdFacturasCompra);


        //IEnumerable<FacturasCompra> FacturasCompraInfoAll { get; }
        //FacturasCompra GetFacturaCompraById(int idFactura);
        //void InsertFacturaCompra(FacturasCompra nuevaFactura);
        //void UpdateFacturaCompra(FacturasCompra facturaActualizada);
        //void DeleteFacturaCompra(FacturasCompra facturaEliminar);
        //void DeleteFacturaCompraById(int idFactura);
    }
}
