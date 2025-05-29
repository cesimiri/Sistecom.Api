using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IServiciosServidor
    {
        IEnumerable<ServiciosServidor> ServiciosServidorInfoAll { get; }
        ServiciosServidor GetServiciosServidorById(int IdServiciosServidor);
        void InsertServiciosServidor(ServiciosServidor New);
        void UpdateServiciosServidor(ServiciosServidor UpdItem);
        void DeleteServiciosServidor(ServiciosServidor DelItem);
        void DeleteServiciosServidorById(int IdServiciosServidor);
    }
}
