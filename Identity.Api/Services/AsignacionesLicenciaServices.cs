using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class AsignacionesLicenciaServices : IAsignacionesLicencia
    {
        private AsignacionesLicenciaRepository _dataRepository = new AsignacionesLicenciaRepository();

        public IEnumerable<LicenciaDTO> GetLicencias
        {
            get { return _dataRepository.GetLicencias(); }
        }

        //trae usauario por mnombre y apellidos 
        public UsuarioDTO? ObtenerUsuarioPorCedula(string cedula) =>
            _dataRepository.ObtenerUsuarioPorCedula(cedula);

        //trae nombre de departamento
        public List<UsuarioDetalleDTO> ObtenerDepartamentosPorCedula(string cedula) =>
            _dataRepository.ObtenerDepartamentosPorCedula(cedula);

        //trae los servidores
        public IEnumerable<ServidoreDTO> GetServidoresActivos()
        {
            return _dataRepository.GetServidoresActivos();
        }
        public AsignacionesLicencia GetAsignacionesLicenciaById(int idAsignacionesLicencia)
        {
            return _dataRepository.GetAsignacionesLicenciaById(idAsignacionesLicencia);
        }

        public void InsertAsignacionesLicencia(AsignacionesLicencia New)
        {
            _dataRepository.InsertAsignacionesLicencia(New);
        }

        public void UpdateAsignacionesLicencia(AsignacionesLicencia UpdItem)
        {
            _dataRepository.UpdateAsignacionesLicencia(UpdItem);
        }

        public void DeleteAsignacionesLicenciaById(int idAsignacionesLicencia)
        {
            _dataRepository.DeleteAsignacionesLicenciaById(idAsignacionesLicencia);
        }

        //paginado
        public PagedResult<AsignacionesLicenciaDTO> GetAsignacioneslicenciaPaginados(
        int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null)
        {
            return _dataRepository.GetAsignacionesLicenciaPaginados(
                pagina, pageSize,
                filtro, estadoActivo
            );
        }
    }
}
