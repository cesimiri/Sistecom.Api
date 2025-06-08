using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Interfaces
{
    public interface ISuscripcione
    {
        IEnumerable<Suscripcione> SuscripcionesAll { get; }
        Suscripcione GetSuscripcionById(int idSuscripcion);
        void InsertSuscripcion(SuscripcionDto dto);
        void UpdateSuscripcion(Suscripcione updatedSuscripcion);
        void DeleteSuscripcion(Suscripcione suscripcionToDelete);
        void DeleteSuscripcionById(int idSuscripcion);
    }
}
