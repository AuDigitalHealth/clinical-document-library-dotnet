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
using Nehta.VendorLibrary.CDA.Consumer;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Sample
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

            AcdCustodianRecordSample.OutputFolderPath = folderPath;
            ConsumerEnteredHealthSummarySample.OutputFolderPath = folderPath;
            ConsumerEnteredNotesSample.OutputFolderPath = folderPath;
            PhysicalMeasurementsSample.OutputFolderPath = folderPath;
            
            PrepareOutputFolder(folderPath);

            var acdCustodianRecordSampleCode = new AcdCustodianRecordSample();
            var minAcdCustodianRecordCda = acdCustodianRecordSampleCode.MinPopulatedAcdCustodianRecordSample("AcdCustodianRecord_3A_Min.xml");
            var maxAcdCustodianRecordCda = acdCustodianRecordSampleCode.MaxPopulatedAcdCustodianRecordSample("AcdCustodianRecord_3A_Max.xml");

            var consumerEnteredNotesSampleCode = new ConsumerEnteredNotesSample();
            var minconsumerEnteredNotesCda = consumerEnteredNotesSampleCode.MinPopulatedConsumerEnteredNotesSample("ConsumerEnteredNotes_3A_Min.xml");
            var maxconsumerEnteredNotesCda = consumerEnteredNotesSampleCode.MaxPopulatedConsumerEnteredNotesSample("ConsumerEnteredNotes_3A_Max.xml");

            var consumerEnteredHealthSummarySampleCode = new ConsumerEnteredHealthSummarySample();
            var minConsumerEnteredHealthSummaryCda = consumerEnteredHealthSummarySampleCode.MinPopulatedConsumerEnteredHealthSummarySample("ConsumerEnteredHealthSummary_3A_Min.xml");
            var maxConsumerEnteredHealthSummaryCda = consumerEnteredHealthSummarySampleCode.MaxPopulatedConsumerEnteredHealthSummarySample("ConsumerEnteredHealthSummary_3A_Max.xml");

            //
            // Note : The following documents have incomplete CDA Documents & Schematron files associated. 
            //
            var physicalMeasurementsSampleCode = new PhysicalMeasurementsSample();
            var minConsumerEnteredMeasurementsCda = physicalMeasurementsSampleCode.MinPopulatedConsumerEnteredMeasurementsSample("ConsumerEnteredMeasurements_3A_Min.xml");
            var maxConsumerEnteredMeasurementsCda = physicalMeasurementsSampleCode.MaxPopulatedConsumerEnteredMeasurementsSample("ConsumerEnteredMeasurements_3A_Max.xml");

            var minHealthcareProviderEnteredMeasurementsCda = physicalMeasurementsSampleCode.MinPopulatedHealthcareProviderEnteredMeasurementsSample("HealthcareProviderEnteredMeasurements_3A_Min.xml");
            var maxHealthcareProviderEnteredMeasurementsCda = physicalMeasurementsSampleCode.MaxPopulatedHealthcareProviderEnteredMeasurementsSample("HealthcareProviderEnteredMeasurements_3A_Max.xml");

            var minPhysicalMeasurementsViewCda = physicalMeasurementsSampleCode.MinPopulatedPhysicalMeasurementsViewSample("PhysicalMeasurementsView_3A_Min.xml");
            var maxPPhysicalMeasurementsViewCda = physicalMeasurementsSampleCode.MaxPopulatedPhysicalMeasurementsViewSample("PhysicalMeasurementsView_3A_Max.xml");

       }

        static void PrepareOutputFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                if (File.Exists(folderPath + @"\logo.png"))
                    File.Delete(folderPath + @"\logo.png");

                if (File.Exists(folderPath + @"\attachment.pdf"))
                    File.Delete(folderPath + @"\attachment.pdf");
            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }

            File.Copy(@"Attachments\attachment.pdf", folderPath + @"\attachment.pdf");
            File.Copy(@"Attachments\logo.png", folderPath + @"\logo.png");
        }
    }
}
