using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IDetalleOrdenEntrega
    {
        IEnumerable<DetalleOrdenEntrega> DetalleOrdenEntregaInfoAll { get; }
        DetalleOrdenEntrega GetDetalleOrdenEntregaById(int IdDetalleOrdenEntrega);
        void InsertDetalleOrdenEntrega(DetalleOrdenEntrega New);
        void UpdateDetalleOrdenEntrega(DetalleOrdenEntrega UpdItem);
        void DeleteDetalleOrdenEntrega(DetalleOrdenEntrega DelItem);
        void DeleteDetalleOrdenEntregaById(int IdDetalleOrdenEntrega);
    }
}
