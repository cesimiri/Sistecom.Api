using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace identity.api.datarepository
{
    public class UsuarioDataRepository
    {

        //obtener todos los usuarios
        public List<UsuarioDTO> GetAllUsuarios()
        {
            using var context = new InvensisContext();
            return context.Usuarios
                .Include(s => s.IdDepartamentoNavigation)
                .Include(s => s.IdCargoNavigation)

                .Select(s => new UsuarioDTO
                {
                    IdUsuario = s.IdUsuario,
                    IdDepartamento = s.IdDepartamento,
                    IdCargo = s.IdCargo,
                    Cedula = s.Cedula,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Email = s.Email,
                    Telefono = s.Telefono,
                    Extension = s.Extension,

                    Estado = s.Estado,

                    // campos relacionados:
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento,
                    NombreCargo = s.IdCargoNavigation.NombreCargo

                })
                .ToList();

        }


        //obtener un usuario por su id
        public UsuarioDTO GetUsuarioById(int idUsuario)
        {
            using var context = new InvensisContext();

            return context.Usuarios
                .Include(s => s.IdDepartamentoNavigation)
                .Include(s => s.IdCargoNavigation)

                .Where(s => s.IdUsuario == idUsuario)
                .Select(s => new UsuarioDTO
                {
                    IdUsuario = s.IdUsuario,
                    IdDepartamento = s.IdDepartamento,
                    IdCargo = s.IdCargo,
                    Cedula = s.Cedula,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Email = s.Email,
                    Telefono = s.Telefono,
                    Extension = s.Extension,

                    Estado = s.Estado,

                    // campos relacionados:
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento,
                    NombreCargo = s.IdCargoNavigation.NombreCargo
                })
                .FirstOrDefault();

        }

        //insertar un nuevo usuario
        public void InsertUsuario(UsuarioDTO dto)
        {
            try {
                using var context = new InvensisContext();

                var departamento = context.Departamentos.Find(dto.IdDepartamento);
                var cargo = context.Cargos.Find(dto.IdCargo);

                if (departamento == null || cargo == null)
                {
                    throw new Exception("departamento o cargo  no existe en la base de datos.");
                }

                var nueva = new Usuario
                {

                    IdDepartamento = dto.IdDepartamento,
                    IdCargo = dto.IdCargo,
                    Cedula = dto.Cedula,
                    Nombres = dto.Nombres,
                    Apellidos = dto.Apellidos,
                    Email = dto.Email,
                    Telefono = dto.Telefono,
                    Extension = dto.Extension,
                    Estado = dto.Estado,

                };

                context.Usuarios.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar al usuario: " + ex.InnerException?.Message ?? ex.Message);
            }


        }

        //actualizar un usuario existente
        public void UpdateUsuario(UsuarioDTO dto)
        {
            using var context = new InvensisContext();

            var usuario = context.Usuarios
                .FirstOrDefault(s => s.IdUsuario == dto.IdUsuario);

            if (usuario != null)
            {

                usuario.IdDepartamento = dto.IdDepartamento;
                usuario.IdCargo = dto.IdCargo;
                usuario.Cedula = dto.Cedula;
                usuario.Nombres = dto.Nombres;
                usuario.Apellidos = dto.Apellidos;
                usuario.Email = dto.Email;
                usuario.Telefono = dto.Telefono;
                usuario.Extension = dto.Extension;
                usuario.Estado = dto.Estado;


                //actualizar navegación(opcional)
                usuario.IdDepartamentoNavigation = context.Departamentos
                    .FirstOrDefault(e => e.IdDepartamento == dto.IdDepartamento);
                usuario.IdCargoNavigation = context.Cargos
                    .FirstOrDefault(e => e.IdCargo == dto.IdCargo);

                context.SaveChanges();
            }
        }

        ////eliminar un usuario por objeto se debe cambiar 
        //public void deleteusuario(usuariodto dto)
        //{
        //    using var context = new invensiscontext();

        //    var usuario = context.usuarios
        //        .firstordefault(s => s.idusuario == dto.idusuario);

        //    if (usuario != null)
        //    {
        //        context.usuarios.remove(usuario);
        //        context.savechanges();
        //    }

        //}

        //eliminar un usuario por id
        public void DeleteUsuarioById(int idUsuario)
        {
            using (var context = new InvensisContext())
            {
                var usuario = context.Usuarios
                    .FirstOrDefault(s => s.IdUsuario == idUsuario);

                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();
                }
            }

        }
    }
}
