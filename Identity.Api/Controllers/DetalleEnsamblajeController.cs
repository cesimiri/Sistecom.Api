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

    public class DetalleEnsamblajeController : Controller   
    {
        private readonly IDetalleEnsamblaje _bodega;

        public DetalleEnsamblajeController(IDetalleEnsamblaje iDetalleEnsamblaje)
        {
            _bodega = iDetalleEnsamblaje;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("DetalleEnsamblajeInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.DetalleEnsamblajeInfoAll);
        }


        [HttpGet("GetDetalleEnsamblajeById/{idDetalleEnsamblaje}")]
        public IActionResult GetDetalleEnsamblajeById(int idDetalleEnsamblaje)
        {

            var bodega = _bodega.GetDetalleEnsamblajeById(idDetalleEnsamblaje);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idDetalleEnsamblaje} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertDetalleEnsamblaje")]
        public IActionResult Create([FromBody] DetalleEnsamblaje NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertDetalleEnsamblaje(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateDetalleEnsamblaje")]
        public IActionResult Update([FromBody] DetalleEnsamblaje UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateDetalleEnsamblaje(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteDetalleEnsamblaje")]
        public IActionResult Delete([FromBody] DetalleEnsamblaje DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteDetalleEnsamblaje(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteDetalleEnsamblajeById/{IdDetalleEnsamblaje}")]
        public IActionResult DeleteDetalleEnsamblajeById(int IdDetalleEnsamblaje)
        {
            try
            {
                _bodega.DeleteDetalleEnsamblajeById(IdDetalleEnsamblaje);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
