using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ContratoService : IContrato
    {
        private readonly ContratoDataRepository _dataRepository = new ContratoDataRepository();

        public IEnumerable<Contrato> ContratosInfoAll
        {
            get { return _dataRepository.GetAllContratos(); }
        }

        public Contrato GetContratoById(int idContrato)
        {
            return _dataRepository.GetContratoById(idContrato);
        }

        public void InsertContrato(Contrato newContrato)
        {
            _dataRepository.InsertContrato(newContrato);
        }

        public void UpdateContrato(Contrato updatedContrato)
        {
            _dataRepository.UpdateContrato(updatedContrato);
        }

        public void DeleteContrato(Contrato contratoToDelete)
        {
            _dataRepository.DeleteContrato(contratoToDelete);
        }

        public void DeleteContratoById(int idContrato)
        {
            _dataRepository.DeleteContratoById(idContrato);
        }
    }
}
