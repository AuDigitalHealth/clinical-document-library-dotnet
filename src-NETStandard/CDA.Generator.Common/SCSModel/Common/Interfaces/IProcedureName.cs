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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The IProcedureName interface contains only the name associated with the procedure
    /// </summary>
    public interface IProcedureName
    {
        /// <summary>
        /// Show Ongoing In Narrative
        /// NOTE: This field will always show (Ongoing) in the narrative.
        ///       It will show '(ongoing)' also if DateOfOnset is set eg.. 14 Mar 2012 08:27+1000 -> (ongoing)
        ///       Otherwise it will only show '(ongoing)'
        /// 
        /// </summary>
        [CanBeNull]
        [DataMember]
        bool? ShowOngoingInNarrative { get; set; }

        /// <summary>
        /// Procedure name
        /// </summary>
        [CanBeNull]
        ICodableText ProcedureName { get; set; }

        /// <summary>
        /// Validates this test procedure name
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }
}
