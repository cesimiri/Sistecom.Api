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

    public class AsignacionesLicenciaController : Controller
    {
        private readonly IAsignacionesLicencia _bodega;

        public AsignacionesLicenciaController(IAsignacionesLicencia iAsignacionesLicencia)
        {
            _bodega = iAsignacionesLicencia;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("LicenciaInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.GetLicencias);
        }

        // 🔹 Obtener usuario por cédula (nombres y apellidos)
        [HttpGet("ObtenerUsuarioPorCedula/{cedula}")]
        public IActionResult GetUsuarioPorCedula(string cedula)
        {
            var usuario = _bodega.ObtenerUsuarioPorCedula(cedula);
            return usuario == null ? NotFound($"No se encontró usuario con cédula {cedula}") : Ok(usuario);
        }

        // 🔹 Obtener departamentos de usuario por cédula
        [HttpGet("ObtenerDepartamentosPorCedula/{cedula}")]
        public IActionResult GetDepartamentosPorCedula(string cedula)
        {
            var departamentos = _bodega.ObtenerDepartamentosPorCedula(cedula);
            return departamentos == null || departamentos.Count == 0
                ? NotFound($"No se encontraron departamentos para cédula {cedula}")
                : Ok(departamentos);
        }
        //trae todos los servidores
        [HttpGet("GetServidoresActivos")]
        public IActionResult GetServidoresActivos()
        {

            var bodega = _bodega.GetServidoresActivos();

            if (bodega == null)
            {
                return NotFound($"No existe servidores no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpGet("GetAsignacionesLicenciaById/{idAsignacionesLicencia}")]
        public IActionResult GetAsignacionesLicenciaById(int idAsignacionesLicencia)
        {

            var bodega = _bodega.GetAsignacionesLicenciaById(idAsignacionesLicencia);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idAsignacionesLicencia} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertAsignacionesLicencia")]
        public IActionResult Create([FromBody] AsignacionesLicencia NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertAsignacionesLicencia(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateAsignacionesLicencia")]
        public IActionResult Update([FromBody] AsignacionesLicencia UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateAsignacionesLicencia(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteAsignacionesLicenciaById/{IdAsignacionesLicencia}")]
        public IActionResult DeleteAsignacionesLicenciaById(int IdAsignacionesLicencia)
        {
            try
            {
                _bodega.DeleteAsignacionesLicenciaById(IdAsignacionesLicencia);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        //PAGINADO
        [HttpGet("GetAsignacionesLicenciaPaginados")]
        public IActionResult GetAsignacionesLicenciaPaginados(
            int pagina = 1,
            int pageSize = 8,
            string? codigoActivo = null,
            string? estadoActivo = null)
        {
            var resultado = _bodega.GetAsignacioneslicenciaPaginados(
                pagina, pageSize,
                codigoActivo, estadoActivo);

            return Ok(resultado);
        }

    }
}
