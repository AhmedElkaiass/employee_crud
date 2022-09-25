using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.DTOs
{
    public class PageingDataResponse<TData>
    {
        public List<TData> data { get; set; }
        public int recordsTotal { get; set; }
        //public int recordsFiltered { get; set; }
        //public string Draw { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int total { get; set; }

    }
}
