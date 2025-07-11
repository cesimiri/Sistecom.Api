using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class MovimientosInventarioServices : IMovimientosInventario
    {
        private MovimientosInventarioRepository _dataRepository = new MovimientosInventarioRepository();


        //---/---/-/--/-/-/-/-/-/
        public bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error)
        {
            return _dataRepository.RegistrarMovimientos(movimientos, out error);
        }



        public MovimientosInventario? ObtenerPorId(int id)
        {
            return _dataRepository.ObtenerPorId(id);
        }

        //paginado
        public PagedResult<MovimientosInventarioDTO> GetPaginados(
            int pagina,
            int pageSize,
            string? tipoMovimiento,
            int? idBodega,
            string? nombreProducto,
            DateTime? desde,
            DateTime? hasta)
        {
            return _dataRepository.GetPaginados(pagina, pageSize, tipoMovimiento, idBodega, nombreProducto, desde, hasta);
        }

        //obtener las solicitudes de compras que aun no esten ingresadas
        public Task<List<SolicitudesCompraDTO>> ObtenerSolicitudesNoUsadasAsync()
        {
            return _dataRepository.ObtenerSolicitudesNoUsadasAsync();
        }

        //trae todos los detalles de la idSolcitud que se le pase para ser mostrada en el lista
        public Task<List<DetalleSolicitudDTO>> ObtenerDetalleSolicitudAsync(int idSolicitud)
        {
            return _dataRepository.ObtenerDetalleSolicitudAsync(idSolicitud);
        }

        //obtiene las facturas que no esten aun ingresadas
        public Task<List<FacturasCompraDTO>> ObtenerFacturasNoUsadasAsync()
        {
            return _dataRepository.ObtenerFacturasNoUsadasAsync();
        }

        //trae todos los detalles de la idFctura que se le pase para ser mostrada en el lista
        public Task<List<DetalleFacturaCompraDTO>> ObtenerDetalleFacturaAsync(int idFactura)
        {
            return _dataRepository.ObtenerDetalleFacturaAsync(idFactura);
        }

    }
}
