using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Entities.Models;
using ToDo.Common.Enums;

namespace ToDo.Client.Entities.Responses.Category
{
    public class GetCategoryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Label Label { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }
        public bool IsStarred { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
