using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Services
{
    public class MarcaServices : IMarca
    {
        private MarcaRepository _dataRepository = new MarcaRepository();

        public IEnumerable<Marca> GetAllMarca
        {
            get { return _dataRepository.GetAllMarca(); }
        }

        public Marca GetMarcaById(int idMarca)
        {
            return _dataRepository.GetMarcaById(idMarca);
        }

        public void InsertMarca(MarcaDTO New)
        {
            _dataRepository.InsertMarca(New);
        }

        public void UpdateMarca(Marca UpdItem)
        {
            _dataRepository.UpdateMarca(UpdItem);
        }

        //public void DeleteEmpresaCliente(EmpresasCliente DelItem)
        //{
        //    _dataRepository.DeleteEmpresaCliente(DelItem);
        //}

        public void DeleteMarcaById(int idMarca)
        {
            _dataRepository.DeleteMarcaById(idMarca);
        }
    }
}
