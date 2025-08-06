using Identity.Api.DTO;
using Identity.Api.Paginado;
using Microsoft.EntityFrameworkCore;
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

        public MarcaDTO GetMarcaById(int idMarca)
        {
            using (var context = new InvensisContext())
            {
                var marca = context.Marcas
                    .Include(p => p.IdCategoriaNavigation) // Trae los datos de la categoría
                    .FirstOrDefault(p => p.IdMarca == idMarca);

                if (marca == null)
                    return null;

                return new MarcaDTO
                {
                    IdMarca = marca.IdMarca,
                    Codigo = marca.Codigo,
                    Nombre = marca.Nombre,
                    Descripcion = marca.Descripcion,
                    PaisOrigen = marca.PaisOrigen,
                    SitioWeb = marca.SitioWeb,
                    LogoUrl = marca.LogoUrl,
                    EsMarcaPropia = marca.EsMarcaPropia,
                    Estado = marca.Estado,
                    IdCategoria = marca.IdCategoria,
                    CategoriaNombre = marca.IdCategoriaNavigation?.Nombre // 👈 trae el nombre de la categoría
                };
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
                    Nombre = NewItem.Nombre?.ToUpper(),
                    Descripcion = NewItem.Descripcion?.ToUpper(),
                    PaisOrigen = NewItem.PaisOrigen?.ToUpper(),
                    SitioWeb = NewItem.SitioWeb,
                    LogoUrl = NewItem.LogoUrl,
                    EsMarcaPropia = NewItem.EsMarcaPropia,
                    Estado = NewItem.Estado,
                    IdCategoria = NewItem.IdCategoria // ✅ nuevo campo
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
                    registrado.Nombre = UpdItem.Nombre?.ToUpper();
                    registrado.Descripcion = UpdItem.Descripcion?.ToUpper();
                    registrado.PaisOrigen = UpdItem.PaisOrigen?.ToUpper();
                    registrado.SitioWeb = UpdItem.SitioWeb;
                    registrado.LogoUrl = UpdItem.LogoUrl;
                    registrado.EsMarcaPropia = UpdItem.EsMarcaPropia;
                    registrado.Estado = UpdItem.Estado;
                    registrado.IdCategoria = UpdItem.IdCategoria;

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

        public void DeleteMarcaById(int idMarca)
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


        //PAGINADA 
        public PagedResult<MarcaDTO> GetMarcaPaginados(int pagina, int pageSize, string? filtro = null, string? estado = null)
        {
            using var context = new InvensisContext();

            var query = context.Marcas

                .AsQueryable();

            // Aplicar filtro por texto (en clave, nombres, apellidos o lo que necesites)
            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(u =>
                    u.Nombre.ToLower().Contains(filtro));
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
                .OrderBy(u => u.IdMarca) // importante ordenar antes de Skip/Take
                .Skip((pagina - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new MarcaDTO
                {
                    IdMarca = s.IdMarca,
                    Codigo = s.Codigo,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    PaisOrigen = s.PaisOrigen,
                    SitioWeb = s.SitioWeb,
                    LogoUrl = s.LogoUrl,
                    EsMarcaPropia = s.EsMarcaPropia,
                    Estado = s.Estado,
                    IdCategoria = s.IdCategoria, // ✅ nuevo campo
                    CategoriaNombre = s.IdCategoriaNavigation.Nombre
                })
                .ToList();

            return new PagedResult<MarcaDTO>
            {
                Items = usuarios,
                TotalItems = totalItems,
                Page = pagina,
                PageSize = pageSize
            };
        }

        //busqueda las marcas por idCategoria
        public List<MarcaDTO> GetMarcasByIdCategoria(int idCategoria)
        {
            using (var context = new InvensisContext())
            {
                return context.Marcas
                    .Include(p => p.IdCategoriaNavigation)
                    .Where(p => p.IdCategoria == idCategoria)
                    .Select(m => new MarcaDTO
                    {
                        IdMarca = m.IdMarca,
                        Codigo = m.Codigo,
                        Nombre = m.Nombre,
                        Descripcion = m.Descripcion,
                        PaisOrigen = m.PaisOrigen,
                        SitioWeb = m.SitioWeb,
                        LogoUrl = m.LogoUrl,
                        EsMarcaPropia = m.EsMarcaPropia,
                        Estado = m.Estado,
                        IdCategoria = m.IdCategoria,
                        CategoriaNombre = m.IdCategoriaNavigation.Nombre
                    })
                    .ToList();
            }
        }

    }
}
