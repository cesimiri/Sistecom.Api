using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class OrdenesEntregaServices : IOrdenesEntrega
    {
        private OrdenesEntregaRepository _dataRepository = new OrdenesEntregaRepository();

        public IEnumerable<OrdenesEntrega> OrdenesEntregaInfoAll
        {
            get { return _dataRepository.OrdenesEntregaInfoAll(); }
        }

        public OrdenesEntrega GetOrdenesEntregaById(int IdOrdenesEntrega)
        {
            return _dataRepository.GetOrdenesEntregaById(IdOrdenesEntrega);
        }

        public void InsertOrdenesEntrega(OrdenesEntrega New)
        {
            _dataRepository.InsertOrdenesEntrega(New);
        }

        public void UpdateOrdenesEntrega(OrdenesEntrega UpdItem)
        {
            _dataRepository.UpdateOrdenesEntrega(UpdItem);
        }

        public void DeleteOrdenesEntrega(OrdenesEntrega DelItem)
        {
            _dataRepository.DeleteOrdenesEntrega(DelItem);
        }

        public void DeleteOrdenesEntregaById(int IdOrdenesEntega)
        {
            _dataRepository.DeleteOrdenesEntregaById(IdOrdenesEntega);
        }
    }
}
