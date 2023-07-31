using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Responses.Admin;

namespace ToDo.Business.Contracts.Engines
{
    public interface IAdminEngine
    {
        AdminDataResponse Get();
    }
}
