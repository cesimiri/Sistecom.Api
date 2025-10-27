using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class ServiciosServidorRepository
    {
        public List<ServiciosServidor> ServiciosServidorInfoAll()
        {
            using (var context = new InvensisContext())
            {
                return context.ServiciosServidors.ToList();
            }
        }

        //trae todo lso servidores 
        public List<ServidoreDTO> GetSetvidores()
        {
            using var context = new InvensisContext();
            return context.Servidores

                // 🔍 Filtro por estado ACTIVA (ignora mayúsculas/minúsculas)
                .Where(s => s.Estado != null && s.Estado.ToUpper() == "ACTIVO")
                .Select(s => new ServidoreDTO
                {
                    IdServidor = s.IdServidor,
                    IdActivo = s.IdActivo,
                    NombreServidor = s.NombreServidor,
                    TipoServidor = s.TipoServidor,
                    SistemaOperativo = s.SistemaOperativo,
                    VersionSo = s.VersionSo,
                    Procesadores = s.Procesadores,
                    NucleosPorProcesador = s.NucleosPorProcesador,
                    MemoriaRamGb = s.MemoriaRamGb,
                    AlmacenamientoTb = s.AlmacenamientoTb,
                    DireccionIp = s.DireccionIp,
                    DireccionMac = s.DireccionMac,
                    Virtualizacion = s.Virtualizacion,
                    HostFisico = s.HostFisico,
                    UbicacionRack = s.UbicacionRack,
                    Proposito = s.Proposito,

                    Estado = s.Estado,
                })
                .ToList();
        }

        public ServiciosServidor GetServiciosServidorById(int IdServiciosServidor)
        {
            using (var context = new InvensisContext())
            {
                return context.ServiciosServidors.FirstOrDefault(a => a.IdServicio == IdServiciosServidor);
            }
        }

        public void InsertServiciosServidor(ServiciosServidorDTO dto)
        {
            using var context = new InvensisContext();

            var nuevoServicio = new ServiciosServidor
            {
                IdServidor = dto.IdServidor,
                NombreServicio = dto.NombreServicio?.ToUpper().Trim() ?? string.Empty,
                TipoServicio = dto.TipoServicio?.ToUpper().Trim(),
                Puerto = dto.Puerto,
                Version = dto.Version?.ToUpper().Trim(),
                Estado = dto.Estado?.ToUpper().Trim(),
                FechaInstalacion = dto.FechaInstalacion,
                Observaciones = dto.Observaciones?.ToUpper().Trim()
            };

            context.ServiciosServidors.Add(nuevoServicio);
            context.SaveChanges();
        }



        public void UpdateServiciosServidor(ServiciosServidor historial)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.ServiciosServidors.FirstOrDefault(a => a.IdServicio == historial.IdServicio);
                if (existente != null)
                {
                    existente.IdServidor = historial.IdServidor;
                    existente.NombreServicio = historial.NombreServicio;
                    existente.TipoServicio = historial.TipoServicio;
                    existente.Puerto = historial.Puerto;
                    existente.Version = historial.Version;
                    existente.Estado = historial.Estado;
                    existente.FechaInstalacion = historial.FechaInstalacion;
                    existente.Observaciones = historial.Observaciones;

                    context.SaveChanges();
                }
            }
        }



        public void DeleteServiciosServidorById(int idServiciosServidor)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.ServiciosServidors.FirstOrDefault(a => a.IdServicio == idServiciosServidor);
                if (existente != null)
                {
                    context.ServiciosServidors.Remove(existente);
                    context.SaveChanges();
                }
            }
        }

        //paginado
        public PagedResult<ServiciosServidorDTO> GetServiciosServidorPaginados(
        int pagina,
        int pageSize,
        string? filtro = null,
        string? estado = null)
        {
            using var context = new InvensisContext();

            // 🔹 Incluimos relación con Servidor
            var query = context.ServiciosServidors
                .Include(s => s.IdServidorNavigation)
                .AsQueryable();

            // 🔍 Filtro por nombre del servidor
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var f = filtro.Trim().ToUpper();
                query = query.Where(s =>
                    s.IdServidorNavigation != null &&
                    s.IdServidorNavigation.NombreServidor.ToUpper().Contains(f)
                );
            }

            // 🔍 Filtro por estado
            if (!string.IsNullOrWhiteSpace(estado))
            {
                var e = estado.Trim().ToUpper();
                query = query.Where(s => s.Estado != null && s.Estado.ToUpper().Contains(e));
            }

            // 📊 Total antes de paginar
            var totalItems = query.Count();

            // 🔠 Ordenamiento por nombre del servidor
            query = query.OrderBy(s => s.IdServidorNavigation.NombreServidor);

            // 🔄 Paginación + proyección a DTO
            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ServiciosServidorDTO
                {
                    IdServicio = s.IdServicio,
                    IdServidor = s.IdServidor,
                    NombreServicio = s.NombreServicio,
                    TipoServicio = s.TipoServicio,
                    Puerto = s.Puerto,
                    Version = s.Version,
                    Estado = s.Estado,
                    FechaInstalacion = s.FechaInstalacion,
                    Observaciones = s.Observaciones,
                    NombreServidor = s.IdServidorNavigation != null
                        ? s.IdServidorNavigation.NombreServidor
                        : null
                })
                .ToList();

            // 📦 Retornar resultado paginado
            return new PagedResult<ServiciosServidorDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }

    }
}
