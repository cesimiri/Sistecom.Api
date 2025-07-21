using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface ILicencia
    {
        IEnumerable<LicenciaDTO> LicenciaInfoAll { get; }
        Licencia GetLicenciaById(int IdLicencia);
        void InsertLicencia(LicenciaDTO New);
        void UpdateLicencia(LicenciaDTO UpdItem);
        //void DeleteLicencia(Licencia DelItem);
        void DeleteLicenciaById(int IdLicencia);

        IEnumerable<FacturasCompraDTO> GetFacturasConCategoria6();

        IEnumerable<ProductoDTO> GetProductoConCategoria6();

        // Nuevo método para paginado:
        PagedResult<LicenciaDTO> GetLicenciaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

        //exportar
        List<LicenciaDTO> ObtenerLicenciaFiltradas(string? filtro, string? estado);
    }
}
