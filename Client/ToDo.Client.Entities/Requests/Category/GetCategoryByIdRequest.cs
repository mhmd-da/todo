using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Enums;

namespace ToDo.Client.Entities.Requests.Category
{
    public class GetCategoryByIdRequest : BaseRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
    }
}
