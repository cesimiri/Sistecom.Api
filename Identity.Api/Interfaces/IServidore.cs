using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface IServidore
    {
        IEnumerable<Servidore> ServidoreInfoAll { get; }
        Servidore GetServidoreById(int IdServidore);
        void InsertServidore(Servidore New);
        void UpdateServidore(Servidore UpdItem);
        void DeleteServidore(Servidore DelItem);
        void DeleteServidoreById(int IdServidore);
    }
}
