using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IDetalleEnsamblaje
    {
        IEnumerable<DetalleEnsamblaje> DetalleEnsamblajeInfoAll { get; }
        DetalleEnsamblaje GetDetalleEnsamblajeById(int IdDetalleEnsamblaje);
        void InsertDetalleEnsamblaje(DetalleEnsamblaje New);
        void UpdateDetalleEnsamblaje(DetalleEnsamblaje UpdItem);
        void DeleteDetalleEnsamblaje(DetalleEnsamblaje DelItem);
        void DeleteDetalleEnsamblajeById(int IdDetalleEnsamblaje);
    }
}
