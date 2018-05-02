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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class encapsulates observation information
    /// 
    /// E.g. Body height, body weight etc
    /// </summary>
    [Serializable]
    [DataContract]
    internal class Observation : IObservation
    {
        #region Properties

        /// <summary>
        /// Custom Narrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrative { get; set; }

        /// <summary>
        /// Body weight
        /// </summary>
        [CanBeNull]
        [DataMember]
        public BodyWeight BodyWeight { get; set; }

        /// <summary>
        /// Body height
        /// </summary>
        [CanBeNull]
        [DataMember]
        public BodyHeight BodyHeight { get; set; }
        #endregion

        #region Constructors
        internal Observation()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Observation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (BodyWeight != null)
            {
                BodyWeight.Validate(vb.Path + "BodyWeight", vb.Messages);
            }

            if (BodyHeight != null)
            {
                BodyHeight.Validate(vb.Path + "BodyHeight", vb.Messages);
            }

        }
        #endregion

    }
}