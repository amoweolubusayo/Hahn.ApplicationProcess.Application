using System;
using Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicationProcess.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.Application.Controllers
{
    [Route("api/loan/mandate")]
    [ApiController]
    public class QueryCountryController : ControllerBase
    {
        private readonly ILogger<QueryCountryController> _logger;
        private readonly ICountryLogic _countryLogic;
        private readonly string SERVER_ERROR = "An error occured while processing request. Please try again.";


        public QueryCountryController(ILogger<QueryCountryController> logger, ICountryLogic countryLogic)
        {

            this._logger = logger;
            this._countryLogic = countryLogic;

        }

        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [HttpGet, Route("getcountry")]
        public IActionResult GetCountriesByName([FromBody] CountryModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    this._logger.LogInformation("Model info sent: {model}", model);

                    var response = _countryLogic.GetCountriesByName(model.Name);

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

    }
}