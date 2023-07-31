using System.Collections.Generic;

namespace ToDo.Client.Entities.Responses.Admin
{
    public class AdminDataResponse
    {
        public AdminDataResponse()
        {
            UserCategories = new List<UserCategory>();
        }

        public List<UserCategory> UserCategories { get; set; }

    }

    public class UserCategory
    {
        public UserCategory()
        {
            Categories = new List<CategoryItem>();
        }

        public string Username { get; set; }
        public List<CategoryItem> Categories { get; set; }
    }

    public class CategoryItem
    {
        public CategoryItem()
        {
            Items = new List<ToDo.Business.Entities.Models.Item>();
        }
        public ToDo.Business.Entities.Models.Category Category { get; set; }
        public List<ToDo.Business.Entities.Models.Item> Items { get; set; }
    }
}
