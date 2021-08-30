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
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class DosageSML
    {
        /// <summary>
        /// Sequence
        /// </summary>
        [CanBeNull]
        public int? Sequence { get; set; }

        /// <summary>
        /// Instructions
        /// </summary>
        [CanBeNull]
        public String Instructions { get; set; }

        /// <summary>
        /// Dose Timing
        /// </summary>
        [CanBeNull]
        public ISO8601DateTime DoseTiming { get; set; }

        /// <summary>
        /// As Needed
        /// </summary>
        [CanBeNull]
        public Boolean AsNeeded { get; set; }

        /// <summary>
        /// Clinical Finding
        /// </summary>
        [CanBeNull]
        public ICodableText ClinicalFinding { get; set; }


        /// <summary>
        /// Body Site
        /// </summary>
        [CanBeNull]
        public ICodableText BodySite { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        [CanBeNull]
        public ICodableText Route { get; set; }


        /// <summary>
        /// Administration Method
        /// </summary>
        [CanBeNull]
        public ICodableText AdministrationMethod { get; set; }

        /// <summary>
        /// Dose
        /// </summary>
        [CanBeNull]
        //public CdaInterval Dose { get; set; }
        public Quantity Dose { get; set; }

        /// <summary>
        /// Max Dos eper Period
        /// </summary>
        [CanBeNull]
        //public CdaInterval MaxDosePerPeriod { get; set; }
        public Frequency MaxDosePerPeriod { get; set; }
        
        /// <summary>
        /// Rate
        /// </summary>
        [CanBeNull]
        //public CdaInterval Rate { get; set; }
        public Quantity Rate { get; set; }

        /// <summary>
        /// Validates this reviewed IMedication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
        }
    }
}
