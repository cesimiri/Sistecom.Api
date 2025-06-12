using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamento _departamentoService;

        public DepartamentoController(IDepartamento departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet("GetAllDepartamento")]
        public IActionResult GetAll()
        {
            var departamento = _departamentoService.GetAllDepartamento();
            return Ok(departamento);
        }

        [HttpGet("GetDepartamentoById/{idDepartamento}")]
        public IActionResult GetById(int idDepartamento)
        {
            var departamento = _departamentoService.GetDepartamentoById(idDepartamento);
            if (departamento == null)
            {
                return NotFound($"Departamento con ID {idDepartamento} no encontrada.");
            }
            return Ok(departamento);
        }

        [HttpPost("InsertDepartamento")]
        public IActionResult InsertDepartamento([FromBody] DepartamentoDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _departamentoService.InsertDepartamento(dto);
                return Ok(new { message = "departamento guardada exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdateDepartamento")]
        public IActionResult Update([FromBody] DepartamentoDTO dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest("Datos inválidos.");
            }

            try
            {
                _departamentoService.UpdateDepartamento(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //[HttpDelete("DeleteSuscripcion")]
        //public IActionResult Delete([FromBody] SuscripcionDto dto)
        //{
        //    if (dto == null || !ModelState.IsValid)
        //    {
        //        return BadRequest("Datos inválidos.");
        //    }

        //    try
        //    {
        //        _suscripcioneService.DeleteSuscripcion(dto);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}

        [HttpDelete("DeleteDepartamentoById/{idDepartamento}")]
        public IActionResult DeleteById(int idDepartamento)
        {
            try
            {
                _departamentoService.DeleteDepartamentoById(idDepartamento);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
