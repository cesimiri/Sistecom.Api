using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface ISuscripcione
    {
        IEnumerable<SuscripcionDto> GetAllSuscripciones();
        SuscripcionDto? GetSuscripcionById(int idSuscripcion);
        void InsertSuscripcion(SuscripcionDto dto);
        void UpdateSuscripcion(SuscripcionDto dto);
        void DeleteSuscripcion(SuscripcionDto dto);
        void DeleteSuscripcionById(int idSuscripcion);

        // Nuevo método para paginado:
        PagedResult<SuscripcionDto> GetSuscripcionPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
