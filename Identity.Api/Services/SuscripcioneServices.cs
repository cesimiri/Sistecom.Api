using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Services
{
    public class SuscripcioneService : ISuscripcione
    {
        private readonly SuscripcioneDataRepository _dataRepository = new SuscripcioneDataRepository();

        public IEnumerable<Suscripcione> SuscripcionesAll
        {
            get { return _dataRepository.GetAllSuscripciones(); }
        }

        public Suscripcione GetSuscripcionById(int idSuscripcion)
        {
            return _dataRepository.GetSuscripcionById(idSuscripcion);
        }

        public void InsertSuscripcion(Suscripcione newSuscripcion)
        {
            _dataRepository.InsertSuscripcion(newSuscripcion);
        }

        public void UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            _dataRepository.UpdateSuscripcion(updatedSuscripcion);
        }

        public void DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            _dataRepository.DeleteSuscripcion(suscripcionToDelete);
        }

        public void DeleteSuscripcionById(int idSuscripcion)
        {
            _dataRepository.DeleteSuscripcionById(idSuscripcion);
        }
    }
}
