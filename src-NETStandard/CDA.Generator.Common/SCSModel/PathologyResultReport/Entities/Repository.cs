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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
 /// <summary>
  /// This class models a Device
  /// </summary>
  public class Repository 
    {

        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        public List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Software Name
        /// </summary>
        [CanBeNull]
        public string SoftwareName { get; set; }

        #region Constructors

        internal Repository()
        {
        }

        #endregion

        /// <summary>
        /// Validates this AuthorAuthoringDevice
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          vb.ArgumentRequiredCheck("SoftwareName", SoftwareName);

          if (Identifiers != null)
          {
              for (var x = 0; x < Identifiers.Count; x++)
              {
                  Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
              }
          }
        }
    }
}
