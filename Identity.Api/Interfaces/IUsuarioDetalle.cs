using Identity.Api.DTO;
using Identity.Api.Paginado;

namespace Identity.Api.Interfaces
{
    public interface IUsuarioDetalle
    {
        IEnumerable<UsuarioDetalleDTO> GetAllUsuarioDetalle { get; }

        //trae el listado
        List<UsuarioDetalleDTO> GetUsuarioDetalleById(string cedula);

        void InsertUsuarioDetalle(UsuarioDetalleDTO dto);
        void UpdateUsuarioDetalle(UsuarioDetalleDTO dto);

        void DeleteUsuarioDetalleById(string cedula);

        //eliminar por cedula idDepartamento idCargo
        void DeleteUsuarioDetalle(string cedula, int idDepartamento, int idCargo);


        // Nuevo método para paginado:
        PagedResult<UsuarioDetalleDTO> GetUsuarioDetallePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

        //exportar excel
        List<UsuarioDetalleDTO> ObtenerUsuarioDetalleFiltradas(string? filtro, string? estado);
    }
}
