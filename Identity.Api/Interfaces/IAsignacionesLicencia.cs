using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IAsignacionesLicencia
    {
        //traer todos las licencias
        IEnumerable<LicenciaDTO> GetLicencias { get; }
        //trae nombre y apellidos por cedula
        UsuarioDTO? ObtenerUsuarioPorCedula(string cedula);

        //trae nombre de departamento 
        List<UsuarioDetalleDTO> ObtenerDepartamentosPorCedula(string cedula);

        //trae todos los servidores
        IEnumerable<ServidoreDTO> GetServidoresActivos();

        AsignacionesLicencia GetAsignacionesLicenciaById(int IdAsignacionesLicencia);
        void InsertAsignacionesLicencia(AsignacionesLicencia New);
        void UpdateAsignacionesLicencia(AsignacionesLicencia UpdItem);

        void DeleteAsignacionesLicenciaById(int IdAsignacionesLicencia);

        PagedResult<AsignacionesLicenciaDTO> GetAsignacioneslicenciaPaginados(
       int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null
        );
    }
}
