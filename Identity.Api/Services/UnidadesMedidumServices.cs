using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class UnidadesMedidumServices : IUnidadesMedidum
    {
        private UnidadesMedidumRepository _cargo = new UnidadesMedidumRepository();

        public IEnumerable<UnidadesMedidum> GetAllUnidades()
        {
            return _cargo.GetAllUnidades();
        }


        public UnidadesMedidum GetUnidadesById(int idUnidades)
        {
            return _cargo.GetUnidadesById(idUnidades);
        }

        public void InsertUnidades(UnidadesMedidumDTO dto)
        {
        _cargo.InsertUnidades(dto);
        }

        public void UpdateUnidades(UnidadesMedidum dto)
        {
         _cargo.UpdateUnidades(dto);
        }

        //public void DeleteUnidades(UnidadesMedidumDTO DelItem)
        //{
        //    _cargo.DeleteCargo(DelItem);
        //}

        public void DeleteUnidadesById(int idUnidades)
        {
            _cargo.DeleteUnidadesById(idUnidades);
        }

        //paginado
        public PagedResult<UnidadesMedidumDTO> GetUnidadesMedidumPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            return _cargo.GetUnidadesMedidumPaginados(pagina, pageSize, filtro, estado);
        }

    }
}
