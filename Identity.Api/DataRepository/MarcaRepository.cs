using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class MarcaRepository
    {
        public List<Marca> GetAllMarca()
        {
            using (var context = new InvensisContext())
            {
                return context.Marcas.ToList();
            }
        }

        public Marca GetMarcaById(int idMarca)
        {
            using (var context = new InvensisContext())
            {
                return context.Marcas.FirstOrDefault(p => p.IdMarca == idMarca); ;
            }

        }

        public void InsertMarca(MarcaDTO NewItem)
        {
            try
            {
                using var context = new InvensisContext();

                // Generar el Código Principal automático
                var lastCodigo = context.Marcas
                    .Where(s => s.Codigo.StartsWith("MARCA-"))
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

                var NuevoCodigoPrincipal = $"MARCA-{nextNumber:D4}";

                var nueva = new Marca
                {

                    Codigo = NuevoCodigoPrincipal,
                    Nombre = NewItem.Nombre,
                    Descripcion = NewItem.Descripcion,
                    PaisOrigen = NewItem.PaisOrigen,
                    SitioWeb = NewItem.SitioWeb,
                    LogoUrl = NewItem.LogoUrl,
                    EsMarcaPropia = NewItem.EsMarcaPropia,
                    
                    Estado = NewItem.Estado,

                };

                context.Marcas.Add(nueva);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la Marca: " + ex.InnerException?.Message ?? ex.Message);
            }
        }

        public void UpdateMarca(Marca UpdItem)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Marcas
                                         .Where(a => a.IdMarca == UpdItem.IdMarca)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    registrado.Codigo = UpdItem.Codigo;
                    registrado.Nombre = UpdItem.Nombre;
                    registrado.Descripcion = UpdItem.Descripcion;
                    registrado.PaisOrigen = UpdItem.PaisOrigen;
                    registrado.SitioWeb = UpdItem.SitioWeb;
                    registrado.LogoUrl = UpdItem.LogoUrl;
                    registrado.EsMarcaPropia = UpdItem.EsMarcaPropia;
                    registrado.Estado = UpdItem.Estado;

                    context.SaveChanges();
                }
            }
        }

        //public void DeleteEmpresaCliente(EmpresasCliente NewItem)
        //{

        //    using (var context = new InvensisContext())
        //    {
        //        context.EmpresasClientes.Remove(NewItem);
        //        context.SaveChanges();
        //    }
        //}

        public void DeleteMarcaById(int  idMarca)
        {
            using (var context = new InvensisContext())
            {
                var registrado = context.Marcas
                                         .Where(a => a.IdMarca == idMarca)
                                         .FirstOrDefault();

                if (registrado != null)
                {
                    context.Marcas.Remove(registrado);
                    context.SaveChanges();
                }
            }
        }
    }
}
