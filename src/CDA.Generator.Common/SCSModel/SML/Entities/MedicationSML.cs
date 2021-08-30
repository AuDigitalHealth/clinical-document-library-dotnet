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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class MedicationSML
    {
        /// <summary>
        /// brand Name
        /// </summary>
        [CanBeNull]
        public String BrandName { get; set; }

        /// <summary>
        /// Generic Name
        /// </summary>
        [CanBeNull]
        public String GenericName { get; set; }

        /// <summary>
        /// Batch Lot Number
        /// </summary>
        [CanBeNull]
        public string BatchLotNumber { get; set; }

        /// <summary>
        ///  Batch Expiration Date
        /// </summary>
        [CanBeNull]
        public ISO8601DateTime BatchExpirationDate { get; set; }

        /// <summary>
        /// Item Code
        /// </summary>
        [CanBeNull]
        public ICodableText ItemCode { get; set; }

        /// <summary>
        /// Manufacturer
        /// </summary>
        [CanBeNull]
        public IOrganisation Manufacturer { get; set; }

        /// <summary>
        /// Form
        /// </summary>
        [CanBeNull]
        public ICodableText Form { get; set; }

        /// <summary>
        /// Form
        /// </summary>
        [CanBeNull]
        public List<IngredientsSML> Ingredients { get; set; }


        /// <summary>
        /// Image
        /// </summary>
        [CanBeNull]
        public String Image { get; set; }

        /// <summary>
        /// Validates this reviewed IMedication
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
        }

    }
}
