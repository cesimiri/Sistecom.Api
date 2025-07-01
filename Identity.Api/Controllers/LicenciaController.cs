using Identity.Api.DTO;
using Identity.Api.Interfaces;
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

    public class LicenciaController : Controller
    {
        private readonly ILicencia _bodega;

        public LicenciaController(ILicencia iLicencia)
        {
            _bodega = iLicencia;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("LicenciaInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.LicenciaInfoAll);
        }


        [HttpGet("GetLicenciaById/{idLicencia}")]
        public IActionResult GetLicenciaById(int idLicencia)
        {

            var bodega = _bodega.GetLicenciaById(idLicencia);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idLicencia} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertLicencia")]
        public IActionResult Create([FromBody] LicenciaDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertLicencia(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateLicencia")]
        public IActionResult Update([FromBody] Licencia UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateLicencia(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        

        [HttpDelete("DeleteLicenciaById/{IdLicencia}")]
        public IActionResult DeleteLicenciaById(int IdLicencia)
        {
            try
            {
                _bodega.DeleteLicenciaById(IdLicencia);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }



        [HttpGet("GetFacturasConCategoria6")]
        public IActionResult GetFacturasConCategoria6()
        {
            return Ok(_bodega.GetFacturasConCategoria6());
        }

        [HttpGet("GetProductoConCategoria6")]
        public IActionResult GetProductoConCategoria6()
        {
            return Ok(_bodega.GetProductoConCategoria6());
        }
    }
}
