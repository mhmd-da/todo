using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Client.Entities.Requests.Item
{
    public class UpdateItemRequest : BaseRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
        [JsonIgnore]
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
