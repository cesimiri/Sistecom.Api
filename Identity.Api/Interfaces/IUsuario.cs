using Identity.Api.DTO;
using Identity.Api.Paginado;


namespace Identity.Api.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<UsuarioDTO> GetAllUsuarios { get; }
        UsuarioDTO GetUsuarioById(string cedula);
        void InsertUsuario(UsuarioDTO dto);
        void UpdateUsuario(UsuarioDTO dto);

        void DeleteUsuarioById(string cedula);

        //IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa);

        //IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal);

        // Nuevo método para paginado:
        PagedResult<UsuarioDTO> GetUsuariosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
