using Identity.Api.DTO;
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
    }
}
