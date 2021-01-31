using System;
using Hahn.ApplicationProcess.Application.Utility;
using Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Hahn.ApplicationProcess.Application.Interfaces {
    public interface ICountryLogic {
        BusinessResponse<List<CountryResponseData>> GetCountriesByName(string countryName);
        BusinessResponse<ApplicantResponse> CreateApplicant (string Name,string FamilyName,string Address,string CountryOfOrigin,string EmailAddress,int Age, bool Hired);
        BusinessResponse<Applicant> GetApplicantById(int Id);
        BusinessResponse<Applicant> UpdateApplicantInfo(int Id, string Name,string FamilyName,string Address,string CountryOfOrigin,string EmailAddress,int Age, bool Hired);
        BusinessResponse<Applicant> DeleteApplicant(int Id);


    }
}