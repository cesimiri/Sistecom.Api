using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Api.DTO;

namespace Identity.Api.DataRepository
{
    public class BodegaRepository
    {
        public List<Bodega> GetAllBodega()
        {
            using (var context = new InvensisContext())
            {
                return context.Bodegas.ToList();
            }
        }

        public Bodega GetBodegaById(int IdBodega)
        {
            using (var context = new InvensisContext())
            {
                return context.Bodegas.FirstOrDefault(p => p.IdBodega == IdBodega); ;
            }

        }

        public void InsertBodega(BodegaDTO NewItem)
        {
            try
            {
                using var context = new InvensisContext();


                // Generar el Código Principal automático
                var lastCodigo = context.Bodegas
                    .Where(s => s.Codigo.StartsWith("CODB-"))
                    .OrderByDescending(s => s.Codigo)
                    .Select(s => s.Codigo)
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

                var NuevoCodigoBodega = $"CODB-{nextNumber:D4}";

                var nueva = new Bodega
                {

                    Codigo = NuevoCodigoBodega,
                    Nombre = NewItem.Nombre,
                    Direccion = NewItem.Direccion,
                    Telefono = NewItem.Telefono,
                    Responsable = NewItem.Responsable,
                    Tipo = NewItem.Tipo,
                    PermiteVentas = NewItem.PermiteVentas,
                    PermiteEnsamblaje = NewItem.PermiteEnsamblaje,
                    Estado = NewItem.Estado

                };

                context.Bodegas.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la bodega : " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void UpdateBodega(Bodega UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Bodegas
                                         .Where(a => a.IdBodega == UpdItem.IdBodega)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.Codigo = UpdItem.Codigo;
                    registrado.Nombre = UpdItem.Nombre;
                    registrado.Direccion = UpdItem.Direccion;
                    registrado.Telefono = UpdItem.Telefono;
                    registrado.Responsable = UpdItem.Responsable;
                    registrado.Tipo = UpdItem.Tipo;
                    registrado.PermiteVentas = UpdItem.PermiteVentas;
                    registrado.PermiteEnsamblaje = UpdItem.PermiteEnsamblaje;
                    registrado.Estado = UpdItem.Estado;
                    registrado.FechaRegistro = UpdItem.FechaRegistro;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteBodega(Bodega NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.Bodegas.Remove(NewItem);
                context.SaveChanges();
            }
        }

        public void DeleteBodegaById(int IdBodega)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Bodegas
                                         .Where(a => a.IdBodega == IdBodega)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.Bodegas.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}
