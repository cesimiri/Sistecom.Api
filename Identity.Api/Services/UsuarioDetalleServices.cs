using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;

namespace Identity.Api.Services
{
    public class UsuarioDetalleServices : IUsuarioDetalle
    {
        private UsuarioDetalleRepository _dataRepository = new UsuarioDetalleRepository();

        public IEnumerable<UsuarioDetalleDTO> GetAllUsuarioDetalle
        {
            get { return _dataRepository.GetAllUsuarioDetalle(); }
        }

        public UsuarioDetalleDTO GetUsuarioDetalleById(string cedula)
        {
            return _dataRepository.GetUsuarioDetalleById(cedula);
        }

        public void InsertUsuarioDetalle(UsuarioDetalleDTO usuario)
        {
            _dataRepository.InsertUsuarioDetalle(usuario);
        }

        public void UpdateUsuarioDetalle(UsuarioDetalleDTO usuario)
        {
            _dataRepository.UpdateUsuarioDetalle(usuario);
        }

        //public void DeleteUsuario(UsuarioDTO usuario)
        //{
        //    _dataRepository.DeleteUsuario(usuario);
        //}

        public void DeleteUsuarioDetalleById(string cedula)
        {
            _dataRepository.DeleteUsuarioDetalleById(cedula);
        }

        //traer datos por la empresa seleccionada
        //public IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        //{
        //    return _dataRepository.ObtenerSucursalesByRuc(RucEmpresa);
        //}

        //traer departamentos por la sucursal
        //public IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        //{
        //    return _dataRepository.ObtenerDepartamentosBySucursal(idSucursal);
        //}

        //paginado
        public PagedResult<UsuarioDetalleDTO> GetUsuarioDetallePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetUsuarioDetallePaginados(pagina, pageSize, filtro, estado);
        }
    }
}
