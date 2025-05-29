using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class MantenimientoServices : IMantenimiento
    {
        private MantenimientoRepository _dataRepository = new MantenimientoRepository();

        public IEnumerable<Mantenimiento> MantenimientoInfoAll
        {
            get { return _dataRepository.MantenimientoInfoAll(); }
        }

        public Mantenimiento GetMantenimientoById(int IdMantenimiento)
        {
            return _dataRepository.GetMantenimientoById(IdMantenimiento);
        }

        public void InsertMantenimiento(Mantenimiento New)
        {
            _dataRepository.InsertMantenimiento(New);
        }

        public void UpdateMantenimiento(Mantenimiento UpdItem)
        {
            _dataRepository.UpdateMantenimiento(UpdItem);
        }

        public void DeleteMantenimiento(Mantenimiento DelItem)
        {
            _dataRepository.DeleteMantenimiento(DelItem);
        }

        public void DeleteMantenimientoById(int IdMantenimiento)
        {
            _dataRepository.DeleteMantenimientoById(IdMantenimiento);
        }
    }
}
