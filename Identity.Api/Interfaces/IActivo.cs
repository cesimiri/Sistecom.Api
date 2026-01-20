using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IActivo
    {
        IEnumerable<Activo> ActivoInfoAll { get; }
        Activo GetActivoById(int IdActivo);

        //insertar
        Task<List<SpResponseDTO>> InsertActivos(List<ActivoDTO> activos);

        void UpdateActivo(Activo UpdItem);

        //update 1 a 1 
        void Update1a1(ActivoDTO updActivo);

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
