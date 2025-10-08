using Identity.Api.DTO;
using Identity.Api.Paginado;
namespace Identity.Api.Interfaces
{
    public interface IConfiguracionesActivo
    {
        //listado de activos principal
        IEnumerable<ActivoDTO> GetActivosPrincipal();
        //listaod de componenetes
        IEnumerable<ActivoDTO> GetActivosComponenetes();


        //insertar listado de activo principal y su listadoo
        void InsertarConfiguracion(ConfiguracionActivoInsertDTO dto);

        //trae todo lo agrega por ese id
        IEnumerable<ConfiguracionesActivoDTO> GetConfiguracionesByActivo(int idActivoPrincipal);

        //borar por id
        void DeleteConfiguracionesActivoById(int idActivoPrincipal);

        //// Nuevo método para paginado:
        PagedResult<ConfiguracionesActivoDTO> GetConfiguracionesActivoPaginados(
        int pagina,
        int pageSize,
        string? filtro = null,
        string? estado = null);
    }
}
