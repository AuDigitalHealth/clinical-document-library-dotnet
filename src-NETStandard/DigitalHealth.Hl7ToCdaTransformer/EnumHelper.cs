using System;
using System.Reflection;

namespace DigitalHealth.Hl7ToCdaTransformer
{
    /// <summary>
    /// Helper function
    /// </summary>
    internal static class EnumHelper
    {
        /// <summary>
        /// Attempts to get an enum value.
        /// </summary>
        /// <typeparam name="TEnum">Enumeration</typeparam>
        /// <typeparam name="TAttr">Attribute</typeparam>
        /// <param name="attrMatcher">Attribute matcher</param>
        /// <param name="someEnum">Enumeration value</param>
        /// <returns>True if matched otherwise false</returns>
        public static bool TryGetEnumValue<TEnum, TAttr>(Func<TAttr, bool> attrMatcher, out TEnum someEnum) where TAttr : Attribute
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type");
            }

            Array values = Enum.GetValues(typeof(TEnum));
            foreach (object value in values)
            {
                string name = Enum.GetName(typeof(TEnum), value);
                FieldInfo fieldInfo = typeof(TEnum).GetField(name);

                TAttr attribute = (TAttr)fieldInfo.GetCustomAttribute(typeof(TAttr));
                if (attribute != null && attrMatcher(attribute))
                {
                    someEnum = (TEnum)value;
                    return true;
                }
            }

            someEnum = default(TEnum);
            return false;
        }
    }
}