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

        public Servidore GetServidoreById(int IdServidore)
        {
            using (var context = new InvensisContext())
            {
                return context.Servidores.FirstOrDefault(a => a.IdServidor == IdServidore);
            }
        }

        public void InsertServidore(Servidore newActivo)
        {
            using (var context = new InvensisContext())
            {
                context.Servidores.Add(newActivo);
                context.SaveChanges();
            }
        }


        public void UpdateServidore(Servidore servidorActualizado)
        {
            using (var context = new InvensisContext())
            {
                var existente = context.Servidores.FirstOrDefault(a => a.IdServidor == servidorActualizado.IdServidor);
                if (existente != null)
                {
                    existente.IdActivo = servidorActualizado.IdActivo;
                    existente.NombreServidor = servidorActualizado.NombreServidor;
                    existente.TipoServidor = servidorActualizado.TipoServidor;
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

        public void DeleteServidore(Servidore activoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Servidores.Remove(activoToDelete);
                context.SaveChanges();
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
    }
}
