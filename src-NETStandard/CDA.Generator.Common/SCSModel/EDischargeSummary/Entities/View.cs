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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a View
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class View  
    {
        #region Properties

        /// <summary>
        /// Per View Findings
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText PerViewFindings { get; set; }

        /// <summary>
        /// View Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ViewName { get; set; }

        /// <summary>
        /// The ExternalData Image
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ExternalData Image { get; set; }

        #endregion

        #region Constructors
        internal View()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this View
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages) 
        {
            var vb = new ValidationBuilder(path, messages);

            if (PerViewFindings != null) PerViewFindings.Validate(vb.Path + "PerViewFindings", vb.Messages);
            if (ViewName != null) ViewName.Validate(vb.Path + "ViewName", vb.Messages);
            if (Image != null) Image.Validate(vb.Path + "Image", vb.Messages);
        }
        #endregion
    }
}