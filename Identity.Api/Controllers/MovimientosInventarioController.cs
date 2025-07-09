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

    public class MovimientosInventarioController : Controller
    {
        private readonly IMovimientosInventario _bodega;

        public MovimientosInventarioController(IMovimientosInventario iMovimientosInventario)
        {
            _bodega = iMovimientosInventario;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpGet("MovimientosInventarioInfoAll")]
        //public IActionResult GetAll()
        //{
        //    return Ok(_bodega.MovimientosInventarioInfoAll);
        //}


        //[HttpGet("GetMovimientosInventarioById/{idMovimientosInventario}")]
        //public IActionResult GetMovimientosInventarioById(int idMovimientosInventario)
        //{

        //    var bodega = _bodega.GetMovimientosInventarioById(idMovimientosInventario);

        //    if (bodega == null)
        //    {
        //        return NotFound($"No existe esa Asignaciones Activo con el ID: {idMovimientosInventario} no encontrado.");
        //    }

        //    return Ok(bodega);
        //}

        //[HttpPost("InsertMovimientosInventario")]
        //public IActionResult Create([FromBody] MovimientosInventario NewItem)
        //{
        //    try
        //    {
        //        if (NewItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _bodega.InsertMovimientosInventario(NewItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return Ok(NewItem);
        //}

        //[HttpPut("UpdateMovimientosInventario")]
        //public IActionResult Update([FromBody] MovimientosInventario UpdItem)
        //{
        //    try
        //    {
        //        if (UpdItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _bodega.UpdateMovimientosInventario(UpdItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("DeleteMovimientosInventario")]
        //public IActionResult Delete([FromBody] MovimientosInventario DelItem)
        //{
        //    try
        //    {
        //        if (DelItem == null || !ModelState.IsValid)
        //        {
        //            return BadRequest("Error: Envio de datos");
        //        }

        //        _bodega.DeleteMovimientosInventario(DelItem);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("DeleteMovimientosInventarioById/{IdMovimientosInventario}")]
        //public IActionResult DeleteMovimientosInventarioById(int IdMovimientosInventario)
        //{
        //    try
        //    {
        //        _bodega.DeleteMovimientosInventarioById(IdMovimientosInventario);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error:" + ex.Message);
        //    }

        //    return NoContent();
        //}



        ///----------------//////////////////////-------------------------////////////////--------------//
        [HttpPost("RegistrarMovimientos")]
        public IActionResult Registrar([FromBody] List<MovimientosInventarioDTO> movimientos)
        {
            try
            {
                var exito = _bodega.RegistrarMovimientos(movimientos, out string error);

                if (!exito)
                    return BadRequest(new { error });

                return Ok(new { mensaje = "Movimientos registrados correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }







        [HttpGet("ObtenerPorId/{id}")]
        public ActionResult<MovimientosInventario> ObtenerPorId(int id)
        {
            var movimiento = _bodega.ObtenerPorId(id);
            if (movimiento == null)
                return NotFound($"No se encontró movimiento con ID {id}");

            return Ok(movimiento);
        }

        //lista paginada de movimientos de inventario, con filtro por nombre del producto, tipo de movimiento, bodega y rango de fechas.
        [HttpGet("GetPaginados")]
        public IActionResult GetPaginados(
            int pagina = 1,
            int pageSize = 10,
            string? tipoMovimiento = null,
            int? idBodega = null,
            string? nombreProducto = null,
            DateTime? desde = null,
            DateTime? hasta = null)
        {
            var resultado = _bodega.GetPaginados(pagina, pageSize, tipoMovimiento, idBodega, nombreProducto, desde, hasta);
            return Ok(resultado);
        }
    }
}
