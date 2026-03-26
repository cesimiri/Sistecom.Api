using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class OrdenesEntregaRepository
    {

        public OrdenesEntrega GetOrdenesEntregaById(int IdOrdenesEntrega)
        {
            using (var context = new InvensisContext())
            {
                return context.OrdenesEntregas.FirstOrDefault(p => p.IdOrden == IdOrdenesEntrega); ;
            }

        }

        //traer todas las solicitudes de compra que no esten registradas aquji en ordenes de Entrega
        public List<SolicitudesCompraDTO> GetSolicitudesAprobadasSinOrden()
        {
            using var context = new InvensisContext();

            // 🔹 1. Obtener todos los números de orden ya registrados
            var numerosOrdenExistentes = context.OrdenesEntregas
                .Select(o => o.NumeroOrden)
                .ToList();

            // 🔹 2. Traer todas las solicitudes aprobadas que no tengan orden de entrega
            var solicitudes = context.SolicitudesCompras
        .Include(s => s.RucEmpresaNavigation)
        .Include(s => s.IdDepartamentoNavigation)
        .Include(s => s.CedulaDestinoNavigation)
        .Include(s => s.CedulaAutorizaNavigation)
        .Include(s => s.CedulaSolicitaNavigation)
        .Where(s => s.Estado == "APROBADA"
            && !context.OrdenesEntregas
                .Any(o => o.IdSolicitud == s.IdSolicitud))
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
            NombreSolicitanteCompleto =
                s.CedulaSolicitaNavigation.Apellidos + " " +
                s.CedulaSolicitaNavigation.Nombres,
            NombreAutorizadorCompleto =
                s.CedulaAutorizaNavigation.Apellidos + " " +
                s.CedulaAutorizaNavigation.Nombres,
            NombreDepartamento =
                s.IdDepartamentoNavigation.NombreDepartamento
        })
        .ToList();


            return solicitudes;
        }


        //obtener un usuario por su id que traiga depoaprtamentos y nombre de departamento
        public List<UsuarioDetalleDTO> GetUsuarioDetalleById(string cedula)
        {
            using var context = new InvensisContext();

            return context.UsuarioDetalles
                .Include(s => s.IdDepartamentoNavigation)
                .Include(s => s.IdCargoNavigation)
                .Include(s => s.CedulaNavigation)
                .Where(s => s.Cedula == cedula && s.Estado == "ACTIVO") // 🔹 filtro agregado
                .Select(s => new UsuarioDetalleDTO
                {
                    Cedula = s.Cedula,
                    IdDepartamento = s.IdDepartamento,
                    IdCargo = s.IdCargo,
                    FechaAsignacion = s.FechaAsignacion,
                    FechaBaja = s.FechaBaja,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado,

                    // 🔹 relaciones incluidas
                    NombreDepartamento = s.IdDepartamentoNavigation.NombreDepartamento,
                    NombreCargo = s.IdCargoNavigation.NombreCargo,
                    NombreCedula = s.CedulaNavigation.Apellidos + " " + s.CedulaNavigation.Nombres
                })
                .ToList();
        }


        public int InsertOrdenesEntrega(OrdenesEntregaDTO newItem)
        {
            using var context = new InvensisContext();

            try
            {
                // VALIDACIÓN DE ID SOLICITUD
                if (newItem.IdSolicitud <= 0)
                    throw new ArgumentException("El campo IdSolicitud es obligatorio y debe ser mayor que cero.");

                // VALIDAR ESTADO
                var estadosPermitidos = new[] { "PENDIENTE", "EN_RUTA", "ENTREGADA", "RECHAZADA", "REPROGRAMADA" };
                if (string.IsNullOrWhiteSpace(newItem.Estado) ||
                    !estadosPermitidos.Contains(newItem.Estado.ToUpper()))
                {
                    throw new ArgumentException("El estado debe ser uno de los siguientes: PENDIENTE, EN_RUTA, ENTREGADA, RECHAZADA o REPROGRAMADA.");
                }

                // 🔥 GENERACIÓN DEL NUMERO DE ORDEN (LÓGICA DEL TRIGGER)
                var añoActual = DateTime.Now.Year.ToString();

                // Buscar última orden con formato ENT-YYYY-nnnnnn
                var ultimaOrden = context.OrdenesEntregas
                    .Where(o => o.NumeroOrden.StartsWith($"ENT-{añoActual}-"))
                    .OrderByDescending(o => o.NumeroOrden)
                    .Select(o => o.NumeroOrden)
                    .FirstOrDefault();

                int secuencia = 1;

                if (!string.IsNullOrEmpty(ultimaOrden))
                {
                    var partes = ultimaOrden.Split('-'); // ENT / 2025 / 000001
                    if (partes.Length == 3 && int.TryParse(partes[2], out int num))
                    {
                        secuencia = num + 1;
                    }
                }

                string nuevoNumeroOrden = $"ENT-{añoActual}-{secuencia:D6}";
                newItem.NumeroOrden = nuevoNumeroOrden;

                // MAPEAR ENTIDAD
                var entity = new OrdenesEntrega
                {
                    NumeroOrden = newItem.NumeroOrden,
                    IdSolicitud = newItem.IdSolicitud,
                    FechaProgramada = newItem.FechaProgramada,
                    FechaEntrega = newItem.FechaEntrega,
                    DireccionEntrega = newItem.DireccionEntrega,
                    ContactoRecepcion = newItem.ContactoRecepcion,
                    TelefonoContacto = newItem.TelefonoContacto,
                    Estado = newItem.Estado,
                    GuiaRemision = newItem.GuiaRemision,
                    Transportista = newItem.Transportista,
                    FirmaRecepcion = newItem.FirmaRecepcion,
                    IncluyeLicencias = newItem.IncluyeLicencias,
                    ObservacionesEntrega = newItem.ObservacionesEntrega,
                    CedulaRecibe = newItem.CedulaRecibe,
                    IdDepartamentoEntrega = newItem.IdDepartamentoEntrega
                };

                context.OrdenesEntregas.Add(entity);
                context.SaveChanges();

                // Asignar Id generado al DTO y devolverlo
                newItem.IdOrden = entity.IdOrden;
                return entity.IdOrden; // ✅ ahora devuelve el Id
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar la orden de entrega: {ex.InnerException?.Message ?? ex.Message}");
            }
        }


        public void UpdateOrdenesEntrega(OrdenesEntregaDTO UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.OrdenesEntregas
                                         .Where(a => a.IdOrden == UpdItem.IdOrden)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.NumeroOrden = UpdItem.NumeroOrden;
                    //registrado.IdSolicitud = UpdItem.IdSolicitud;
                    registrado.FechaProgramada = UpdItem.FechaProgramada;
                    //registrado.HoraProgramada = UpdItem.HoraProgramada;
                    registrado.FechaEntrega = UpdItem.FechaEntrega;
                    registrado.DireccionEntrega = UpdItem.DireccionEntrega;
                    registrado.ContactoRecepcion = UpdItem.ContactoRecepcion;
                    registrado.TelefonoContacto = UpdItem.TelefonoContacto;
                    registrado.Estado = UpdItem.Estado;
                    registrado.GuiaRemision = UpdItem.GuiaRemision;
                    registrado.Transportista = UpdItem.Transportista;
                    registrado.FirmaRecepcion = UpdItem.FirmaRecepcion;
                    //registrado.FotoEntrega = UpdItem.FotoEntrega;
                    registrado.IncluyeLicencias = UpdItem.IncluyeLicencias;
                    registrado.ObservacionesEntrega = UpdItem.ObservacionesEntrega;
                    registrado.FechaRegistro = UpdItem.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteOrdenesEntregaById(int IdOrdenesEntega)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.OrdenesEntregas
                                         .Where(a => a.IdOrden == IdOrdenesEntega)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.OrdenesEntregas.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }

        //generar PDF Ordenes automaticamente

        public async Task<(OrdenesEntregaDTO ordenes, List<DetalleOrdenEntregaDTO> detalles)>
        ObtenerOrdenesConDetallesAsync(int idOrdenes)
        {
            using var context = new InvensisContext();

            // Traer la factura con sus relaciones (proveedor y bodega)
            var ordenes = await context.OrdenesEntregas
                .Include(o => o.IdDepartamentoEntregaNavigation)
                .Include(o => o.IdSolicitudNavigation)
                .Where(f => f.IdOrden == idOrdenes)
                .AsQueryable()
                .Select(o => new OrdenesEntregaDTO
                {
                    IdOrden = o.IdOrden,
                    NumeroOrden = o.NumeroOrden,
                    IdSolicitud = o.IdSolicitud,
                    FechaProgramada = o.FechaProgramada,
                    FechaEntrega = o.FechaEntrega,
                    DireccionEntrega = o.DireccionEntrega,
                    ContactoRecepcion = o.ContactoRecepcion,
                    TelefonoContacto = o.TelefonoContacto,
                    Estado = o.Estado,
                    GuiaRemision = o.GuiaRemision,
                    Transportista = o.Transportista,
                    FirmaRecepcion = o.FirmaRecepcion,
                    IncluyeLicencias = o.IncluyeLicencias,
                    ObservacionesEntrega = o.ObservacionesEntrega,
                    FechaRegistro = o.FechaRegistro,
                    CedulaRecibe = o.CedulaRecibe,
                    IdDepartamentoEntrega = o.IdDepartamentoEntrega,

                    NombreDepartamento = o.IdDepartamentoEntregaNavigation != null
                        ? o.IdDepartamentoEntregaNavigation.NombreDepartamento
                        : null,

                    NumeroSolicitud = o.IdSolicitudNavigation != null
                        ? o.IdSolicitudNavigation.NumeroSolicitud
                        : null
                })
                .FirstOrDefaultAsync();

            if (ordenes == null)
                return (null, null);

            var detalles = await context.DetalleOrdenEntregas
                .Include(f => f.IdProductoNavigation)
                .Where(d => d.IdOrden == idOrdenes)
                .Select(s => new DetalleOrdenEntregaDTO
                {
                    IdDetalle = s.IdDetalle,
                    IdOrden = s.IdOrden,
                    IdProducto = s.IdProducto,
                    CantidadProgramada = s.CantidadProgramada,
                    CantidadEntregada = s.CantidadEntregada,
                    IdActivo = s.IdActivo,
                    IdLicencia = s.IdLicencia,
                    Observaciones = s.Observaciones,
                    NombreProducto = s.IdProductoNavigation.Nombre
                })
                .ToListAsync();

            return (ordenes, detalles);
        }


        //paginado por Número de orden
        public PagedResult<OrdenesEntregaDTO> GetOrdenesEntregaPaginadas(
         int pagina,
         int pageSize,
         string? filtro = null,
         string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.OrdenesEntregas
                .Include(o => o.IdDepartamentoEntregaNavigation) // Relación con Departamento
                .Include(o => o.IdSolicitudNavigation)            // Relación con SolicitudesCompra
                .AsQueryable();

            // 🔍 Filtro SOLO por NumeroOrden
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var f = filtro.Trim().ToUpper();
                query = query.Where(o =>
                    o.NumeroOrden != null && o.NumeroOrden.ToUpper().Contains(f)
                );
            }

            // 🎯 Filtro por estado (PENDIENTE, EN_RUTA, ENTREGADA, RECHAZADA, REPROGRAMADA)
            if (!string.IsNullOrWhiteSpace(estado))
            {
                var estadoFiltro = estado.Trim().ToUpper();
                query = query.Where(o => o.Estado != null && o.Estado.ToUpper().Contains(estadoFiltro));
            }

            var totalItems = query.Count();

            // 📋 Ordenamiento fijo por NumeroOrden
            query = query.OrderBy(o => o.NumeroOrden);

            // 🔄 Proyección a DTO
            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrdenesEntregaDTO
                {
                    IdOrden = o.IdOrden,
                    NumeroOrden = o.NumeroOrden,
                    IdSolicitud = o.IdSolicitud,
                    FechaProgramada = o.FechaProgramada,
                    FechaEntrega = o.FechaEntrega,
                    DireccionEntrega = o.DireccionEntrega,
                    ContactoRecepcion = o.ContactoRecepcion,
                    TelefonoContacto = o.TelefonoContacto,
                    Estado = o.Estado,
                    GuiaRemision = o.GuiaRemision,
                    Transportista = o.Transportista,
                    FirmaRecepcion = o.FirmaRecepcion,
                    IncluyeLicencias = o.IncluyeLicencias,
                    ObservacionesEntrega = o.ObservacionesEntrega,
                    FechaRegistro = o.FechaRegistro,
                    CedulaRecibe = o.CedulaRecibe,
                    IdDepartamentoEntrega = o.IdDepartamentoEntrega,

                    // 🧩 Relaciones
                    NombreDepartamento = o.IdDepartamentoEntregaNavigation != null
                        ? o.IdDepartamentoEntregaNavigation.NombreDepartamento
                        : null,
                    NumeroSolicitud = o.IdSolicitudNavigation != null
                        ? o.IdSolicitudNavigation.NumeroSolicitud
                        : null,

                })
                .ToList();

            return new PagedResult<OrdenesEntregaDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }



    }
}
