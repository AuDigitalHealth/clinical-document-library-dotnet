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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces
{
  /// <summary>
  /// This class models a Device
  /// </summary>
  public class InformationProviderDevice : Device, IInformationProviderCollection
    {
        /// <summary>
        /// Date Time Authored
        /// </summary>
        [CanBeNull]
        public ISO8601DateTime ParticipationPeriod  { get; set; }

        #region Constructors

        internal InformationProviderDevice()
        {
        }

        #endregion

        /// <summary>
        /// Validates this AuthorAuthoringDevice
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public new void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          vb.ArgumentRequiredCheck("SoftwareName", SoftwareName);
        }
    }
}
