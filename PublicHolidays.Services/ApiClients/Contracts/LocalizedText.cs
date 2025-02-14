using Newtonsoft.Json;

namespace PublicHolidays.Services.ApiClients.Contracts
{
    public class LocalizedText
    {
        [JsonProperty("lang")]
        public string Language { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}