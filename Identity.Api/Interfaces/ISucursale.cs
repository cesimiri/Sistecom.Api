using Identity.Api.DTO;
using Identity.Api.Paginado;

namespace Identity.Api.Interfaces
{
    public interface ISucursale
    {
        IEnumerable<SucursaleDTO> GetAllSucursale();
        SucursaleDTO? GetSucursaleById(int idSucursal);
        void InsertSucursale(SucursaleDTO dto);
        void UpdateSucursale(SucursaleDTO dto);
        //void DeleteSuscripcion(SuscripcionDto dto);
        void DeleteSucursaleById(int idSucursal);


        // Nuevo método para paginado:
        PagedResult<SucursaleDTO> GetSucursalePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

        //exportar
        List<SucursaleDTO> ObtenerSucursalesFiltradas(string? filtro, string? estado);
    }
}
