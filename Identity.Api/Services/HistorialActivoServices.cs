using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class HistorialActivoServices : IHistorialActivo
    {
        private HistorialActivoRepository _dataRepository = new HistorialActivoRepository();

        public IEnumerable<HistorialActivo> HistorialActivoInfoAll
        {
            get { return _dataRepository.HistorialActivoInfoAll(); }
        }

        public HistorialActivo GetHistorialActivoById(int IdHistorialActivo)
        {
            return _dataRepository.GetHistorialActivoById(IdHistorialActivo);
        }

        public void InsertHistorialActivo(HistorialActivo New)
        {
            _dataRepository.InsertHistorialActivo(New);
        }

        public void UpdateHistorialActivo(HistorialActivo UpdItem)
        {
            _dataRepository.UpdateHistorialActivo(UpdItem);
        }

        public void DeleteHistorialActivo(HistorialActivo DelItem)
        {
            _dataRepository.DeleteHistorialActivo(DelItem);
        }

        public void DeleteHistorialActivoById(int IdHistorialActivo)
        {
            _dataRepository.DeleteHistorialActivoById(IdHistorialActivo);
        }

        //busqueda por codigo activo 
        public ActivoDTO? ObtnerActivoByCodigo(string codigoActivo)
        {
            return _dataRepository.ObtnerActivoByCodigo(codigoActivo);
        }

        ////obtener por evento
        public object? ObtenerInfoPorEvento(string tipoEvento, int idActivo)
        {
            return _dataRepository.ObtenerInfoPorEvento(tipoEvento, idActivo);
        }
    }
}
