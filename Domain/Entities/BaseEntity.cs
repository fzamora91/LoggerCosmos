using Newtonsoft.Json;

namespace Domain.Entities
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
