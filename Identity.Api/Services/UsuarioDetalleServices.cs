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

        public List<UsuarioDetalleDTO> GetUsuarioDetalleById(string cedula)
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

        //eliminar por cedula idDepartamento idCargo
        public void DeleteUsuarioDetalle(string cedula, int idDepartamento, int idCargo)
        {
            _dataRepository.DeleteUsuarioDetalle(cedula, idDepartamento, idCargo);
        }

        //paginado
        public PagedResult<UsuarioDetalleDTO> GetUsuarioDetallePaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetUsuarioDetallePaginados(pagina, pageSize, filtro, estado);
        }

        //exportar
        public List<UsuarioDetalleDTO> ObtenerUsuarioDetalleFiltradas(string? filtro, string? estado)
        {
            return _dataRepository.ObtenerUsuarioDetalleFiltradas(filtro, estado);
        }
    }
}
