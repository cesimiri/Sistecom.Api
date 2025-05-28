using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IContrato
    {
        IEnumerable<Contrato> ContratosInfoAll { get; }
        Contrato GetContratoById(int idContrato);
        void InsertContrato(Contrato newContrato);
        void UpdateContrato(Contrato updatedContrato);
        void DeleteContrato(Contrato contratoToDelete);
        void DeleteContratoById(int idContrato);
    }
}
