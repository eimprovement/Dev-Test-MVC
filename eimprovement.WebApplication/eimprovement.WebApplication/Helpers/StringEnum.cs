using System;
using System.Reflection;

namespace eimprovement.WebApplication.Helpers
{
    public class StringEnum
    {
        public static string GetStringFromEnum(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}