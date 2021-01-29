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
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an imaging examination result
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Immunisation : IImmunisation
    {
        #region Properties
        /// <summary>
        /// Medicine
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Medicine { get; set; }

        /// <summary>
        /// Date / Time of the immunisation
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTime { get; set; }

        /// <summary>
        /// Sequence Number
        /// </summary>
        [CanBeNull]
        [DataMember]
        public int? SequenceNumber { get; set; }

        #endregion

        #region Constructors
        internal Immunisation()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this IImmunisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IImmunisation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("Medicine", Medicine);
            vb.ArgumentRequiredCheck("DateTime", DateTime);

            if (Medicine != null)
            {
               Medicine.ValidateMandatory(path + ".Medicine", messages);
            }
        }

        #endregion
    }
}