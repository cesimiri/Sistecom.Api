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

    public class OrdenesEnsamblajeController : Controller
    {
        private readonly IOrdenesEnsamblaje _bodega;

        public OrdenesEnsamblajeController(IOrdenesEnsamblaje iOrdenesEnsamblaje)
        {
            _bodega = iOrdenesEnsamblaje;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("OrdenesEnsamblajeInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.OrdenesEnsamblajeInfoAll);
        }


        [HttpGet("GetOrdenesEnsamblajeById/{idOrdenesEnsamblaje}")]
        public IActionResult GetOrdenesEnsamblajeById(int idOrdenesEnsamblaje)
        {

            var bodega = _bodega.GetOrdenesEnsamblajeById(idOrdenesEnsamblaje);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idOrdenesEnsamblaje} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertOrdenesEnsamblaje")]
        public IActionResult Create([FromBody] OrdenesEnsamblaje NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertOrdenesEnsamblaje(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateOrdenesEnsamblaje")]
        public IActionResult Update([FromBody] OrdenesEnsamblaje UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateOrdenesEnsamblaje(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOrdenesEnsamblajes")]
        public IActionResult Delete([FromBody] OrdenesEnsamblaje DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteOrdenesEnsamblaje(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteOrdenesEnsamblajeById/{IdOrdenesEnsamblaje}")]
        public IActionResult DeleteOrdenesEnsamblajeById(int IdOrdenesEnsamblaje)
        {
            try
            {
                _bodega.DeleteOrdenesEnsamblajeById(IdOrdenesEnsamblaje);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
