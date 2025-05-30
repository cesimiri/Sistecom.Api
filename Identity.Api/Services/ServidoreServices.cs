using Identity.Api.DataRepository;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class ServidoreServices : IServidore 
    {
        private ServidoreRepository _dataRepository = new ServidoreRepository();

        public IEnumerable<Servidore> ServidoreInfoAll
        {
            get { return _dataRepository.ServidoreInfoAll(); }
        }

        public Servidore GetServidoreById(int IdServidore)
        {
            return _dataRepository.GetServidoreById(IdServidore);
        }

        public void InsertServidore(Servidore New)
        {
            _dataRepository.InsertServidore(New);
        }

        public void UpdateServidore(Servidore UpdItem)
        {
            _dataRepository.UpdateServidore(UpdItem);
        }

        public void DeleteServidore(Servidore DelItem)
        {
            _dataRepository.DeleteServidore(DelItem);
        }

        public void DeleteServidoreById(int IdServidore)
        {
            _dataRepository.DeleteServidoreById(IdServidore);
        }
    }
}
