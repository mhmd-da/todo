using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Enums;

namespace ToDo.Client.Entities.Requests.Category
{
    public class GetCategoryRequest : BasePagingRequest
    {
        public string Name { get; set; }
        public Label? Label { get; set; }
        public bool? IsStarred { get; set; }
    }
}
