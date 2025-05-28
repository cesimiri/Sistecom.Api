using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> GetAllUsuarios { get; }
        Usuario GetById(int id);
        void InsertUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(Usuario usuario);
        void DeleteUsuarioById(int id);
    }
}
