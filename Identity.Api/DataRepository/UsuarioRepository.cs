using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.DataRepository
{
    public class UsuarioDataRepository
    {

        // Obtener todos los usuarios
        public List<Usuario> GetAllUsuarios()
        {
            using (var context = new InvensisContext())
            {
                return context.Usuarios.ToList();
            }
            
        }
       

        // Obtener un usuario por su ID
        public Usuario GetById(int id)
        {
            using (var context = new InvensisContext())
            {
                return context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            }
            
        }

        // Insertar un nuevo usuario
        public void InsertUsuario(Usuario usuario)
        {
            using (var context = new InvensisContext())
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
                
        }

        // Actualizar un usuario existente
        public void UpdateUsuario(Usuario usuario)
        {
            using (var context = new InvensisContext())
            {
                var existingUsuario = context.Usuarios
                                          .Where(u => u.IdUsuario == usuario.IdUsuario)
                                          .FirstOrDefault();

                if (existingUsuario != null)
                {
                    existingUsuario.Cedula = usuario.Cedula;
                    existingUsuario.Nombres = usuario.Nombres;
                    existingUsuario.Apellidos = usuario.Apellidos;
                    existingUsuario.Cargo = usuario.Cargo;
                    existingUsuario.Departamento = usuario.Departamento;
                    existingUsuario.Email = usuario.Email;
                    existingUsuario.Telefono = usuario.Telefono;
                    existingUsuario.Extension = usuario.Extension;
                    existingUsuario.PuedeSolicitar = usuario.PuedeSolicitar;
                    existingUsuario.LimiteSolicitud = usuario.LimiteSolicitud;
                    existingUsuario.Estado = usuario.Estado;
                    existingUsuario.FechaRegistro = usuario.FechaRegistro;

                    context.SaveChanges();
                }
            }
                
        }

        // Eliminar un usuario por objeto
        public void DeleteUsuario(Usuario usuario)
        {
            using (var context = new InvensisContext())
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }
                
        }

        // Eliminar un usuario por ID
        public void DeleteUsuarioById(int id)
        {
            using (var context = new InvensisContext())
            {
                var usuario = context.Usuarios
                                       .FirstOrDefault(u => u.IdUsuario == id);

                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();
                }
            }
                
        }
    }
}
