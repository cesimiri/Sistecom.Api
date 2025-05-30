using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IOrdenesEnsamblaje
    {
        IEnumerable<OrdenesEnsamblaje> OrdenesEnsamblajeInfoAll { get; }
        OrdenesEnsamblaje GetOrdenesEnsamblajeById(int IdOrdenesEnsamblaje);
        void InsertOrdenesEnsamblaje(OrdenesEnsamblaje New);
        void UpdateOrdenesEnsamblaje(OrdenesEnsamblaje UpdItem);
        void DeleteOrdenesEnsamblaje(OrdenesEnsamblaje DelItem);
        void DeleteOrdenesEnsamblajeById(int IdOrdenesEnsamblaje);
    }
}
