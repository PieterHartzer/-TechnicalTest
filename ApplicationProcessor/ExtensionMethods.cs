using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ULaw.ApplicationProcessor.Enums;

namespace ULaw.ApplicationProcessor
{
    static class ExtensionMethods
    {
        public static string ToDescription(this Enum en)
        {
            var fieldInfo = en.GetType().GetField(en.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description ?? en.ToString();
        }
    }
}
