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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a Quantity Unit
    /// </summary>
    [Serializable]
    [DataContract]
    public class QuantityUnit
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity Quantity { get; set; }

        /// <summary>
        /// Quantity Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Unit { get; set; }

        /// <summary>
        /// Quantity Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string QuantityDescription { get; set; }

        #region Constructors
        internal QuantityUnit()
        {
        }
        #endregion

        /// <summary>
        /// Validates this Quantity Unit
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Unit != null)
            {
               if (Unit.CodeSystemName != CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(x => x.Name))
               {
                 vb.AddValidationMessage(vb.PathName, null, string.Format("Unit must use a {0} CodeSystemName", CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(x => x.Name)));
               }

               if (Unit.CodeSystemCode != CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(x => x.Code))
               {
                 vb.AddValidationMessage(vb.Path + "Unit", null, "Route must have a code system of SNOMED");
               }
            }
        }

        /// <summary>
        /// Validates this Quantity Unit for Dispensing Unit
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void ValidateDispensingUnit(string path, List<ValidationMessage> messages)
        {
          Validate(path, messages);

          var vb = new ValidationBuilder(path, messages);

          if (Quantity != null && Quantity.Value != null && (Quantity.UnitCode == null && Unit == null))
          {
            vb.AddValidationMessage(vb.PathName, string.Empty, "'Quantity' and 'Dose Unit' elements SHALL be provided together.");
          }

          if (Quantity != null && (Quantity.UnitCode != null && Unit != null))
          {
            vb.AddValidationMessage(vb.PathName, string.Empty, "Please choose only 'Quantity.Units' or 'Unit' not both");
          }

          if (!QuantityDescription.IsNullOrEmptyWhitespace() && (Quantity != null || Unit != null))
          {
            vb.AddValidationMessage(vb.PathName, string.Empty, "'Quantity Description' SHALL NOT co-exist with 'Quantity' and 'Unit'");
          }
        }
    }
}
