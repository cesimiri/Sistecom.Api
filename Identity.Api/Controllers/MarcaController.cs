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
    public class MarcaController : ControllerBase
    {
        private readonly IMarca _marca;

        public MarcaController(IMarca iMarca)
        {
            _marca = iMarca;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllMarca")]
        public IActionResult GetAll()
        {
            return Ok(_marca.GetAllMarca);
        }


        [HttpGet("GetMarcaById/{idMarca}")]
        public IActionResult GetMarcaById(int idMarca)
        {

            var bodega = _marca.GetMarcaById(idMarca);

            if (bodega == null)
            {
                return NotFound($"No existe esa bodega con el ID: {idMarca} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertMarca")]
        public IActionResult Create([FromBody] MarcaDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _marca.InsertMarca(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateMarca")]
        public IActionResult Update([FromBody] Marca UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _marca.UpdateMarca(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        //[HttpDelete("DeleteBodega")]
        //public IActionResult Delete([FromBody] Bodega DelItem)
        //{
        //    try
        //    {
        //        if (DelItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _bodega.DeleteBodega(DelItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        [HttpDelete("DeleteMarcaById/{idMarca}")]
        public IActionResult DeleteMarcaById(int idMarca)
        {
            try
            {
                _marca.DeleteMarcaById(idMarca);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
