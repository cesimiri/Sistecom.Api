using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class UsuarioDetalleRepository
    {
        //obtener todos los usuarios
        public List<UsuarioDetalleDTO> GetAllUsuarioDetalle()
        {
            using var context = new InvensisContext();
            return context.UsuarioDetalles

                .Where(u => u.Estado == "ACTIVO")
                .Select(s => new UsuarioDetalleDTO
                {
                    Cedula = s.Cedula,
                    IdDepartamento = s.IdDepartamento,
                    IdCargo = s.IdCargo,
                    PuedeSolicitar = s.PuedeSolicitar,
                    LimiteSolicitud = s.LimiteSolicitud,
                    RequiereAutorizacion = s.RequiereAutorizacion,
                    UsuarioSistema = s.UsuarioSistema,
                    PasswordHash = s.PasswordHash,
                    UltimoAcceso = s.UltimoAcceso,
                    IntentosFallidos = s.IntentosFallidos,
                    Bloqueado = s.Bloqueado,
                    FechaAsignacion = s.FechaAsignacion,
                    FechaBaja = s.FechaBaja,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado

                    // campos relacionados:


                })
                .ToList();

        }



        //obtener un usuario por su id
        public UsuarioDetalleDTO GetUsuarioDetalleById(string cedula)
        {
            using var context = new InvensisContext();

            return context.UsuarioDetalles
                //.Include(s => s.IdDepartamentoNavigation)
                //.Include(s => s.IdCargoNavigation)

                .Where(s => s.Cedula == cedula)
                .Select(s => new UsuarioDetalleDTO
                {
                    Cedula = s.Cedula,
                    IdDepartamento = s.IdDepartamento,
                    IdCargo = s.IdCargo,
                    PuedeSolicitar = s.PuedeSolicitar,
                    LimiteSolicitud = s.LimiteSolicitud,
                    RequiereAutorizacion = s.RequiereAutorizacion,
                    UsuarioSistema = s.UsuarioSistema,
                    PasswordHash = s.PasswordHash,
                    UltimoAcceso = s.UltimoAcceso,
                    IntentosFallidos = s.IntentosFallidos,
                    Bloqueado = s.Bloqueado,
                    FechaAsignacion = s.FechaAsignacion,
                    FechaBaja = s.FechaBaja,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado

                    // campos relacionados:
                    //NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento,
                    //NombreCargo = s.IdCargoNavigation.NombreCargo
                })
                .FirstOrDefault();

        }

        //insertar un nuevo usuario
        public void InsertUsuarioDetalle(UsuarioDetalleDTO dto)
        {
            try
            {
                using var context = new InvensisContext();

                var nueva = new UsuarioDetalle
                {
                    //Nombres = dto.Nombres?.ToUpper(),

                    Cedula = dto.Cedula,
                    IdDepartamento = dto.IdDepartamento,
                    IdCargo = dto.IdCargo,
                    PuedeSolicitar = dto.PuedeSolicitar,
                    LimiteSolicitud = dto.LimiteSolicitud,
                    RequiereAutorizacion = dto.RequiereAutorizacion,
                    UsuarioSistema = dto.UsuarioSistema,
                    PasswordHash = dto.PasswordHash,
                    UltimoAcceso = dto.UltimoAcceso,
                    IntentosFallidos = dto.IntentosFallidos,
                    Bloqueado = dto.Bloqueado,
                    FechaAsignacion = dto.FechaAsignacion,
                    FechaBaja = dto.FechaBaja,
                    Observaciones = dto.Observaciones,
                    Estado = dto.Estado

                };

                context.UsuarioDetalles.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar al usuario Detalle: " + ex.InnerException?.Message ?? ex.Message);
            }


        }

        //actualizar un usuario existente
        public void UpdateUsuarioDetalle(UsuarioDetalleDTO dto)
        {
            using var context = new InvensisContext();

            var usuario = context.UsuarioDetalles
                .FirstOrDefault(s => s.Cedula == dto.Cedula);

            if (usuario != null)
            {

                usuario.Cedula = dto.Cedula;
                usuario.IdDepartamento = dto.IdDepartamento;
                usuario.IdCargo = dto.IdCargo;
                usuario.PuedeSolicitar = dto.PuedeSolicitar;
                usuario.LimiteSolicitud = dto.LimiteSolicitud;
                usuario.RequiereAutorizacion = dto.RequiereAutorizacion;
                usuario.UsuarioSistema = dto.UsuarioSistema;
                usuario.PasswordHash = dto.PasswordHash;
                usuario.UltimoAcceso = dto.UltimoAcceso;
                usuario.IntentosFallidos = dto.IntentosFallidos;
                usuario.Bloqueado = dto.Bloqueado;
                usuario.FechaAsignacion = dto.FechaAsignacion;
                usuario.FechaBaja = dto.FechaBaja;
                usuario.Observaciones = dto.Observaciones;
                usuario.Estado = dto.Estado;

                context.SaveChanges();
            }
        }



        //eliminar un usuario por id
        public void DeleteUsuarioDetalleById(string cedula)
        {
            using (var context = new InvensisContext())
            {
                var usuario = context.UsuarioDetalles
                    .FirstOrDefault(s => s.Cedula == cedula);

                if (usuario != null)
                {
                    context.UsuarioDetalles.Remove(usuario);
                    context.SaveChanges();
                }
            }

        }



        public PagedResult<UsuarioDetalleDTO> GetUsuarioDetallePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.UsuarioDetalles

                .AsQueryable();

            // Aplicar filtro por texto
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Cedula.ToLower().Contains(filtro)
                    //u.Nombres.ToLower().Contains(filtro) ||
                    //u.Apellidos.ToLower().Contains(filtro) ||
                    //u.Email.ToLower().Contains(filtro)

                    );
            }

            // Filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            var totalItems = query.Count();

            var usuarios = query
                .OrderBy(u => u.Cedula)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new UsuarioDetalleDTO
                {
                    Cedula = s.Cedula,
                    IdDepartamento = s.IdDepartamento,
                    IdCargo = s.IdCargo,
                    PuedeSolicitar = s.PuedeSolicitar,
                    LimiteSolicitud = s.LimiteSolicitud,
                    RequiereAutorizacion = s.RequiereAutorizacion,
                    UsuarioSistema = s.UsuarioSistema,
                    PasswordHash = s.PasswordHash,
                    UltimoAcceso = s.UltimoAcceso,
                    IntentosFallidos = s.IntentosFallidos,
                    Bloqueado = s.Bloqueado,
                    FechaAsignacion = s.FechaAsignacion,
                    FechaBaja = s.FechaBaja,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado
                })
                .ToList();

            return new PagedResult<UsuarioDetalleDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
