using Identity.Api.DTO;
using Identity.Api.Paginado;

namespace Identity.Api.Interfaces
{
    public interface IUsuarioDetalle
    {
        IEnumerable<UsuarioDetalleDTO> GetAllUsuarioDetalle { get; }
        UsuarioDetalleDTO GetUsuarioDetalleById(string cedula);
        void InsertUsuarioDetalle(UsuarioDetalleDTO dto);
        void UpdateUsuarioDetalle(UsuarioDetalleDTO dto);

        void DeleteUsuarioDetalleById(string cedula);

        //IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa);

        //IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal);

        // Nuevo método para paginado:
        PagedResult<UsuarioDetalleDTO> GetUsuarioDetallePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
