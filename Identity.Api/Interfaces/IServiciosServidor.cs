using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IServiciosServidor
    {
        IEnumerable<ServiciosServidor> ServiciosServidorInfoAll { get; }

        //para traer listado de servidores 
        IEnumerable<ServidoreDTO> GetSetvidores { get; }
        ServiciosServidor GetServiciosServidorById(int IdServiciosServidor);
        void InsertServiciosServidor(ServiciosServidorDTO New);
        void UpdateServiciosServidor(ServiciosServidor UpdItem);

        void DeleteServiciosServidorById(int IdServiciosServidor);

        //paginado
        PagedResult<ServiciosServidorDTO> GetServiciosServidorPaginados(
       int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null
        );
    }
}
