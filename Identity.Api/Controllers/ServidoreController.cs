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

    public class ServidoreController : Controller
    {
        private readonly IServidore _bodega;

        public ServidoreController(IServidore iServidore)
        {
            _bodega = iServidore;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ServidoreInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.ServidoreInfoAll);
        }


        [HttpGet("GetServidoreById/{idServidore}")]
        public IActionResult GetServidoreById(int idServidore)
        {

            var bodega = _bodega.GetServidoreById(idServidore);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idServidore} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertServidore")]
        public IActionResult Create([FromBody] Servidore NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertServidore(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateServidore")]
        public IActionResult Update([FromBody] Servidore UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateServidore(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteServidore")]
        public IActionResult Delete([FromBody] Servidore DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteServidore(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteServidoreById/{IdServidore}")]
        public IActionResult DeleteServidoreById(int IdServidore)
        {
            try
            {
                _bodega.DeleteServidoreById(IdServidore);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
