using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EmployeeCRUD.Core.DTOs.Common
{
    public class DataPagingRequest
    {
        public DataPagingRequest()
        {
            if (this.take == 0)
                take = int.MaxValue;
        }
        public int skip { get; set; }
        public int take { get; set; }
        public List<KendoGridSorting> Sort { get; set; }
        public KendoGridFilter Filter { get; set; }
        public string ExtraFilter { get; set; }
        /// <summary>
        /// it should Comes in JSON Formate
        /// </summary>
        public string AdvancedFilter { get; set; }
    }
    public class KendoGridSorting
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class KendoGridFilter
    {
        public List<KendoGridFilterCollection> filters { get; set; }
    }
    public class KendoGridFilterCollection
    {
        public List<KendoGridFilterFileds> filters { get; set; }
        public string Logic { get; set; }

    }
    public class KendoGridFilterFileds
    {
        public string field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }

    }
}
