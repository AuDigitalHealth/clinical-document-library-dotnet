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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an reaction 
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Reaction
    {
        #region Properties

        private string _id;
        public string Id
        {
            get { return _id ?? (_id = Guid.NewGuid().ToString()); }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Substance or agent
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText SubstanceOrAgent { get; set; }

        /// <summary>
        /// For Used in SML
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<NoteSML> AdditionalComments { get; set; }

        /// <summary>
        /// Reaction event
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ReactionEvent ReactionEvent { get; set; }

        #endregion

        #region Constructors
        internal Reaction()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this reaction
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("SubstanceOrAgent", SubstanceOrAgent))
            {
                SubstanceOrAgent.ValidateMandatory(vb.Path + "SubstanceOrAgent", vb.Messages);
            }

            if (ReactionEvent != null)
            {
                ReactionEvent.Validate(vb.Path + "ReactionEvent", vb.Messages);
            }
        }

        #endregion
    }
}