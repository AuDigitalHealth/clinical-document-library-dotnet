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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This Qualifier Codes class is designed to encapsulate the properties within a CDA document that make up a Qualifier Code
    /// </summary>
    [Serializable]
    [DataContract]
    public class QualifierCode
    {
        #region Properties
        /// <summary>
        /// Name 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Value { get; set; }

        #endregion

        #region Constructors
        internal QualifierCode()
        {
            
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Qualifier Code
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);
            validationBuilder.ArgumentRequiredCheck("Name", Name);
            validationBuilder.ArgumentRequiredCheck("Value", Value);
        }
        #endregion
    }
}
