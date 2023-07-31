using System.Text.Json.Serialization;

namespace ToDo.Client.Entities.Requests
{
    public class BaseRequest
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
