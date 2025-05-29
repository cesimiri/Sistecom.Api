using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IActivo
    {
        IEnumerable<Activo> ActivoInfoAll { get; }
        Activo GetActivoById(int IdActivo);
        void InsertActivo(Activo New);
        void UpdateActivo(Activo UpdItem);
        void DeleteActivo(Activo DelItem);
        void DeleteActivoById(int IdActivo);
    }
}
