using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using ToDo.Common.Enums;

namespace ToDo.Client.Entities.Requests.Category
{
    public class UpdateCategoryRequest : BaseRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public Label? Label { get; set; }
        public string Color { get; set; }
        public bool IsStarred { get; set; }
    }
}
