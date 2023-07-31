using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Common.Configurations
{
    public class IdentityConfiguration
    {
        public static bool ValidateIssuer { get; set; }
        public static bool ValidateAudience { get; set; }
        public static bool ValidateSigningSecret { get; set; }
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string SigningSecret { get; set; }
    }
}
