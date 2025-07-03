using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CargoController : Controller
    {
        private readonly ICargo _cargo;

        public CargoController(ICargo iCargo)
        {
            _cargo = iCargo;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllCargo")]
        public IActionResult GetAll()
        {
            return Ok(_cargo.GetAllCargo);
        }

        [HttpGet("GetCargoById/{idCargo}")]
        public IActionResult GetCargoById(int idCargo)
        {
            

            var cargo = _cargo.GetCargoById(idCargo);

            if (cargo == null)
            {
                return NotFound($"Cargo con ID {idCargo} no encontrado.");
            }

            return Ok(cargo);

        }


        [HttpPost("InsertCargo")]
        public IActionResult Create([FromBody] Cargo NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envío de datos inválido.");
                }

                _cargo.InsertCargo(NewItem);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"Error al insertar cargo: {innerMessage}");
            }

            return CreatedAtAction(nameof(GetCargoById), new { idCargo = NewItem.IdCargo }, NewItem);
        }


        [HttpPut("UpdateCargo")]
        public IActionResult Update([FromBody] Cargo UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _cargo.UpdateCargo(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("DeleteCargo")]
        public IActionResult Delete([FromBody] Cargo DelItem)
        {
            try
            {
                if (DelItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _cargo.DeleteCargo(DelItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        [HttpDelete("DeleteCargoById/{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _cargo.DeleteCargoById(id);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetCargoPaginados")]
        public IActionResult GetCargoPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _cargo.GetCargoPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
