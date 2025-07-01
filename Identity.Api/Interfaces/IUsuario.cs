using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Api.Paginado;


namespace Identity.Api.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<UsuarioDTO> GetAllUsuarios { get; }
        UsuarioDTO GetUsuarioById(int idUsuario);
        void InsertUsuario(UsuarioDTO dto);
        void UpdateUsuario(UsuarioDTO dto);
        //void DeleteUsuario(UsuarioDTO dto);
        void DeleteUsuarioById(int idUsuario);

        IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa);

        IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal);

        // Nuevo método para paginado:
        PagedResult<UsuarioDTO> GetUsuariosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
