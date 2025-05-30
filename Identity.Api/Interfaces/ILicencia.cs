﻿using Modelo.Sistecom.Modelo.Database;

namespace Identity.Api.Interfaces
{
    public interface ILicencia
    {
        IEnumerable<Licencia> LicenciaInfoAll { get; }
        Licencia GetLicenciaById(int IdLicencia);
        void InsertLicencia(Licencia New);
        void UpdateLicencia(Licencia UpdItem);
        void DeleteLicencia(Licencia DelItem);
        void DeleteLicenciaById(int IdLicencia);
    }
}
