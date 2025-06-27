using Identity.Api.DTO;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class LicenciaRepository
    {
        public List<LicenciaDTO> LicenciaInfoAll()
        {
            using var context = new InvensisContext();
            return context.Licencias
                .Include(s => s.IdTipoLicencia)
                .Include(s => s.IdProductoNavigation)
                .Include(s => s.IdFacturaCompraNavigation)



                .Select(s => new LicenciaDTO
                {

                    IdLicencia = s.IdLicencia,
                    IdTipoLicencia = s.IdTipoLicencia,
                    IdProducto = s.IdProducto,
                    IdFacturaCompra = s.IdFacturaCompra,
                    NumeroLicencia = s.NumeroLicencia,
                    ClaveProducto = s.ClaveProducto,
                    FechaAdquisicion = s.FechaAdquisicion,
                    FechaInicioVigencia = s.FechaInicioVigencia,
                    FechaFinVigencia = s.FechaFinVigencia,
                    TipoSuscripcion = s.TipoSuscripcion,
                    CantidadUsuarios = s.CantidadUsuarios,
                    CostoLicencia = s.CostoLicencia,
                    RenovacionAutomatica = s.RenovacionAutomatica,
                    Observaciones = s.Observaciones,
                    Estado = s.Estado,
                    

                    // campos relacionados:
                    nombreLicencia = s.IdTipoLicenciaNavigation.Nombre,
                    nombreProducto = s.IdProductoNavigation.Nombre,
                    numeroFactura = s.IdFacturaCompraNavigation.NumeroFactura

                })
                .ToList();
        }


        public Licencia GetLicenciaById(int IdLicencia)
        {
            using (var context = new InvensisContext())
            {
                return context.Licencias.FirstOrDefault(m => m.IdLicencia == IdLicencia);
            }
        }

        public void InsertLicencia(LicenciaDTO dto)
        {
            try 
            {
                using var context = new InvensisContext();
                //validación para el ingreso de los id relacionados.
                var idTiposLicencia = context.TiposLicencia.Find(dto.IdTipoLicencia);
                var idProducto = context.Productos.Find(dto.IdProducto);
                var idFactura = context.FacturasCompras.Find(dto.IdFacturaCompra);

                if (idTiposLicencia == null || idProducto == null || idFactura == null )
                {
                    throw new Exception("Esa idTiposLicencia, idProducto, idFactura no existe en la base de datos.");
                }

                // Generar el Código Licenica automático
                var lastCodigo = context.Productos
                    .Where(s => s.CodigoPrincipal.StartsWith("LIC-"))
                    .OrderByDescending(s => s.CodigoPrincipal)
                    .Select(s => s.CodigoPrincipal)
                    .FirstOrDefault();

                int nextNumber = 1;
                if (lastCodigo != null)
                {
                    var lastNumberStr = lastCodigo.Split('-').Last();
                    if (int.TryParse(lastNumberStr, out var parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                var NuevoCodigoPrincipal = $"LIC-{nextNumber:D4}";

                var nueva = new Licencia
                {
                    IdTipoLicencia = dto.IdTipoLicencia,
                    IdProducto = dto.IdProducto,
                    IdFacturaCompra = dto.IdFacturaCompra,
                    NumeroLicencia = dto.NumeroLicencia,
                    ClaveProducto = dto.ClaveProducto,
                    FechaAdquisicion = dto.FechaAdquisicion,
                    FechaInicioVigencia = dto.FechaInicioVigencia,
                    FechaFinVigencia = dto.FechaFinVigencia,
                    TipoSuscripcion = dto.TipoSuscripcion,
                    CantidadUsuarios = dto.CantidadUsuarios,
                    CostoLicencia = dto.CostoLicencia,
                    RenovacionAutomatica = dto.RenovacionAutomatica,
                    Observaciones = dto.Observaciones,
                    Estado = dto.Estado,


                };
                context.Licencias.Add(nueva);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Producto: " + ex.InnerException?.Message ?? ex.Message);
            }
            
        }


        public void UpdateLicencia(Licencia licenciaActualizada)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Licencias.FirstOrDefault(a => a.IdLicencia == licenciaActualizada.IdLicencia);
                if (existente != null)
                {
                    existente.IdTipoLicencia = licenciaActualizada.IdTipoLicencia;
                    existente.IdProducto = licenciaActualizada.IdProducto;
                    existente.IdFacturaCompra = licenciaActualizada.IdFacturaCompra;
                    existente.NumeroLicencia = licenciaActualizada.NumeroLicencia;
                    existente.ClaveProducto = licenciaActualizada.ClaveProducto;
                    existente.FechaAdquisicion = licenciaActualizada.FechaAdquisicion;
                    existente.FechaInicioVigencia = licenciaActualizada.FechaInicioVigencia;
                    existente.FechaFinVigencia = licenciaActualizada.FechaFinVigencia;
                    existente.TipoSuscripcion = licenciaActualizada.TipoSuscripcion;
                    existente.CantidadUsuarios = licenciaActualizada.CantidadUsuarios;
                    existente.CostoLicencia = licenciaActualizada.CostoLicencia;
                    existente.RenovacionAutomatica = licenciaActualizada.RenovacionAutomatica;
                    existente.Observaciones = licenciaActualizada.Observaciones;
                    existente.Estado = licenciaActualizada.Estado;
                    existente.FechaRegistro = licenciaActualizada.FechaRegistro;


                    context.SaveChanges();
                }
            }
        }

        //public void DeleteLicencia(Licencia activoToDelete)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.Licencias.Remove(activoToDelete);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteLicenciaById(int idLicencia)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Licencias.FirstOrDefault(a => a.IdLicencia == idLicencia);
                if (existente != null)
                {
                    context.Licencias.Remove(existente);
                    context.SaveChanges();
                }
            }
        }
    }
}
