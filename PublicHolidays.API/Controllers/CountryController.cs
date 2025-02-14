using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using PublicHolidays.Services.Abstractions;
using PublicHolidays.Services.Mappers;
using Shared.Contracts.Countries;

namespace PublicHolidays.API.Controllers
{
    [ApiController]
    [Route("api/countries")]
    [Produces("application/json")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Returns available counrties
        /// </summary>
        /// <returns>A list of supported countries</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CountryContract>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetAllCountriesAsync();

            var result = countries.Select(x => x.ToContract());

            return Ok(result);
        }
    }
}
