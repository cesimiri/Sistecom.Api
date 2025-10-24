using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class ServidoreRepository
    {
        public List<Servidore> ServidoreInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.Servidores.ToList();
            }
        }

        //Para trawer todos los activos donde el producto sea de idCategoria 8 que es servidores 
        public List<ActivoDTO> GetActivosServidores()
        {
            using var context = new InvensisContext();

            var query = context.Activos
                .Include(a => a.IdProductoNavigation)
                    .ThenInclude(p => p.IdMarcaNavigation)        // 🔹 Relación con Marca
                .Include(a => a.IdFacturaCompraNavigation)        // 🔹 Relación con Factura
                .Include(a => a.IdOrdenEnsamblajeNavigation)      // 🔹 Relación con Orden
                .AsQueryable();

            // 🔍 Filtrar por IdCategoria = 8 (desde la marca del producto)
            query = query.Where(a =>
                a.IdProductoNavigation != null &&
                a.IdProductoNavigation.IdMarcaNavigation != null &&
                a.IdProductoNavigation.IdMarcaNavigation.IdCategoria == 8);

            // 🔹 Proyección a DTO
            var lista = query
                .OrderBy(a => a.CodigoActivo)
                .Select(a => new ActivoDTO
                {
                    IdActivo = a.IdActivo,
                    CodigoActivo = a.CodigoActivo,
                    IdProducto = a.IdProducto,
                    NumeroSerie = a.NumeroSerie,
                    NumeroParte = a.NumeroParte,

                    // 🔸 Manejo seguro de DateOnly y DateOnly?
                    FechaAdquisicion = a.FechaAdquisicion.ToDateTime(TimeOnly.MinValue),
                    FechaGarantiaFin = a.FechaGarantiaFin.HasValue
                        ? a.FechaGarantiaFin.Value.ToDateTime(TimeOnly.MinValue)
                        : (DateTime?)null,

                    IdFacturaCompra = a.IdFacturaCompra,
                    IdOrdenEnsamblaje = a.IdOrdenEnsamblaje,
                    ValorCompra = a.ValorCompra,
                    ValorResidual = a.ValorResidual,
                    VidaUtilMeses = a.VidaUtilMeses,
                    UbicacionActual = a.UbicacionActual,
                    EstadoActivo = a.EstadoActivo,
                    CondicionFisica = a.CondicionFisica,
                    EsServidor = a.EsServidor,
                    Observaciones = a.Observaciones,
                    FechaRegistro = a.FechaRegistro,
                    IdActivoPadre = a.IdActivoPadre,
                    EsComponente = a.EsComponente,
                    TipoRelacion = a.TipoRelacion,

                    // 🔹 Relaciones
                    NombreProducto = a.IdProductoNavigation != null ? a.IdProductoNavigation.Nombre : null,
                    //NombreMarca = a.IdProductoNavigation != null && a.IdProductoNavigation.IdMarcaNavigation != null
                    //    ? a.IdProductoNavigation.IdMarcaNavigation.Nombre
                    //    : null,
                    NumeroFactura = a.IdFacturaCompraNavigation != null ? a.IdFacturaCompraNavigation.NumeroFactura : null,
                    NumeroOrden = a.IdOrdenEnsamblajeNavigation != null ? a.IdOrdenEnsamblajeNavigation.NumeroOrden : null
                })
                .ToList();

            return lista;
        }



        public Servidore GetServidoreById(int IdServidore)
        {
            using (var context = new InvensisContext())
            {
                return context.Servidores.FirstOrDefault(a => a.IdServidor == IdServidore);
            }
        }

        //public void InsertServidore(Servidore newActivo)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.Servidores.Add(newActivo);
        //        context.SaveChanges();
        //    }
        //}

        public void InsertServidore(ServidoreDTO dto)
        {
            using var context = new InvensisContext();

            // 🔍 Validar TipoServidor permitido
            var tiposPermitidos = new[] { "FISICO", "VIRTUAL", "CLOUD" };
            if (!tiposPermitidos.Contains(dto.TipoServidor?.ToUpper()))
                throw new ArgumentException("TipoServidor no válido. Debe ser FISICO, VIRTUAL o CLOUD.");

            // 🔍 Validar Estado permitido
            var estadosPermitidos = new[] { "ACTIVO", "INACTIVO", "MANTENIMIENTO", "BAJA" };
            if (!string.IsNullOrEmpty(dto.Estado) && !estadosPermitidos.Contains(dto.Estado.ToUpper()))
                throw new ArgumentException("Estado no válido. Debe ser ACTIVO, INACTIVO, MANTENIMIENTO o BAJA.");

            // 🧩 Mapear DTO → Entidad
            var entity = new Servidore
            {
                IdActivo = dto.IdActivo,
                NombreServidor = dto.NombreServidor?.ToUpper(),
                TipoServidor = dto.TipoServidor?.ToUpper(),
                SistemaOperativo = dto.SistemaOperativo?.ToUpper(),
                VersionSo = dto.VersionSo?.ToUpper(),
                Procesadores = dto.Procesadores,
                NucleosPorProcesador = dto.NucleosPorProcesador,
                MemoriaRamGb = dto.MemoriaRamGb,
                AlmacenamientoTb = dto.AlmacenamientoTb,
                DireccionIp = dto.DireccionIp?.ToUpper(),
                DireccionMac = dto.DireccionMac?.ToUpper(),
                Virtualizacion = dto.Virtualizacion?.ToUpper(),
                HostFisico = dto.HostFisico?.ToUpper(),
                UbicacionRack = dto.UbicacionRack?.ToUpper(),
                Proposito = dto.Proposito?.ToUpper(),
                Estado = dto.Estado?.ToUpper()
            };

            // 💾 Guardar en la base de datos
            context.Servidores.Add(entity);
            context.SaveChanges();
        }



        public void UpdateServidore(Servidore servidorActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Servidores.FirstOrDefault(a => a.IdServidor == servidorActualizado.IdServidor);
                if (existente != null)
                {
                    existente.IdActivo = servidorActualizado.IdActivo;
                    existente.NombreServidor = servidorActualizado.NombreServidor.ToUpper();
                    existente.TipoServidor = servidorActualizado.TipoServidor.ToUpper();
                    existente.SistemaOperativo = servidorActualizado.SistemaOperativo;
                    existente.VersionSo = servidorActualizado.VersionSo;
                    existente.Procesadores = servidorActualizado.Procesadores;
                    existente.NucleosPorProcesador = servidorActualizado.NucleosPorProcesador;
                    existente.MemoriaRamGb = servidorActualizado.MemoriaRamGb;
                    existente.AlmacenamientoTb = servidorActualizado.AlmacenamientoTb;
                    existente.DireccionIp = servidorActualizado.DireccionIp;
                    existente.DireccionMac = servidorActualizado.DireccionMac;
                    existente.Virtualizacion = servidorActualizado.Virtualizacion;
                    existente.HostFisico = servidorActualizado.HostFisico;
                    existente.UbicacionRack = servidorActualizado.UbicacionRack;
                    existente.Proposito = servidorActualizado.Proposito;
                    existente.Estado = servidorActualizado.Estado;
                    existente.FechaInstalacion = servidorActualizado.FechaInstalacion;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteServidoreById(int idServidore)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Servidores.FirstOrDefault(a => a.IdServidor == idServidore);
                if (existente != null)
                {
                    context.Servidores.Remove(existente);
                    context.SaveChanges();
                }
            }
        }


        public PagedResult<ServidoreDTO> GetServidorePaginados(
    int pagina,
    int pageSize,
    string? filtro = null,
    string? estado = null)
        {
            using var context = new InvensisContext();

            // 🔍 Base query
            var query = context.Servidores
                .Join(
                    context.Activos,                  // join con tabla Activos
                    s => s.IdActivo,                  // clave foránea en Servidore
                    a => a.IdActivo,                  // clave primaria en Activo
                    (s, a) => new { Servidor = s, Activo = a }
                )
                .Join(
                    context.Productos,                // join con tabla Productos
                    sa => sa.Activo.IdProducto,       // FK en Activo
                    p => p.IdProducto,                // PK en Producto
                    (sa, p) => new { sa.Servidor, sa.Activo, Producto = p }
                )
                .AsQueryable();

            // 🔍 Filtro por nombre, SO o IP
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(x =>
                    x.Servidor.NombreServidor.ToLower().Contains(filtro) ||
                    (x.Servidor.SistemaOperativo != null && x.Servidor.SistemaOperativo.ToLower().Contains(filtro)) ||
                    (x.Servidor.DireccionIp != null && x.Servidor.DireccionIp.ToLower().Contains(filtro)) ||
                    (x.Producto.Nombre != null && x.Producto.Nombre.ToLower().Contains(filtro))
                );
            }

            // 🔍 Filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(x => x.Servidor.Estado == estado);
            }

            // 📊 Total de registros
            var totalItems = query.Count();

            // ✅ Paginación y proyección al DTO
            var servidores = query
                .OrderBy(x => x.Servidor.IdServidor)
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ServidoreDTO
                {
                    IdServidor = x.Servidor.IdServidor,
                    IdActivo = x.Servidor.IdActivo,
                    NombreServidor = x.Servidor.NombreServidor,
                    TipoServidor = x.Servidor.TipoServidor,
                    SistemaOperativo = x.Servidor.SistemaOperativo,
                    VersionSo = x.Servidor.VersionSo,
                    Procesadores = x.Servidor.Procesadores,
                    NucleosPorProcesador = x.Servidor.NucleosPorProcesador,
                    MemoriaRamGb = x.Servidor.MemoriaRamGb,
                    AlmacenamientoTb = x.Servidor.AlmacenamientoTb,
                    DireccionIp = x.Servidor.DireccionIp,
                    DireccionMac = x.Servidor.DireccionMac,
                    Virtualizacion = x.Servidor.Virtualizacion,
                    HostFisico = x.Servidor.HostFisico,
                    UbicacionRack = x.Servidor.UbicacionRack,
                    Proposito = x.Servidor.Proposito,
                    Estado = x.Servidor.Estado,

                    // 🔹 Campos relacionados por los JOINs
                    NombreProducto = x.Producto.Nombre,
                    NumeroSerie = x.Activo.NumeroSerie
                })
                .ToList();

            // 📦 Resultado final
            return new PagedResult<ServidoreDTO>
            {
                Items = servidores,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}
