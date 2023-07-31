using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Enums;

namespace ToDo.Client.Entities.Requests.Category
{
    public class AddCategoryRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public Label? Label { get; set; }
        public string Color { get; set; }
        public bool IsStarred { get; set; }
    }
}
