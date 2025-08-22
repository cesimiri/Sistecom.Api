using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IActivo
    {
        IEnumerable<Activo> ActivoInfoAll { get; }
        Activo GetActivoById(int IdActivo);

        (int Insertados, int Fallidos, List<string> DetalleErrores) InsertActivos(IEnumerable<ActivoDTO> nuevosActivos);


        void UpdateActivo(Activo UpdItem);
        void DeleteActivoById(int IdActivo);

        //PAGINADO
        PagedResult<ActivoDTO> GetActivoPaginados(
       int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null
        );

    }
}
