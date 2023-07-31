using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Requests.User;
using AspNetCore.Identity.Mongo.Model;

namespace ToDo.Business.Contracts.Engines
{
    public interface IUserEngine
    {
        void Create(CreateUserRequest request);
        MongoUser GetByUsername(string username);
    }
}
