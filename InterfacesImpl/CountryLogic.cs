using System;
using System.Linq;
using Hahn.ApplicationProcess.Application.Interfaces;
using Hahn.ApplicationProcess.Application.Utility;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Data;
using Microsoft.AspNetCore.Mvc;


namespace Hahn.ApplicationProcess.Application.InterfacesImpl
{
    public class CountryLogic : ICountryLogic
    {
        private readonly ILogger<CountryLogic> _logger;

        private readonly ApplicationDbContext _context;
        public CountryLogic(ILogger<CountryLogic> logger, ApplicationDbContext context)
        {
            this._logger = logger;
            _context = context;
        }

        public BusinessResponse<List<CountryResponseData>> GetCountriesByName(string countryName)
        {
            CountryRequestData countryRequestData = new CountryRequestData();
            countryRequestData.countryName = countryName;
            String endpointurl = "https://restcountries.eu/rest/v2/name/" + countryName + "?fullText=true";
            var response = GetAsync(endpointurl, null, null).Result;
            if (response.IsSuccessStatusCode)
            {
                var retresponse = JsonConvert.DeserializeObject<List<CountryResponseData>>(response.Result);
                return new BusinessResponse<List<CountryResponseData>>()
                {
                    IsSuccessful = retresponse != null ? true : false,
                    ResponseObject = retresponse

                };
            }
            else
            {
                return new BusinessResponse<List<CountryResponseData>>()
                {
                    IsSuccessful = false,
                    Message = $"{response.StatusCode}"
                };
            }
        }

        public BusinessResponse<ApplicantResponse> CreateApplicant(string Name, string FamilyName, string Address, string CountryOfOrigin, string EmailAddress, int Age, bool Hired)
        {
            try
            {
                Applicant applicant = new Applicant
                {
                    Name = Name,
                    FamilyName = FamilyName,
                    Address = Address,
                    CountryOfOrigin = CountryOfOrigin,
                    EmailAddress = EmailAddress,
                    Age = Age,
                    Hired = Hired,
                };
                _context.Applicant.Add(applicant);
                _context.SaveChanges();
                return new BusinessResponse<ApplicantResponse>()
                {
                    IsSuccessful = true,
                    Message = $" Applicant with applicant ID - {applicant.ID} successfully added"
                };
            }
            catch (Exception ex)
            {
                return new BusinessResponse<ApplicantResponse>()
                {
                    IsSuccessful = false,
                    Message = $" An error has occured"
                };
            }

        }

        public BusinessResponse<Applicant> GetApplicantById(int Id)
        {
            try
            {
                var applicant = _context.Applicant.SingleOrDefault(x => x.ID == Id);
                 if (applicant == null)
                {
                    return new BusinessResponse<Applicant>()
                    {
                        IsSuccessful = false,
                        Message = $" Not found",
                        ResponseObject = applicant
                    };

                }
                return new BusinessResponse<Applicant>()
                {
                    IsSuccessful = true,
                    Message = $" Details of applicant with applicant ID - {applicant.ID} fetched successfully",
                    ResponseObject = applicant
                };
            }
            catch (Exception ex)
            {
                return new BusinessResponse<Applicant>()
                {
                    IsSuccessful = false,
                    Message = $" An error has occured"
                };
            }

        }

