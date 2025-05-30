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
    public class MovimientosInventarioController : Controller
    {
        private readonly IMovimientosInventario _bodega;

        public MovimientosInventarioController(IMovimientosInventario iMovimientosInventario)
        {
            _bodega = iMovimientosInventario;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("MovimientosInventarioInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.MovimientosInventarioInfoAll);
        }


        [HttpGet("GetMovimientosInventarioById/{idMovimientosInventario}")]
        public IActionResult GetMovimientosInventarioById(int idMovimientosInventario)
        {

            var bodega = _bodega.GetMovimientosInventarioById(idMovimientosInventario);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idMovimientosInventario} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertMovimientosInventario")]
        public IActionResult Create([FromBody] MovimientosInventario NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertMovimientosInventario(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateMovimientosInventario")]
        public IActionResult Update([FromBody] MovimientosInventario UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateMovimientosInventario(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteMovimientosInventario")]
        public IActionResult Delete([FromBody] MovimientosInventario DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteMovimientosInventario(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteMovimientosInventarioById/{IdMovimientosInventario}")]
        public IActionResult DeleteMovimientosInventarioById(int IdMovimientosInventario)
        {
            try
            {
                _bodega.DeleteMovimientosInventarioById(IdMovimientosInventario);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
