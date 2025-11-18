using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class OrdenesEntregaServices : IOrdenesEntrega
    {
        private OrdenesEntregaRepository _dataRepository = new OrdenesEntregaRepository();

        //traer todas las solicitudes de compra que no esten registradas aqui en ordenes de Entrega
        public List<SolicitudesCompraDTO> GetSolicitudesAprobadasSinOrden()
        {
            return _dataRepository.GetSolicitudesAprobadasSinOrden();
        }

        //trae detalle de usuario por departamento 
        public List<UsuarioDetalleDTO> GetUsuarioDetalleById(string cedula)
        {
            return _dataRepository.GetUsuarioDetalleById(cedula);
        }

        public OrdenesEntrega GetOrdenesEntregaById(int IdOrdenesEntrega)
        {
            return _dataRepository.GetOrdenesEntregaById(IdOrdenesEntrega);
        }

        public void InsertOrdenesEntrega(OrdenesEntregaDTO New)
        {
            _dataRepository.InsertOrdenesEntrega(New);
        }

        public void UpdateOrdenesEntrega(OrdenesEntrega UpdItem)
        {
            _dataRepository.UpdateOrdenesEntrega(UpdItem);
        }

        public void DeleteOrdenesEntregaById(int IdOrdenesEntega)
        {
            _dataRepository.DeleteOrdenesEntregaById(IdOrdenesEntega);
        }

        //paginado
        public PagedResult<OrdenesEntregaDTO> GetOrdenesEntregaPaginadas(
        int pagina,
            int pageSize,
            string? filtro = null,
            string? estadoActivo = null)
        {
            return _dataRepository.GetOrdenesEntregaPaginadas(
                pagina, pageSize,
                filtro, estadoActivo
            );
        }

    }
}
