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
using CDA.Generator.Common.Common.Time;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a Administration Details
    /// </summary>
    [Serializable]
    [DataContract]
    public class AdministrationDetails
    {
        /// <summary>
        /// Route
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Route { get; set; }

        /// <summary>
        /// Quantity Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText AnatomicalSite { get; set; }

        /// <summary>
        /// Medication Delivery Method
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText MedicationDeliveryMethod { get; set; }

        #region Constructors
        internal AdministrationDetails()
        {
        }
        #endregion

        /// <summary>
        /// Validates this AdministrationDetails
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Route != null)
            {
                Route.Validate(vb.Path + "Route", messages);

                if (Route.CodeSystemCode != CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(x => x.Code))
                {
                  vb.AddValidationMessage(vb.Path + "Route", null, "Route must have a code system of SNOMED");
                }
            }

            if (AnatomicalSite != null)
            {
                AnatomicalSite.Validate(vb.Path + "AnatomicalSite", messages);

                if (AnatomicalSite.CodeSystemCode != CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(x => x.Code))
                {
                  vb.AddValidationMessage(vb.Path + "AnatomicalSite", null, "AnatomicalSite must have a code system of SNOMED");
                }
            }

            if (MedicationDeliveryMethod != null)
            {
                MedicationDeliveryMethod.Validate(vb.Path + "MedicationDeliveryMethod", messages);

            }
        }
    }
}
