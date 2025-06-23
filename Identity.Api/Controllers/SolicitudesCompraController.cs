using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class SolicitudesCompraController : Controller
    {
        private readonly ISolicitudesCompra _solicitudesCompraService;

        public SolicitudesCompraController(ISolicitudesCompra solicitudesCompraService)
        {
            _solicitudesCompraService = solicitudesCompraService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllSolicitudesCompra")]
        public IActionResult GetAll()
        {
            return Ok(_solicitudesCompraService.GetAllSolicitudesCompra);
        }

        //BUSQUEDA USUARIO DESTINO POR DEPARTAMENTO
        [HttpGet("ObtenerUsuarioDestino/{idDepartamento}")]
        public IActionResult ObtenerUsuarioDestino(int idDepartamento)
        {
            var modelos = _solicitudesCompraService.ObtenerUsuarioDestinoAsync(idDepartamento);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron Usuario que pertenezcan a ese departamento{idDepartamento}.");
            }

            return Ok(modelos);
        }


        //busqueda de usuario que autoriza porque sean 1 gerencia,2 subjefe,3 jefe
        [HttpGet("ObtenerUsuariosAutoriza/{idSucursal}")]
        public IActionResult ObtenerUsuariosAutorizaAsync(int idSucursal)
        {
            var usuarios = _solicitudesCompraService.ObtenerUsuariosAutorizaAsync(idSucursal);

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("No se encontraron usuarios solicitantes.");
            }

            return Ok(usuarios);
        }

        //busqueda de usuario que solicita porque sean rol 4,3,2
        [HttpGet("ObtenerUsuariosSolicitantes")]
        public IActionResult ObtenerUsuariosSolicitantes()
        {
            var usuarios = _solicitudesCompraService.ObtenerUsuarioSolicitaAsync();

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("No se encontraron usuarios solicitantes.");
            }

            return Ok(usuarios);
        }

        //Obtener las sucursales por empresa
        [HttpGet("ObtenerSucursalesByRuc/{RucEmpresa}")]
        public IActionResult ObtenerSucursalesByRuc(string RucEmpresa)
        {
            var modelos = _solicitudesCompraService.ObtenerSucursalesByRuc(RucEmpresa);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron Sucursales con esa Ruc de empresa {RucEmpresa}.");
            }

            return Ok(modelos);
        }

        //Obtener las departamentos por sucursal
        [HttpGet("ObtenerDepartamentosBySucursal/{idSucursal}")]
        public IActionResult ObtenerDepartamentosBySucursal(int idSucursal)
        {
            var modelos = _solicitudesCompraService.ObtenerDepartamentosBySucursal(idSucursal);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron los departamentos por esa id de sucursal: {idSucursal}.");
            }

            return Ok(modelos);
        }

        [HttpGet("GetSolicitudById/{idSolicitud}")]
        public IActionResult GetById(int idSolicitud)
        {
            var solicitud = _solicitudesCompraService.GetSolicitudById(idSolicitud);

            if (solicitud == null)
            {
                return NotFound($"Solicitud con ID {idSolicitud} no encontrada.");
            }

            return Ok(solicitud);
        }

        [HttpPost("InsertSolicitud")]
        public IActionResult Insert([FromBody] SolicitudesCompraDTO newSolicitud)
        {
            if (newSolicitud == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _solicitudesCompraService.InsertSolicitud(newSolicitud);
            return Ok(newSolicitud);
        }

        [HttpPut("UpdateSolicitud")]
        public IActionResult Update([FromBody] SolicitudesCompraDTO UpdateSolicitud)
        {
            if (UpdateSolicitud == null || !ModelState.IsValid)
            {
                return BadRequest("Error: Datos inválidos");
            }

            _solicitudesCompraService.UpdateSolicitud(UpdateSolicitud);
            return NoContent();
        }

        //[HttpDelete("DeleteSolicitud")]
        //public IActionResult Delete([FromBody] SoliciudesCompraDTO solicitudToDelete)
        //{
        //    if (solicitudToDelete == null || !ModelState.IsValid)
        //    {
        //        return BadRequest("Error: Datos inválidos");
        //    }

        //    _solicitudesCompraService.DeleteSolicitud(solicitudToDelete);
        //    return NoContent();
        //}

        [HttpDelete("DeleteSolicitudById/{idSolicitud}")]
        public IActionResult DeleteById(int idSolicitud)
        {
            _solicitudesCompraService.DeleteSolicitudById(idSolicitud);
            return NoContent();
        }
    }
}
