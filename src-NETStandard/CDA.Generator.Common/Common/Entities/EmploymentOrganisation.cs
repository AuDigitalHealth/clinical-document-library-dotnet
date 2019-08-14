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

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    [Serializable]
    [DataContract]
    internal class EmploymentOrganisation : Organisation, IEmploymentOrganisation
    {
        /// <summary>
        /// EmploymentType
        /// </summary>
        [DataMember]
        public ICodableText EmploymentType { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        [DataMember]
        public ICodableText Occupation { get; set; }

        /// <summary>
        /// PositionInOrganisation
        /// </summary>
        [DataMember]
        public ICodableText PositionInOrganisation { get; set; }

        /// <summary>
        /// A list of addresses
        /// </summary>
        [DataMember]
        public List<IAddress> Addresses { get; set; }

        /// <summary>
        /// A list of electronic communication details, E.g. Telephone numbers, email addresses etc
        /// </summary>
        [DataMember]
        public List<ElectronicCommunicationDetail> ElectronicCommunicationDetails { get; set; }

        /// <summary>
        /// Validates this EmploymentOrganisation.
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IEmploymentOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Validate Identifiers
            // This is a mandatory field but has been relaxed to make the CDA library more flexible
            if (Identifiers != null)
            {
                // Check for HPIO
                //if (!Identifiers.Select(identifiers => identifiers.AssigningAuthorityName).Contains(HealthIdentifierType.HPIO.GetAttributeValue<NameAttribute, string>(x => x.Code)))
                //{
                //    vb.AddValidationMessage(vb.PathName, null, "At least one HPI-O Required");
                //}

                // Validate each Identifier
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    if (Identifiers[x] != null)
                    {
                        Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                    }
                }
            }

            //vb.ArgumentRequiredCheck("Name", Name);

            if (NameUsage.HasValue)
            {
                vb.NoMatchCheck("NameUsage", NameUsage.Value, OrganisationNameUsage.Undefined);
            }

            // Validate EmploymentOrganisation
            if (EmploymentType != null)
            {
                EmploymentType.Validate(vb.Path + "EmploymentType", vb.Messages);
            }

            if (Occupation != null)
            {
                Occupation.Validate(vb.Path + "Occupation", vb.Messages);
            }

            if (PositionInOrganisation != null)
            {
                PositionInOrganisation.Validate(vb.Path + "PositionInOrganisation", vb.Messages);
            }

            if (Addresses != null)
            {
                // Validate each Identifier
                for (var x = 0; x < Addresses.Count; x++)
                {
                    if (Addresses[x] != null)
                    {
                        Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                // Validate each Identifier
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    if (ElectronicCommunicationDetails[x] != null)
                    {
                        ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("v[{0}]", x), vb.Messages);
                    }

                }
            }

        }
    }
}
