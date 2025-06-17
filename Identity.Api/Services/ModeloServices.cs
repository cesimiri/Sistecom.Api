using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;

namespace Identity.Api.Services
{
    public class ModeloServices : IModelo
    {
        private ModeloRepository _dataRepository = new ModeloRepository();

        public IEnumerable<ModeloDTO> GetAllModelo
        {
            get { return _dataRepository.GetAllModelo(); }
        }

        public ModeloDTO GetModeloById(int idModelo)
        {
            return _dataRepository.GetModeloById(idModelo);
        }

        public void InsertModelo(ModeloDTO New)
        {
            _dataRepository.InsertModelo(New);
        }

        public void UpdateModelo(ModeloDTO UpdItem)
        {
            _dataRepository.UpdateModelo(UpdItem);
        }

        //public void DeleteProducto(Producto DelItem)
        //{
        //    _dataRepository.DeleteProducto(DelItem);
        //}

        public void DeleteModeloById(int idModelo)
        {
            _dataRepository.DeleteModeloById(idModelo);
        }
    }
}
