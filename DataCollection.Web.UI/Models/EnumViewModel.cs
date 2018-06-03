using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DataCollection.Web.UI.Models
{
    public class EnumViewModel
    {

        public string Description { get; set; }

        public int Value { get; set; }

        public static string GetDescription(Enum _enum)
        {
            Type type = _enum.GetType();
            MemberInfo[] memInfo = type.GetMember(_enum.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return _enum.ToString();
        }
    }
}