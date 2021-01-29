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
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the CDA specific content for an IPhysicalMeasurementsContext
    /// </summary>
    public interface IPhysicalMeasurementsContext
    {
        /// <summary>
        /// The author
        /// </summary>
        [CanBeNull]
        IAuthorCollection Author { get; set; }

        /// <summary>
        /// The subject of care
        /// </summary>
        [CanBeNull]
        IParticipationSubjectOfCare SubjectOfCare { get; set; }

        /// <summary>
        /// Healthcare Facility
        /// </summary>
        [CanBeNull]
        IParticipationHealthcareFacility HealthcareFacility { get; set; }

      /// <summary>
      /// Validate the CDA Content for this IPhysicalMeasurementsContext
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="physicalMeasurementsDocumentType">Physical Measurements Document Type</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      void Validate(string path, PhysicalMeasurementsDocumentType physicalMeasurementsDocumentType, List<ValidationMessage> messages);


    }
}
