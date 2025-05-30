using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class TiposLicenciumRepository
    {
        public List<TiposLicencium> TiposLicenciumInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.TiposLicencia.ToList();
            }
        }

        public TiposLicencium GetTiposLicenciumById(int IdTiposLicencium)
        {
            using (var context = new InvensisContext())
            {
                return context.TiposLicencia.FirstOrDefault(a => a.IdTipoLicencia == IdTiposLicencium);
            }
        }

        public void InsertTiposLicencium(TiposLicencium newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.TiposLicencia.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateTiposLicencium(TiposLicencium tipoActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.TiposLicencia.FirstOrDefault(a => a.IdTipoLicencia == tipoActualizado.IdTipoLicencia);
                if (existente != null)
                {
                    existente.Nombre = tipoActualizado.Nombre;
                    existente.Fabricante = tipoActualizado.Fabricante;
                    existente.Categoria = tipoActualizado.Categoria;
                    existente.TipoLicenciamiento = tipoActualizado.TipoLicenciamiento;
                    existente.PermiteMultipleUso = tipoActualizado.PermiteMultipleUso;
                    existente.Descripcion = tipoActualizado.Descripcion;
                    existente.Estado = tipoActualizado.Estado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteTiposLicencium(TiposLicencium activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.TiposLicencia.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteTiposLicenciumById(int idTiposLicencium)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.TiposLicencia.FirstOrDefault(a => a.IdTipoLicencia == idTiposLicencium);
                if (existente != null)
                {
                    context.TiposLicencia.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
