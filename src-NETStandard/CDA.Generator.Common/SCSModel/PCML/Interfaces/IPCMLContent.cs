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
using CDA.Generator.Common.SCSModel.Common.Entities;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// This interface encapsulates all the SCS specific context for an BirthDetailsRecordContent
    /// </summary>
    public interface IPCMLContent
    {
       
        /// <summary>
        /// A list of referenced documents that is the payload for this CDA document
        /// </summary>
        [CanBeNull]
        EncapsulatedData EncapsulatedData { get; set; }

        [CanBeNull]
        List<NarrativeOnlyDocument> CustomNarrativePcmlRecord { get; set; }

        /// <summary>
        /// Validate the SCS Content for this BirthDetailsRecordContent
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void Validate(string path, List<ValidationMessage> messages);
    }

    
}
