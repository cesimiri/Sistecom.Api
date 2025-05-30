using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ComponentesEnsamblajeServices : IComponentesEnsamblaje
    {
        private ComponentesEnsamblajeRepository _dataRepository = new ComponentesEnsamblajeRepository();

        public IEnumerable<ComponentesEnsamblaje> ComponentesEnsamblajeInfoAll
        {
            get { return _dataRepository.ComponentesEnsamblajeInfoAll(); }
        }

        public ComponentesEnsamblaje GetComponentesEnsamblajeById(int idComponentesEnsamblaje)
        {
            return _dataRepository.GetComponentesEnsamblajeById(idComponentesEnsamblaje);
        }

        public void InsertComponentesEnsamblaje(ComponentesEnsamblaje New)
        {
            _dataRepository.InsertComponentesEnsamblaje(New);
        }

        public void UpdateComponentesEnsamblaje(ComponentesEnsamblaje UpdItem)
        {
            _dataRepository.UpdateComponentesEnsamblaje(UpdItem);
        }

        public void DeleteComponentesEnsamblaje(ComponentesEnsamblaje DelItem)
        {
            _dataRepository.DeleteComponentesEnsamblaje(DelItem);
        }

        public void DeleteComponentesEnsamblajeById(int idComponentesEnsamblaje)
        {
            _dataRepository.DeleteComponentesEnsamblajeById(idComponentesEnsamblaje);
        }
    }
}
