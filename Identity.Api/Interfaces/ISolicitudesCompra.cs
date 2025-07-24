//using Identity.Api.DTO;
//using Identity.Api.Paginado;
//using System.Collections.Generic;

//namespace Identity.Api.Interfaces
//{
//    public interface ISolicitudesCompra
//    {
//        IEnumerable<SolicitudesCompraDTO> GetAllSolicitudesCompra { get; }
//        // Nuevo método para paginado:
//        PagedResult<SolicitudesCompraDTO> GetSolicitudesCompraPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null);

//        SolicitudesCompraDTO GetSolicitudById(int idSolicitud);
//        //devuelve el id de la solicitud
//        int InsertSolicitud(SolicitudesCompraDTO newSolicitud);
//        void UpdateSolicitud(SolicitudesCompraDTO updatedSolicitud);
//        //void DeleteSolicitud(SoliciudesCompraDTO solicitudToDelete);
//        void DeleteSolicitudById(int idSolicitud);

//        //FUNCIONES DE BACKEND


//        IEnumerable<UsuarioDTO> ObtenerUsuarioSolicitaAsync();
//        //por sucursal
//        IEnumerable<UsuarioDTO> ObtenerUsuariosAutorizaAsync(int idSucursal);
//        IEnumerable<UsuarioDTO> ObtenerUsuarioDestinoAsync(int idDepartamento);


//        IEnumerable<SucursaleDTO> ObtenerSucursalesByRuc(string RucEmpresa);
//        IEnumerable<DepartamentoDTO> ObtenerDepartamentosBySucursal(int idSucursal);


//    }
//}
