using Newtonsoft.Json;

namespace PublicHolidays.Services.ApiClients.Contracts
{
    public class DateDto
    {
        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

    }
}