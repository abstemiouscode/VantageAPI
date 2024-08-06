using System.Text.Json.Serialization;

namespace VantageAPI
{
    public class Customer
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set;}
        
        [JsonPropertyName("address")]
        public required string Address { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("website")]
        public string? Website { get; set; }
        public List<Contact>? Contacts { get; set; }
    }
}
