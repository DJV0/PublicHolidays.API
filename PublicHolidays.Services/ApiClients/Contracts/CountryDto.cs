using Newtonsoft.Json;

namespace PublicHolidays.Services.ApiClients.Contracts
{
    public class CountryDto
    {
        [JsonProperty("countryCode")]
        public string Code { get; set; }

        [JsonProperty("fullName")]
        public string Name { get; set; }

        [JsonProperty("fromDate")]
        public DateDto FromDate { get; set; }

        [JsonProperty("toDate")]
        public DateDto ToDate { get; set; }

    }
}
