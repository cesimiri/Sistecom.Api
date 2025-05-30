﻿using Identity.Api.Interfaces;
using Identity.Api.DataRepository;
using Modelo.Sistecom.Modelo.Database;
namespace Identity.Api.Services
{

    public class FacturasCompraServices : IFacturasCompra
    {
        private FacturasCompraRepository _dataRepository = new FacturasCompraRepository();

        public IEnumerable<FacturasCompra> FacturasCompraInfoAll
        {
            get { return _dataRepository.GetAllFacturasCompra(); }
        }

        public FacturasCompra GetFacturasCompraById(int idFacturasCompra)
        {
            return _dataRepository.GetFacturasCompraById(idFacturasCompra);
        }

        public void InsertFacturasCompra(FacturasCompra New)
        {
            _dataRepository.InsertFacturasCompra(New);
        }

        public void UpdateFacturasCompra(FacturasCompra UpdItem)
        {
            _dataRepository.UpdateFacturasCompra(UpdItem);
        }

        public void DeleteFacturasCompra(FacturasCompra DelItem)
        {
            _dataRepository.DeleteFacturasCompra(DelItem);
        }

        public void DeleteFacturasCompraById(int idFacturasCompra)
        {
            _dataRepository.DeleteFacturasCompraById(idFacturasCompra);
        }
    }
}
