using Newtonsoft.Json;

namespace BigCart.Models
{
    public class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nameCode")]
        public string NameCode { get; set; }

        [JsonProperty("callingCode")]
        public string CallingCode { get; set; }

        [JsonProperty("flagUrl")]
        public string FlagUrl { get; set; }
    }
}
