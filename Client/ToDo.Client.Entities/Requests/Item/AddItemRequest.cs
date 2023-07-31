using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ToDo.Client.Entities.Requests.Item
{
    public class AddItemRequest : BaseRequest
    {
        [JsonIgnore]
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
