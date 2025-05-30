﻿using Identity.Api.Interfaces;
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
        public IActionResult Create([FromBody] FacturasCompra NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _facturasCompra.InsertFacturasCompra(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
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


        [HttpDelete("DeleteFacturasCompra")]
        public IActionResult Delete([FromBody] FacturasCompra DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _facturasCompra.DeleteFacturasCompra(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

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
