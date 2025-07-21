using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{
    public class ActivoServices : IActivo
    {
        private ActivoRepository _dataRepository = new ActivoRepository();

        public IEnumerable<Activo> ActivoInfoAll
        {
            get { return _dataRepository.ActivoInfoAll(); }
        }

        public Activo GetActivoById(int IdActivo)
        {
            return _dataRepository.GetActivoById(IdActivo);
        }

        public void InsertActivo(Activo New)
        {
            _dataRepository.InsertActivo(New);
        }

        public void UpdateActivo(Activo UpdItem)
        {
            _dataRepository.UpdateActivo(UpdItem);
        }

        public void DeleteActivo(Activo DelItem)
        {
            _dataRepository.DeleteActivo(DelItem);
        }

        public void DeleteActivoById(int IdActivo)
        {
            _dataRepository.DeleteActivoById(IdActivo);
        }


        //paginado
        public PagedResult<ActivoDTO> GetPaginados(
        int pagina,
        int pageSize,
        string? codigoActivo,
        int? idProducto,
        DateTime? desde,
        DateTime? hasta,
        int? idFacturaCompra,
        string? estadoActivo,
        string? ordenColumna = null,
        bool ordenAscendente = true)
        {
            return _dataRepository.GetPaginados(
                pagina, pageSize,
                codigoActivo, idProducto,
                desde, hasta,
                idFacturaCompra, estadoActivo,
                ordenColumna, ordenAscendente
            );
        }

    }
}
