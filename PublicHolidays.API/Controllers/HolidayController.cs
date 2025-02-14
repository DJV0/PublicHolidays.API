using Microsoft.AspNetCore.Mvc;
using PublicHolidays.Services.Abstractions;
using PublicHolidays.Services.Mappers;
using Shared.Contracts;
using Shared.Contracts.Holidays;

namespace PublicHolidays.API.Controllers
{
    [ApiController]
    [Route("api/holidays")]
    [Produces("application/json")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        /// <summary>
        /// Returns list of holidays by year and country grouped by month
        /// </summary>
        /// <param name="year">Year to return holidays for</param>
        /// <param name="countryCode">Country to return holidays for</param>
        /// <returns>A list of holidays</returns>
        [HttpGet("getHolidaysForYear")]
        [ProducesResponseType(typeof(IEnumerable<HolidaysByYearResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHolidaysByYearAsync([FromQuery] int year, [FromQuery] string countryCode)
        {
            var holidays = await _holidayService.GetHolidaysByYearAsync(year, countryCode);

            var result = holidays
                .GroupBy(x => x.Date.Month)
                .Select(x => new HolidaysByYearResult()
                {
                    Month = x.Key,
                    Holidays = x.Select(x => x.ToContract()).ToList()
                });

            return Ok(result);
        }

        /// <summary>
        /// Returns date status (workday, freeday, holiday)
        /// </summary>
        /// <param name="date">Date to check status for</param>
        /// <param name="countryCode">Country to check date status for</param>
        /// <returns>Date status</returns>
        [HttpGet("getDateStatus")]
        [ProducesResponseType(typeof(DateStatusResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDateStatusAsync([FromQuery] DateOnly date, [FromQuery] string countryCode)
        {
            var dateStatus = await _holidayService.GetDateStatusAsync(date, countryCode);

            var result = new DateStatusResult()
            {
                Date = date.ToString("yyyy-MM-dd"),
                Status = dateStatus.ToString()
            };

            return Ok(result);
        }

        /// <summary>
        /// Returns max number of freedays in a row for given year and country
        /// </summary>
        /// <param name="year">Year to calcutale max number for</param>
        /// <param name="countryCode">Country to calculate max number for</param>
        /// <returns>Number of max freedays in a row</returns>
        [HttpGet("maxNumberOfFreeDaysInRow")]
        [ProducesResponseType(typeof(MaxNumberOfFreedaysInRowResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMaxNumberOfFreeDaysInRowAsync([FromQuery] int year, [FromQuery] string countryCode)
        {
            var numberOfDays = await _holidayService.GetMaxNumberOfFreeDaysInRowAsync(year, countryCode);

            var result = new MaxNumberOfFreedaysInRowResult() { MaxNumberOfFreedaysInRow = numberOfDays };

            return Ok(result);
        }
    }
}
