using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Common.Static
{
    public static class Messages
    {
        public static readonly string UsernameRequired = "Username required";
        public static readonly string UsedUsername = "The username is already used";
        public static readonly string InvalidRoleName = "The rolename is not valid";
        public static readonly string UserNotFound = "User {0} is not found";
        public static readonly string WrongPassword = "You entered a wrong password for {0}";

        public static readonly string CategoriesNotFound = "Couldn't find any categories matching the requested criteria";
        public static readonly string CategoryNotFound = "Couldn't find any category with id {0}";
        public static readonly string CreateCategoryError = "Something went wrong while creating the category";
        public static readonly string UpdateCategoryError = "Something went wrong while updating the category";
        public static readonly string DeleteCategoryError = "Something went wrong while deleting the category";


        public static readonly string ItemsNotFound = "Couldn't find any items matching the requested criteria for category {0}";
        public static readonly string ItemNotFound = "Couldn't find any item with id {0} for category {1}";
        public static readonly string CreateItemError = "Something went wrong while creating the item";
        public static readonly string UpdateItemError = "Something went wrong while updating the item";
        public static readonly string DeleteItemError = "Something went wrong while deleting the item";

        public static readonly string NameRequired = "Name is Required";


    }
}
