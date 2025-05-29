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

        public void InsertActivo(Activo newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.Activos.Add(newActivo);
                context.SaveChanges();
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
                    existente.Modelo = updActivo.Modelo;
                    existente.Marca = updActivo.Marca;
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
                    existente.FechaRegistro = updActivo.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteActivo(Activo activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Activos.Remove(activoToDelete);
                context.SaveChanges();
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

    }
}
