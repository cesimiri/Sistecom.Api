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
    public class ComponentesEnsamblajeController : Controller
    {
        private readonly IComponentesEnsamblaje _bodega;

        public ComponentesEnsamblajeController(IComponentesEnsamblaje iComponentesEnsamblaje)
        {
            _bodega = iComponentesEnsamblaje;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ComponentesEnsamblajeInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.ComponentesEnsamblajeInfoAll);
        }


        [HttpGet("GetComponentesEnsamblajeById/{idComponentesEnsamblaje}")]
        public IActionResult GetComponentesEnsamblajeById(int idComponentesEnsamblaje)
        {

            var bodega = _bodega.GetComponentesEnsamblajeById(idComponentesEnsamblaje);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idComponentesEnsamblaje} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertComponentesEnsamblaje")]
        public IActionResult Create([FromBody] ComponentesEnsamblaje NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertComponentesEnsamblaje(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateComponentesEnsamblaje")]
        public IActionResult Update([FromBody] ComponentesEnsamblaje UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateComponentesEnsamblaje(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteComponentesEnsamblaje")]
        public IActionResult Delete([FromBody] ComponentesEnsamblaje DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.DeleteComponentesEnsamblaje(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteComponentesEnsamblajeById/{IdComponentesEnsamblaje}")]
        public IActionResult DeleteComponentesEnsamblajeById(int IdComponentesEnsamblaje)
        {
            try
            {
                _bodega.DeleteComponentesEnsamblajeById(IdComponentesEnsamblaje);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
