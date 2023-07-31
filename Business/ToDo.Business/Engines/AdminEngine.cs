using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Responses.Admin;
using ToDo.Data.Contracts;
using ToDo.Data.Contracts.Repositories;

namespace ToDo.Business.Engines
{
    public class AdminEngine: BaseEngine, IAdminEngine
    {
        private readonly UserManager<MongoUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AdminEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<MongoUser>>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public AdminDataResponse Get()
        {
            var users = _userManager.Users.ToList();
            var categories = _unitOfWork.CategoryRepository.GetAll().GroupBy(c => c.UserId).Select(c => new { Key = c.Key, List = c.ToList() }).ToDictionary(k=>k.Key, v=>v.List);
            var items = _unitOfWork.ItemRepository.GetAll().GroupBy(i => i.CategoryId).Select(i => new { Key = i.Key, List = i.ToList() }).ToDictionary(k=>k.Key, v=>v.List);

            AdminDataResponse result = new AdminDataResponse();

            foreach(var user in users)
            {
                UserCategory userCategory = new UserCategory
                {
                    Username = user.UserName
                };

                if (categories.TryGetValue(user.Id.ToString(), out List<Category> userCategoriesList))
                {
                    foreach (var category in userCategoriesList)
                    {
                        CategoryItem categoryItem = new CategoryItem
                        {
                            Category = category
                        };

                        if (items.TryGetValue(category.Id, out List<Item> categoryItemsList))
                        {
                            categoryItem.Items = categoryItemsList;
                        }

                        userCategory.Categories.Add(categoryItem);
                    }
                }

                result.UserCategories.Add(userCategory);
            }

            return result;
        }
    }
}
