using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using DigitalHealth.Hl7ToCdaTransformer.Interfaces;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.Hl7ToCdaTransformer.Services;
using DigitalHealth.HL7.Common.Message;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Digitalhealth.Hl7ToCda.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            TransformToCdaDocumentSample();
        }

        /// <summary>
        /// Sample code showing how an HL7 V2 pathology message is transformed into a CDA document.
        /// </summary>
        public static void TransformToCdaDocumentSample()
        {
            // Obtain HL7 V2 report in a string.
            var v2PathologyText = File.ReadAllText($"{AppContext.BaseDirectory}/Sample/pathologyreport.hl7");

            // Create an instance of the PathologyTransformer.
            IPathologyTransformer cdaPathologyTransformer = new PathologyTransformer();

            // Parse the HL7 V2 string into an object model of the pathology message.
            HL7GenericPathMessage hl7PathMessage = cdaPathologyTransformer.ParseHl7Message(v2PathologyText);

            // Create a PathologyMetadata instance (to provide additional information required for CDA document generation, that is not
            // available from the HL7 V2 report).
            PathologyMetadata requiredMetadata = CreateRequiredMetadataForCdaTransform();

            // Transform the pathology message into a PathologyTransformResult. PathologyTransformResult contains two properties:
            // 1) PathologyResultReport - This is the object model of the pathology report CDA document. Prior to CDA document generation, you can
            //    modify this instance to include more information for the CDA document.
            // 2) Attachment - This contains the byte array of the extracted report attachment
            // Note: reportData argument must not be supplied if the report attachment is found within the HL7 V2 report. Otherwise, reportData
            // has to be provided in this function.
            PathologyTransformResult pathologyTransformResult = cdaPathologyTransformer.Transform(hl7PathMessage, requiredMetadata);

            // This would be how you insert and external report not embedded in the OBX ED segment
            // byte[] reportdata = File.ReadAllBytes($"{AppContext.BaseDirectory}/Sample/prdfreport.pdf");
            // PathologyTransformResult pathologyTransformResult = cdaPathologyTransformer.Transform(hl7PathMessage, requiredMetadata, reportdata);

            // An example of how you can modify PathologyResultReport instance to include more information for the CDA document.
            // For more examples, please view the CDA.R5Samples project.
            pathologyTransformResult.PathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = "Sample Organisation Name";

            // Generates the CDA document XML
            XmlDocument cdaDocument = cdaPathologyTransformer.Generate(pathologyTransformResult.PathologyResultReport);

            // Save the CDA document XML to file
            cdaDocument.Save($"{AppContext.BaseDirectory}/cdadocument.xml");
            
            // Save the report attachment to file
            File.WriteAllBytes($"{AppContext.BaseDirectory}/{pathologyTransformResult.Attachment.Filename}", pathologyTransformResult.Attachment.Data);
        }

        /// <summary>
        /// Creates a PathologyMetadata instance for this sample.
        /// </summary>
        /// <returns></returns>
        public static PathologyMetadata CreateRequiredMetadataForCdaTransform()
        {
            PathologyMetadata metadata = new PathologyMetadata();

            // Author organisation HPIO
            metadata.AuthorOrganisationHpio = "8003628233352432";

            // Report identifier
            metadata.ReportIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.54.8003628233352432", "HOM07051718571.7841");

            // Requester order identifier
            metadata.RequesterOrderIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003628233352432", "100041");

            // Reporting pathologist
            metadata.ReportingPathologist = new ReportingPathologist();
            metadata.ReportingPathologist.Hpii = "8003610102030405"; // Hpii must be set here, or provided in OBR-32 of HL7 V2 report    
            metadata.ReportingPathologist.OrganisationHpio = "8003628233352432";
            metadata.ReportingPathologist.Role = Occupation.Pathologist;
            
            // Reporting pathologist - address
            var address = BaseCDAModel.CreateAddress();
            address.AddressPurpose = AddressPurpose.Business;
            address.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
            address.AustralianAddress.UnstructuredAddressLines = new List<string> { "400 George Street" };
            address.AustralianAddress.PostCode = "4000";
            address.AustralianAddress.State = AustralianState.QLD;
            metadata.ReportingPathologist.Address = new List<IAddress>() { address };

            // Reporting pathologist - contact details
            var coms = BaseCDAModel.CreateElectronicCommunicationDetail(
                "(08) 8888 6666",
                ElectronicCommunicationMedium.Telephone,
                ElectronicCommunicationUsage.WorkPlace);
            metadata.ReportingPathologist.ContactDetails = new List<ElectronicCommunicationDetail>() { coms };

            return metadata;
        }

    }
}
