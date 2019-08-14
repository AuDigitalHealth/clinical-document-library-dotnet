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

using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Pathology
{
      /// <summary>
      /// This interface encapsulates all the SCS specific context for an Advance Care Information
      /// </summary>
      public interface IAdvanceCareInformationContent
      {
        /// <summary>
        /// Provide a custom Narrative for Administrative Observations
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

        /// <summary>
        /// Provide a custom Narrative for Advance Care Information Section
        /// </summary>
        [CanBeNull]
        [DataMember]
        StrucDocText CustomNarrativeAdvanceCareInformationSection { get; set; }

        /// <summary>
        /// Related Document
        /// </summary>
        [DataMember]
        IDocumentDetails DocumentDetails { get; set; }
 
        /// <summary>
        /// Validate the SCS Content for this Advance Care Information Content
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
      }
}
