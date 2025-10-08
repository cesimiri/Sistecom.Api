using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;

namespace Identity.Api.Services
{
    public class ConfiguracionesActivoServices : IConfiguracionesActivo
    {
        private readonly ConfiguracionesActivoRepository _repository = new ConfiguracionesActivoRepository();

        //listado de activos principal
        public IEnumerable<ActivoDTO> GetActivosPrincipal()
        {
            return _repository.GetActivosPrincipal();
        }
        //listado de actvis componentes
        public IEnumerable<ActivoDTO> GetActivosComponenetes()
        {
            return _repository.GetActivosComponenetes();
        }

        public void InsertarConfiguracion(ConfiguracionActivoInsertDTO dto)
        {
            _repository.InsertarConfiguracion(dto);
        }

        //trae todo lo agrega por ese id

        public IEnumerable<ConfiguracionesActivoDTO> GetConfiguracionesByActivo(int idActivoPrincipal)
        {
            return _repository.GetConfiguracionesByActivo(idActivoPrincipal);
        }

        //borrar
        public void DeleteConfiguracionesActivoById(int idActivoPrincipal)
        {
            _repository.DeleteConfiguracionesActivoById(idActivoPrincipal);
        }

        //paginado
        public PagedResult<ConfiguracionesActivoDTO> GetConfiguracionesActivoPaginados(
            int pagina,
            int pageSize,
            string? filtro = null,
            string? estado = null)
        {
            return _repository.GetConfiguracionesActivoPaginados(pagina, pageSize, filtro, estado);
        }

    }
}
