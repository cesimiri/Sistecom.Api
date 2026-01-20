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

        //obtener los departamentos despues de seleccionar la sucursal  pendiente
        public List<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        {
            using var context = new InvensisContext();

            return context.Departamentos
                .Where(m => m.IdSucursal == idSucursal && m.Estado == "ACTIVO")
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

            // Incluir las relaciones necesarias para acceder a NombreSucursal
            var query = context.Usuarios
                .Include(u => u.UsuarioDetalles)
                    .ThenInclude(ud => ud.IdDepartamentoNavigation)
                        .ThenInclude(d => d.IdSucursalNavigation)
                .AsQueryable();

            // Aplicar filtro de texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Cedula.ToLower().Contains(filtro) ||
                    u.Nombres.ToLower().Contains(filtro) ||
                    u.Apellidos.ToLower().Contains(filtro) ||
                    u.Email.ToLower().Contains(filtro) ||
                    u.UsuarioDetalles.Any(ud =>
                        ud.IdDepartamentoNavigation.IdSucursalNavigation.NombreSucursal.ToLower().Contains(filtro))
                );
            }

            // Filtro por estado
            if (string.IsNullOrWhiteSpace(estado))
            {
                estado = "ACTIVO";
            }

            query = query.Where(u => u.Estado == estado);

            var totalItems = query.Count();

            var usuarios = query
                .OrderBy(u => u.Apellidos)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UsuarioDTO
                {
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Telefono = u.Telefono,
                    Email = u.Email,
                    Extension = u.Extension,
                    Estado = u.Estado,

                    // Tomamos el primer NombreSucursal disponible (si hay)
                    NombreSucursal = u.UsuarioDetalles
                        .Where(ud => ud.Estado == estado)
                        .Select(ud => ud.IdDepartamentoNavigation.IdSucursalNavigation.NombreSucursal)
                        .FirstOrDefault()
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


        //paginado usuario sin empresa asignada
        public PagedResult<UsuarioDTO> GetUsuariosSinEmpresaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            // 1. Base query: usuarios que NO estén en UsuarioDetalle
            var query = context.Usuarios
                .Where(u => !context.UsuarioDetalles.Any(ud => ud.Cedula == u.Cedula))
                .AsQueryable();

            // 2. Filtro por texto (cedula, nombres, apellidos, email)
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

            // 3. Si estado no viene, forzar "ACTIVO"
            if (string.IsNullOrWhiteSpace(estado))
            {
                estado = "ACTIVO";
            }

            // 3.1 Filtro por estado (siempre aplica, ya está garantizado que tiene valor)

            query = query.Where(u => u.Estado == estado);


            // 4. Total antes de paginar
            var totalItems = query.Count();

            // 5. Aplicar orden, paginación y proyección al DTO
            var usuarios = query
                .OrderBy(u => u.Apellidos)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UsuarioDTO
                {
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Telefono = u.Telefono,
                    Email = u.Email,
                    Extension = u.Extension,
                    Estado = u.Estado
                })
                .ToList();

            // 6. Retornar resultado paginado
            return new PagedResult<UsuarioDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }

        //exportar PDF 
        public List<UsuarioDTO> ObtenerUsuarioFiltradas(string? filtro, string? estado)
        {
            using var context = new InvensisContext();

            var query = context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var lowerFiltro = filtro.ToLower();
                query = query.Where(u =>
                    u.Cedula.ToLower().Contains(filtro) ||
                    u.Nombres.ToLower().Contains(filtro) ||
                    u.Apellidos.ToLower().Contains(filtro) ||
                    u.Email.ToLower().Contains(filtro)

                    );
            }

            // Si estado no viene, forzar "ACTIVO"
            if (string.IsNullOrWhiteSpace(estado))
            {
                estado = "ACTIVO";
            }

            // Filtro por estado (siempre aplica, ya está garantizado que tiene valor)

            query = query.Where(e => e.Estado == estado);


            return query
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
        }

        //exportar PDF usuario sin empresa
        public List<UsuarioDTO> ObtenerUsuarioSinEmpresaFiltradas(string? filtro, string? estado)
        {
            using var context = new InvensisContext();

            // 1. Base query: usuarios que NO estén en UsuarioDetalle
            var query = context.Usuarios
                .Where(u => !context.UsuarioDetalles.Any(ud => ud.Cedula == u.Cedula))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var lowerFiltro = filtro.ToLower();
                query = query.Where(u =>
                    u.Cedula.ToLower().Contains(filtro) ||
                    u.Nombres.ToLower().Contains(filtro) ||
                    u.Apellidos.ToLower().Contains(filtro) ||
                    u.Email.ToLower().Contains(filtro)
                );
            }

            // Si estado no viene, forzar "ACTIVO"
            if (string.IsNullOrWhiteSpace(estado))
            {
                estado = "ACTIVO";
            }

            // Filtro por estado (siempre aplica, ya está garantizado que tiene valor)
            query = query.Where(e => e.Estado == estado);


            return query
                .Select(u => new UsuarioDTO
                {
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Telefono = u.Telefono,
                    Email = u.Email,
                    Extension = u.Extension,
                    Estado = u.Estado
                })
                .ToList();
        }
    }
}
