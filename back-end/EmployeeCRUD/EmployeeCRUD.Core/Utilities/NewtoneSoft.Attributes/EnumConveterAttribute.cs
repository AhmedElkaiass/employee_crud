using  EmployeeCRUD.Core.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace  EmployeeCRUD.Core.Utilities.NewtoneSoft.Attributes
{
    public class EnumConveterAttribute<TEnum> : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
        public override object ReadJson(
            Newtonsoft.Json.JsonReader reader,
            Type objectType,
            object existingValue,
            Newtonsoft.Json.JsonSerializer serializer
            )
        {
            var value = (string)reader.Value;
            if (string.Equals("null", value, StringComparison.InvariantCultureIgnoreCase) || value == null)
            {
                return null;
            }
            var enumValue = value.ToEnum<TEnum>();
            return enumValue;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class LongConveterAttribute : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.Value == null || reader.Value.ToString() == "null")
                return null;
            var value = (long?)reader.Value;
            return value;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}