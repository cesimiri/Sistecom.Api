using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class OrdenesEnsamblajeServices : IOrdenesEnsamblaje
    {
        private OrdenesEnsamblajeRepository _dataRepository = new OrdenesEnsamblajeRepository();

        public IEnumerable<OrdenesEnsamblaje> OrdenesEnsamblajeInfoAll
        {
            get { return _dataRepository.OrdenesEnsamblajeInfoAll(); }
        }

        public OrdenesEnsamblaje GetOrdenesEnsamblajeById(int idOrdenesEnsamblaje)
        {
            return _dataRepository.GetOrdenesEnsamblajeById(idOrdenesEnsamblaje);
        }

        public void InsertOrdenesEnsamblaje(OrdenesEnsamblaje New)
        {
            _dataRepository.InsertOrdenesEnsamblaje(New);
        }

        public void UpdateOrdenesEnsamblaje(OrdenesEnsamblaje UpdItem)
        {
            _dataRepository.UpdateOrdenesEnsamblaje(UpdItem);
        }

        public void DeleteOrdenesEnsamblaje(OrdenesEnsamblaje DelItem)
        {
            _dataRepository.DeleteOrdenesEnsamblaje(DelItem);
        }

        public void DeleteOrdenesEnsamblajeById(int idOrdenesEnsamblaje)
        {
            _dataRepository.DeleteOrdenesEnsamblajeById(idOrdenesEnsamblaje);
        }
    }
}
