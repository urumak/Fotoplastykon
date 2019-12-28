using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.Tools.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumType)
        {
            Type genericEnumType = enumType.GetType();
            var memberInfo = genericEnumType.GetMember(enumType.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes != null && attributes.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return enumType.ToString();
        }
    }
}
