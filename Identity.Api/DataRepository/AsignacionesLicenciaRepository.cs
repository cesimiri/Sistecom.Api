using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class AsignacionesLicenciaRepository
    {
        //trae listado de licencias 
        public List<LicenciaDTO> GetLicencias()
        {
            using var context = new InvensisContext();

            return context.Licencias
                .Include(s => s.IdTipoLicenciaNavigation)
                .Include(s => s.IdProductoNavigation)
                .Include(s => s.IdFacturaCompraNavigation)
                // 🔍 Filtro por estado ACTIVA (ignora mayúsculas/minúsculas)
                .Where(s => s.Estado != null && s.Estado.ToUpper() == "ACTIVA")
                .Select(s => new LicenciaDTO
                {
                    IdLicencia = s.IdLicencia,
                    IdTipoLicencia = s.IdTipoLicencia,
                    IdProducto = s.IdProducto,
                    IdFacturaCompra = s.IdFacturaCompra,
                    NumeroLicencia = s.NumeroLicencia,
                    ClaveProducto = s.ClaveProducto,
                    FechaAdquisicion = s.FechaAdquisicion,
                    FechaInicioVigencia = s.FechaInicioVigencia,
                    FechaFinVigencia = s.FechaFinVigencia,
                    TipoSuscripcion = s.TipoSuscripcion,
                    CantidadUsuarios = s.CantidadUsuarios,
                    CostoLicencia = s.CostoLicencia,
                    RenovacionAutomatica = s.RenovacionAutomatica,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado,

                    // 🧩 Campos relacionados
                    nombreTipoLicencia = s.IdTipoLicenciaNavigation != null
                        ? s.IdTipoLicenciaNavigation.Nombre
                        : null,

                    nombreProducto = s.IdProductoNavigation != null
                        ? s.IdProductoNavigation.Nombre
                        : null,

                    numeroFactura = s.IdFacturaCompraNavigation != null
                        ? s.IdFacturaCompraNavigation.NumeroFactura
                        : null
                })
                .ToList();
        }

        //trae solo los nombnres y apellidos
        public UsuarioDTO? ObtenerUsuarioPorCedula(string cedula)
        {
            using var context = new InvensisContext();
            return context.Usuarios
                .Where(u => u.Cedula == cedula)
                .Select(u => new UsuarioDTO
                {
                    Cedula = u.Cedula,
                    Nombres = u.Nombres,
                    Apellidos = u.Apellidos,
                    Telefono = u.Telefono,
                    Email = u.Email,
                    Extension = u.Extension,
                    ApellidosNombre = u.Apellidos + " " + u.Nombres,
                    Estado = u.Estado
                })
                .FirstOrDefault();
        }

        //trae lista de departamento por cedula
        public List<UsuarioDetalleDTO> ObtenerDepartamentosPorCedula(string cedula)
        {
            using var context = new InvensisContext();
            return context.UsuarioDetalles
                .Include(d => d.IdDepartamentoNavigation)
                .Include(d => d.IdCargoNavigation)
                .Include(d => d.IdDepartamentoNavigation.IdSucursalNavigation)
                .Where(d => d.Cedula == cedula)
                .Select(d => new UsuarioDetalleDTO
                {
                    Cedula = d.Cedula,
                    IdDepartamento = d.IdDepartamento,
                    IdCargo = d.IdCargo,
                    FechaAsignacion = d.FechaAsignacion,
                    FechaBaja = d.FechaBaja,
                    Estado = d.Estado,
                    Observaciones = d.Observaciones,
                    NombreSucursal = d.IdDepartamentoNavigation.IdSucursalNavigation.NombreSucursal,
                    NombreCargo = d.IdCargoNavigation.NombreCargo,
                    NombreDepartamento = d.IdDepartamentoNavigation.NombreDepartamento
                })
                .ToList();
        }

        //trae los servidores por estado 
        public List<ServidoreDTO> GetServidoresActivos()
        {
            using var context = new InvensisContext();

            return context.Servidores
                .Where(s => s.Estado != null && s.Estado.ToUpper() == "ACTIVO")
                .Select(s => new ServidoreDTO
                {
                    IdServidor = s.IdServidor,
                    IdActivo = s.IdActivo,
                    NombreServidor = s.NombreServidor,
                    TipoServidor = s.TipoServidor,
                    SistemaOperativo = s.SistemaOperativo,
                    VersionSo = s.VersionSo,
                    Procesadores = s.Procesadores,
                    NucleosPorProcesador = s.NucleosPorProcesador,
                    MemoriaRamGb = s.MemoriaRamGb,
                    AlmacenamientoTb = s.AlmacenamientoTb,
                    DireccionIp = s.DireccionIp,
                    DireccionMac = s.DireccionMac,
                    Virtualizacion = s.Virtualizacion,
                    HostFisico = s.HostFisico,
                    UbicacionRack = s.UbicacionRack,
                    Proposito = s.Proposito,
                    Estado = s.Estado
                })
                .OrderBy(s => s.NombreServidor)
                .ToList();
        }

        public AsignacionesLicencia GetAsignacionesLicenciaById(int IdAsignacionesLicencia)
        {
            using (var context = new InvensisContext())
            {
                return context.AsignacionesLicencias.FirstOrDefault(a => a.IdAsignacionLicencia == IdAsignacionesLicencia);
            }
        }

        public void InsertAsignacionesLicencia(AsignacionesLicencia newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.AsignacionesLicencias.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateAsignacionesLicencia(AsignacionesLicencia asignacionActualizada)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.AsignacionesLicencias.FirstOrDefault(a => a.IdAsignacionLicencia == asignacionActualizada.IdAsignacionLicencia);
                if (existente != null)
                {
                    existente.IdLicencia = asignacionActualizada.IdLicencia;
                    existente.IdActivo = asignacionActualizada.IdActivo;
                    //existente.IdUsuario = asignacionActualizada.IdUsuario;
                    existente.IdServidor = asignacionActualizada.IdServidor;
                    existente.FechaAsignacion = asignacionActualizada.FechaAsignacion;
                    existente.FechaDesasignacion = asignacionActualizada.FechaDesasignacion;
                    existente.TipoAsignacion = asignacionActualizada.TipoAsignacion;
                    existente.Estado = asignacionActualizada.Estado;
                    existente.Observaciones = asignacionActualizada.Observaciones;

                    context.SaveChanges();
                }
            }
        }


        public void DeleteAsignacionesLicenciaById(int idAsignacionesLicencia)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.AsignacionesLicencias.FirstOrDefault(a => a.IdAsignacionLicencia == idAsignacionesLicencia);
                if (existente != null)
                {
                    context.AsignacionesLicencias.Remove(existente);
                    context.SaveChanges();
                }
            }
        }

        public PagedResult<AsignacionesLicenciaDTO> GetAsignacionesLicenciaPaginados(
    int pagina,
    int pageSize,
    string? filtro = null,
    string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.AsignacionesLicencias
                // 🔹 Relaciones principales
                .Include(a => a.IdLicenciaNavigation)
                    .ThenInclude(l => l.IdTipoLicenciaNavigation) // TipoLicencia
                .Include(a => a.IdLicenciaNavigation.IdProductoNavigation) // Producto
                .Include(a => a.IdActivoNavigation) // Activo
                .Include(a => a.CedulaUsuarioNavigation) // Usuario (para Nombres y Apellidos)
                .Include(a => a.UsuarioDetalle)
                    .ThenInclude(ud => ud.IdDepartamentoNavigation) // Departamento desde UsuarioDetalle
                .AsQueryable();

            // 🔍 Filtro por texto (Producto.Nombre)
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var f = filtro.Trim().ToUpper();
                query = query.Where(a =>
                    a.IdLicenciaNavigation.IdProductoNavigation != null &&
                    a.IdLicenciaNavigation.IdProductoNavigation.Nombre.ToUpper().Contains(f)
                );
            }

            // 🔍 Filtro por estado
            if (!string.IsNullOrWhiteSpace(estado))
            {
                var e = estado.Trim().ToUpper();
                query = query.Where(a => a.Estado != null && a.Estado.ToUpper().Contains(e));
            }

            // 📊 Total antes de paginar
            var totalItems = query.Count();

            // 🔠 Ordenamiento por Producto.Nombre
            query = query.OrderBy(a => a.IdLicenciaNavigation.IdProductoNavigation.Nombre);

            // 🔄 Paginación + Proyección a DTO
            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AsignacionesLicenciaDTO
                {
                    IdAsignacionLicencia = a.IdAsignacionLicencia,
                    IdLicencia = a.IdLicencia,
                    IdActivo = a.IdActivo,
                    IdServidor = a.IdServidor,
                    FechaAsignacion = a.FechaAsignacion,
                    FechaDesasignacion = a.FechaDesasignacion,
                    TipoAsignacion = a.TipoAsignacion,
                    Estado = a.Estado,
                    Observaciones = a.Observaciones,
                    CedulaUsuario = a.CedulaUsuario,
                    IdDepartamentoUsuario = a.IdDepartamentoUsuario,

                    // 🔗 Relaciones
                    NombreTipoLicencia = a.IdLicenciaNavigation.IdTipoLicenciaNavigation != null
                        ? a.IdLicenciaNavigation.IdTipoLicenciaNavigation.Nombre
                        : null,

                    NombreProducto = a.IdLicenciaNavigation.IdProductoNavigation != null
                        ? a.IdLicenciaNavigation.IdProductoNavigation.Nombre
                        : null,

                    NombreActivo = a.IdActivoNavigation != null
                        ? a.IdActivoNavigation.CodigoActivo
                        : null,

                    // ✅ Departamento → desde UsuarioDetalle → Departamento
                    NombreDepartamento = a.UsuarioDetalle != null &&
                                         a.UsuarioDetalle.IdDepartamentoNavigation != null
                        ? a.UsuarioDetalle.IdDepartamentoNavigation.NombreDepartamento
                        : null,

                    // ✅ Nombres y Apellidos → desde CedulaUsuarioNavigation (tabla Usuarios)
                    NombresApellidos = a.CedulaUsuarioNavigation != null
                        ? (a.CedulaUsuarioNavigation.Apellidos + " " + a.CedulaUsuarioNavigation.Nombres)
                        : null
                })
                .ToList();

            // 📦 Retornar paginado
            return new PagedResult<AsignacionesLicenciaDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }



    }
}
