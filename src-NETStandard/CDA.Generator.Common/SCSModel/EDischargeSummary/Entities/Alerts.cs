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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System;
using System.Runtime.Serialization;
using Nehta.HL7.CDA;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a Alert class.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Alerts
    {
        #region Properties

        /// <summary>
        /// Alert Type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Alert> AlertList { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        public StrucDocText CustomNarrativeAlerts { get; set; }

        #endregion

        #region Constructors
        internal Alerts()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Alerts
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("AlertList", AlertList))
            {
              if (AlertList != null)
              {
                for (var x = 0; x < AlertList.Count; x++)
                {
                  AlertList[x].Validate(vb.Path + string.Format("AlertList[{0}]", x), vb.Messages);
                }
              }
            }
        }

        #endregion
    }
}