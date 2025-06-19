using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;

using System.Collections.Generic;

namespace Identity.Api.Services
{
    public class SolicitudesCompraService : ISolicitudesCompra
    {
        private readonly SolicitudesCompraDataRepository _dataRepository = new SolicitudesCompraDataRepository();

        public IEnumerable<SolicitudesCompraDTO> GetAllSolicitudesCompra
        {
            get { return _dataRepository.GetAllSolicitudesCompra(); }
        }

        public SolicitudesCompraDTO GetSolicitudById(int idSolicitud)
        {
            return _dataRepository.GetSolicitudById(idSolicitud);
        }

        public void InsertSolicitud(SolicitudesCompraDTO newSolicitud)
        {
            _dataRepository.InsertSolicitud(newSolicitud);
        }

        public void UpdateSolicitud(SolicitudesCompraDTO updatedSolicitud)
        {
            _dataRepository.UpdateSolicitud(updatedSolicitud);
        }

        //public void DeleteSolicitud(solic solicitudToDelete)
        //{
        //    _dataRepository.DeleteSolicitud(solicitudToDelete);
        //}

        public void DeleteSolicitudById(int idSolicitud)
        {
            _dataRepository.DeleteSolicitudById(idSolicitud);
        }

        public IEnumerable<UsuarioDTO> ObtenerUsuarioSolicitaAsync()
        {
            return _dataRepository.ObtenerUsuarioSolicitaAsync();
        }
    }
}
