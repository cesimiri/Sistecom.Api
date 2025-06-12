using identity.api.datarepository;
using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{
    public class UsuarioService : IUsuario
    {
        private UsuarioDataRepository _dataRepository = new UsuarioDataRepository();

        public IEnumerable<UsuarioDTO> GetAllUsuarios
        {
            get { return _dataRepository.GetAllUsuarios(); }
        }

        public UsuarioDTO GetUsuarioById(int idUsuario)
        {
            return _dataRepository.GetUsuarioById(idUsuario);
        }

        public void InsertUsuario(UsuarioDTO usuario)
        {
            _dataRepository.InsertUsuario(usuario);
        }

        public void UpdateUsuario(UsuarioDTO usuario)
        {
            _dataRepository.UpdateUsuario(usuario);
        }

        //public void DeleteUsuario(UsuarioDTO usuario)
        //{
        //    _dataRepository.DeleteUsuario(usuario);
        //}

        public void DeleteUsuarioById(int IdUsuario)
        {
            _dataRepository.DeleteUsuarioById(IdUsuario);
        }
    }
}
