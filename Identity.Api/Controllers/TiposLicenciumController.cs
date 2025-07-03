using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Identity.Api.Services;
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

    public class TiposLicenciumController : Controller
    {
        private readonly ITiposLicencium _bodega;

        public TiposLicenciumController(ITiposLicencium iTiposLicencium)
        {
            _bodega = iTiposLicencium;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("TiposLicenciumInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.TiposLicenciumInfoAll);
        }


        [HttpGet("GetTiposLicenciumById/{idTiposLicencium}")]
        public IActionResult GetTiposLicenciumById(int idTiposLicencium)
        {

            var bodega = _bodega.GetTiposLicenciumById(idTiposLicencium);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idTiposLicencium} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertTiposLicencium")]
        public IActionResult Create([FromBody] TiposLicencium NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertTiposLicencium(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateTiposLicencium")]
        public IActionResult Update([FromBody] TiposLicencium UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateTiposLicencium(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteTiposLicencium")]
        public IActionResult Delete([FromBody] TiposLicencium DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteTiposLicencium(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteTiposLicenciumById/{IdTiposLicencium}")]
        public IActionResult DeleteTiposLicenciumById(int IdTiposLicencium)
        {
            try
            {
                _bodega.DeleteTiposLicenciumById(IdTiposLicencium);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetTiposLicenciumPaginados")]
        public IActionResult GetTiposLicenciumPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _bodega.GetTiposLicenciumPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
