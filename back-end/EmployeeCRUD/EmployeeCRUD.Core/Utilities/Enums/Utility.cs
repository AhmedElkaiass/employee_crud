using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EmployeeCRUD.Core.Utilities.Enums
{
    public static class Utility
    {
        public static T ToEnum<T>(this string value)
        {
            T returnValue = (T)Enum.Parse(typeof(T), value, true);
            return returnValue;
        }
        public static T ToEnum<T>(this long value)
        {
            return (T)Enum.Parse(typeof(T), value.ToString(), true);
        }
        public static T ToEnum<T>(this int value)
        {
            return (T)Enum.Parse(typeof(T), value.ToString(), true);
        }
    }
}
