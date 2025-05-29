using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IOrdenesEntrega
    {
        IEnumerable<OrdenesEntrega> OrdenesEntregaInfoAll { get; }
        OrdenesEntrega GetOrdenesEntregaById(int IdOrdenesEntrega);
        void InsertOrdenesEntrega(OrdenesEntrega New);
        void UpdateOrdenesEntrega(OrdenesEntrega UpdItem);
        void DeleteOrdenesEntrega(OrdenesEntrega DelItem);
        void DeleteOrdenesEntregaById(int IdOrdenesEntrega);
    }
}
