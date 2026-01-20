using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class MantenimientoServices : IMantenimiento
    {
        private MantenimientoRepository _dataRepository = new MantenimientoRepository();



        public Mantenimiento GetMantenimientoById(int IdMantenimiento)
        {
            return _dataRepository.GetMantenimientoById(IdMantenimiento);
        }

        public void InsertMantenimiento(MantenimientoDTO New)
        {
            _dataRepository.InsertMantenimiento(New);
        }

        public void UpdateMantenimiento(MantenimientoDTO UpdItem)
        {
            _dataRepository.UpdateMantenimiento(UpdItem);
        }

        public void DeleteMantenimientoById(int IdMantenimiento)
        {
            _dataRepository.DeleteMantenimientoById(IdMantenimiento);
        }

        //busqueda de tecnico por cedula 
        public UsuarioDTO ObtenerApellidosNombreByCedula(string cedula)
        {
            return _dataRepository.ObtenerApellidosNombreByCedula(cedula);
        }

        //busqueda por codigo activo 
        public ActivoDTO? ObtnerActivoByCodigo(string codigoActivo)
        {
            return _dataRepository.ObtnerActivoByCodigo(codigoActivo);
        }

        //paginado
        public PagedResult<MantenimientoDTO> GetMantenimientoPaginados(
        int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null)
        {
            return _dataRepository.GetMantenimientoPaginados(
                pagina, pageSize,
                filtro, estadoActivo
            );
        }

    }
}
