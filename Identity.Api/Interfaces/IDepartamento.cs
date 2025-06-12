using Identity.Api.DTO;
using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IDepartamento
    {
        
        IEnumerable<DepartamentoDTO> GetAllDepartamento();
        DepartamentoDTO GetDepartamentoById(int idDepartamento);
        void InsertDepartamento(DepartamentoDTO New);
        void UpdateDepartamento(DepartamentoDTO UpdItem);
        //void DeleteDepartamento(Cargo DelItem);
        void DeleteDepartamentoById(int idDepartamento);
    }
}
