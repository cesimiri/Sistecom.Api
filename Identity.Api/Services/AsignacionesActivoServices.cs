using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class AsignacionesActivoServices : IAsignacionesActivo
    {
        private AsignacionesActivoRepository _dataRepository = new AsignacionesActivoRepository();

        public IEnumerable<AsignacionesActivo> AsignacionesActivoInfoAll
        {
            get { return _dataRepository.AsignacionesActivoInfoAll(); }
        }

        public AsignacionesActivo GetAsignacionesActivoById(int IdAsignacionesActivo)
        {
            return _dataRepository.GetAsignacionesActivoById(IdAsignacionesActivo);
        }

        public void InsertAsignacionesActivo(AsignacionesActivo New)
        {
            _dataRepository.InsertAsignacionesActivo(New);
        }

        public void UpdateAsignacionesActivo(AsignacionesActivo UpdItem)
        {
            _dataRepository.UpdateAsignacionesActivo(UpdItem);
        }

        public void DeleteAsignacionesActivo(AsignacionesActivo DelItem)
        {
            _dataRepository.DeleteAsignacionesActivo(DelItem);
        }

        public void DeleteAsignacionesActivoById(int IdAsignacionesActivo)
        {
            _dataRepository.DeleteAsignacionesActivoById(IdAsignacionesActivo);
        }
    }
}
