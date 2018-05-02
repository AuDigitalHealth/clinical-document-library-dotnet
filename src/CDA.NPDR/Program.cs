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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.NPDR.Sample
{
    public class Program
    {
        static void Main(string[] args)
        {
            string folderPath = null;
            if (args != null && args.Length > 0)
                folderPath = args[0];
            else
                folderPath = ConfigurationManager.AppSettings["OutputFolder"];

            if (folderPath.IsNullOrEmptyWhitespace()) folderPath = ".";

            PrescriptionRecordSample.OutputFolderPath = folderPath;
            DispenseRecordSample.OutputFolderPath = folderPath;
            
            PrepareOutputFolder(folderPath);

            var prescriptionRecord = new PrescriptionRecordSample();
            var minPrescriptionRecord = prescriptionRecord.MinPopulatedPrescriptionRecordSample("PCEHRPrescriptionRecord_3A_Min.xml");
            var maxPrescriptionRecord = prescriptionRecord.MaxPopulatedPrescriptionRecordSample("PCEHRPrescriptionRecord_3A_Max.xml");

            var dispenseRecord = new DispenseRecordSample();
            var minDispenseRecord = dispenseRecord.MinPopulatedDispenseRecordSample("PCEHRDispenseRecord_3A_Min.xml");
            var maxDispenseRecord = dispenseRecord.MaxPopulatedDispenseRecordSample("PCEHRDispenseRecord_3A_Max.xml");
        }

        static void PrepareOutputFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                if (File.Exists(folderPath + @"\logo.png"))
                    File.Delete(folderPath + @"\logo.png");

            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }

            File.Copy(@"Attachments\logo.png", folderPath + @"\logo.png");
        }
    }
}
