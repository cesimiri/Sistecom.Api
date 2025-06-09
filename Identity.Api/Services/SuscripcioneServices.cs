using Identity.Api.DataRepository;
using Identity.Api.DTO;
using Identity.Api.Interfaces;
using System.Collections.Generic;

namespace Identity.Api.Services
{
    public class SuscripcioneService : ISuscripcione
    {
        private readonly SuscripcioneDataRepository _dataRepository = new SuscripcioneDataRepository();

        public IEnumerable<SuscripcionDto> GetAllSuscripciones()
        {
            return _dataRepository.GetAllSuscripciones();
        }

        public SuscripcionDto? GetSuscripcionById(int idSuscripcion)
        {
            return _dataRepository.GetSuscripcionById(idSuscripcion);
        }

        public void InsertSuscripcion(SuscripcionDto dto)
        {
            _dataRepository.InsertSuscripcion(dto);
        }

        public void UpdateSuscripcion(SuscripcionDto dto)
        {
            _dataRepository.UpdateSuscripcion(dto);
        }

        public void DeleteSuscripcion(SuscripcionDto dto)
        {
            _dataRepository.DeleteSuscripcion(dto);
        }

        public void DeleteSuscripcionById(int idSuscripcion)
        {
            _dataRepository.DeleteSuscripcionById(idSuscripcion);
        }
    }
}
