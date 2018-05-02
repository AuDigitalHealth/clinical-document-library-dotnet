/*
 * Copyright 2013 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System.Configuration;
using System.IO;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace CDA.R5Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = null;
            if (args != null && args.Length > 0)
                folderPath = args[0];
            else
                folderPath = ConfigurationManager.AppSettings["OutputFolder"];

            if (folderPath.IsNullOrEmptyWhitespace()) folderPath = ".";

            PathologyResultReportSample.OutputFolderPath = folderPath;
            PathologyReportWithStructuredContentSample.OutputFolderPath = folderPath;
            DiagnosticImagingReportSample.OutputFolderPath = folderPath;
            AdvanceCareInformationSample.OutputFolderPath = folderPath;
            GenericObjectReuseSample.OutputFolderPath = folderPath;
            
            PrepareOutputFolder(folderPath);

            var advanceCareInformationSample = new AdvanceCareInformationSample();
            var minAdvanceCareInformationAuthorHealthcareProviderSampleCda = advanceCareInformationSample.MinPopulatedAdvanceCareInformation("AdvanceCareInformationSampleAuthorHealthcareProvider_3A_Min.xml", AuthorType.AuthorHealthcareProvider);
            var maxAdvanceCareInformationAuthorHealthcareProviderSampleCda = advanceCareInformationSample.MaxPopulatedAdvanceCareInformation("AdvanceCareInformationSampleAuthorHealthcareProvider_3A_Max.xml", AuthorType.AuthorHealthcareProvider);

            var minAdvanceCareInformationAuthorNonHealthcareProviderSampleCda = advanceCareInformationSample.MinPopulatedAdvanceCareInformation("AdvanceCareInformationSampleAuthorNonHealthcareProvider_3A_Min.xml", AuthorType.AuthorNonHealthcareProvider);
            var maxAdvanceCareInformationAuthorNonHealthcareProviderSampleCda = advanceCareInformationSample.MaxPopulatedAdvanceCareInformation("AdvanceCareInformationSampleAuthorNonHealthcareProvider_3A_Max.xml", AuthorType.AuthorNonHealthcareProvider);

            var ePathologyResultReportSampleCode = new PathologyResultReportSample();
            var minPathologyReportContent = ePathologyResultReportSampleCode.MinPopulatedPathologyResultReport("PathologyResultReport_3A_Min.xml");
            var maxPathologyReportContent = ePathologyResultReportSampleCode.MaxPopulatedPathologyResultReport("PathologyResultReport_3A_Max.xml");

            var ePathologyReportWithStructuredContent = new PathologyReportWithStructuredContentSample();
            var minPathologyReportWithStructuredContent = ePathologyReportWithStructuredContent.MinPopulatedPathologyReportWithStructuredContent("PopulatedPathologyReportWithStructuredContent_3A_Min.xml");
            var maxPathologyReportWithStructuredContent = ePathologyReportWithStructuredContent.MaxPopulatedPathologyReportWithStructuredContent("PopulatedPathologyReportWithStructuredContent_3A_Max.xml");

            var eDiagnosticImagingReportSampleCode = new DiagnosticImagingReportSample();
            var minDiagnosticImagingReportHealthcareProviderAuthorCda = eDiagnosticImagingReportSampleCode.MinPopulatedDiagnosticImagingReport("DiagnosticImagingReport_3A_Min.xml");
            var maxDiagnosticImagingReportHealthcareProviderAuthorCda = eDiagnosticImagingReportSampleCode.MaxPopulatedDiagnosticImagingReport("DiagnosticImagingReport_3A_Max.xml");
        }

        static void PrepareOutputFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                if (File.Exists(folderPath + @"\logo.png"))
                    File.Delete(folderPath + @"\logo.png");

                if (File.Exists(folderPath + @"\path1234.pdf"))
                    File.Delete(folderPath + @"\path1234.pdf");

                if (File.Exists(folderPath + @"\other1234.pdf"))
                    File.Delete(folderPath + @"\other1234.pdf");

                if (File.Exists(folderPath + @"\x-ray.jpg"))
                    File.Delete(folderPath + @"\x-ray.jpg");

                if (File.Exists(folderPath + @"\pit.txt"))
                    File.Delete(folderPath + @"\pit.txt");

                if (File.Exists(folderPath + @"\attachment.pdf"))
                    File.Delete(folderPath + @"\attachment.pdf");
            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }

            File.Copy(@"Attachments\logo.png", folderPath + @"\logo.png");
            File.Copy(@"Attachments\attachment.pdf", folderPath + @"\attachment.pdf");
            File.Copy(@"Attachments\path1234.pdf", folderPath + @"\path1234.pdf");
            File.Copy(@"Attachments\other1234.pdf", folderPath + @"\other1234.pdf");
            File.Copy(@"Attachments\x-ray.jpg", folderPath + @"\x-ray.jpg");
            File.Copy(@"Attachments\pit.txt", folderPath + @"\pit.txt");
        }
    }
}
