using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public void InsertBodega(Bodega NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.Bodegas.Add(NewItem);
                context.SaveChanges();
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
