using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Client.Entities.Responses.Auth
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}
