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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.Common.Entities
{
    /// <summary>
    /// This class represents a file. The file can either be embedded or referenced.
    /// </summary>
    [Serializable]
    [DataContract]
    public class NarrativeOnlyDocument
    {
        #region Properties

        /// <summary>
        /// The Title of the Component Section
        /// </summary>
        public string Title;

        /// <summary>
        /// The Narrative For the Component
        /// </summary>
        public StrucDocText Narrative;

        #endregion

        #region Constructors

        internal NarrativeOnlyDocument()
        {
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validates this NarrativeOnlyDocument
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">The validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Title", Title);
            vb.ArgumentRequiredCheck("Narrative", Narrative);

        }

        #endregion

    }

}

