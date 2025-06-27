using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface ILicencia
    {
        IEnumerable<LicenciaDTO> LicenciaInfoAll { get; }
        Licencia GetLicenciaById(int IdLicencia);
        void InsertLicencia(LicenciaDTO New);
        void UpdateLicencia(Licencia UpdItem);
        //void DeleteLicencia(Licencia DelItem);
        void DeleteLicenciaById(int IdLicencia);
    }
}
