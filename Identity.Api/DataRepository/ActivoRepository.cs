using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;



namespace Identity.Api.DataRepository
{
    public class ActivoRepository
    {
        public List<Activo> ActivoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.Activos.ToList();
            }
        }

        public Activo GetActivoById(int IdActivo)
        {
            using (var context = new InvensisContext())
            {
                return context.Activos.FirstOrDefault(a => a.IdActivo == IdActivo);
            }
        }

        //inserción masiva con store procedure
        public async Task<List<SpResponseDTO>> InsertarActivos(List<ActivoDTO> activos)
        {
            var responses = new List<SpResponseDTO>();

            using var context = new InvensisContext();

            foreach (var activo in activos)
            {
                try
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@IdProducto", activo.IdProducto),
                        new SqlParameter("@NumeroSerie", activo.NumeroSerie ?? (object)DBNull.Value),
                        new SqlParameter("@NumeroParte", activo.NumeroParte ?? (object)DBNull.Value),
                        new SqlParameter("@FechaAdquisicion", activo.FechaAdquisicion.Date), // toma solo la fecha
                        new SqlParameter("@FechaGarantiaFin", activo.FechaGarantiaFin?.Date ?? (object)DBNull.Value),
                        new SqlParameter("@IdFacturaCompra", activo.IdFacturaCompra ?? (object)DBNull.Value),
                        new SqlParameter("@ValorCompra", activo.ValorCompra),
                        new SqlParameter("@ValorResidual", activo.ValorResidual ?? 0m),
                        new SqlParameter("@VidaUtilMeses", activo.VidaUtilMeses ?? 36),
                        new SqlParameter("@UbicacionActual", activo.UbicacionActual ?? (object)DBNull.Value),
                        new SqlParameter("@EstadoActivo", activo.EstadoActivo ?? "DISPONIBLE"),
                        new SqlParameter("@CondicionFisica", activo.CondicionFisica ?? "NUEVO"),
                        new SqlParameter("@EsServidor", activo.EsServidor ?? false),
                        new SqlParameter("@Observaciones", activo.Observaciones ?? (object)DBNull.Value),
                        // 👇 nuevos parámetros
                        new SqlParameter("@IdActivoPadre", activo.IdActivoPadre ?? (object)DBNull.Value),
                        new SqlParameter("@EsComponente", activo.EsComponente ?? (object)DBNull.Value),
                        new SqlParameter("@TipoRelacion", activo.TipoRelacion) // SIEMPRE se envía
                    };

                    await context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_InsertarActivo @IdProducto, @NumeroSerie, @NumeroParte, @FechaAdquisicion, @FechaGarantiaFin, @IdFacturaCompra, @ValorCompra, @ValorResidual, @VidaUtilMeses, @UbicacionActual, @EstadoActivo, @CondicionFisica, @EsServidor, @Observaciones",
                        parameters
                    );


                    responses.Add(new SpResponseDTO
                    {
                        Success = 1,
                        Message = $"Activo {activo.NumeroSerie ?? "(sin serie)"} insertado correctamente"
                    });
                }
                catch (SqlException ex)
                {
                    responses.Add(new SpResponseDTO
                    {
                        Success = 0,
                        Message = ex.Message,
                        ErrorNumber = ex.Number,
                        Severity = ex.Class,
                        State = ex.State,
                        ErrorLine = ex.LineNumber,
                        ProcedureName = ex.Procedure
                    });
                }
            }

            return responses;
        }



        public void UpdateActivo(Activo updActivo)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Activos.FirstOrDefault(a => a.IdActivo == updActivo.IdActivo);
                if (existente != null)
                {
                    existente.CodigoActivo = updActivo.CodigoActivo;
                    existente.IdProducto = updActivo.IdProducto;
                    existente.NumeroSerie = updActivo.NumeroSerie;
                    existente.NumeroParte = updActivo.NumeroParte;

                    existente.FechaAdquisicion = updActivo.FechaAdquisicion;
                    existente.FechaGarantiaFin = updActivo.FechaGarantiaFin;
                    existente.IdFacturaCompra = updActivo.IdFacturaCompra;
                    existente.IdOrdenEnsamblaje = updActivo.IdOrdenEnsamblaje;
                    existente.ValorCompra = updActivo.ValorCompra;
                    existente.ValorResidual = updActivo.ValorResidual;
                    existente.VidaUtilMeses = updActivo.VidaUtilMeses;
                    existente.UbicacionActual = updActivo.UbicacionActual;
                    existente.EstadoActivo = updActivo.EstadoActivo;
                    existente.CondicionFisica = updActivo.CondicionFisica;
                    existente.EsServidor = updActivo.EsServidor;
                    existente.Observaciones = updActivo.Observaciones;
                    //existente.FechaRegistro = updActivo.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteActivoById(int idActivo)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Activos.FirstOrDefault(a => a.IdActivo == idActivo);
                if (existente != null)
                {
                    context.Activos.Remove(existente);
                    context.SaveChanges();
                }
            }
        }


        //paginado
        public PagedResult<ActivoDTO> GetActivoPaginados(
            int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null)
        {
            using var context = new InvensisContext();

            var query = context.Activos
                .Include(a => a.IdProductoNavigation)   // Producto
                .Include(a => a.IdFacturaCompraNavigation) // Factura
                .AsQueryable();

            // Filtro de texto en CodigoActivo, Producto.Nombre o Factura.NumeroFactura
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var f = filtro.Trim().ToUpper();
                query = query.Where(a =>
                    (a.CodigoActivo != null && a.CodigoActivo.ToUpper().Contains(f)) ||
                    (a.IdProductoNavigation != null && a.IdProductoNavigation.Nombre.ToUpper().Contains(f)) ||
                    (a.IdFacturaCompraNavigation != null && a.IdFacturaCompraNavigation.NumeroFactura.ToUpper().Contains(f))
                );
            }

            // Filtro por estado
            if (!string.IsNullOrWhiteSpace(estadoActivo))
                query = query.Where(a => a.EstadoActivo != null &&
                    a.EstadoActivo.ToUpper().Contains(estadoActivo.Trim().ToUpper()));

            var totalItems = query.Count();

            // Ordenamiento fijo por CodigoActivo
            query = query.OrderBy(a => a.CodigoActivo);

            // Proyección a DTO
            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ActivoDTO
                {
                    IdActivo = a.IdActivo,
                    CodigoActivo = a.CodigoActivo,
                    IdProducto = a.IdProducto,
                    NumeroSerie = a.NumeroSerie,
                    NumeroParte = a.NumeroParte,
                    // Si es DateOnly (no nullable)
                    FechaAdquisicion = a.FechaAdquisicion.ToDateTime(TimeOnly.MinValue),

                    // Si es DateOnly? (nullable)
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

                    // relaciones
                    NombreProducto = a.IdProductoNavigation != null ? a.IdProductoNavigation.Nombre : null,
                    NumeroFactura = a.IdFacturaCompraNavigation != null ? a.IdFacturaCompraNavigation.NumeroFactura : null,
                    NumeroOrden = a.IdOrdenEnsamblajeNavigation != null ? a.IdOrdenEnsamblajeNavigation.NumeroOrden : null
                })
                .ToList();

            return new PagedResult<ActivoDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }




    }
}
