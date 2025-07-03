using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface ITiposLicencium
    {
        IEnumerable<TiposLicencium> TiposLicenciumInfoAll { get; }
        TiposLicencium GetTiposLicenciumById(int IdTiposLicencium);
        void InsertTiposLicencium(TiposLicencium New);
        void UpdateTiposLicencium(TiposLicencium UpdItem);
        void DeleteTiposLicencium(TiposLicencium DelItem);
        void DeleteTiposLicenciumById(int IdTiposLicencium);

        // Nuevo método para paginado:
        PagedResult<TiposLicencium> GetTiposLicenciumPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
