using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App.Web.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value, bool translate = false)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] att = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (att != null && att.Length > 0)
                {
                    return att[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
