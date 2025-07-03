using Identity.Api.DTO;
using Identity.Api.Paginado;

namespace Identity.Api.Interfaces
{
    public interface IModelo
    {
        IEnumerable<ModeloDTO> GetAllModelo { get; }
        ModeloDTO GetModeloById(int idModelo);
        void InsertModelo (ModeloDTO dto);
        void UpdateModelo(ModeloDTO dto);
        //void DeleteUsuario(UsuarioDTO dto);
        void DeleteModeloById(int idModelo);

        // Nuevo método para paginado:
        PagedResult<ModeloDTO> GetModeloPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
