using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IAsignacionesLicencia
    {
        IEnumerable<AsignacionesLicencia> AsignacionesLicenciaInfoAll { get; }
        AsignacionesLicencia GetAsignacionesLicenciaById(int IdAsignacionesLicencia);
        void InsertAsignacionesLicencia(AsignacionesLicencia New);
        void UpdateAsignacionesLicencia(AsignacionesLicencia UpdItem);
        void DeleteAsignacionesLicencia(AsignacionesLicencia DelItem);
        void DeleteAsignacionesLicenciaById(int IdAsignacionesLicencia);
    }
}
