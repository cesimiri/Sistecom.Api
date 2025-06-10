using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{
    public class CargoServices : ICargo
    {
        private CargoRepository _cargo = new CargoRepository();

        public IEnumerable<Cargo> GetAllCargo
        {
            get { return _cargo.GetAllCargo(); }
        }

        public Cargo GetCargoById(int id)
        {
            return _cargo.GetCargoById(id);
        }

        public void InsertCargo(Cargo New)
        {
            _cargo.InsertCargo(New);
        }

        public void UpdateCargo(Cargo UpdItem)
        {
            _cargo.UpdateCargo(UpdItem);
        }

        public void DeleteCargo(Cargo DelItem)
        {
            _cargo.DeleteCargo(DelItem);
        }

        public void DeleteCargoById(int id)
        {
            _cargo.DeleteCargoById(id);
        }
    }
}
