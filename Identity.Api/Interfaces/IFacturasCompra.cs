using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Interfaces
{
    public interface IFacturasCompra
    {
        IEnumerable<FacturasCompraDTO> FacturasCompraInfoAll { get; }
        FacturasCompra GetFacturasCompraById(int idFacturasCompra);
        int InsertFacturasCompra(FacturasCompraDTO New);
        void UpdateFacturasCompra(FacturasCompra UpdItem);

        //void DeleteFacturasCompra(FacturasCompra DelItem);
        void DeleteFacturasCompraById(int IdFacturasCompra);


    }
}
