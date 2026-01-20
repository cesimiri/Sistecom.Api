using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IOrdenesEntrega
    {
        //traer todas las solicitudes de compra que no esten registradas aqui en ordenes de Entrega
        List<SolicitudesCompraDTO> GetSolicitudesAprobadasSinOrden();

        // traer los usuarios que tengan por su departamentos
        List<UsuarioDetalleDTO> GetUsuarioDetalleById(string cedula);

        OrdenesEntrega GetOrdenesEntregaById(int IdOrdenesEntrega);
        int InsertOrdenesEntrega(OrdenesEntregaDTO New);
        void UpdateOrdenesEntrega(OrdenesEntregaDTO UpdItem);

        void DeleteOrdenesEntregaById(int IdOrdenesEntrega);

        //generar PDF Ordenes automaticamente
        Task<(OrdenesEntregaDTO ordenes, List<DetalleOrdenEntregaDTO> detalles)> ObtenerOrdenesConDetallesAsync(int idOrdenes);

        //paginado
        PagedResult<OrdenesEntregaDTO> GetOrdenesEntregaPaginadas(
       int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null
        );
    }
}
