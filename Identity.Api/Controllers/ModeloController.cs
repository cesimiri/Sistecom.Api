using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ModeloController : Controller
    {
        private readonly IModelo _empresaCliente;

        public ModeloController(IModelo iModelo)
        {
            _empresaCliente = iModelo;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllModelo")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.GetAllModelo);
        }


        [HttpGet("GetModeloById/{idModelo}")]
        public IActionResult GetModeloById(int idModelo)
        {

            var empresaCliente = _empresaCliente.GetModeloById(idModelo);

            if (empresaCliente == null)
            {
                return NotFound($"Modelo con ese ID: {idModelo} no encontrado.");
            }

            return Ok(empresaCliente);
        }

        [HttpPost("InsertModelo")]
        public IActionResult Create([FromBody] ModeloDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertModelo(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateModelo")]
        public IActionResult Update([FromBody] ModeloDTO UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateModelo(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        //[HttpDelete("DeleteProducto")]
        //public IActionResult Delete([FromBody] Producto DelItem)
        //{
        //    try
        //    {
        //        if (DelItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _empresaCliente.DeleteProducto(DelItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        [HttpDelete("DeleteModeloById/{idModelo}")]
        public IActionResult DeleteById(int idModelo)
        {
            try
            {
                _empresaCliente.DeleteModeloById(idModelo);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
