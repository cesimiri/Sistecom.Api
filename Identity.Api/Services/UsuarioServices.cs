using identity.api.datarepository;
using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{
    public class UsuarioService : IUsuario
    {
        private UsuarioDataRepository _dataRepository = new UsuarioDataRepository();

        public IEnumerable<UsuarioDTO> GetAllUsuarios
        {
            get { return _dataRepository.GetAllUsuarios(); }
        }

        public UsuarioDTO GetUsuarioById(int idUsuario)
        {
            return _dataRepository.GetUsuarioById(idUsuario);
        }

        public void InsertUsuario(UsuarioDTO usuario)
        {
            _dataRepository.InsertUsuario(usuario);
        }

        public void UpdateUsuario(UsuarioDTO usuario)
        {
            _dataRepository.UpdateUsuario(usuario);
        }

        //public void DeleteUsuario(UsuarioDTO usuario)
        //{
        //    _dataRepository.DeleteUsuario(usuario);
        //}

        public void DeleteUsuarioById(int IdUsuario)
        {
            _dataRepository.DeleteUsuarioById(IdUsuario);
        }

        //traer datos por la empresa seleccionada
        public IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        {
            return _dataRepository.ObtenerSucursalesByRuc(RucEmpresa);
        }

        //traer departamentos por la sucursal
        public IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        {
            return _dataRepository.ObtenerDepartamentosBySucursal(idSucursal);
        }

        //paginado
        public PagedResult<UsuarioDTO> GetUsuariosPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _dataRepository.GetUsuariosPaginados(pagina, pageSize, filtro, estado);
        }
    }
}
