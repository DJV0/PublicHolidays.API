using Newtonsoft.Json;

namespace PublicHolidays.Services.ApiClients.Contracts
{
    public class HolidayDto
    {
        [JsonProperty("date")]
        public DateDto Date { get; set; }

        [JsonProperty("name")]
        public List<LocalizedText> Name { get; set; }
    }
}