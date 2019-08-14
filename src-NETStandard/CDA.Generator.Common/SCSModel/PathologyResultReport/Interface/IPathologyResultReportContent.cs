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
      /// This interface encapsulates all the SCS specific context for an PathologyResultReport
      /// </summary>
      public interface IPathologyResultReportContent
      {
           /// <summary>
           /// Provide a custom Narrative for AdministrativeObservations
           /// </summary>
           [CanBeNull]
           [DataMember]
           StrucDocText CustomNarrativeAdministrativeObservations { get; set; }

           /// <summary>
           /// Provide a custom Narrative 
           /// </summary>
           [CanBeNull]
           [DataMember]
           StrucDocText CustomNarrative { get; set; }

           /// <summary>
           /// Pathology Test Result Custom Narrative
           /// 
           /// NOTE: Structure flattened for ease of use the Pathology Test Result Custom Narrative 
           ///       should contain all entries contained in IList<PathologyTestResult/>
           /// 
           /// </summary>
           [CanBeNull]
           [DataMember]
           StrucDocText CustomNarrativePathologyTestResult { get; set; }

          /// <summary>
          /// Test Result Name
          /// </summary>
          [CanBeNull]
          [DataMember]
          IList<PathologyTestResult> PathologyTestResult { get; set; }

          /// <summary>
          /// Related Document
          /// </summary>
          [CanBeNull]
          [DataMember]
          RelatedDocument RelatedDocument { get; set; }
          
          /// <summary>
          /// Validate the SCS Content for this Pathology Result Report Content
          /// </summary>
          /// <param name="path">The path to this object as a string</param>
          /// <param name="messages">the validation messages, these may be added to within this method</param>
          void Validate(string path, List<ValidationMessage> messages);
      }
}
