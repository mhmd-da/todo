using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToDo.Client.Entities.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode Status { get; set; }
        public object? Data { get; set; }
    }
}
