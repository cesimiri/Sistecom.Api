using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.DataRepository
{
    public class UsuarioDataRepository
    {

        // Obtener todos los usuarios
        //public List<UsuarioDTO> GetAllUsuarios()
        //{
            //using var context = new InvensisContext();
            //return context.Usuarios
            //    .Include(s => s.IdEmpresaNavigation)
            //    .Select(s => new UsuarioDTO
            //    {
            //        IdUsuario = s.IdUsuario,
            //        IdEmpresa = s.IdEmpresa,
            //        Cedula = s.Cedula,
            //        Nombres = s.Nombres,
            //        Apellidos = s.Apellidos,
            //        Cargo = s.Cargo,
            //        Departamento = s.Departamento,
            //        Email = s.Email,
            //        Telefono = s.Telefono,
            //        Extension = s.Extension,
            //        PuedeSolicitar = s.PuedeSolicitar,
            //        LimiteSolicitud = s.LimiteSolicitud,
            //        Estado = s.Estado,
                    
            //        // Campos relacionados:
            //        RazonSocialEmpresa = s.IdEmpresaNavigation.RazonSocial,
                    
            //    })
            //    .ToList();

        //}
       

        // Obtener un usuario por su ID
        //public UsuarioDTO GetUsuarioById(int IdUsuario)
        //{
            //using var context = new InvensisContext();

            //return context.Usuarios
            //    .Include(s => s.IdEmpresaNavigation)

            //    .Where(s => s.IdUsuario == IdUsuario)
            //    .Select(s => new UsuarioDTO
            //    {
            //        IdUsuario = s.IdUsuario,
            //        IdEmpresa = s.IdEmpresa,
            //        Cedula = s.Cedula,
            //        Nombres = s.Nombres,
            //        Apellidos = s.Apellidos,
            //        Cargo = s.Cargo,
            //        Departamento = s.Departamento,
            //        Email = s.Email,
            //        Telefono = s.Telefono,
            //        Extension = s.Extension,
            //        PuedeSolicitar = s.PuedeSolicitar,
            //        LimiteSolicitud = s.LimiteSolicitud,
            //        Estado = s.Estado,

            //        // Campos relacionados:
            //        RazonSocialEmpresa = s.IdEmpresaNavigation.RazonSocial,
            //    })
            //    .FirstOrDefault();

        //}

        // Insertar un nuevo usuario
        public void InsertUsuario(UsuarioDTO dto)
        {
            //using var context = new InvensisContext();

            //var empresa = context.EmpresasClientes.Find(dto.IdEmpresa);

            //if (empresa == null )
            //{
            //    throw new Exception("IdEmpresa no existe en la base de datos.");
            //}

            //var nueva = new Usuario
            //{

            //    IdEmpresa = dto.IdEmpresa,
            //    Cedula = dto.Cedula,
            //    Nombres = dto.Nombres,
            //    Apellidos = dto.Apellidos,
            //    Cargo = dto.Cargo,
            //    Departamento = dto.Departamento,
            //    Email = dto.Email,
            //    Telefono = dto.Telefono,
            //    Extension = dto.Extension,
            //    PuedeSolicitar = dto.PuedeSolicitar,
            //    LimiteSolicitud = dto.LimiteSolicitud,
            //    Estado = dto.Estado
               
            //};

            //context.Usuarios.Add(nueva);
            //context.SaveChanges();

        }

        // Actualizar un usuario existente
        public void UpdateUsuario(UsuarioDTO dto)
        {
            using var context = new InvensisContext();

            var usuario = context.Usuarios
                .FirstOrDefault(s => s.IdUsuario == dto.IdUsuario);

            if (usuario != null)
            {

                usuario.IdUsuario = dto.IdUsuario;
                usuario.IdDepartamento = dto.IdDepartamento;
                usuario.IdCargo = dto.IdCargo;
                usuario.Cedula = dto.Cedula;
                usuario.Nombres = dto.Nombres;
                usuario.Apellidos = dto.Apellidos;
                usuario.Email = dto.Email;
                usuario.Telefono = dto.Telefono;
                usuario.Extension = dto.Extension;
                //usuario.PuedeSolicitar = dto.PuedeSolicitar;
                //usuario.LimiteSolicitud = dto.LimiteSolicitud;


                usuario.Estado = dto.Estado;


                // Actualizar navegación (opcional)
                //usuario.IdEmpresaNavigation = context.EmpresasClientes
                //    .FirstOrDefault(e => e.Ruc == dto.RucEmpresa);

                //context.SaveChanges();
            }     
        }

        // Eliminar un usuario por objeto
        public void DeleteUsuario(UsuarioDTO dto)
        {
            using var context = new InvensisContext();

            var usuario = context.Usuarios
                .FirstOrDefault(s => s.IdUsuario == dto.IdUsuario);

            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
            }

        }

        // Eliminar un usuario por ID
        public void DeleteUsuarioById(int IdUsuario)
        {
            using (var context = new InvensisContext())
            {
                var usuario = context.Usuarios
                    .FirstOrDefault(s => s.IdUsuario == IdUsuario);

                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();
                }
            }

        }
    }
}
