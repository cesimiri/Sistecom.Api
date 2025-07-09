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

        //public IEnumerable<MovimientosInventario> MovimientosInventarioInfoAll
        //{
        //    get { return _dataRepository.MovimientosInventarioInfoAll(); }
        //}

        //public MovimientosInventario GetMovimientosInventarioById(int idMovimientosInventario)
        //{
        //    return _dataRepository.GetMovimientosInventarioById(idMovimientosInventario);
        //}

        //public void InsertMovimientosInventario(MovimientosInventario New)
        //{
        //    _dataRepository.InsertMovimientosInventario(New);
        //}

        ////ingreso masivo
        ////public void InsertarMovimientoInventarioMasivo(List<MovimientosInventarioDTO> lista)
        ////{
        ////    _dataRepository.InsertarMovimientoInventarioMasivo(lista);
        ////}

        //public void UpdateMovimientosInventario(MovimientosInventario UpdItem)
        //{
        //    _dataRepository.UpdateMovimientosInventario(UpdItem);
        //}

        //public void DeleteMovimientosInventario(MovimientosInventario DelItem)
        //{
        //    _dataRepository.DeleteMovimientosInventario(DelItem);
        //}

        //public void DeleteMovimientosInventarioById(int idMovimientosInventario)
        //{
        //    _dataRepository.DeleteMovimientosInventarioById(idMovimientosInventario);
        //}

        //paginado
        //public PagedResult<MovimientosInventarioDTO> GetMovimientoInventarioPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        //{
        //    return _dataRepository.GetMovimientoInventarioPaginados(pagina, pageSize, filtro, estado);
        //}

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


    }
}
