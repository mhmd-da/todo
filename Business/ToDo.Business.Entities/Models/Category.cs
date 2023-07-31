using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Enums;

namespace ToDo.Business.Entities.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Label? Label { get; set; }
        public string Color { get; set; }
        public string UserId { get; set; }
        public bool IsStarred { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
