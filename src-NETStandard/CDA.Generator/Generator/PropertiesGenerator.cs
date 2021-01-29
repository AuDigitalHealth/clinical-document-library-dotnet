using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// PropertiesGenerator
    /// </summary>
    public class PropertiesGenerator
    {
       /// <summary>
       /// Get all of the properties for the CDA document Type
       /// </summary>
        /// <param name="obj">Type</param>
        /// <param name="rootPath">string</param>
        /// <param name="filename">string</param>
       public static void GetProperties(Type obj, string rootPath, string filename)
       {
           var typeListToSeperate = new List<Type>
                               {
                                   typeof(Identifier),
                                   typeof(ElectronicCommunicationDetail),
                                   typeof(CdaInterval),
                                   typeof(Nehta.VendorLibrary.CDA.SCSModel.Common.Entitlement),
                                   typeof(IPersonName),
                                   typeof(IImageDetails),
                                   typeof(ExternalData),
                                   typeof(InstanceIdentifier),
                                   typeof(ICodableText),
                                   typeof(StrucDocText)

                               };

           var removeList = new List<string>();
           foreach (var type in typeListToSeperate) removeList.Add(type.Name);

           File.Delete(filename);

           WriteToLog(obj.Name, filename);

           GetSubTypes(obj, rootPath, filename, removeList);

           WriteToLog("", filename);
           WriteToLog("**********************Common Types************************", filename);
           WriteToLog( "", filename);

           foreach (var item in typeListToSeperate)
           {
             if (item.UnderlyingSystemType != typeof(StrucDocText))
             {
               WriteToLog(item.Name, filename);
               GetSubTypes(item, item.ToString(), filename, null);
               WriteToLog("", filename);
             }
           }
       }

       /// <summary>
       ///  Loops and gets the type of document
       /// </summary>
       /// <param name="obj">Type</param>
       /// <param name="rootPath">string</param>
       /// <param name="filename">string</param>
       /// <param name="removeList">string[]</param>
       public static void GetSubTypes(Type obj, string rootPath, string filename, List<string> removeList)
        {
            var simplePropertyTypes = new List<string> { "String", "string", "Boolean", "Nullable`1", "Int32", "Char", "ISO8601DateTime", "Byte[]" };
            var systemPropertyTypes = new List<string> { "Count", "IsReadOnly", "Item", "NarrativeText"};

            if (removeList != null)
            simplePropertyTypes.AddRange(removeList);

            PropertyInfo[] pi = obj.GetProperties();

            foreach (PropertyInfo info in pi)
            {
                var name = info.PropertyType.Name;
                var description = (info.Name == "Item" ? name : info.Name);

                if (simplePropertyTypes.Contains(name))
                {
                    name = info.Name;

                    if (!systemPropertyTypes.Contains(name))
                        WriteToLog(string.Format("{0}.{1} ({2})", rootPath, name, info.PropertyType), filename);
                }
                else
                {
                    if (info.PropertyType.Name == "List`1" || info.PropertyType.Name == "IList`1" || info.PropertyType.Name == "IList")
                    {
                        description = info.Name + "[]";
                        WriteToLog(string.Format("{0}.{1} (IList<{2}>)", rootPath, description, info.Name), filename);
                    }
                    else
                    {
                        WriteToLog(string.Format("{0}.{1} ({2})", rootPath, description, info.PropertyType), filename);

                        if (info.PropertyType.UnderlyingSystemType.BaseType != typeof(Enum) && info.PropertyType.UnderlyingSystemType.BaseType != typeof(Nehta.HL7.CDA.StrucDocText))
                        {
                            GetSubTypes(info.PropertyType.UnderlyingSystemType, rootPath + "." + description, filename, removeList);
                        }
                    }

                    foreach (Type tinterface in info.PropertyType.UnderlyingSystemType.GetInterfaces())
                    {
                        if (tinterface != typeof(IComparable) && tinterface != typeof(IList) && tinterface != typeof(ICollection))
                            {
                                GetSubTypes(tinterface, rootPath + "." + description, filename, removeList);
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Writes the file to the log
        /// </summary>
        /// <param name="text"></param>
        /// <param name="filename"></param>
        private static void WriteToLog(string text, string filename)
        {
            TextWriter tw = new StreamWriter(filename, true);
            tw.WriteLine(text.Replace("`1", ""));
            tw.Close();
        }
    }
}

//        TO Run EG..
            //PropertiesGenerator.GetProperties((new EDischargeSummary()).GetType(), "EDischargeSummary", @"C:\TEST" + @"\EDischargeSummaryProperties.txt");
            //PropertiesGenerator.GetProperties((new EReferral()).GetType(), "EReferral", @"C:\TEST" + @"\EReferralProperties.txt");
            //PropertiesGenerator.GetProperties((new EventSummary()).GetType(), "EventSummary", @"C:\TEST" + @"\EventSummaryProperties.txt");
            //PropertiesGenerator.GetProperties((new SpecialistLetter()).GetType(), "SpecialistLetter", @"C:\TEST" + @"\SpecialistLetterProperties.txt");
            //PropertiesGenerator.GetProperties((new SharedHealthSummary()).GetType(), "SharedHealthSummary", @"C:\TEST" + @"\SharedHealthSummaryProperties.txt");
            //PropertiesGenerator.GetProperties((new AcdCustodianRecord()).GetType(), "AcdCustodianRecord", @"C:\TEST" + @"\AcdCustodianRecordProperties.txt");
            //PropertiesGenerator.GetProperties((new ConsumerEnteredNotes()).GetType(), "ConsumerEnteredNotes", @"C:\TEST" + @"\ConsumerEnteredNotesProperties.txt");
            //PropertiesGenerator.GetProperties((new ConsumerEnteredHealthSummary()).GetType(), "ConsumerEnteredHealthSummary", @"C:\TEST" + @"\ConsumerEnteredHealthSummaryProperties.txt");
