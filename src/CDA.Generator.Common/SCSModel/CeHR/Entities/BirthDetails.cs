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
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.CeHR.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an Birth Details 
    /// </summary>
    [Serializable]
    [DataContract]
    public class BirthDetails
    {
        #region Properties

        /// <summary>
        /// A list of ExternalData
        /// </summary>
        [CanBeNull]
        public List<ExternalData> ExternalData { get; set; }

        #endregion

        #region Constructors
        internal BirthDetails()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this BirthDetails
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (ExternalData != null)
          {
              for (var x = 0; x < ExternalData.Count; x++)
              {
                  var externalData = ExternalData[x];

                  if (vb.ArgumentRequiredCheck(string.Format("ExternalData[{0}]", x), externalData))
                      ExternalData[0].Validate(vb.Path + string.Format("ExternalData[{0}]", x), vb.Messages);
              }
          }
        }

        #endregion
    }
}