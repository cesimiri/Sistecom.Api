using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IComponentesEnsamblaje
    {
        IEnumerable<ComponentesEnsamblaje> ComponentesEnsamblajeInfoAll { get; }
        ComponentesEnsamblaje GetComponentesEnsamblajeById(int IdComponentesEnsamblaje);
        void InsertComponentesEnsamblaje(ComponentesEnsamblaje New);
        void UpdateComponentesEnsamblaje(ComponentesEnsamblaje UpdItem);
        void DeleteComponentesEnsamblaje(ComponentesEnsamblaje DelItem);
        void DeleteComponentesEnsamblajeById(int IdComponentesEnsamblaje);
    }
}
