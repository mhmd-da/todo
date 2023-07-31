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
    public class GetItemRequest : BasePagingRequest
    {
        [JsonIgnore]
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public bool? IsDone { get; set; }

    }
}
