using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.DataRepository
{
    public class ConfiguracionesActivoRepository
    {
        //tare los idactivos por iporelacion principal
        public List<ActivoDTO> GetActivosPrincipal()
        {
            using var context = new InvensisContext();

            var lista = context.Activos
                .Where(a => a.TipoRelacion == "PRINCIPAL" &&
                            !(context.ConfiguracionesActivos
                                .Any(c => c.IdActivoPrincipal == a.IdActivo && c.EstadoConfiguracion == "INSTALADO")))
                .Select(a => new ActivoDTO
                {
                    IdActivo = a.IdActivo,
                    CodigoActivo = a.CodigoActivo,
                    IdProducto = a.IdProducto,
                    NumeroSerie = a.NumeroSerie,
                    NumeroParte = a.NumeroParte,
                    FechaAdquisicion = a.FechaAdquisicion.ToDateTime(TimeOnly.MinValue),
                    FechaGarantiaFin = a.FechaGarantiaFin.HasValue
                    ? a.FechaGarantiaFin.Value.ToDateTime(TimeOnly.MinValue)
                    : (DateTime?)null,
                    IdFacturaCompra = a.IdFacturaCompra,
                    IdOrdenEnsamblaje = a.IdOrdenEnsamblaje,
                    ValorCompra = a.ValorCompra,
                    ValorResidual = a.ValorResidual,
                    VidaUtilMeses = a.VidaUtilMeses,
                    UbicacionActual = a.UbicacionActual,
                    EstadoActivo = a.EstadoActivo,
                    CondicionFisica = a.CondicionFisica,
                    EsServidor = a.EsServidor,
                    Observaciones = a.Observaciones,
                    FechaRegistro = a.FechaRegistro,
                    IdActivoPadre = a.IdActivoPadre,
                    EsComponente = a.EsComponente,
                    TipoRelacion = a.TipoRelacion
                })
                .ToList();

            return lista;
        }

        //trae todos los idcopononetes 
        public List<ActivoDTO> GetActivosComponenetes()
        {
            using var context = new InvensisContext();

            var lista = context.Activos
                .Include(a => a.IdProductoNavigation) // 👈 para traer el nombre del producto
                .Where(a => a.TipoRelacion != null &&
                            a.TipoRelacion.Trim().ToUpper() != "PRINCIPAL" &&
                            !(context.ConfiguracionesActivos
                                .Any(c => c.IdComponente == a.IdActivo && c.EstadoConfiguracion == "INSTALADO")))
                .Select(a => new ActivoDTO
                {
                    IdActivo = a.IdActivo,
                    CodigoActivo = a.CodigoActivo,
                    IdProducto = a.IdProducto,
                    NumeroSerie = a.NumeroSerie,
                    NumeroParte = a.NumeroParte,
                    FechaAdquisicion = a.FechaAdquisicion.ToDateTime(TimeOnly.MinValue),
                    FechaGarantiaFin = a.FechaGarantiaFin.HasValue
                        ? a.FechaGarantiaFin.Value.ToDateTime(TimeOnly.MinValue)
                        : (DateTime?)null,
                    IdFacturaCompra = a.IdFacturaCompra,
                    IdOrdenEnsamblaje = a.IdOrdenEnsamblaje,
                    ValorCompra = a.ValorCompra,
                    ValorResidual = a.ValorResidual,
                    VidaUtilMeses = a.VidaUtilMeses,
                    UbicacionActual = a.UbicacionActual,
                    EstadoActivo = a.EstadoActivo,
                    CondicionFisica = a.CondicionFisica,
                    EsServidor = a.EsServidor,
                    Observaciones = a.Observaciones,
                    FechaRegistro = a.FechaRegistro,
                    IdActivoPadre = a.IdActivoPadre,
                    EsComponente = a.EsComponente,
                    TipoRelacion = a.TipoRelacion,

                    // ✅ Nueva propiedad
                    NombreProducto = a.IdProductoNavigation != null ? a.IdProductoNavigation.Nombre : null
                })
                .ToList();

            return lista;
        }


        //insertar
        public void InsertarConfiguracion(ConfiguracionActivoInsertDTO dto)
        {
            using var context = new InvensisContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var activoPrincipal = context.Activos.Find(dto.IdActivoPrincipal);
                if (activoPrincipal == null)
                    throw new Exception($"El Activo Principal con ID {dto.IdActivoPrincipal} no existe.");

                if (activoPrincipal.TipoRelacion != "PRINCIPAL")
                    throw new Exception($"El Activo con ID {dto.IdActivoPrincipal} no es un PRINCIPAL.");

                // Traemos configuraciones actuales en BD
                var existentes = context.ConfiguracionesActivos
                    .Where(c => c.IdActivoPrincipal == dto.IdActivoPrincipal)
                    .ToList();

                foreach (var comp in dto.Componentes)
                {
                    var existente = existentes
                        .FirstOrDefault(e => e.IdComponente == comp.IdComponente);

                    if (existente != null)
                    {
                        // 🔄 Update
                        existente.FechaInstalacion = comp.FechaInstalacion;
                        existente.FechaRemocion = comp.FechaRemocion;
                        existente.UbicacionFisica = comp.UbicacionFisica;
                        existente.EstadoConfiguracion = comp.EstadoConfiguracion;
                        existente.Observaciones = comp.Observaciones;
                        existente.CedulaTecnico = comp.CedulaTecnico;
                    }
                    else
                    {
                        // ➕ Insert
                        context.ConfiguracionesActivos.Add(new ConfiguracionesActivo
                        {
                            IdActivoPrincipal = dto.IdActivoPrincipal,
                            IdComponente = comp.IdComponente,
                            FechaInstalacion = comp.FechaInstalacion,
                            FechaRemocion = comp.FechaRemocion,
                            UbicacionFisica = comp.UbicacionFisica,
                            EstadoConfiguracion = comp.EstadoConfiguracion,
                            Observaciones = comp.Observaciones,
                            CedulaTecnico = comp.CedulaTecnico
                        });
                    }
                }

                // ❌ Borramos solo los que ya no están en el DTO
                var idsNuevos = dto.Componentes.Select(c => c.IdComponente).ToList();
                var paraEliminar = existentes
                    .Where(e => !idsNuevos.Contains(e.IdComponente))
                    .ToList();

                if (paraEliminar.Any())
                    context.ConfiguracionesActivos.RemoveRange(paraEliminar);

                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Error al insertar/actualizar configuración: {ex.Message}");
            }
        }


        //trae todo lo agrega por ese id
        public IEnumerable<ConfiguracionesActivoDTO> GetConfiguracionesByActivo(int idActivoPrincipal)
        {
            using var context = new InvensisContext();

            return context.ConfiguracionesActivos
                .Include(c => c.IdActivoPrincipalNavigation) // Carga el activo principal
                .Include(c => c.IdComponenteNavigation)      // Carga el componente
                .ThenInclude(a => a.IdProductoNavigation)    // Incluye el producto del componente
                .Where(c => c.IdActivoPrincipal == idActivoPrincipal)
                .Select(c => new ConfiguracionesActivoDTO
                {
                    IdConfiguracion = c.IdConfiguracion,
                    IdActivoPrincipal = c.IdActivoPrincipal,
                    IdComponente = c.IdComponente,
                    FechaInstalacion = c.FechaInstalacion,
                    FechaRemocion = c.FechaRemocion,
                    UbicacionFisica = c.UbicacionFisica,
                    EstadoConfiguracion = c.EstadoConfiguracion,
                    Observaciones = c.Observaciones,
                    CedulaTecnico = c.CedulaTecnico,

                    // Códigos desde las navegaciones
                    CodigoActivo = c.IdActivoPrincipalNavigation.CodigoActivo,      // principal
                    CodigoActivoComponente = c.IdComponenteNavigation.CodigoActivo, // componente

                    // ✅ Nombre del producto del componente
                    NombreProducto = c.IdComponenteNavigation.IdProductoNavigation.Nombre
                })
                .ToList();
        }


        // borrar
        public void DeleteConfiguracionesActivoById(int idActivoPrincipal)
        {
            using (var context = new InvensisContext())
            {
                var configuraciones = context.ConfiguracionesActivos
                    .Where(c => c.IdActivoPrincipal == idActivoPrincipal)
                    .ToList();

                if (configuraciones.Any())
                {
                    context.ConfiguracionesActivos.RemoveRange(configuraciones);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<ConfiguracionesActivoDTO> GetConfiguracionesActivoPaginados(
    int pagina,
    int pageSize,
    string? filtro = null,
    string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.ConfiguracionesActivos
                .Include(c => c.IdActivoPrincipalNavigation)
                    .ThenInclude(a => a.IdProductoNavigation) // 🔹 Incluye el producto del activo principal
                .AsQueryable();

            // 👉 Estado por defecto INSTALADO
            if (string.IsNullOrWhiteSpace(estado))
                estado = "INSTALADO";

            query = query.Where(c => c.EstadoConfiguracion == estado);

            // 👉 Filtro por CódigoActivo del principal
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(c =>
                    c.IdActivoPrincipalNavigation.CodigoActivo.ToLower().Contains(filtro));
            }

            // 👉 Agrupamos por activo principal (para no repetirlo por cada componente)
            var agrupados = query
                .GroupBy(c => c.IdActivoPrincipal)
                .Select(g => new ConfiguracionesActivoDTO
                {
                    IdActivoPrincipal = g.Key,
                    CodigoActivo = g.First().IdActivoPrincipalNavigation.CodigoActivo,
                    EstadoConfiguracion = g.First().EstadoConfiguracion,
                    FechaInstalacion = g.Max(x => x.FechaInstalacion),
                    // 🔹 Nuevo: nombre del producto del activo principal
                    NombreProducto = g.First().IdActivoPrincipalNavigation.IdProductoNavigation.Nombre
                });

            // 👉 Total registros
            var totalItems = agrupados.Count();

            // 👉 Paginación
            var configuraciones = agrupados
                .OrderBy(c => c.CodigoActivo)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<ConfiguracionesActivoDTO>
            {
                Items = configuraciones,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }



    }
}

