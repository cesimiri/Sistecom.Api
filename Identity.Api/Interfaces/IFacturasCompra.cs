using Identity.Api.DTO;
using Identity.Api.Paginado;
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
        //por enventos en el front solo en update
        Task UpdateFacturasCompra(FacturasCompraDTO UpdItem);

        //void DeleteFacturasCompra(FacturasCompra DelItem);
        void DeleteFacturasCompraById(int IdFacturasCompra);


        // Nuevo método para paginado:
        PagedResult<FacturasCompraDTO> GetFacturasCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

    }
}
