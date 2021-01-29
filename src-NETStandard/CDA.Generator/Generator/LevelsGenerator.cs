using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace Nehta.VendorLibrary.CDA.Generator
{
    /// <summary>
    /// Generate different levels of conformance for the CDA library
    /// </summary>
    public class LevelsGenerator
    {
        /// <summary>
        /// The base xPath for the CDA StructuredBody 
        /// </summary>
        public const string StructuredBody = "d:ClinicalDocument/d:component/d:structuredBody";


        /// <summary>
        /// 1A - Generate1A
        /// </summary>
        /// <param name="inputFilePath">inputFilePath</param>
        /// <param name="outputFilePath">outputFilePath</param>
        /// <param name="templateFileNameAndPath">templateFileNameAndPath</param>
        /// <param name="attachmentFileNameAndPath">attachmentFileNameAndPath</param>
        public static void Generate1A(string inputFilePath, string outputFilePath, string templateFileNameAndPath, string attachmentFileNameAndPath)
        {
            var input = new XmlDocument();
            input.Load(new MemoryStream(File.ReadAllBytes(inputFilePath)));

            var nm = new XmlNamespaceManager(input.NameTable);
            nm.AddNamespace("ext", "http://ns.electronichealth.net.au/Ci/Cda/Extensions/3.0");
            nm.AddNamespace("d", "urn:hl7-org:v3");

            // Remove all children from structuredBody
            var structuredBody = input.SelectNodes(StructuredBody, nm)[0];
            structuredBody.RemoveAll();

            var fragmentXml = new XmlDocument();
            fragmentXml.Load(new MemoryStream(File.ReadAllBytes(templateFileNameAndPath)));

            var fragmentNode = input.ImportNode(fragmentXml.DocumentElement, true);
            structuredBody.InsertAfter(fragmentNode, null);

            var outputText = input.OuterXml.Replace(" xmlns=\"\"", "");

            if (!string.IsNullOrEmpty(attachmentFileNameAndPath))
            {
                string attachmentFilename = Path.GetFileName(attachmentFileNameAndPath);
                //string attachmentFilename = attachmentFileNameAndPath.Substring(attachmentFileNameAndPath.LastIndexOf("/") + 1);
                outputText = outputText.Replace("<reference value=\"attachment.pdf\" />",
                                                "<reference value=\"" + attachmentFilename + "\" />");

                byte[] attachmentContent = File.ReadAllBytes(attachmentFileNameAndPath);
                var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
                byte[] sha1Hash = sha1CryptoServiceProvider.ComputeHash(attachmentContent);
                outputText = Regex.Replace(outputText, "integrityCheck=\".+?\"",
                                           "integrityCheck=\"" + Convert.ToBase64String(sha1Hash) + "\"",
                                           RegexOptions.IgnoreCase);
            }

            input = new XmlDocument();
            input.LoadXml(outputText);

            // Write output file
            using (var writer = XmlWriter.Create(outputFilePath, new XmlWriterSettings() { Indent = true }))
            {
                input.Save(writer);
            }
        }

        /// <summary>
        /// Generate Level 1B
        /// </summary>
        /// <param name="inputFilePath">The inputFilePath</param>
        /// <param name="outputFilePath">The outputFilePath</param>
        public static void Generate1B(string inputFilePath, string outputFilePath)
        {
            var input = new XmlDocument();
            input.Load(new MemoryStream(File.ReadAllBytes(inputFilePath)));

            var nm = new XmlNamespaceManager(input.NameTable);
            nm.AddNamespace("ext", "http://ns.electronichealth.net.au/Ci/Cda/Extensions/3.0");
            nm.AddNamespace("d", "urn:hl7-org:v3");

            // Get the first Section
            var component = input.SelectNodes(StructuredBody + "/d:component", nm)[0];
            // Get the text in the first Section
            var text = component.SelectNodes("d:section/d:text", nm)[0];

            string titleSection = "<content styleCode='Bold Underline'>{0}</content><br/>";

            // Get Narrative from /d:component/d:section/d:text
            foreach (XmlNode node in input.SelectNodes(StructuredBody + "/d:component", nm))
            {
                foreach (XmlNode innerNarrativeNode in node.SelectNodes("d:section/d:text", nm))
                {
                  string storeResult = innerNarrativeNode.InnerXml;

                  if (text.InnerXml == innerNarrativeNode.InnerXml)
                    text.InnerXml = string.Format(titleSection, innerNarrativeNode.ParentNode.SelectNodes("d:title", nm)[0].InnerText);
                  else
                    text.InnerXml += string.Format(titleSection, innerNarrativeNode.ParentNode.SelectNodes("d:title", nm)[0].InnerText);

                  text.InnerXml += storeResult;
                }

                foreach (XmlNode innerNarrativeNode in node.SelectNodes("d:section/d:component/d:section/d:text", nm))
                {
                  text.InnerXml += string.Format(titleSection, innerNarrativeNode.ParentNode.SelectNodes("d:title", nm)[0].InnerText);
                  text.InnerXml += innerNarrativeNode.InnerXml; 
                }

                foreach (XmlNode innerNarrativeNode in node.SelectNodes("d:section/d:component/d:section/d:component/d:section/d:text", nm))
                {
                  text.InnerXml += string.Format(titleSection, innerNarrativeNode.ParentNode.SelectNodes("d:title", nm)[0].InnerText);
                  text.InnerXml += innerNarrativeNode.InnerXml;
                }

                foreach (XmlNode innerNarrativeNode in node.SelectNodes("d:section/d:component/d:section/d:component/d:section/d:component/d:section/d:text", nm))
                {
                  text.InnerXml += string.Format(titleSection, innerNarrativeNode.ParentNode.SelectNodes("d:title", nm)[0].InnerText);
                  text.InnerXml += innerNarrativeNode.InnerXml;
                }
            }

            // Get the text in the first Section
            var title = component.SelectNodes("d:section/d:title", nm)[0];
            title.InnerXml = "Content";

            // Remove Attachments
            var elements = input.GetElementsByTagName("renderMultiMedia");
            while (elements.Count != 0) { elements[0].ParentNode.RemoveChild(elements[0]); }
            text.InnerXml = text.InnerXml.Replace("See above..", string.Empty);


            // Clear the first Section
            var section = input.SelectNodes(StructuredBody + "/d:component/d:section", nm)[0];
            var sectionXmlNodes = section.ChildNodes.Cast<XmlNode>().Where(child => child.Name != "text" && child.Name != "title").ToList();
            sectionXmlNodes.ForEach(xmlNode => section.RemoveChild(xmlNode));

            // Remove all children from structuredBody
            var structuredBody = input.SelectNodes(StructuredBody, nm)[0];
            structuredBody.RemoveAll();
            structuredBody.InsertAfter(component, null);

            // Write output file
            using (var writer = XmlWriter.Create(outputFilePath, new XmlWriterSettings() { Indent = true }))
            {
                input.Save(writer);
            }
        }

        /// <summary>
        /// Generate Level 2
        /// </summary>
        /// <param name="inputFilePath">The inputFilePath</param>
        /// <param name="outputFilePath">The outputFilePath</param>
        public static void Generate2(string inputFilePath, string outputFilePath)
        {
            var input = new XmlDocument();
            input.Load(new MemoryStream(File.ReadAllBytes(inputFilePath)));

            var nm = new XmlNamespaceManager(input.NameTable);
            nm.AddNamespace("ext", "http://ns.electronichealth.net.au/Ci/Cda/Extensions/3.0");
            nm.AddNamespace("d", "urn:hl7-org:v3");

            // Remove logo
            var logoNode = input.SelectSingleNode("//d:observationMedia[@ID='LOGO']", nm);
            if (logoNode != null)
            {
                var componentNode = logoNode.ParentNode.ParentNode.ParentNode;
                componentNode.ParentNode.RemoveChild(componentNode);
            }

            var entries = input.SelectNodes("//d:section/d:entry", nm);
            foreach (XmlNode entry in entries)
            {
                entry.ParentNode.RemoveChild(entry);
            }

            var coverage = input.SelectNodes("//d:section/ext:coverage2", nm);
            foreach (XmlNode entry in coverage)
            {
                entry.ParentNode.RemoveChild(entry);
            }

            // Remove Attachments
            var elements = input.GetElementsByTagName("renderMultiMedia");
            while (elements.Count != 0) { elements[0].ParentNode.RemoveChild(elements[0]); }
            input.InnerXml = input.InnerXml.Replace("See above..", string.Empty);

            // Write output file
            // Write output file
            using (var writer = XmlWriter.Create(outputFilePath, new XmlWriterSettings() { Indent = true }))
            {
                input.Save(writer);
            }
        }
    }



}