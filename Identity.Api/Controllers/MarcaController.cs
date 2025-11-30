using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Update([FromBody] MarcaDTO UpdItem)
        {
            if (UpdItem == null || !ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Error: Envío de datos inválido." });
            }

            try
            {
                _marca.UpdateMarca(UpdItem);

                // Retornamos mensaje de éxito
                return Ok(new { success = true, message = "Marca actualizada correctamente." });
            }
            catch (Exception ex)
            {
                // Retornamos mensaje de error
                return BadRequest(new { success = false, message = "Error al actualizar la marca: " + ex.Message });
            }
        }


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


        [HttpGet("GetMarcaPaginados")]
        public IActionResult GetMarcaPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _marca.GetMarcaPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //busqueda las marcas por idCategoria

        [HttpGet("GetMarcaByIdCategoria/{idCategoria}")]
        public IActionResult GetMarcasByIdCategoria(int idCategoria)
        {
            var marcas = _marca.GetMarcasByIdCategoria(idCategoria);

            if (marcas == null || !marcas.Any())
            {
                return NotFound($"No existen marcas para la categoría con ID: {idCategoria}.");
            }

            return Ok(marcas);
        }
    }
}
