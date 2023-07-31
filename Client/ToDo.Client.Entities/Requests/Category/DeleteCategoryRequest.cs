using Newtonsoft.Json;

namespace ToDo.Client.Entities.Requests.Category
{
    public class DeleteCategoryRequest : BaseRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}
