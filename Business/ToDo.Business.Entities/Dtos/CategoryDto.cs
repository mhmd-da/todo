using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Enums;

namespace ToDo.Business.Entities.Models
{
    public class CategoryDto : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Label? Label { get; set; }
        public string Color { get; set; }
        public List<Item> Items { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
