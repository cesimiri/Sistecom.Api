using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{

    public class HistorialActivoRepository
    {
        public List<HistorialActivo> HistorialActivoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.HistorialActivos.ToList();
            }
        }

        public HistorialActivo GetHistorialActivoById(int IdHistorialActivo)
        {
            using (var context = new InvensisContext())
            {
                return context.HistorialActivos.FirstOrDefault(a => a.IdHistorial == IdHistorialActivo);
            }
        }

        public void InsertHistorialActivo(HistorialActivo newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.HistorialActivos.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateHistorialActivo(HistorialActivo historial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.HistorialActivos.FirstOrDefault(a => a.IdHistorial == historial.IdHistorial);
                if (existente != null)
                {
                    existente.IdActivo = historial.IdActivo;
                    existente.TipoEvento = historial.TipoEvento;
                    existente.FechaEvento = historial.FechaEvento;
                    existente.Descripcion = historial.Descripcion;
                    //existente.IdUsuarioResponsable = historial.IdUsuarioResponsable;
                    existente.IdDocumentoReferencia = historial.IdDocumentoReferencia;
                    existente.CostoAsociado = historial.CostoAsociado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteHistorialActivo(HistorialActivo activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.HistorialActivos.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteHistorialActivoById(int idHistorial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.HistorialActivos.FirstOrDefault(a => a.IdHistorial == idHistorial);
                if (existente != null)
                {
                    context.HistorialActivos.Remove(existente);
                    context.SaveChanges();
                }
            }
        }

        //busqueda por codigo de activo 
        public ActivoDTO? ObtnerActivoByCodigo(string codigoActivo)
        {
            using var context = new InvensisContext();

            return context.Activos
                .Where(a => a.CodigoActivo == codigoActivo)
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

                    // Relaciones
                    NombreProducto = a.IdProductoNavigation != null
                        ? a.IdProductoNavigation.Nombre
                        : null,

                    NumeroFactura = a.IdFacturaCompraNavigation != null
                        ? a.IdFacturaCompraNavigation.NumeroFactura
                        : null,

                    NumeroOrden = a.IdOrdenEnsamblajeNavigation != null
                        ? a.IdOrdenEnsamblajeNavigation.NumeroOrden
                        : null,


                    TipoRelacion = a.TipoRelacion,
                    IdActivoPadre = a.IdActivoPadre,
                    EsComponente = a.EsComponente
                })
                .FirstOrDefault();
        }

        //obtener por evento
        public object? ObtenerInfoPorEvento(string tipoEvento, int idActivo)
        {
            using var context = new InvensisContext();

            return tipoEvento switch
            {
                "COMPRA" =>
                    context.Activos
                        .Where(a => a.IdActivo == idActivo)
                        .Select(a => new ActivoDTO
                        {
                            IdActivo = a.IdActivo,
                            CodigoActivo = a.CodigoActivo,
                            EstadoActivo = a.EstadoActivo
                        })
                        .FirstOrDefault(),

                "BAJA" =>
                    context.Activos
                        .Where(a => a.IdActivo == idActivo && a.EstadoActivo == "BAJA")
                        .Select(a => new ActivoDTO
                        {
                            IdActivo = a.IdActivo,
                            CodigoActivo = a.CodigoActivo,
                            EstadoActivo = a.EstadoActivo
                        })
                        .FirstOrDefault(),

                "MANTENIMIENTO" =>
                    context.Mantenimientos
                        .Where(m => m.IdActivo == idActivo)
                        .Select(m => new MantenimientoDTO
                        {
                            IdMantenimiento = m.IdMantenimiento,
                            IdActivo = m.IdActivo,
                            FechaRealizada = m.FechaRealizada
                        })
                        .FirstOrDefault(),

                _ => null
            };
        }

    }
}