        public BusinessResponse<Applicant> UpdateApplicantInfo(int Id, string Name, string FamilyName, string Address, string CountryOfOrigin, string EmailAddress, int Age, bool Hired)
        {
            try
            {
                var applicant = _context.Applicant.SingleOrDefault(x => x.ID == Id);
                if (applicant == null)
                {
                    return new BusinessResponse<Applicant>()
                    {
                        IsSuccessful = false,
                        Message = $" Not found",
                        ResponseObject = applicant
                    };

                }
                applicant.Name = Name;
                applicant.FamilyName = FamilyName;
                applicant.Address = Address;
                applicant.CountryOfOrigin = CountryOfOrigin;
                applicant.EmailAddress = EmailAddress;
                applicant.Age = Age;
                applicant.Hired = Hired;
                _context.Update(applicant);
                _context.SaveChanges();
                return new BusinessResponse<Applicant>()
                {
                    IsSuccessful = true,
                    Message = $" Details of applicant with applicant ID - {applicant.ID} updated successfully",
                    ResponseObject = applicant
                };
            }
            catch (Exception ex)
            {
                return new BusinessResponse<Applicant>()
                {
                    IsSuccessful = false,
                    Message = $" An error has occured"
                };
            }
        }
        public BusinessResponse<Applicant> DeleteApplicant(int Id)
        {
            try
            {
                var applicant = _context.Applicant.SingleOrDefault(x => x.ID == Id);
                 if (applicant == null)
                {
                    return new BusinessResponse<Applicant>()
                    {
                        IsSuccessful = false,
                        Message = $" Not found",
                        ResponseObject = applicant
                    };

                }
                _context.Remove(applicant);
                _context.SaveChanges();
                return new BusinessResponse<Applicant>()
                {
                    IsSuccessful = true,
                    Message = $" Details of applicant with applicant ID - {applicant.ID} deleted successfully",
                    ResponseObject = applicant
                };
            }
            catch (Exception ex)
            {
                return new BusinessResponse<Applicant>()
                {
                    IsSuccessful = false,
                    Message = $" An error has occured"
                };
            }

        }










        public async Task<RestResponse> GetAsync(string endPointUrl, Dictionary<string, string> headers, ILogger logger = null)
        {
            try
            {


                using (var client = new HttpClient())
                {


                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (headers != null)
                    {
                        AddHeaders(client, headers);
                    }

                    HttpResponseMessage responsemessage = await client.GetAsync(endPointUrl).ConfigureAwait(false);
                    RestResponse response = null;

                    if (responsemessage.IsSuccessStatusCode)
                    {

                        // Debug.WriteLine("Success");
                        //  log($"Success getting loan {endPointUrl}");
                        using (HttpContent content = responsemessage.Content)
                        {
                            if (logger != null)
                            {
                                logger.LogInformation($"GetAsync Before ReadAsStringAsync{responsemessage.StatusCode}");
                            }
                            Task<string> result = content.ReadAsStringAsync();

                            if (logger != null)
                            {
                                logger.LogInformation($"GetAsync After ReadAsStringAsync{responsemessage.StatusCode}");
                            }

                            //  Debug.WriteLine(ExtractJSON(result.Result));
                            response = new RestResponse(responsemessage.IsSuccessStatusCode, responsemessage.StatusCode.ToString(), responsemessage.ReasonPhrase, ExtractJSON(result.Result));
                            return response;
                        }
                    }
                    else
                    {
                        if (logger != null)
                        {
                            logger.LogInformation($"GetAsync Before ReadAsStringAsync{responsemessage.StatusCode}");
                        }
                        Task<string> result = responsemessage.Content?.ReadAsStringAsync();
                        Debug.WriteLine($"Error getting loan({responsemessage.StatusCode.ToString()}) {endPointUrl} {result?.Result }");
                        //  log($"{responsemessage.StatusCode.ToString()} {endPointUrl}");
                        if (logger != null)
                        {
                            logger.LogInformation($"GetAsync Afer ReadAsStringAsync{responsemessage.StatusCode}");
                        }
                        response = new RestResponse(responsemessage.IsSuccessStatusCode, responsemessage.StatusCode.ToString(), responsemessage.ReasonPhrase, "");
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.LogInformation($"GetAsync Exception{ex?.StackTrace} - {ex?.Message}");
                }
                return null;
            }
        }
        private void AddHeaders(HttpClient client, Dictionary<string, string> headers)
        {

            foreach (var c in headers)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(c.Key, c.Value);
                Debug.WriteLine($"Header| {c.Key}:{c.Value}");
            }

        }

        private string ExtractJSON(string result)
        {
            //\{(.|\s)*\}
            // JsonConvert.SerializeObject(result,Formatting.Indented)
            if (result.StartsWith("{") || result.StartsWith("[")) return result;
            var match = Regex.Match(result, @"\{(.|\s)*\}");
            return match?.Value;
        }


    }
}