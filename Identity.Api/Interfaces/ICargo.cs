using Modelo.Sistecom.Modelo.Database;


namespace Identity.Api.Interfaces
{
    public interface ICargo
    {
        IEnumerable<Cargo> GetAllCargo { get; }
        Cargo GetCargoById(int id);
        void InsertCargo(Cargo New);
        void UpdateCargo(Cargo UpdItem);
        void DeleteCargo(Cargo DelItem);
        void DeleteCargoById(int id);
    }
}
