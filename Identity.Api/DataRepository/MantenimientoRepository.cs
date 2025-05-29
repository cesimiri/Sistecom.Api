using Modelo.Sistecom.Modelo.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Identity.Api.DataRepository
{
    public class MantenimientoRepository
    {
        public List<Mantenimiento> MantenimientoInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.Mantenimientos.ToList();
            }
        }

        public Mantenimiento GetMantenimientoById(int IdMantenimiento)
        {
            using (var context = new InvensisContext())
            {
                return context.Mantenimientos.FirstOrDefault(a => a.IdActivo == IdMantenimiento);
            }
        }

        public void InsertMantenimiento(Mantenimiento newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.Mantenimientos.Add(newActivo);
                context.SaveChanges();
            }
        }

        public void UpdateMantenimiento(Mantenimiento updItem)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Mantenimientos.FirstOrDefault(a => a.IdMantenimiento == updItem.IdMantenimiento);
                if (existente != null)
                {
                    existente.IdActivo = updItem.IdActivo;
                    existente.FechaProgramada = updItem.FechaProgramada;
                    existente.FechaRealizada = updItem.FechaRealizada;
                    existente.TipoMantenimiento = updItem.TipoMantenimiento;
                    existente.Descripcion = updItem.Descripcion;
                    existente.Diagnostico = updItem.Diagnostico;
                    existente.AccionesRealizadas = updItem.AccionesRealizadas;
                    existente.RepuestosUsados = updItem.RepuestosUsados;
                    existente.CostoManoObra = updItem.CostoManoObra;
                    existente.CostoRepuestos = updItem.CostoRepuestos;
                    existente.CostoTotal = updItem.CostoTotal;
                    existente.TiempoFueraServicioHoras = updItem.TiempoFueraServicioHoras;
                    existente.TecnicoResponsable = updItem.TecnicoResponsable;
                    existente.ProveedorServicio = updItem.ProveedorServicio;
                    existente.NumeroOrdenServicio = updItem.NumeroOrdenServicio;
                    existente.GarantiaTrabajosDias = updItem.GarantiaTrabajosDias;
                    existente.ProximoMantenimiento = updItem.ProximoMantenimiento;
                    existente.Estado = updItem.Estado;
                    existente.InformeTecnico = updItem.InformeTecnico;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteMantenimiento(Mantenimiento activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Mantenimientos.Remove(activoToDelete);
                context.SaveChanges();
            }
        }


        public void DeleteMantenimientoById(int IdMantenimiento)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Mantenimientos.FirstOrDefault(a => a.IdMantenimiento == IdMantenimiento);
                if (existente != null)
                {
                    context.Mantenimientos.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
