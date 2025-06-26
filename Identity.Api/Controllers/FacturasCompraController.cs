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

    public class FacturasCompraController : Controller
    {
        private readonly IFacturasCompra _facturasCompra;

        public FacturasCompraController(IFacturasCompra iFacturasCompra)
        {
            _facturasCompra = iFacturasCompra;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("FacturasCompraInfoAll")]

        public IActionResult GetAll()
        {
            return Ok(_facturasCompra.FacturasCompraInfoAll);
        }

        [HttpGet("GetFacturasCompraById/{idFacturasCompra}")]
        public IActionResult GetFacturasCompraById(int idFacturasCompra)
        {
            ;

            var facturasCompra = _facturasCompra.GetFacturasCompraById(idFacturasCompra);

            if (facturasCompra == null)
            {
                return NotFound($"La factura con ID {idFacturasCompra} no encontrado.");
            }

            return Ok(facturasCompra);
        }

        [HttpPost("InsertFacturasCompra")]
        public IActionResult Create([FromBody] FacturasCompraDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envío de datos inválido.");
                }

                int idGenerado = _facturasCompra.InsertFacturasCompra(NewItem);

                return Ok(idGenerado); // ← retorna solo el ID
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpPut("UpdateFacturasCompra")]
        public IActionResult Update([FromBody] FacturasCompra UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _facturasCompra.UpdateFacturasCompra(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        //[HttpDelete("DeleteFacturasCompra")]
        //public IActionResult Delete([FromBody] FacturasCompra DelItem)
        //{
        //    try
        //    {
        //        if (DelItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _facturasCompra.DeleteFacturasCompra(DelItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        [HttpDelete("DeleteFacturasCompraById/{IdFacturasCompra}")]
        public IActionResult DeleteById(int IdFacturasCompra)
        {
            try
            {
                _facturasCompra.DeleteFacturasCompraById(IdFacturasCompra);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }
    }
}
