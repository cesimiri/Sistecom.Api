using Identity.Api.DTO;
using Identity.Api.Paginado;

namespace Identity.Api.Interfaces
{
    public interface IMovimientosInventario
    {


        // Registrar uno o varios movimientos de inventario a la vez (entradas, salidas, transferencias, ajustes). 
        bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error);




        //paginado con busqueda
        PagedResult<MovimientosInventarioDTO> GetPaginados(
            int pagina,
            int pageSize,
            string? tipoMovimiento,
            int? idBodega,
            string? nombreProducto,
            DateTime? desde,
            DateTime? hasta,
            string? ordenColumna = null,
            bool ordenAscendente = true,
            int? idProducto = null); // <- NUEVO





        //obtener las solicitudes de compras que aun no esten ingresadas
        Task<List<SolicitudesCompraDTO>> ObtenerSolicitudesNoUsadasAsync();

        //traer detalles de la solicitud de compra
        Task<List<DetalleSolicitudDTO>> ObtenerDetalleSolicitudAsync(int idFactura);


        //para traer las facturas que no esten en MovimientoInventario
        Task<List<FacturasCompraDTO>> ObtenerFacturasNoUsadasAsync();

        //obtiene todo los detalles del idFactura que se le pase 
        Task<List<DetalleFacturaCompraDTO>> ObtenerDetalleFacturaAsync(int idFactura);


        ///en n futuro hacer un tipo resuemen
        /*
         * 
         * "Quiero que me ayudes a crear un método en el backend que me devuelva un resumen estadístico de los movimientos de 
         * inventario, agrupados por tipo de movimiento (ENTRADA, SALIDA, TRANSFERENCIA, AJUSTE), 
         * con filtros opcionales por bodega, rango de fechas y nombre del producto. 
         * Quiero que cada resultado incluya el tipo de movimiento, total de cantidad movida y total monetario 
         * (si hay precio unitario). También quiero que me sugieras el DTO adecuado y que implementes la lógica usando GroupBy
         * en el repository, interface, service y controller."
         * 
         * */
    }
}
