using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IMovimientosInventario
    {
        // Nuevo método para paginado:
        //PagedResult<MovimientosInventarioDTO> GetMovimientoInventarioPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
        //IEnumerable<MovimientosInventario> MovimientosInventarioInfoAll { get; }
        //MovimientosInventario GetMovimientosInventarioById(int IdMovimientosInventario);

        ////ingreso masivo de items 
        ////void InsertarMovimientoInventarioMasivo(List<MovimientosInventarioDTO> lista);
        //void InsertMovimientosInventario(MovimientosInventario New);
        //void UpdateMovimientosInventario(MovimientosInventario UpdItem);
        //void DeleteMovimientosInventario(MovimientosInventario DelItem);
        //void DeleteMovimientosInventarioById(int IdMovimientosInventario);


        // Registrar uno o varios movimientos de inventario a la vez (entradas, salidas, transferencias, ajustes). 
        bool RegistrarMovimientos(List<MovimientosInventarioDTO> movimientos, out string error);



        MovimientosInventario? ObtenerPorId(int id);

        //paginado con busqueda
        PagedResult<MovimientosInventarioDTO> GetPaginados(
            int pagina,
            int pageSize,
            string? tipoMovimiento,
            int? idBodega,
            string? nombreProducto,
            DateTime? desde,
            DateTime? hasta);

        ///en n futuri hacer un tipo resuemn 
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
