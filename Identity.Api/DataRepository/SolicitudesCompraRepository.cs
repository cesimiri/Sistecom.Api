using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class SolicitudesCompraDataRepository
    {
        public List<SolicitudesCompraDTO> GetAllSolicitudesCompra()
        {
            using var context = new InvensisContext();
            return context.SolicitudesCompras
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.IdDepartamentoNavigation)
                .Include(s => s.CedulaDestinoNavigation)
                .Include(s => s.CedulaAutorizaNavigation)
                .Include(s => s.CedulaSolicitaNavigation)
                .Select(s => new SolicitudesCompraDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    NumeroSolicitud = s.NumeroSolicitud,
                    RucEmpresa = s.RucEmpresa,
                    IdDepartamento = s.IdDepartamento,
                    CedulaSolicita = s.CedulaSolicita,
                    CedulaAutoriza = s.CedulaAutoriza,
                    CedulaDestino = s.CedulaDestino,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaAprobacion = s.FechaAprobacion,
                    FechaRequerida = s.FechaRequerida,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    Justificacion = s.Justificacion,
                    Prioridad = s.Prioridad,
                    MotivoRechazo = s.MotivoRechazo,
                    Observaciones = s.Observaciones,
                    ArchivoOc = s.ArchivoOc,
                    Estado = s.Estado,
                    RazonSocial = s.RucEmpresaNavigation.RazonSocial,
                    NombreSolicitanteCompleto = s.CedulaSolicitaNavigation.Apellidos + " " + s.CedulaSolicitaNavigation.Nombres,
                    NombreAutorizadorCompleto = s.CedulaAutorizaNavigation.Apellidos + " " + s.CedulaAutorizaNavigation.Nombres,
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento
                })
                .ToList();
        }

        //traer los usuarios destino por departamento ya que es identificador unico 
        public List<UsuarioDetalleDTO> ObtenerUsuarioDestino(int idDepartamento)
        {
            using var context = new InvensisContext();

            return context.UsuarioDetalles
                .Include(u => u.CedulaNavigation)
                .Where(u => u.Estado == "ACTIVO" && u.IdDepartamento == idDepartamento)
                .Select(u => new UsuarioDetalleDTO
                {
                    Cedula = u.Cedula,
                    IdDepartamento = u.IdDepartamento,
                    NombreDepartamento = u.IdDepartamentoNavigation.NombreDepartamento,
                    IdCargo = u.IdCargo,
                    NombreCargo = u.IdCargoNavigation.NombreCargo,
                    NombreCedula = u.CedulaNavigation.Apellidos + " " + u.CedulaNavigation.Nombres,
                    Estado = u.Estado
                })
                .ToList();
        }

        //traer los usuarios quienes pueden autorizar solo por cargo jefe subjefe y gerencia
        public List<UsuarioDetalleDTO> ObtenerUsuariosAutorizaAsync(int idSucursal)
        {
            using var context = new InvensisContext();

            return context.UsuarioDetalles
                .Include(u => u.IdCargoNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Where(u => u.Estado == "ACTIVO"
                && u.IdDepartamentoNavigation.IdSucursal == idSucursal

                 && new[] { 1, 2, 3 }.Contains(u.IdCargoNavigation.NivelJerarquico.GetValueOrDefault()))
                .Select(u => new UsuarioDetalleDTO
                {

                    Cedula = u.Cedula,
                    IdDepartamento = u.IdDepartamento,
                    NombreDepartamento = u.IdDepartamentoNavigation.NombreDepartamento,
                    IdCargo = u.IdCargo,
                    NombreCargo = u.IdCargoNavigation.NombreCargo,
                    NombreCedula = u.CedulaNavigation.Apellidos + " " + u.CedulaNavigation.Nombres,
                    Estado = u.Estado

                })
                .ToList();

        }

        //traer los usuarios q solicita dependiendo cargo 4,3,2 o tecnico jefe subjefe
        public List<UsuarioDetalleDTO> ObtenerUsuarioSolicitaAsync()
        {
            using var context = new InvensisContext();

            return context.UsuarioDetalles
                .Include(u => u.IdCargoNavigation)
                .Include(u => u.IdDepartamentoNavigation)
                .Where(u => u.Estado == "ACTIVO"


                 && new[] { 2, 3, 4 }.Contains(u.IdCargoNavigation.NivelJerarquico.GetValueOrDefault()))
                .Select(u => new UsuarioDetalleDTO
                {

                    Cedula = u.Cedula,
                    IdDepartamento = u.IdDepartamento,
                    NombreDepartamento = u.IdDepartamentoNavigation.NombreDepartamento,
                    IdCargo = u.IdCargo,
                    NombreCargo = u.IdCargoNavigation.NombreCargo,
                    NombreCedula = u.CedulaNavigation.Apellidos + " " + u.CedulaNavigation.Nombres,
                    Estado = u.Estado

                })
                .ToList();
        }

        //obtener las sucursales despues de seleccionar la empresa
        public List<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        {
            using var context = new InvensisContext();

            return context.Sucursales
                .Where(m => m.RucEmpresa == RucEmpresa)
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

        public SolicitudesCompraDTO GetSolicitudById(int idSolicitud)
        {
            using var context = new InvensisContext();

            return context.SolicitudesCompras
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.CedulaSolicitaNavigation)
                .Include(s => s.CedulaAutorizaNavigation)
                .Include(s => s.IdDepartamentoNavigation)
                .Where(s => s.IdSolicitud == idSolicitud)
                .Select(s => new SolicitudesCompraDTO
                {
                    IdSolicitud = s.IdSolicitud,
                    NumeroSolicitud = s.NumeroSolicitud,
                    RucEmpresa = s.RucEmpresa,
                    IdDepartamento = s.IdDepartamento,
                    CedulaSolicita = s.CedulaSolicita,
                    CedulaAutoriza = s.CedulaAutoriza,
                    CedulaDestino = s.CedulaDestino,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaAprobacion = s.FechaAprobacion,
                    FechaRequerida = s.FechaRequerida,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    Justificacion = s.Justificacion,
                    Prioridad = s.Prioridad,
                    MotivoRechazo = s.MotivoRechazo,
                    Observaciones = s.Observaciones,
                    ArchivoOc = s.ArchivoOc,
                    Estado = s.Estado,
                    RazonSocial = s.RucEmpresaNavigation.RazonSocial,
                    NombreSolicitanteCompleto = s.CedulaSolicitaNavigation.Apellidos + " " + s.CedulaSolicitaNavigation.Nombres,
                    NombreAutorizadorCompleto = s.CedulaAutorizaNavigation.Apellidos + " " + s.CedulaAutorizaNavigation.Nombres,
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento
                })
                .FirstOrDefault();
        }


        //ingresa la solicitud y devuelve el idSolcitud
        public int InsertSolicitud(SolicitudesCompraDTO dto)
        {
            try
            {
                using var context = new InvensisContext();
                var empresa = context.EmpresasClientes.Find(dto.RucEmpresa);
                var usuarioSolicita = context.Usuarios.Find(dto.CedulaSolicita);
                var usuarioAutoriza = context.Usuarios.Find(dto.CedulaAutoriza);
                var usuarioDestino = context.Usuarios.Find(dto.CedulaDestino);
                var departamento = context.Departamentos.Find(dto.IdDepartamento);

                if (empresa == null || usuarioSolicita == null || usuarioAutoriza == null || usuarioDestino == null || departamento == null)
                {
                    throw new Exception("Uno o más datos no existen en la base de datos.");
                }

                var year = DateTime.Now.Year;
                var lastNumero = context.SolicitudesCompras
                    .Where(s => s.NumeroSolicitud.StartsWith($"SC-{year}"))
                    .OrderByDescending(s => s.NumeroSolicitud)
                    .Select(s => s.NumeroSolicitud)
                    .FirstOrDefault();

                int nextNumber = 1;
                if (lastNumero != null && int.TryParse(lastNumero.Split('-').Last(), out var lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
                var nuevoNumeroSolicitud = $"SC-{year}-{nextNumber:D4}";

                var nueva = new SolicitudesCompra
                {
                    NumeroSolicitud = nuevoNumeroSolicitud,
                    RucEmpresa = dto.RucEmpresa,
                    IdDepartamento = dto.IdDepartamento,
                    CedulaSolicita = dto.CedulaSolicita,
                    CedulaAutoriza = dto.CedulaAutoriza,
                    CedulaDestino = dto.CedulaDestino,
                    FechaSolicitud = DateTime.Now,
                    FechaAprobacion = dto.FechaAprobacion,
                    FechaRequerida = DateOnly.FromDateTime(dto.FechaSolicitud.AddDays(10)),
                    SubtotalSinImpuestos = 0,
                    DescuentoTotal = 0,
                    Iva = 0,
                    ValorTotal = 0,
                    Justificacion = dto.Justificacion,
                    Prioridad = "NORMAL",
                    Estado = dto.Estado,
                    MotivoRechazo = dto.MotivoRechazo,
                    Observaciones = dto.Observaciones,
                    ArchivoOc = dto.ArchivoOc
                };

                context.SolicitudesCompras.Add(nueva);
                context.SaveChanges();

                return nueva.IdSolicitud;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la solicitud: " + (ex.InnerException?.Message ?? ex.Message), ex);
            }
        }

        public void UpdateSolicitud(SolicitudesCompraDTO updatedSolicitud)
        {
            using var context = new InvensisContext();
            var solicitud = context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == updatedSolicitud.IdSolicitud);

            if (solicitud != null)
            {
                solicitud.RucEmpresa = updatedSolicitud.RucEmpresa;
                solicitud.IdDepartamento = updatedSolicitud.IdDepartamento;
                solicitud.CedulaSolicita = updatedSolicitud.CedulaSolicita;
                solicitud.CedulaAutoriza = updatedSolicitud.CedulaAutoriza;
                solicitud.CedulaDestino = updatedSolicitud.CedulaDestino;
                solicitud.FechaSolicitud = updatedSolicitud.FechaSolicitud;
                solicitud.FechaAprobacion = updatedSolicitud.FechaAprobacion;
                solicitud.FechaRequerida = updatedSolicitud.FechaRequerida;
                solicitud.SubtotalSinImpuestos = updatedSolicitud.SubtotalSinImpuestos;
                solicitud.DescuentoTotal = updatedSolicitud.DescuentoTotal;
                solicitud.Iva = updatedSolicitud.Iva;
                solicitud.ValorTotal = updatedSolicitud.ValorTotal;
                solicitud.Justificacion = updatedSolicitud.Justificacion;
                solicitud.Prioridad = updatedSolicitud.Prioridad;
                solicitud.Estado = updatedSolicitud.Estado;
                solicitud.MotivoRechazo = updatedSolicitud.MotivoRechazo;
                solicitud.Observaciones = updatedSolicitud.Observaciones;
                solicitud.ArchivoOc = updatedSolicitud.ArchivoOc;

                context.SaveChanges();
            }
        }

        public void DeleteSolicitudById(int idSolicitud)
        {
            using (var context = new InvensisContext())
            {
                var solicitud = context.SolicitudesCompras.FirstOrDefault(s => s.IdSolicitud == idSolicitud);

                if (solicitud != null)
                {
                    context.SolicitudesCompras.Remove(solicitud);
                    context.SaveChanges();
                }
            }
        }


        //PAGINADA 
        public PagedResult<SolicitudesCompraDTO> GetSolicitudesCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.SolicitudesCompras
                .Include(s => s.RucEmpresaNavigation)
                .Include(s => s.CedulaSolicitaNavigation)
                .Include(s => s.IdDepartamentoNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.RucEmpresa.ToLower().Contains(filtro) ||
                    u.NumeroSolicitud.ToLower().Contains(filtro) ||
                    u.RucEmpresaNavigation.RazonSocial.ToLower().Contains(filtro) ||
                    (u.CedulaSolicitaNavigation.Apellidos + " " + u.CedulaSolicitaNavigation.Nombres).ToLower().Contains(filtro)
                );
            }

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            var totalItems = query.Count();

            var usuarios = query
                .OrderBy(u => u.IdSolicitud)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SolicitudesCompraDTO
                {
                    NumeroSolicitud = s.NumeroSolicitud,
                    IdSolicitud = s.IdSolicitud,
                    RucEmpresa = s.RucEmpresa,
                    IdDepartamento = s.IdDepartamento,
                    CedulaSolicita = s.CedulaSolicita,
                    CedulaAutoriza = s.CedulaAutoriza,
                    CedulaDestino = s.CedulaDestino,
                    FechaSolicitud = s.FechaSolicitud,
                    FechaAprobacion = s.FechaAprobacion,
                    FechaRequerida = s.FechaRequerida,
                    SubtotalSinImpuestos = s.SubtotalSinImpuestos,
                    DescuentoTotal = s.DescuentoTotal,
                    Iva = s.Iva,
                    ValorTotal = s.ValorTotal,
                    Justificacion = s.Justificacion,
                    Prioridad = s.Prioridad,
                    MotivoRechazo = s.MotivoRechazo,
                    Observaciones = s.Observaciones,
                    ArchivoOc = s.ArchivoOc,
                    Estado = s.Estado,
                    RazonSocial = s.RucEmpresaNavigation.RazonSocial,
                    NombreSolicitanteCompleto = s.CedulaSolicitaNavigation.Apellidos + " " + s.CedulaSolicitaNavigation.Nombres,
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento
                })
                .ToList();

            return new PagedResult<SolicitudesCompraDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
