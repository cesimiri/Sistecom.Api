using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class StockBodegaDataRepository
    {
        public List<StockBodega> GetAllStockBodega()
        {
            using (var context = new InvensisContext())
            {
                return context.StockBodegas
                              .Include(x => x.IdBodegaNavigation)
                              .Include(x => x.IdProductoNavigation)
                              .ToList();
            }
        }

        public StockBodega GetStockBodegaById(int idStock)
        {
            using (var context = new InvensisContext())
            {
                return context.StockBodegas
                              .Include(x => x.IdBodegaNavigation)
                              .Include(x => x.IdProductoNavigation)
                              .FirstOrDefault(p => p.IdStock == idStock);
            }
        }



        public void UpdateStockBodega(StockBodega item)
        {
            using (var context = new InvensisContext())
            {
                var existing = context.StockBodegas.FirstOrDefault(p => p.IdStock == item.IdStock);

                if (existing != null)
                {
                    existing.IdBodega = item.IdBodega;
                    existing.IdProducto = item.IdProducto;
                    existing.CantidadDisponible = item.CantidadDisponible;
                    existing.CantidadReservada = item.CantidadReservada;
                    existing.CantidadEnsamblaje = item.CantidadEnsamblaje;
                    existing.ValorPromedio = item.ValorPromedio;
                    existing.UltimaEntrada = item.UltimaEntrada;
                    existing.UltimaSalida = item.UltimaSalida;
                    existing.FechaActualizacion = item.FechaActualizacion;

                    context.SaveChanges();
                }
            }
        }



        public void DeleteStockBodegaById(int idStock)
        {
            using (var context = new InvensisContext())
            {
                var existing = context.StockBodegas.FirstOrDefault(p => p.IdStock == idStock);
                if (existing != null)
                {
                    context.StockBodegas.Remove(existing);
                    context.SaveChanges();
                }
            }
        }




        //paginado por bodegga
        public PagedResult<stockBodegaDTO> GetPaginadosPorBodega(int idBodega, int pagina, int pageSize, string? filtro = null)
        {
            using var context = new InvensisContext();

            var query = context.StockBodegas
                .Where(sb => sb.IdBodega == idBodega)
                .Include(sb => sb.IdBodegaNavigation)
                .Include(sb => sb.IdProductoNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroFormateado = filtro.Trim().ToLower();
                query = query.Where(sb =>
                    sb.IdProductoNavigation.Nombre.ToLower().Contains(filtroFormateado)
                );
            }

            var totalItems = query.Count();

            var items = query
                .OrderBy(sb => sb.IdStock)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(sb => new stockBodegaDTO
                {
                    IdStock = sb.IdStock,
                    IdBodega = sb.IdBodega,
                    IdProducto = sb.IdProducto,
                    nombreBodega = sb.IdBodegaNavigation.Nombre,
                    nombreProducto = sb.IdProductoNavigation.Nombre,
                    CantidadDisponible = sb.CantidadDisponible,
                    CantidadReservada = sb.CantidadReservada,
                    CantidadEnsamblaje = sb.CantidadEnsamblaje,
                    ValorPromedio = sb.ValorPromedio,
                    UltimaEntrada = sb.UltimaEntrada,
                    UltimaSalida = sb.UltimaSalida,
                    FechaActualizacion = sb.FechaActualizacion
                })
                .ToList();

            return new PagedResult<stockBodegaDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }


        //exportar
        // En tu repo o service
        public List<stockBodegaDTO> ObtenerParaExportar(int idBodega, string? filtro = null, string? correo = null)
        {
            using var context = new InvensisContext();

            var query = context.StockBodegas
                .Where(sb => sb.IdBodega == idBodega)
                .Include(sb => sb.IdBodegaNavigation)
                .Include(sb => sb.IdProductoNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroFormateado = filtro.Trim().ToLower();
                query = query.Where(sb =>
                    sb.IdProductoNavigation.Nombre.ToLower().Contains(filtroFormateado)
                );
            }

            return query
                .OrderBy(sb => sb.IdStock)
                .Select(sb => new stockBodegaDTO
                {
                    IdStock = sb.IdStock,
                    IdBodega = sb.IdBodega,
                    IdProducto = sb.IdProducto,
                    nombreBodega = sb.IdBodegaNavigation.Nombre,
                    nombreProducto = sb.IdProductoNavigation.Nombre,
                    CantidadDisponible = sb.CantidadDisponible,
                    CantidadReservada = sb.CantidadReservada,
                    CantidadEnsamblaje = sb.CantidadEnsamblaje,
                    ValorPromedio = sb.ValorPromedio,
                    UltimaEntrada = sb.UltimaEntrada,
                    UltimaSalida = sb.UltimaSalida,
                    FechaActualizacion = sb.FechaActualizacion
                })
                .ToList();
        }





    }
}
