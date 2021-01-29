using System;
using System.Reflection;
using DigitalHealth.Hl7.Common;
using DigitalHealth.HL7.Common.Exceptions;

namespace DigitalHealth.HL7.Common
{
    public class HL7Component
    {
        public static object Parse(Type type, string componentForm, HL7Separators sep)
        {
            object component;

            // A component can either be a string, or it will consist of sub-components.
            if (type.Equals(typeof(string)))
            {
                component = sep.Decode(componentForm);
            }
            else
            {
                FieldInfo[] subcomponents = type.GetFields();
                string[] subcomponentForm = componentForm.Split(sep.SubcomponentSeparator);
                int n = Math.Min(subcomponents.Length, subcomponentForm.Length);

                // Invoke the constructor that takes no parameters
                component = type.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                for (int i = 0; i < n; i++)
                {
                    // Each subcomponent must be a string
                    if (subcomponents[i].FieldType.Equals(typeof(string)))
                    {
                        subcomponents[i].SetValue(component, sep.Decode(subcomponentForm[i]));
                    }
                    else if (subcomponents[i].FieldType.Equals(typeof(HL7.Common.DataStructure.HD))) // For OBR segment PrincipalResultsInterpreter field we have assigningAuthority that needs to be set
                    {
                        object objHD = new HL7.Common.DataStructure.HD();
                        FieldInfo[] subcomponentsHD = objHD.GetType().GetFields();
                        for (int j = 0; j < subcomponentsHD.Length; j++)
                        {
                            if (subcomponentsHD[j].Name == "universalID") //  Set the UniversalID
                            {
                                subcomponentsHD[j].SetValue(objHD, sep.Decode(subcomponentForm[i])); //
                                subcomponents[i].SetValue(component, objHD);
                            }
                        }
                    }
                    else
                    {
                        throw new HL7ParseException(string.Format(ConstantsResource.SubcomponentNotString, subcomponents[i].GetType()));
                    }
                }
            }
            return component;
        }

        internal static void Encode(HL7Separators seps, System.Text.StringBuilder sb, Type type, object value)
        {
            // A component is either empty, a string or an HL7 data structure containing subcomponents.
            if (value == null)
            {
                // Do nothing
            }
            else if (value is string)
            {
                sb.Append(seps.Encode(value as string));
            }
            else
            {
                foreach (FieldInfo subcomponent in type.GetFields())
                {
                    // A subcomponent is always a string
                    sb.Append(seps.Encode(subcomponent.GetValue(value) as string));
                    sb.Append(seps.SubcomponentSeparator);
                }

                // Remove the extraneous subcomponent separator
                sb.Remove(sb.Length - 1, 1);
            }
        }
    }
}