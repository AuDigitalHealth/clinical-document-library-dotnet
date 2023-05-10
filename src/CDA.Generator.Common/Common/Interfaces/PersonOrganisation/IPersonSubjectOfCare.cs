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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA
{
    /// <summary>
    /// This interface defines and constrains the person object down to those properties that are applicable
    /// for a Subject of Care.
    /// </summary>
    public interface IPersonSubjectOfCare
    {
        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Person Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        List<IPersonName> PersonNames { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [CanBeNull]
        Gender? Gender { get; set; }

        /// <summary>
        /// A Boolean indicating if the date of birth has been calculated from their age
        /// </summary>
        [CanBeNull]
        Boolean? DateOfBirthCalculatedFromAge { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        [CanBeNull]
        ISO8601DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Date of birth accuracy indicator
        /// </summary>
        [CanBeNull]
        DateAccuracyIndicator DateOfBirthAccuracyIndicator { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [CanBeNull]
        Int32? Age { get; set; }

        /// <summary>
        /// The Unit of Measure used for the Age : Default is Year
        /// </summary>
        [CanBeNull]
        AgeUnitOfMeasure? AgeUnitOfMeasure { get; set; }

        /// <summary>
        /// Age accuracy indicator
        /// </summary>
        [CanBeNull]
        Boolean? AgeAccuracyIndicator { get; set; }

        /// <summary>
        /// Birth polarity; the position in relation to their siblings
        /// </summary>
        [CanBeNull]
        Int32? BirthPlurality { get; set; }

        /// <summary>
        /// Birth order
        /// </summary>
        [CanBeNull]
        Int32? BirthOrder { get; set; }

        /// <summary>
        /// Date of death
        /// </summary>
        [CanBeNull]
        ISO8601DateTime DateOfDeath { get; set; }

        /// <summary>
        /// Date of death accuracy indicator
        /// </summary>
        [CanBeNull]
        DateAccuracyIndicator DateOfDeathAccuracyIndicator { get; set; }

        /// <summary>
        /// Country of birth
        /// </summary>
        Country CountryOfBirth { get; set; }

        /// <summary>
        /// State of birth
        /// </summary>
        AustralianState StateOfBirth { get; set; }

        /// <summary>
        /// Indigenous status
        /// </summary>
        IndigenousStatus IndigenousStatus { get; set; }

        /// <summary>
        /// Indigenous status
        /// </summary>
        IndigenousStatus AuIndigenousStatus { get; set; }

        /// <summary>
        /// Mother's Original Family Name
        /// </summary>
        [CanBeNull]
        IPersonName MothersOriginalFamilyName { get; set; }

        /// <summary>
        /// Source Of Death Notification
        /// </summary>
        [CanBeNull]
        SourceOfDeathNotification? SourceOfDeathNotification { get; set; }

        /// <summary>
        /// Interpreter Required Alert
        /// </summary>
        [CanBeNull]
        InterpreterRequiredAlert InterpreterRequired { get; set; }

        /// <summary>
        /// Validates this person as a subject of care
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
