using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Identity.Api.Paginado;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductoController : Controller
    {
        private readonly IProducto _empresaCliente;

        public ProductoController(IProducto iProducto)
        {
            _empresaCliente = iProducto;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ProductoInfoAll")]
        public IActionResult GetAll()
        {
            return Ok(_empresaCliente.GetAllProducto);
        }


        [HttpGet("GetProductoById/{IdProducto}")]
        public IActionResult GetProductoById(int idProducto)
        {

            var empresaCliente = _empresaCliente.GetProductoById(idProducto);

            if (empresaCliente == null)
            {
                return NotFound($"Producto con ese ID: {idProducto} no encontrado.");
            }

            return Ok(empresaCliente);
        }
        // trae todos los modelos con esa marca
        [HttpGet("GetModelosByIdMarca/{idMarca}")]
        public IActionResult GetModelosByIdMarca(int idMarca)
        {
            var modelos = _empresaCliente.GetModelosByIdMarca(idMarca);

            if (modelos == null || !modelos.Any())
            {
                return NotFound($"No se encontraron modelos para la marca con ID: {idMarca}.");
            }

            return Ok(modelos);
        }

        [HttpPost("InsertProducto")]
        public IActionResult Create([FromBody] ProductoDTO NewItem)
        {
            try
            {
                if (NewItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.InsertProducto(NewItem);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return Ok(NewItem);
        }

        [HttpPut("UpdateProducto")]
        public IActionResult Update([FromBody] ProductoDTO UpdItem)
        {
            try
            {
                if (UpdItem == null || !ModelState.IsValid)
                {
                    return BadRequest("Error: Envio de datos");
                }

                _empresaCliente.UpdateProducto(UpdItem);
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

        [HttpDelete("DeleteProductoById/{IdProducto}")]
        public IActionResult DeleteById(int IdProducto)
        {
            try
            {
                _empresaCliente.DeleteProductoById(IdProducto);
            }
            catch (Exception ex)
            {
                return BadRequest("Error:" + ex.Message);
            }

            return NoContent();
        }

        [HttpGet("GetProductoPaginados")]
        public IActionResult GetProductoPaginados(
        int pagina = 1,
        int pageSize = PaginadorHelper.NumeroDeDatosPorPagina,
        string? filtro = null,
        string? estado = null)
        {
            try
            {
                // Llamamos al método que devuelve el paginado (en el servicio)
                var resultado = _empresaCliente.GetProductoPaginados(pagina, pageSize, filtro, estado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
