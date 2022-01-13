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

namespace CDA.PSML
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

            PSMLSample.OutputFolderPath = folderPath;

            PrepareOutputFolder(folderPath);

            string templatepackageidhpii = "1.2.36.1.2001.1006.1.100065.1";
            string templatepackageidnohpii = "1.2.36.1.2001.1006.1.100065.2";
            string templatepackagever = "40672";


            var smlCda = new SMLSample();
            smlCda.MaxPopulatedSMLAuthorHealthcareProviderSample_3A("SML_AuthorHealthcareProvider_3A_Max.xml",  templatepackageidhpii, templatepackagever, true);
            smlCda.MidMaxPopulatedSMLAuthorHealthcareProviderSample_3A("SML_AuthorHealthcareProvider_3A_MidMax.xml", templatepackageidhpii, templatepackagever, true);
            smlCda.MidMinPopulatedSMLAuthorHealthcareProviderSample_3A("SML_AuthorHealthcareProvider_3A_MidMin.xml", templatepackageidhpii, templatepackagever, true);
            smlCda.MinPopulatedSMLAuthorHealthcareProviderSample_3A("SML_AuthorHealthcareProvider_3A_Min.xml", templatepackageidhpii, templatepackagever, true);

            smlCda.MaxPopulatedSMLAuthorHealthcareProviderSample_3A("SML_AuthorHealthcareProvider_3A_MaxNoHpii.xml", templatepackageidnohpii, templatepackagever, false);
            smlCda.MinPopulatedSMLAuthorHealthcareProviderSample_3A("SML_AuthorHealthcareProvider_3A_MinNoHpii.xml", templatepackageidnohpii, templatepackagever, false);

            var psmlCda = new PSMLSample();
            psmlCda.MaxPopulatedPSMLAuthorHealthcareProviderSample_1A("PSML_AuthorHealthcareProvider_1A_Max.xml");
            psmlCda.MinPopulatedPSMLAuthorHealthcareProviderSample_1A("PSML_AuthorHealthcareProvider_1A_Min.xml");


            //Test Custom Narrative - not currently allowed
            //psmlCda.MaxPopulatedPSMLAuthorHealthcareProviderSample_1B("PSML_AuthorHealthcareProvider_1B_Max.xml");
            //psmlCda.MinPopulatedPSMLAuthorHealthcareProviderSample_1B("PSML_AuthorHealthcareProvider_1B_Min.xml");

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
