using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.DataRepository
{
    public class CargoRepository
    {
        public List<Cargo> GetAllCargo()
        {
            using (var context = new InvensisContext())
            {
                return context.Cargos.ToList();
            }
        }

        public Cargo GetCargoById(int  id)
        {
            using (var context = new InvensisContext())
            {
                return context.Cargos.FirstOrDefault(p => p.IdCargo == id); ;
            }

        }

        public void InsertCargo(Cargo NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.Cargos.Add(NewItem);
                context.SaveChanges();
            }
        }


        public void UpdateCargo(Cargo UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Cargos
                                         .Where(a => a.IdCargo == UpdItem.IdCargo)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.CodigoCargo = UpdItem.CodigoCargo;
                    registrado.NombreCargo = UpdItem.NombreCargo;
                    registrado.Descripcion = UpdItem.Descripcion;
                    registrado.NivelJerarquico = UpdItem.NivelJerarquico;
                    registrado.PuedeAutorizarCompras = UpdItem.PuedeAutorizarCompras;
                    registrado.LimiteAutorizacion = UpdItem.LimiteAutorizacion;
                    
                    registrado.Estado = UpdItem.Estado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteCargo(Cargo NewItem)
        {

            using (var context = new InvensisContext())
            {
                context.Cargos.Remove(NewItem);
                context.SaveChanges();
            }
        }


        public void DeleteCargoById(int id)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Cargos
                                         .Where(a => a.IdCargo == id)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.Cargos.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}
