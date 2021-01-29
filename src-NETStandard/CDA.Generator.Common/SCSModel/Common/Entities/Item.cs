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
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that are common to each 
    /// item entry
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Item : IItem
    {
        #region Properties
        /// <summary>
        /// Therapeutic good ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText TherapeuticGoodId { get; set; }

        /// <summary>
        /// Formula
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Formula { get; set; }

        /// <summary>
        /// The quantity of the therapeutic good
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String QuantityOfTherapeuticGood { get; set; }

        /// <summary>
        /// A boolean indicating if brand substitution is allowed
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? BrandSubstituteAllowed { get; set; }

        /// <summary>
        /// The maximum numer of repeats
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? MaximumNumberOfRepeats { get; set; }

        /// <summary>
        /// Additional comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AdditionalComments { get; set; }
        #endregion

        #region Constructors
        internal Item()
        {

        }
        #endregion
    }
}