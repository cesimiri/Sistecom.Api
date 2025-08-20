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

        //inserción masiva 
        //public void InsertActivos(List<ActivoDTO> activosDto)
        //{
        //    using var context = new InvensisContext();
        //    using var transaction = context.Database.BeginTransaction();

        //    try
        //    {
        //        foreach (var dto in activosDto)
        //        {
        //            // Validar producto
        //            var producto = context.Productos.Find(dto.IdProducto)
        //                ?? throw new Exception($"El IdProducto {dto.IdProducto} no existe.");

        //            // Prefijo de 3 letras
        //            string prefijo = new string(
        //                producto.Nombre.Trim().Replace(" ", "").ToUpper().Take(3).ToArray()
        //            );

        //            // Último código para este prefijo
        //            var lastCodigo = context.Activos
        //                .Where(a => a.CodigoActivo.StartsWith(prefijo + "-"))
        //                .OrderByDescending(a => a.CodigoActivo)
        //                .Select(a => a.CodigoActivo)
        //                .FirstOrDefault();

        //            int nextNumber = 1;
        //            if (!string.IsNullOrEmpty(lastCodigo))
        //            {
        //                var lastNumberStr = lastCodigo.Split('-').Last();
        //                if (int.TryParse(lastNumberStr, out var parsedNumber))
        //                    nextNumber = parsedNumber + 1;
        //            }

        //            string nuevoCodigo = $"{prefijo}-{nextNumber:D4}";

        //            // Crear activo
        //            var nuevoActivo = new Activo
        //            {
        //                CodigoActivo = nuevoCodigo,
        //                IdProducto = dto.IdProducto,
        //                NumeroSerie = dto.NumeroSerie,
        //                NumeroParte = dto.NumeroParte,
        //                FechaAdquisicion = dto.FechaAdquisicion,         // <-- DateOnly directo
        //                FechaGarantiaFin = dto.FechaGarantiaFin,
        //                IdFacturaCompra = dto.IdFacturaCompra,
        //                IdOrdenEnsamblaje = dto.IdOrdenEnsamblaje,
        //                ValorCompra = dto.ValorCompra,
        //                ValorResidual = dto.ValorResidual,
        //                VidaUtilMeses = dto.VidaUtilMeses,
        //                UbicacionActual = dto.UbicacionActual?.ToUpper(),
        //                EstadoActivo = dto.EstadoActivo,
        //                CondicionFisica = dto.CondicionFisica,
        //                EsServidor = dto.EsServidor,
        //                Observaciones = dto.Observaciones?.ToUpper(),
        //                FechaRegistro = DateTime.Now
        //            };

        //            context.Activos.Add(nuevoActivo);
        //        }

        //        context.SaveChanges();
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        Console.WriteLine(ex.ToString()); // log completo
        //        throw new Exception("Error al insertar activos: " + ex.Message);

        //    }
        //}
        public (int Insertados, int Fallidos, List<string> DetalleErrores) InsertActivos(IEnumerable<ActivoDTO> activosDto)
        {
            var errores = new List<string>();
            int exitos = 0;

            using var context = new InvensisContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                // Traer todos los productos y facturas de una sola vez
                var idsProductos = activosDto.Select(a => a.IdProducto).Distinct().ToList();
                var productos = context.Productos
                    .Where(p => idsProductos.Contains(p.IdProducto))
                    .ToDictionary(p => p.IdProducto, p => p);

                var idsFacturas = activosDto
                    .Where(a => a.IdFacturaCompra.HasValue)
                    .Select(a => a.IdFacturaCompra!.Value)
                    .Distinct()
                    .ToList();

                var facturas = context.FacturasCompras
                    .Where(f => idsFacturas.Contains(f.IdFactura))
                    .Select(f => f.IdFactura)
                    .ToHashSet();

                // Prefijos para código activo
                var prefijos = productos.Values
                    .Select(p => new string(p.Nombre.Trim().Replace(" ", "").ToUpper().Take(3).ToArray()))
                    .Distinct()
                    .ToList();

                var lastCodigosDict = context.Activos
                    .Where(a => prefijos.Any(pre => a.CodigoActivo.StartsWith(pre + "-")))
                    .AsEnumerable()
                    .GroupBy(a => a.CodigoActivo.Substring(0, 3))
                    .ToDictionary(
                        g => g.Key,
                        g => g.Max(a => a.CodigoActivo)
                    );

                var estadosValidos = new List<string> { "DISPONIBLE", "NUEVO", "USADO", "EN_REPARACION" };

                foreach (var dto in activosDto)
                {
                    try
                    {
                        // Validaciones
                        if (!productos.ContainsKey(dto.IdProducto))
                        {
                            errores.Add($"IdProducto {dto.IdProducto} no existe.");
                            continue;
                        }

                        if (dto.IdFacturaCompra.HasValue && !facturas.Contains(dto.IdFacturaCompra.Value))
                        {
                            errores.Add($"IdFacturaCompra {dto.IdFacturaCompra} no existe.");
                            continue;
                        }

                        if (!estadosValidos.Contains(dto.EstadoActivo?.ToUpper()))
                        {
                            errores.Add($"IdProducto {dto.IdProducto}: EstadoActivo '{dto.EstadoActivo}' no es válido.");
                            continue;
                        }

                        var producto = productos[dto.IdProducto];
                        string prefijo = new string(producto.Nombre.Trim().Replace(" ", "").ToUpper().Take(3).ToArray());

                        int nextNumber = 1;
                        if (lastCodigosDict.TryGetValue(prefijo, out var lastCodigo))
                        {
                            var lastNumberStr = lastCodigo.Split('-').Last();
                            if (int.TryParse(lastNumberStr, out var parsedNumber))
                                nextNumber = parsedNumber + 1;
                        }

                        string nuevoCodigo = $"{prefijo}-{nextNumber:D4}";
                        lastCodigosDict[prefijo] = nuevoCodigo;

                        var nuevoActivo = new Activo
                        {
                            CodigoActivo = nuevoCodigo,
                            IdProducto = dto.IdProducto,
                            NumeroSerie = dto.NumeroSerie,
                            NumeroParte = dto.NumeroParte,
                            FechaAdquisicion = DateOnly.FromDateTime(dto.FechaAdquisicion),
                            FechaGarantiaFin = dto.FechaGarantiaFin.HasValue ? DateOnly.FromDateTime(dto.FechaGarantiaFin.Value) : null,
                            IdFacturaCompra = dto.IdFacturaCompra,
                            IdOrdenEnsamblaje = dto.IdOrdenEnsamblaje,
                            ValorCompra = dto.ValorCompra,
                            ValorResidual = dto.ValorResidual,
                            VidaUtilMeses = dto.VidaUtilMeses,
                            UbicacionActual = dto.UbicacionActual?.ToUpper(),
                            EstadoActivo = dto.EstadoActivo.ToUpper(),
                            CondicionFisica = dto.CondicionFisica?.ToUpper(),
                            EsServidor = dto.EsServidor,
                            Observaciones = dto.Observaciones?.ToUpper(),
                            FechaRegistro = DateTime.Now
                        };

                        context.Activos.Add(nuevoActivo);
                        context.SaveChanges();
                        exitos++;
                    }
                    catch (Exception exFila)
                    {
                        errores.Add($"Error en IdProducto {dto.IdProducto}: {exFila.Message} | Detalle: {exFila.InnerException?.Message}");
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                errores.Add("Error general: " + ex.Message + " | Detalle: " + ex.InnerException?.Message);
            }

            return (Insertados: exitos, Fallidos: errores.Count, DetalleErrores: errores);
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
                    //FechaAdquisicion = a.FechaAdquisicion,
                    //FechaGarantiaFin = a.FechaGarantiaFin,
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
