using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class MantenimientoRepository
    {


        public Mantenimiento GetMantenimientoById(int IdMantenimiento)
        {
            using (var context = new InvensisContext())
            {
                return context.Mantenimientos.FirstOrDefault(a => a.IdActivo == IdMantenimiento);
            }
        }

        public void InsertMantenimiento(MantenimientoDTO dto)
        {
            using var context = new InvensisContext();

            // 1️⃣ Validaciones backend
            if (dto.IdActivo <= 0)
                throw new Exception("El activo es obligatorio.");

            if (dto.FechaProgramada == default)
                throw new Exception("La fecha programada es obligatoria.");

            if (string.IsNullOrWhiteSpace(dto.TipoMantenimiento))
                throw new Exception("El tipo de mantenimiento es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Descripcion))
                throw new Exception("La descripción es obligatoria.");

            if (!dto.IdDepartamentoSolicita.HasValue)
                throw new Exception("El departamento solicitante es obligatorio.");

            // 2️⃣ Generar NúmeroOrdenServicio basado solo en el año actual
            int year = DateTime.Now.Year;

            // Obtener la última secuencia del año actual
            int ultimaSecuencia = context.Mantenimientos
                .Where(m => m.NumeroOrdenServicio.StartsWith($"OrdenServicio#{year}-"))
                .Select(m => m.NumeroOrdenServicio)
                .AsEnumerable() // pasar a memoria para usar Substring
                .Select(nos =>
                {
                    int index = nos.LastIndexOf('-');
                    return int.TryParse(nos.Substring(index + 1), out int seq) ? seq : 0;
                })
                .DefaultIfEmpty(0)
                .Max();

            int nuevaSecuencia = ultimaSecuencia + 1;

            string numeroOrdenServicio = $"OrdenServicio#{year}-{nuevaSecuencia:D4}";

            // 3️⃣ Crear entidad
            var mantenimiento = new Mantenimiento
            {
                IdActivo = dto.IdActivo,
                FechaProgramada = dto.FechaProgramada,
                FechaRealizada = dto.FechaRealizada,
                TipoMantenimiento = dto.TipoMantenimiento,
                Descripcion = dto.Descripcion,
                Diagnostico = dto.Diagnostico,
                AccionesRealizadas = dto.AccionesRealizadas,
                RepuestosUsados = dto.RepuestosUsados,
                CostoManoObra = dto.CostoManoObra,
                CostoRepuestos = dto.CostoRepuestos,
                CostoTotal = dto.CostoTotal,
                TiempoFueraServicioHoras = dto.TiempoFueraServicioHoras,
                TecnicoResponsable = dto.TecnicoResponsable,
                ProveedorServicio = dto.ProveedorServicio,
                NumeroOrdenServicio = numeroOrdenServicio,
                GarantiaTrabajosDias = dto.GarantiaTrabajosDias,
                ProximoMantenimiento = dto.ProximoMantenimiento,
                Estado = dto.Estado ?? "PROGRAMADO",
                InformeTecnico = dto.InformeTecnico,
                IdDepartamentoSolicita = dto.IdDepartamentoSolicita,
                CedulaTecnico = dto.CedulaTecnico
            };

            // 4️⃣ Guardar
            context.Mantenimientos.Add(mantenimiento);
            context.SaveChanges();
        }

        //public void UpdateMantenimiento(MantenimientoDTO updItem)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        var existente = context.Mantenimientos.FirstOrDefault(a => a.IdMantenimiento == updItem.IdMantenimiento);
        //        if (existente != null)
        //        {
        //            //existente.IdActivo = updItem.IdActivo;
        //            existente.FechaProgramada = updItem.FechaProgramada;
        //            existente.FechaRealizada = updItem.FechaRealizada;
        //            existente.TipoMantenimiento = updItem.TipoMantenimiento;
        //            existente.Descripcion = updItem.Descripcion;
        //            existente.Diagnostico = updItem.Diagnostico;
        //            existente.AccionesRealizadas = updItem.AccionesRealizadas;
        //            existente.RepuestosUsados = updItem.RepuestosUsados;
        //            existente.CostoManoObra = updItem.CostoManoObra;
        //            existente.CostoRepuestos = updItem.CostoRepuestos;
        //            existente.CostoTotal = updItem.CostoTotal;
        //            existente.TiempoFueraServicioHoras = updItem.TiempoFueraServicioHoras;
        //            existente.TecnicoResponsable = updItem.TecnicoResponsable;
        //            existente.ProveedorServicio = updItem.ProveedorServicio;
        //            existente.NumeroOrdenServicio = updItem.NumeroOrdenServicio;
        //            existente.GarantiaTrabajosDias = updItem.GarantiaTrabajosDias;
        //            existente.ProximoMantenimiento = updItem.ProximoMantenimiento;
        //            existente.Estado = updItem.Estado;
        //            existente.InformeTecnico = updItem.InformeTecnico;

        //            context.SaveChanges();
        //        }
        //    }
        //}

        public void UpdateMantenimiento(MantenimientoDTO updItem)
        {
            using var context = new InvensisContext();

            // 1️⃣ Buscar el mantenimiento SELECCIONADO
            var existente = context.Mantenimientos
                .FirstOrDefault(m => m.IdMantenimiento == updItem.IdMantenimiento);

            if (existente == null)
                throw new Exception("No existe el mantenimiento seleccionado.");

            // 2️⃣ Validar estado de ESA orden
            if (!string.IsNullOrEmpty(existente.Estado) &&
                existente.Estado.ToUpper() == "COMPLETADO")
            {
                throw new InvalidOperationException(
                    "NO SE PUEDE GUARDAR PORQUE LA ORDEN YA FUE COMPLETADA."
                );
            }

            // 3️⃣ Crear NUEVO registro histórico
            var nuevo = new Mantenimiento
            {
                IdActivo = existente.IdActivo, // 🔥 mismo activo
                NumeroOrdenServicio = existente.NumeroOrdenServicio, // 🔥 misma orden

                FechaProgramada = updItem.FechaProgramada,
                FechaRealizada = updItem.FechaRealizada,
                TipoMantenimiento = updItem.TipoMantenimiento,
                Descripcion = updItem.Descripcion,
                Diagnostico = updItem.Diagnostico,
                AccionesRealizadas = updItem.AccionesRealizadas,
                RepuestosUsados = updItem.RepuestosUsados,
                CostoManoObra = updItem.CostoManoObra,
                CostoRepuestos = updItem.CostoRepuestos,
                CostoTotal = updItem.CostoTotal,
                TiempoFueraServicioHoras = updItem.TiempoFueraServicioHoras,
                TecnicoResponsable = updItem.TecnicoResponsable,
                ProveedorServicio = updItem.ProveedorServicio,

                GarantiaTrabajosDias = updItem.GarantiaTrabajosDias,
                ProximoMantenimiento = updItem.ProximoMantenimiento,
                Estado = updItem.Estado,
                InformeTecnico = updItem.InformeTecnico,
                IdDepartamentoSolicita = updItem.IdDepartamentoSolicita,
                CedulaTecnico = updItem.CedulaTecnico
            };

            context.Mantenimientos.Add(nuevo);
            context.SaveChanges();
        }






        public void DeleteMantenimientoById(int IdMantenimiento)
        {
            using var context = new InvensisContext();

            var existente = context.Mantenimientos
                .FirstOrDefault(a => a.IdMantenimiento == IdMantenimiento);

            if (existente == null)
                throw new Exception("Mantenimiento no encontrado");

            context.Mantenimientos.Remove(existente);
            context.SaveChanges();
        }


        //busqueda de tecnico por cedula 
        public UsuarioDTO ObtenerApellidosNombreByCedula(string cedula)
        {
            using var context = new InvensisContext();

            return context.Usuarios

                .Where(s => s.Cedula == cedula)
                .Select(s => new UsuarioDTO
                {
                    Cedula = s.Cedula,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    Extension = s.Extension,
                    Estado = s.Estado,


                    ApellidosNombre = s.Apellidos + " " + s.Nombres,
                })
                .FirstOrDefault();

        }

        //busqueda por codigo de activo 
        public ActivoDTO? ObtnerActivoByCodigo(string codigoActivo)
        {
            using var context = new InvensisContext();

            return context.Activos
                .Where(a => a.CodigoActivo == codigoActivo)
                .Select(a => new ActivoDTO
                {
                    IdActivo = a.IdActivo,
                    CodigoActivo = a.CodigoActivo,
                    IdProducto = a.IdProducto,
                    NumeroSerie = a.NumeroSerie,
                    NumeroParte = a.NumeroParte,
                    //FechaAdquisicion = a.FechaAdquisicion,
                    //FechaGarantiaFin = a.FechaGarantiaFin,
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

                    // Relaciones
                    NombreProducto = a.IdProductoNavigation != null
                        ? a.IdProductoNavigation.Nombre
                        : null,

                    NumeroFactura = a.IdFacturaCompraNavigation != null
                        ? a.IdFacturaCompraNavigation.NumeroFactura
                        : null,

                    NumeroOrden = a.IdOrdenEnsamblajeNavigation != null
                        ? a.IdOrdenEnsamblajeNavigation.NumeroOrden
                        : null,


                    TipoRelacion = a.TipoRelacion,
                    IdActivoPadre = a.IdActivoPadre,
                    EsComponente = a.EsComponente
                })
                .FirstOrDefault();
        }

        //paginado
        public PagedResult<MantenimientoDTO> GetMantenimientoPaginados(
        int pagina,
        int pageSize,
        string? filtro = null,
        string? estadoActivo = null)
        {
            using var context = new InvensisContext();

            var query = context.Mantenimientos
                .Include(m => m.IdActivoNavigation)
                .Include(m => m.CedulaTecnicoNavigation)
                .AsQueryable();

            // 🔎 Filtro texto (Código Activo o Técnico)
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var f = filtro.Trim().ToUpper();

                query = query.Where(m =>
                    (m.IdActivoNavigation != null &&
                     m.IdActivoNavigation.CodigoActivo != null &&
                     m.IdActivoNavigation.CodigoActivo.ToUpper().Contains(f)) ||

                    (m.CedulaTecnicoNavigation != null &&
                     (
                         (m.CedulaTecnicoNavigation.Apellidos + " " +
                          m.CedulaTecnicoNavigation.Nombres)
                         .ToUpper()
                         .Contains(f)
                     ))
                );
            }

            // 🔎 Filtro por estado
            if (!string.IsNullOrWhiteSpace(estadoActivo))
            {
                var e = estadoActivo.Trim().ToUpper();
                query = query.Where(m =>
                    m.Estado != null &&
                    m.Estado.ToUpper().Contains(e)
                );
            }

            var totalItems = query.Count();

            query = query
                .OrderByDescending(m => m.FechaProgramada)
                .ThenByDescending(m => m.IdMantenimiento);

            var items = query
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MantenimientoDTO
                {
                    IdMantenimiento = m.IdMantenimiento,
                    IdActivo = m.IdActivo,
                    CodigoActivo = m.IdActivoNavigation != null
                        ? m.IdActivoNavigation.CodigoActivo
                        : null,

                    FechaProgramada = m.FechaProgramada,
                    FechaRealizada = m.FechaRealizada,
                    TipoMantenimiento = m.TipoMantenimiento,
                    Descripcion = m.Descripcion,
                    Diagnostico = m.Diagnostico,
                    AccionesRealizadas = m.AccionesRealizadas,
                    RepuestosUsados = m.RepuestosUsados,

                    CostoManoObra = m.CostoManoObra,
                    CostoRepuestos = m.CostoRepuestos,
                    CostoTotal = m.CostoTotal,

                    TiempoFueraServicioHoras = m.TiempoFueraServicioHoras,
                    TecnicoResponsable = m.TecnicoResponsable,
                    ProveedorServicio = m.ProveedorServicio,
                    NumeroOrdenServicio = m.NumeroOrdenServicio,
                    GarantiaTrabajosDias = m.GarantiaTrabajosDias,
                    ProximoMantenimiento = m.ProximoMantenimiento,

                    Estado = m.Estado,
                    InformeTecnico = m.InformeTecnico,
                    IdDepartamentoSolicita = m.IdDepartamentoSolicita,
                    CedulaTecnico = m.CedulaTecnico,

                    ApellidosNombre = m.CedulaTecnicoNavigation != null
                        ? m.CedulaTecnicoNavigation.Apellidos + " " +
                          m.CedulaTecnicoNavigation.Nombres
                        : null
                })
                .ToList();

            return new PagedResult<MantenimientoDTO>
            {
                Items = items,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }


    }
}
