using Identity.Api.DTO;
using Identity.Api.Paginado;
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

        public void InsertActivo(ActivoDTO newActivoDto)
        {
            try
            {
                using var context = new InvensisContext();
                //validación para el ingreso de los id relacionados.
                var idProducto = context.TiposLicencia.Find(newActivoDto.IdProducto);
                var idFacturaCompra = context.Productos.Find(newActivoDto.IdFacturaCompra);


                if (idProducto == null || idFacturaCompra == null)
                {
                    throw new Exception("Esa idTiposLicencia, idProducto, idFactura no existe en la base de datos.");
                }

                // Generar el Código Licenica automático
                var lastCodigo = context.Productos
                    .Where(s => s.CodigoPrincipal.StartsWith("LIC-"))
                    .OrderByDescending(s => s.CodigoPrincipal)
                    .Select(s => s.CodigoPrincipal)
                    .FirstOrDefault();

                int nextNumber = 1;
                if (lastCodigo != null)
                {
                    var lastNumberStr = lastCodigo.Split('-').Last();
                    if (int.TryParse(lastNumberStr, out var parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                var NuevoCodigoPrincipal = $"LIC-{nextNumber:D4}";

                var nueva = new Activo
                {
                    CodigoActivo = newActivoDto.CodigoActivo,
                    IdProducto = newActivoDto.IdProducto,
                    NumeroSerie = newActivoDto.NumeroSerie,
                    NumeroParte = newActivoDto.NumeroParte,
                    FechaAdquisicion = newActivoDto.FechaAdquisicion,
                    FechaGarantiaFin = newActivoDto.FechaGarantiaFin,
                    IdFacturaCompra = newActivoDto.IdFacturaCompra,
                    IdOrdenEnsamblaje = newActivoDto.IdOrdenEnsamblaje,
                    ValorCompra = newActivoDto.ValorCompra,
                    ValorResidual = newActivoDto.ValorResidual,
                    VidaUtilMeses = newActivoDto.VidaUtilMeses,
                    UbicacionActual = newActivoDto.UbicacionActual?.ToUpper(),
                    EstadoActivo = newActivoDto.EstadoActivo,
                    CondicionFisica = newActivoDto.CondicionFisica,
                    EsServidor = newActivoDto.EsServidor,
                    Observaciones = newActivoDto.Observaciones?.ToUpper(),

                };
                context.Activos.Add(nueva);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Activo: " + ex.InnerException?.Message ?? ex.Message);
            }

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
        public PagedResult<ActivoDTO> GetPaginados(
            int pagina,
            int pageSize,
            string? codigoActivo,
            int? idProducto,
            DateTime? desde,
            DateTime? hasta,
            int? idFacturaCompra,
            string? estadoActivo,
            string? ordenColumna = null,
            bool ordenAscendente = true)
        {
            using var context = new InvensisContext();

            var query = context.Activos
                .Include(a => a.IdProductoNavigation)
                .AsQueryable();

            // Validar rango de fechas
            if (desde.HasValue && hasta.HasValue && desde > hasta)
            {
                return new PagedResult<ActivoDTO>
                {
                    Items = new List<ActivoDTO>(),
                    TotalItems = 0,
                    Page = pagina,
                    PageSize = pageSize
                };
            }

            // Filtros
            if (!string.IsNullOrWhiteSpace(codigoActivo))
                query = query.Where(a => a.CodigoActivo.ToUpper().Contains(codigoActivo.Trim().ToUpper()));

            if (idProducto.HasValue)
                query = query.Where(a => a.IdProducto == idProducto.Value);

            if (desde.HasValue)
                query = query.Where(a => a.FechaAdquisicion >= DateOnly.FromDateTime(desde.Value));

            if (hasta.HasValue)
                query = query.Where(a => a.FechaAdquisicion <= DateOnly.FromDateTime(hasta.Value));

            if (idFacturaCompra.HasValue)
                query = query.Where(a => a.IdFacturaCompra == idFacturaCompra.Value);

            if (!string.IsNullOrWhiteSpace(estadoActivo))
                query = query.Where(a => a.EstadoActivo != null &&
                    a.EstadoActivo.ToUpper().Contains(estadoActivo.Trim().ToUpper()));

            var totalItems = query.Count();

            // Ordenamiento
            query = ApplyOrdering(query, ordenColumna, ordenAscendente);

            // Proyección a DTO
            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                //.OrderBy()
                .Select(a => new ActivoDTO
                {
                    IdActivo = a.IdActivo,
                    CodigoActivo = a.CodigoActivo,
                    IdProducto = a.IdProducto,
                    NumeroSerie = a.NumeroSerie,
                    NumeroParte = a.NumeroParte,
                    FechaAdquisicion = a.FechaAdquisicion,
                    FechaGarantiaFin = a.FechaGarantiaFin,
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

        // 🧠 Ordenamiento dinámico
        private IQueryable<Activo> ApplyOrdering(
            IQueryable<Activo> query,
            string? columna,
            bool ascendente)
        {
            return columna switch
            {
                "CodigoActivo" => ascendente ? query.OrderBy(a => a.CodigoActivo) : query.OrderByDescending(a => a.CodigoActivo),
                "FechaAdquisicion" => ascendente ? query.OrderBy(a => a.FechaAdquisicion) : query.OrderByDescending(a => a.FechaAdquisicion),
                "ValorCompra" => ascendente ? query.OrderBy(a => a.ValorCompra) : query.OrderByDescending(a => a.ValorCompra),
                "EstadoActivo" => ascendente ? query.OrderBy(a => a.EstadoActivo) : query.OrderByDescending(a => a.EstadoActivo),
                _ => query.OrderByDescending(a => a.FechaAdquisicion)
            };
        }


    }
}
