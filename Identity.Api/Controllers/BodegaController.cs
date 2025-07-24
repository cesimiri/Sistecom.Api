using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
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

    public class BodegaController : Controller
    {
        private readonly IBodega _bodega;

        public BodegaController(IBodega iBodega)
        {
            _bodega = iBodega;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("BodegaInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_bodega.BodegaInfoAll);
        }


        [HttpGet("GetBodegaById/{idBodega}")]
        public IActionResult GetBodegaById(int idBodega)
        {

            var bodega = _bodega.GetBodegaById(idBodega);

            if (bodega == null)
            {
                return NotFound($"No existe esa bodega con el ID: {idBodega} no encontrado.");
            }

            return Ok(bodega);
        }

        [HttpPost("InsertBodega")]
        public IActionResult Create([FromBody] BodegaDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.InsertBodega(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateBodega")]
        public IActionResult Update([FromBody] Bodega UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _bodega.UpdateBodega(UpdItem);
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

        [HttpDelete("DeleteBodegaById/{IdBodega}")]
        public IActionResult DeleteBodegaById(int IdBodega)
        {
            try
            {
                _bodega.DeleteBodegaById(IdBodega);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }


        [HttpGet("GetBodegaPaginados")]
        public IActionResult GetUsuariosPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _bodega.GetBodegaPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //USUARIOS SISTECOM
        //[HttpGet("GetUsuarioSistecom")]
        //public IActionResult GetUsuariosSistecom()
        //{
        //    try
        //    {
        //        var usuarios = _bodega.GetUsuarioSistecom();
        //        return Ok(usuarios);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Para desarrollo, devuelve el detalle completo
        //        return BadRequest(new { error = "Error en GetUsuarioSistecom", detalle = ex.ToString() });
        //    }
        //}

        //trae bodegas por usuario logueado (correo)
        [HttpGet("GetBodegasPorResponsable/{correo}")]
        public IActionResult GetBodegasPorResponsable(string correo)
        {
            try
            {
                var bodegas = _bodega.GetBodegasPorResponsable(correo);
                return Ok(bodegas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error al obtener bodegas por responsable", detalle = ex.Message });
            }
        }


    }
}
