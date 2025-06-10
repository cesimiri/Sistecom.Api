using Identity.Api.DTO;

namespace Identity.Api.Interfaces
{
    public interface ISucursale
    {
        IEnumerable<SucursaleDTO> GetAllSucursale();
        SucursaleDTO? GetSucursaleById(int idSucursal);
        void InsertSucursale(SucursaleDTO dto);
        void UpdateSucursale(SucursaleDTO dto);
        //void DeleteSuscripcion(SuscripcionDto dto);
        void DeleteSucursaleById(int idSucursal);
    }
}
