using System;
using System.Reflection;

namespace DigitalHealth.HL7.Common
{
    public class HL7Field
    {
        public static object Parse(Type type, string fieldForm, HL7Separators sep)
        {
            object field;

            // A field can be an array of repeats, or a string identifier, or a data structure with components
            if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                string[] elementForm;
                if (fieldForm.IndexOf(sep.FieldRepeatSeparator) == -1 && fieldForm.IndexOf(sep.ComponentSeparator) > -1 && elementType.Equals(typeof(string)))
                    elementForm = fieldForm.Split(sep.ComponentSeparator);
                else 
                    elementForm = fieldForm.Split(sep.FieldRepeatSeparator);  
                Array array = Array.CreateInstance(elementType, elementForm.Length);
                for (int i = 0; i < elementForm.Length; i++)
                {
                    array.SetValue(HL7Field.Parse(elementType, elementForm[i], sep), i);
                }
                field = array;
            }
            else if (type.Equals(typeof(string)))
            {
                field = sep.Decode(fieldForm);
            }
            else
            {
                FieldInfo[] components = type.GetFields();
                string[] componentForm = fieldForm.Split(sep.ComponentSeparator);
                int n = Math.Min(components.Length, componentForm.Length);

                // Invoke the constructor that takes no parameters
                field = type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                for (int i = 0; i < n; i++)
                {
                    object value = HL7Component.Parse(components[i].FieldType, componentForm[i], sep);
                    components[i].SetValue(field, value);
                }
            }
            return field;
        }

        internal static void Encode(HL7Separators seps, System.Text.StringBuilder sb, Type type, object value)
        {
            if (value == null)
            {
                // Do nothing
            }
            else if (type.IsArray)
            {
                foreach (object element in value as Array)
                {
                    Encode(seps, sb, type.GetElementType(), element);
                    sb.Append(seps.FieldRepeatSeparator);
                }

                // Remove the last field repeat separator.
                sb.Remove(sb.Length - 1, 1);
            }
            else if (type.Equals(typeof(string)))
            {
                sb.Append(seps.Encode(value as string));
            }
            else
            {
                foreach (FieldInfo component in type.GetFields())
                {
                    HL7Component.Encode(seps, sb, component.FieldType, component.GetValue(value));
                    sb.Append(seps.ComponentSeparator);
                }

                // Remove the last component separator.
                sb.Remove(sb.Length - 1, 1);
            }
        }
    }
}