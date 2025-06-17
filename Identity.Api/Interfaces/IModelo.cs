using Identity.Api.DTO;

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
    }
}
