using Identity.Api.DTO;
using Identity.Api.Paginado;
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

        //obtener las sucursales despues de seleccionar la empresa
        public List<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        {
            using var context = new InvensisContext();

            return context.Sucursales
                .Where(m => m.RucEmpresa == RucEmpresa)
                .OrderBy(s => s.NombreSucursal)
        .Select(m => new SucursaleDTO
        {
            IdSucursal = m.IdSucursal,
            RucEmpresa = m.RucEmpresa,
            CodigoSucursal = m.CodigoSucursal!,
            NombreSucursal = m.NombreSucursal,
            Direccion = m.Direccion,
            Ciudad = m.Ciudad,
            Telefono = m.Telefono,
            Email = m.Email,
            //FechaDescontinuacion = m.FechaDescontinuacion.HasValue
            //    ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
            //: null,
            Responsable = m.Responsable,
            TelefonoResponsable = m.TelefonoResponsable,
            EsMatriz = m.EsMatriz,
            Estado = m.Estado
        })
        .ToList();
        }

        //obtener los departamentos despues de seleccionar la sucursal
        public List<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        {
            using var context = new InvensisContext();

            return context.Departamentos
                .Where(m => m.IdSucursal == idSucursal)
                .OrderBy(s => s.NombreDepartamento)
        .Select(m => new DepartamentoDTO
        {
            IdDepartamento = m.IdDepartamento,
            IdSucursal = m.IdSucursal,
            CodigoDepartamento = m.CodigoDepartamento,
            NombreDepartamento = m.NombreDepartamento!,
            Descripcion = m.Descripcion,
            Responsable = m.Responsable,
            EmailDepartamento = m.EmailDepartamento,
            Extension = m.Extension,
            CentroCosto = m.CentroCosto,
            //FechaDescontinuacion = m.FechaDescontinuacion.HasValue
            //    ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
            //: null,
            Estado = m.Estado
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
            try
            {
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
                    Nombres = dto.Nombres?.ToUpper(),
                    Apellidos = dto.Apellidos?.ToUpper(),
                    Email = dto.Email?.Trim().ToLower(),
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
                usuario.Nombres = dto.Nombres?.ToUpper();
                usuario.Apellidos = dto.Apellidos?.ToUpper();
                usuario.Email = dto.Email?.Trim().ToLower();
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



        public PagedResult<UsuarioDTO> GetUsuariosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Usuarios
                .Include(u => u.IdDepartamentoNavigation)
                    .ThenInclude(d => d.IdSucursalNavigation)
                        .ThenInclude(s => s.RucEmpresaNavigation)
                .Include(u => u.IdCargoNavigation)
                .AsQueryable();

            // Aplicar filtro por texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Cedula.ToLower().Contains(filtro) ||
                    u.Nombres.ToLower().Contains(filtro) ||
                    u.Apellidos.ToLower().Contains(filtro) ||
                    u.Email.ToLower().Contains(filtro) ||
                    u.IdDepartamentoNavigation.IdSucursalNavigation.RucEmpresaNavigation.RazonSocial.ToLower().Contains(filtro));
            }

            // Filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            var totalItems = query.Count();

            var usuarios = query
                .OrderBy(u => u.IdUsuario)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    IdDepartamento = u.IdDepartamento,
                    IdCargo = u.IdCargo,
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Email = u.Email,
                    Telefono = u.Telefono,
                    Extension = u.Extension,
                    Estado = u.Estado,
                    NombreDepartamento = u.IdDepartamentoNavigation.NombreDepartamento,
                    NombreCargo = u.IdCargoNavigation.NombreCargo,
                    RazonSocial = u.IdDepartamentoNavigation.IdSucursalNavigation.RucEmpresaNavigation.RazonSocial
                })
                .ToList();

            return new PagedResult<UsuarioDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }

    }
}
