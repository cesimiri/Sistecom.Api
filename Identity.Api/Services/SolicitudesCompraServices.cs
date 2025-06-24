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


        public int InsertSolicitud(SolicitudesCompraDTO newSolicitud)
        {
            return _dataRepository.InsertSolicitud(newSolicitud);
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

        //buscar los usuarios por sucursal y que sea gerencia, subjefe, jefe
        public IEnumerable<UsuarioDTO> ObtenerUsuariosAutorizaAsync( int idSucursal)
        {
            return _dataRepository.ObtenerUsuariosAutorizaAsync(idSucursal);
        }

        public IEnumerable<UsuarioDTO> ObtenerUsuarioDestinoAsync(int idDepartamento)
        {
            return _dataRepository.ObtenerUsuarioDestino(idDepartamento);
        }


        //traer los datos de combobox anidados
        public IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa)
        {
            return _dataRepository.ObtenerSucursalesByRuc(RucEmpresa);
        }

        //traer departamentos por la sucursal
        public IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal)
        {
            return _dataRepository.ObtenerDepartamentosBySucursal(idSucursal);
        }
    }
}
