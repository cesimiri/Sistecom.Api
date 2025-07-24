using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IBodega
    {
        IEnumerable<Bodega> BodegaInfoAll { get; }
        Bodega GetBodegaById(int idBodega);
        void InsertBodega(BodegaDTO New);
        void UpdateBodega(Bodega UpdItem);
        //void DeleteBodega(Bodega DelItem);
        void DeleteBodegaById(int idBodega);

        // Nuevo método para paginado:
        PagedResult<BodegaDTO> GetBodegaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
        // solo trae personal de sistecom por ruc de sistecom
        //IEnumerable<UsuarioDTO> GetUsuarioSistecom();

        //traer todas las bodegas por responsable 
        IEnumerable<BodegaDTO> GetBodegasPorResponsable(string correo);
    }
}
