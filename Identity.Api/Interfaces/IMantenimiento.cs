using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IMantenimiento
    {
        IEnumerable<Mantenimiento> MantenimientoInfoAll { get; }
        Mantenimiento GetMantenimientoById(int IdMantenimiento);
        void InsertMantenimiento(Mantenimiento New);
        void UpdateMantenimiento(Mantenimiento UpdItem);
        void DeleteMantenimiento(Mantenimiento DelItem);
        void DeleteMantenimientoById(int IdMantenimiento);
    }
}
