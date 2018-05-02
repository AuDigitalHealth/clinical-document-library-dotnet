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
using Nehta.VendorLibrary.Common;
using JetBrains.Annotations;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a recomendation
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Participation))]
    public class Recommendation
    {
        #region Properties
        /// <summary>
        /// A list of addressees
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationAddressee Addressee { get; set; }

        /// <summary>
        /// The recomendation time frame
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval TimeFrame { get; set; }

        /// <summary>
        /// Recommendation narrative
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Narrative { get; set; }
        #endregion

        #region Constructors
        internal Recommendation()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this recomendation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("TimeFrame", TimeFrame);
            vb.ArgumentRequiredCheck("Narrative", Narrative);

            if (vb.ArgumentRequiredCheck("Addressee", Addressee))
            {
                Addressee.Validate(vb.Path + "Addressee", messages);
            }
        }
        #endregion
    }
}
