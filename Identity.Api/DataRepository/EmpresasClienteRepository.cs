using Modelo.Sistecom.Modelo.Database;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Api.DataRepository
{
    public class EmpresaClienteDataRepository
    {
        public List<EmpresasCliente> GetAllEmpresasClientes()
        {
            using (var context = new InvensisContext())
            {
                return context.EmpresasClientes.ToList();
            }
        }

        public EmpresasCliente GetEmpresaClienteById(int idEmpresaCliente)
        {
            using (var context = new InvensisContext())
            {
                return context.EmpresasClientes.FirstOrDefault(p => p.IdEmpresa == idEmpresaCliente); ;
            }
           
        }

        public void InsertEmpresaCliente(EmpresasCliente NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.EmpresasClientes.Add(NewItem);
                context.SaveChanges();
            }
        }

        public void UpdateEmpresaCliente(EmpresasCliente UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.EmpresasClientes
                                         .Where(a => a.IdEmpresa == UpdItem.IdEmpresa)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.Ruc = UpdItem.Ruc;
                    registrado.RazonSocial = UpdItem.RazonSocial;
                    registrado.NombreComercial = UpdItem.NombreComercial;
                    registrado.Direccion = UpdItem.Direccion;
                    registrado.Ciudad = UpdItem.Ciudad;
                    registrado.Telefono = UpdItem.Telefono;
                    registrado.Email = UpdItem.Email;
                    registrado.ContactoPrincipal = UpdItem.ContactoPrincipal;
                    registrado.TelefonoContacto = UpdItem.TelefonoContacto;
                    registrado.TipoCliente = UpdItem.TipoCliente;
                    registrado.LimiteCredito = UpdItem.LimiteCredito;
                    registrado.DiasCredito = UpdItem.DiasCredito;
                    registrado.Estado = UpdItem.Estado;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteEmpresaCliente(EmpresasCliente NewItem)
        {
            using (var context = new InvensisContext())
            {
                context.EmpresasClientes.Remove(NewItem);
                context.SaveChanges();
            }
        }

        public void DeleteEmpresaClienteById(int IdRegistrado)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.EmpresasClientes
                                         .Where(a => a.IdEmpresa == IdRegistrado)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.EmpresasClientes.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}

