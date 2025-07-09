using Identity.Api.DTO;
using Identity.Api.Paginado;
using Identity.Api.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Api.Paginado;

namespace Identity.Api.DataRepository
{
    public class EmpresaClienteDataRepository
    {
        public List<EmpresasCliente> GetAllEmpresasClientes()
        {
            using (var context = new InvensisContext())
            {
                return context.EmpresasClientes
                              .OrderBy(e => e.RazonSocial) // o el campo que corresponda
                              .ToList();
            }
        }

        public EmpresasCliente GetEmpresaClienteById(string ruc)
        {
            using (var context = new InvensisContext())
            {
                return context.EmpresasClientes.FirstOrDefault(p => p.Ruc == ruc); ;
            }
           
        }

        //public void InsertEmpresaCliente(EmpresasCliente NewItem)
        //{
        //    using (var context = new InvensisContext())
        //    {
        //        context.EmpresasClientes.Add(NewItem);
        //        context.SaveChanges();
        //    }
        //}
        public void InsertEmpresaCliente(EmpresasCliente NewItem)
        {
            using (var context = new InvensisContext())
            {
                var nuevo = new EmpresasCliente
                {
                    Ruc = NewItem.Ruc, // asumimos que el RUC debe mantenerse como fue ingresado
                    RazonSocial = NewItem.RazonSocial?.ToUpper(),
                    NombreComercial = NewItem.NombreComercial?.ToUpper(),
                    DireccionMatriz = NewItem.DireccionMatriz?.ToUpper(),
                    Ciudad = NewItem.Ciudad?.ToUpper(),
                    Telefono = NewItem.Telefono,
                    Email = NewItem.Email,
                    ContactoPrincipal = NewItem.ContactoPrincipal?.ToUpper(),
                    TelefonoContacto = NewItem.TelefonoContacto,
                    TipoCliente = NewItem.TipoCliente,
                    LimiteCredito = NewItem.LimiteCredito,
                    DiasCredito = NewItem.DiasCredito,
                    Estado = NewItem.Estado
                };

                context.EmpresasClientes.Add(nuevo);
                context.SaveChanges();
            }
        }

        public void UpdateEmpresaCliente(EmpresasCliente UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.EmpresasClientes
                                         .Where(a => a.Ruc == UpdItem.Ruc)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.RazonSocial = UpdItem.RazonSocial?.ToUpper();
                    registrado.NombreComercial = UpdItem.NombreComercial?.ToUpper();
                    registrado.DireccionMatriz = UpdItem.DireccionMatriz?.ToUpper();
                    registrado.Ciudad = UpdItem.Ciudad?.ToUpper();
                    registrado.Telefono = UpdItem.Telefono;
                    registrado.Email = UpdItem.Email;
                    registrado.ContactoPrincipal = UpdItem.ContactoPrincipal?.ToUpper();
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

        public void DeleteEmpresaClienteById(string ruc)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.EmpresasClientes
                                         .Where(a => a.Ruc == ruc)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.EmpresasClientes.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }

        //PAGINADA 
        public PagedResult<EmpresasCliente> GetEmpresasPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.EmpresasClientes
                
                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.NombreComercial.ToLower().Contains(filtro) 
                    );
            }

            // Aplicar filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(u => u.Estado == estado);
            }

            // Total de registros filtrados
            var totalItems = query.Count();

            // Obtener página solicitada con paginado
            var usuarios = query
                .OrderBy(u => u.Ruc) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new EmpresasCliente
                {
                    Ruc = s.Ruc,
                    RazonSocial = s.RazonSocial,
                    NombreComercial = s.NombreComercial,
                    DireccionMatriz = s.DireccionMatriz,
                    Ciudad = s.Ciudad,
                    Telefono = s.Telefono,
                    Email = s.Email,
                    ContactoPrincipal = s.ContactoPrincipal,
                    TelefonoContacto = s.TelefonoContacto,
                    TipoCliente = s.TipoCliente,
                    LimiteCredito = s.LimiteCredito,
                    DiasCredito = s.DiasCredito,
                    Estado = s.Estado


                })
                .ToList();

            return new PagedResult<EmpresasCliente>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }
    }
}

