using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Business.Entities.Models
{
    public class Item : BaseEntity
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
