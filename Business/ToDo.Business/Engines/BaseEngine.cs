using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Exceptions;

namespace ToDo.Business.Engines
{
    public class BaseEngine : BaseExceptionHandler
    {
        public BaseEngine(IServiceProvider serviceProvider)
        {

        }
    }
}
