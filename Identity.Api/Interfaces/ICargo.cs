using Identity.Api.DTO;
using Identity.Api.Paginado;
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

        // Nuevo método para paginado:
        PagedResult<Cargo> GetCargoPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);
    }
}
