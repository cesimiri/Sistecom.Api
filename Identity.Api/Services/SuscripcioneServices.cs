using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
using System.Collections.Generic;

namespace Identity.Api.Services
{
    public class SuscripcioneService : ISuscripcione
    {
        private readonly SuscripcioneDataRepository _dataRepository = new SuscripcioneDataRepository();

        //public async Task<IEnumerable<Suscripcione>> SuscripcionesAll
        //{
        //    get => await _dataRepository.SuscripcionesAll();
        //}
        public async Task<IEnumerable<Suscripcione>> SuscripcionesAll()
        {
            return await _dataRepository.SuscripcionesAll();
        }

        public async Task<Suscripcione?> GetSuscripcionById(int idSuscripcion)
        {
            return await _dataRepository.GetSuscripcionById(idSuscripcion);
        }

        public async Task InsertSuscripcion(Suscripcione newSuscripcion)
        {
            await _dataRepository.InsertSuscripcion(newSuscripcion);
        }

        public async Task UpdateSuscripcion(Suscripcione updatedSuscripcion)
        {
            await _dataRepository.UpdateSuscripcion(updatedSuscripcion);
        }

        public async Task DeleteSuscripcion(Suscripcione suscripcionToDelete)
        {
            await _dataRepository.DeleteSuscripcion(suscripcionToDelete);
        }

        public async Task DeleteSuscripcionById(int idSuscripcion)
        {
            await _dataRepository.DeleteSuscripcionById(idSuscripcion);
        }


        // Agregado 
        public async Task<IEnumerable<EmpresasCliente>> GetEmpresaClienteAsync()
        {
            return await _dataRepository.GetEmpresaClienteAsync();
        }

        public async Task<IEnumerable<Proveedore>> GetProveedoreAsync()
        {
            return await _dataRepository.GetProveedoreAsync();
        }
    }
}
