using System;
using Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicationProcess.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace Hahn.ApplicationProcess.Application.Controllers
{
    [Route("api/applicant")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {

        private readonly ILogger<ApplicantController> _logger;
        private readonly ICountryLogic _countryLogic;

        private readonly string SERVER_ERROR = "An error occured while processing request. Please try again.";


        public ApplicantController(ILogger<ApplicantController> logger, ICountryLogic countryLogic)
        {

            this._logger = logger;
            this._countryLogic = countryLogic;

        }

        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [Produces("application/json")]
        [HttpPost, Route("createapplicant")]
        public IActionResult CreateApplicant([FromBody] ApplicantModel model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    this._logger.LogInformation("Model info sent: {model}", model);

                    var response = _countryLogic.CreateApplicant(model.Name, model.FamilyName, model.Address, model.CountryOfOrigin, model.EmailAddress, model.Age, model.Hired);

                    if (response.IsSuccessful)
                    {
                        return new OkObjectResult(response);
                    }
                    else
                        return new BadRequestObjectResult(response);

                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(SERVER_ERROR);
            }
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [Produces("application/json")]
        [HttpGet, Route("getapplicantbyId")]
        public IActionResult GetApplicantById(int Id)
        {

            try
            {


                this._logger.LogInformation("Id sent: {Id}", Id);

                var response = _countryLogic.GetApplicantById(Id);

                if (response.IsSuccessful)
                {
                    return new OkObjectResult(response);
                }
                else
                    return new BadRequestObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(SERVER_ERROR);
            }
        }


        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [Produces("application/json")]
        [HttpPut, Route("updateapplicantinfo")]
        public IActionResult UpdateApplicantInfo(int Id, string Name, string FamilyName, string Address, string CountryOfOrigin, string EmailAddress, int Age, bool Hired)
        {

            try
            {


                this._logger.LogInformation("Id sent: {Id}", Id);

                var response = _countryLogic.UpdateApplicantInfo(Id, Name, FamilyName, Address, CountryOfOrigin, EmailAddress, Age, Hired);

                if (response.IsSuccessful)
                {
                    return new OkObjectResult(response);
                }
                else
                    return new BadRequestObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(SERVER_ERROR);
            }
        }

        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [Produces("application/json")]
        [HttpDelete, Route("deleteapplicant")]
        public IActionResult DeleteApplicant(int Id)
        {

            try
            {


                this._logger.LogInformation("Id sent: {Id}", Id);

                var response = _countryLogic.DeleteApplicant(Id);

                if (response.IsSuccessful)
                {
                    return new OkObjectResult(response);
                }
                else
                    return new BadRequestObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(SERVER_ERROR);
            }
        }
    }
}