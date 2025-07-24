using Identity.Api.DTO;
using Identity.Api.Paginado;
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

                .Where(u => u.Estado == "ACTIVO")
                .Select(s => new UsuarioDTO
                {
                    Cedula = s.Cedula,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Extension = s.Extension,
                    Estado = s.Estado,

                    // campos relacionados:


                })
                .ToList();

        }

        //obtener las sucursales despues de seleccionar la empresa pendiente
        //public List<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        //{
        //    using var context = new InvensisContext();

        //    return context.Sucursales
        //        .Where(m => m.RucEmpresa == RucEmpresa)
        //        .OrderBy(s => s.NombreSucursal)
        //.Select(m => new SucursaleDTO
        //{
        //    IdSucursal = m.IdSucursal,
        //    RucEmpresa = m.RucEmpresa,
        //    CodigoSucursal = m.CodigoSucursal!,
        //    NombreSucursal = m.NombreSucursal,
        //    Direccion = m.Direccion,
        //    Ciudad = m.Ciudad,
        //    Telefono = m.Telefono,
        //    Email = m.Email,
        //    //FechaDescontinuacion = m.FechaDescontinuacion.HasValue
        //    //    ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
        //    //: null,
        //    Responsable = m.Responsable,
        //    TelefonoResponsable = m.TelefonoResponsable,
        //    EsMatriz = m.EsMatriz,
        //    Estado = m.Estado
        //})
        //.ToList();
        //}

        //obtener los departamentos despues de seleccionar la sucursal  pendiente
        //public List<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        //{
        //    using var context = new InvensisContext();

        //    return context.Departamentos
        //        .Where(m => m.IdSucursal == idSucursal)
        //        .OrderBy(s => s.NombreDepartamento)
        //.Select(m => new DepartamentoDTO
        //{
        //    IdDepartamento = m.IdDepartamento,
        //    IdSucursal = m.IdSucursal,
        //    CodigoDepartamento = m.CodigoDepartamento,
        //    NombreDepartamento = m.NombreDepartamento!,
        //    Descripcion = m.Descripcion,
        //    Responsable = m.Responsable,
        //    EmailDepartamento = m.EmailDepartamento,
        //    Extension = m.Extension,
        //    CentroCosto = m.CentroCosto,
        //    //FechaDescontinuacion = m.FechaDescontinuacion.HasValue
        //    //    ? m.FechaDescontinuacion.Value.ToDateTime(TimeOnly.MinValue)
        //    //: null,
        //    Estado = m.Estado
        //})
        //.ToList();
        //}


        //obtener un usuario por su id
        public UsuarioDTO GetUsuarioById(string cedula)
        {
            using var context = new InvensisContext();

            return context.Usuarios
                //.Include(s => s.IdDepartamentoNavigation)
                //.Include(s => s.IdCargoNavigation)

                .Where(s => s.Cedula == cedula)
                .Select(s => new UsuarioDTO
                {
                    Cedula = s.Cedula,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Extension = s.Extension,
                    Estado = s.Estado

                    // campos relacionados:
                    //NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento,
                    //NombreCargo = s.IdCargoNavigation.NombreCargo
                })
                .FirstOrDefault();

        }

        //insertar un nuevo usuario
        public void InsertUsuario(UsuarioDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                var nueva = new Usuario
                {

                    Cedula = dto.Cedula,
                    Nombres = dto.Nombres?.ToUpper(),
                    Apellidos = dto.Apellidos?.ToUpper(),
                    Telefono = dto.Telefono,
                    Email = dto.Email?.ToLower(),
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
                .FirstOrDefault(s => s.Cedula == dto.Cedula);

            if (usuario != null)
            {

                usuario.Cedula = dto.Cedula;
                usuario.Nombres = dto.Nombres?.ToUpper();
                usuario.Apellidos = dto.Apellidos?.ToUpper();
                usuario.Telefono = dto.Telefono;
                usuario.Email = dto.Email?.Trim().ToLower();
                usuario.Extension = dto.Extension;
                usuario.Estado = dto.Estado;

                context.SaveChanges();
            }
        }



        //eliminar un usuario por id
        public void DeleteUsuarioById(string cedula)
        {
            using (var context = new InvensisContext())
            {
                var usuario = context.Usuarios
                    .FirstOrDefault(s => s.Cedula == cedula);

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

                .AsQueryable();

            // Aplicar filtro por texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Cedula.ToLower().Contains(filtro) ||
                    u.Nombres.ToLower().Contains(filtro) ||
                    u.Apellidos.ToLower().Contains(filtro) ||
                    u.Email.ToLower().Contains(filtro)

                    );
            }

            // Filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            var totalItems = query.Count();

            var usuarios = query
                .OrderBy(u => u.Apellidos)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new UsuarioDTO
                {
                    Cedula = s.Cedula,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Extension = s.Extension,
                    Estado = s.Estado
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
