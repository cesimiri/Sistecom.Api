using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class ComponentesEnsamblajeRepository
    {
        public List<ComponentesEnsamblaje> ComponentesEnsamblajeInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.ComponentesEnsamblajes.ToList();
            }
        }

        public ComponentesEnsamblaje GetComponentesEnsamblajeById(int IdComponentesEnsamblaje)
        {
            using (var context = new InvensisContext())
            {
                return context.ComponentesEnsamblajes.FirstOrDefault(m => m.IdComponenteEnsamblaje == IdComponentesEnsamblaje);
            }
        }

        public void InsertComponentesEnsamblaje(ComponentesEnsamblaje nuevoMovimiento)
        {
            using (var context = new InvensisContext())
            {
                context.ComponentesEnsamblajes.Add(nuevoMovimiento);
                context.SaveChanges();
            }
        }


        public void UpdateComponentesEnsamblaje(ComponentesEnsamblaje componenteActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.ComponentesEnsamblajes.FirstOrDefault(a => a.IdComponenteEnsamblaje == componenteActualizado.IdComponenteEnsamblaje);
                if (existente != null)
                {
                    existente.IdProductoFinal = componenteActualizado.IdProductoFinal;
                    existente.IdComponente = componenteActualizado.IdComponente;
                    existente.CantidadRequerida = componenteActualizado.CantidadRequerida;
                    existente.EsObligatorio = componenteActualizado.EsObligatorio;
                    existente.OrdenEnsamblaje = componenteActualizado.OrdenEnsamblaje;
                    existente.Instrucciones = componenteActualizado.Instrucciones;


                    context.SaveChanges();
                }
            }
        }

        public void DeleteComponentesEnsamblaje(ComponentesEnsamblaje activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.ComponentesEnsamblajes.Remove(activoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteComponentesEnsamblajeById(int idComponentesEnsamblaje)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.ComponentesEnsamblajes.FirstOrDefault(a => a.IdComponenteEnsamblaje == idComponentesEnsamblaje);
                if (existente != null)
                {
                    context.ComponentesEnsamblajes.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
