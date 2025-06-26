using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface IDetalleFacturaCompra
    {
        IEnumerable<DetalleFacturaCompra> DetallesFacturaCompraInfoAll { get; }
        DetalleFacturaCompra GetDetalleFacturaCompraById(int idDetalle);
        void InsertDetalleFacturaCompra(DetalleFacturaCompra NewItem);
        void UpdateDetalleFacturaCompra(DetalleFacturaCompra UpdItem);
        //void DeleteDetalleFacturaCompra(DetalleFacturaCompra DelItem);
        void DeleteDetalleFacturaCompraById(int idRegistrado);

        void InsertarDetallesMasivos(List<DetalleFacturaCompraDTO> lista);


    }
}
