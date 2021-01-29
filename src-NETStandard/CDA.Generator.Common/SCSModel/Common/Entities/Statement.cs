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
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class encapsulates a statement
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Statement 
    {
        #region Properties

        /// <summary>
        /// A NCTIS global statement value, E.g. NotAsked
        /// </summary>
        [CanBeNull]
        [DataMember]
        public NCTISGlobalStatementValues? Value { get; set; }

        #endregion

        #region Constructors
        internal Statement()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this statement
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Statement.Value", Value);
        }

        #endregion
    }
}