using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.DataRepository
{
    public class ContratoDataRepository
    {
        public List<Contrato> GetAllContratos()
        {
            using (var context = new InvensisContext())
            {
                return context.Contratos.ToList();
            }
        }

        public Contrato GetContratoById(int idContrato)
        {
            using (var context = new InvensisContext())
            {
                return context.Contratos.FirstOrDefault(c => c.IdContrato == idContrato);
            }
        }

        public void InsertContrato(Contrato newContrato)
        {
            using (var context = new InvensisContext())
            {
                context.Contratos.Add(newContrato);
                context.SaveChanges();
            }
        }

        public void UpdateContrato(Contrato updatedContrato)
        {
            using (var context = new InvensisContext())
            {
                var contrato = context.Contratos.FirstOrDefault(c => c.IdContrato == updatedContrato.IdContrato);

                if (contrato != null)
                {
                    contrato.NumeroContrato = updatedContrato.NumeroContrato;
                    contrato.IdEmpresa = updatedContrato.IdEmpresa;
                    contrato.FechaInicio = updatedContrato.FechaInicio;
                    contrato.FechaFin = updatedContrato.FechaFin;
                    contrato.TipoContrato = updatedContrato.TipoContrato;
                    contrato.ValorTotal = updatedContrato.ValorTotal;
                    contrato.Estado = updatedContrato.Estado;
                    contrato.ArchivoContrato = updatedContrato.ArchivoContrato;
                    contrato.Observaciones = updatedContrato.Observaciones;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteContrato(Contrato contratoToDelete)
        {
            using (var context = new InvensisContext())
            {
                context.Contratos.Remove(contratoToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteContratoById(int idContrato)
        {
            using (var context = new InvensisContext())
            {
                var contrato = context.Contratos.FirstOrDefault(c => c.IdContrato == idContrato);
                if (contrato != null)
                {
                    context.Contratos.Remove(contrato);
                    context.SaveChanges();
                }
            }
        }
    }
}
