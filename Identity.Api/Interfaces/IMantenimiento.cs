using Identity.Api.DTO;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IMantenimiento
    {

        Mantenimiento GetMantenimientoById(int IdMantenimiento);
        void InsertMantenimiento(MantenimientoDTO New);
        void UpdateMantenimiento(MantenimientoDTO UpdItem);
        void DeleteMantenimientoById(int IdMantenimiento);

        //busqueda de tecnico por cedula 
        UsuarioDTO ObtenerApellidosNombreByCedula(string cedula);

        //busqueda por codigo de activo 
        ActivoDTO? ObtnerActivoByCodigo(string codigoActivo);

        //paginado 
        PagedResult<MantenimientoDTO> GetMantenimientoPaginados(
        int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null
        );
    }
}
