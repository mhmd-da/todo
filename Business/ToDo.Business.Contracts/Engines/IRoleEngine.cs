using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Business.Contracts.Engines
{
    public interface IRoleEngine
    {
        MongoRole Get(string roleName);
        void Create(string roleName);
        bool Exist(string roleName);
    }
}
