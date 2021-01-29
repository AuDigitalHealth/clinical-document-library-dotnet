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
    /// an procedure
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class Procedure : IProcedureName
    {
        #region Properties

        /// <summary>
        /// Show Ongoing In Narrative
        /// NOTE: This field will always show (Ongoing) in the narrative.
        ///       It will show '(ongoing)' also if DateOfOnset is set eg.. 14 Mar 2012 08:27+1000 -> (ongoing)
        ///       Otherwise it will only show '(ongoing)'
        /// 
        /// </summary>
        [DataMember]
        public bool? ShowOngoingInNarrative { get; set; }

        /// <summary>
        /// Procedure name
        /// </summary>
        [DataMember]
        public ICodableText ProcedureName { get; set; }

        /// <summary>
        /// The date range during which the Procedure action occurred.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval ProcedureDateTime { get; set; }

        /// <summary>
        /// comments
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Comment { get; set; }
        #endregion

        #region Constructors
        internal Procedure()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this procedure
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Procedure Name", ProcedureName))
            {
                if (ProcedureName != null) ProcedureName.ValidateMandatory(vb.Path + "ProcedureName", vb.Messages);
            }
        }
        #endregion
    }
}
