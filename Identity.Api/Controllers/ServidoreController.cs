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

    public class ServidoreController : Controller
    {
        private readonly IServidore _service;

        public ServidoreController(IServidore iServidore)
        {
            _service = iServidore;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ServidoreInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.ServidoreInfoAll);
        }

        //Para trawer todos los activos donde sea el nombre del producto servidores o servidor en mayuscula 
        [HttpGet("ActivosServidores")]
        public ActionResult<IEnumerable<ActivoDTO>> GetAllActivosServidores()
        {
            var resultado = _service.GetActivosServidores;

            if (resultado == null || !resultado.Any())
            {
                return NotFound(new
                {
                    message = "No se encontraron activos con nombre de producto que contenga 'SERVIDOR'."
                });
            }

            return Ok(resultado);
        }


        [HttpGet("GetServidoreById/{idServidore}")]
        public IActionResult GetServidoreById(int idServidore)
        {

            var bodega = _service.GetServidoreById(idServidore);

            if (bodega == null)
            {
                return NotFound($"No existe esa Asignaciones Activo con el ID: {idServidore} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertServidore")]
        public IActionResult Create([FromBody] ServidoreDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _service.InsertServidore(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateServidore")]
        public IActionResult Update([FromBody] ServidoreDTO UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _service.UpdateServidore(UpdItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }



        [HttpDelete("DeleteServidoreById/{IdServidore}")]
        public IActionResult DeleteServidoreById(int IdServidore)
        {
            try
            {
                // Validamos si el registro existe antes de eliminarlo
                var existente = _service.GetServidoreById(IdServidore);
                if (existente == null)
                {
                    return NotFound(new
                    {
                        message = $"No se encontró ningún servidor con el Id {IdServidore}."
                    });
                }

                // Procedemos a eliminar
                _service.DeleteServidoreById(IdServidore);

                return Ok(new
                {
                    message = $"Servidor con Id {IdServidore} eliminado correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error al eliminar el servidor.",
                    detalle = ex.Message
                });
            }
        }


        [HttpGet("GetServidorePaginados")]
        public IActionResult GetServidorePaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                var resultado = _service.GetServidorePaginados(pagina, pageSize, filtro, estado);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
