using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Common.Static;

namespace ToDo.Client.Entities.Requests
{
    public class BasePagingRequest : BaseRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public int GetPageIndex()
        {
            return PageIndex ?? AppConstants.DefaultPageIndex;
        }

        public int GetPageSize()
        {
            return PageSize ?? AppConstants.DefaultPageSize;
        }
    }
}
