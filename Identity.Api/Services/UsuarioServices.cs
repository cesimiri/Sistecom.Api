using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class UsuarioService : IUsuario
    {
        private  UsuarioDataRepository _dataRepository = new UsuarioDataRepository();

        public IEnumerable<Usuario> GetAllUsuarios
        {
            get { return _dataRepository.GetAllUsuarios(); }
        }

        public Usuario GetById(int id)
        {
            return _dataRepository.GetById(id);
        }

        public void InsertUsuario(Usuario usuario)
        {
            _dataRepository.InsertUsuario(usuario);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _dataRepository.UpdateUsuario(usuario);
        }

        public void DeleteUsuario(Usuario usuario)
        {
            _dataRepository.DeleteUsuario(usuario);
        }

        public void DeleteUsuarioById(int id)
        {
            _dataRepository.DeleteUsuarioById(id);
        }
    }
}
