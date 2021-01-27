using System;
using Hahn.ApplicationProcess.Application.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Hahn.ApplicationProcess.Application.Interfaces {
    public interface ICountryLogic {
        BusinessResponse<CountryResponseData> GetCountriesByName (string countryName);

    }
}