using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IBodega
    {
        IEnumerable<Bodega> BodegaInfoAll { get; }
        Bodega GetBodegaById(int idBodega);
        void InsertBodega(BodegaDTO  New);
        void UpdateBodega(Bodega UpdItem);
        //void DeleteBodega(Bodega DelItem);
        void DeleteBodegaById(int idBodega);
    }
}
