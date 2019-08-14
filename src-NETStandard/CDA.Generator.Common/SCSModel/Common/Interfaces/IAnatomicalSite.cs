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

using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This interface defines all the properties that make up a IAnatomicalSite
    /// </summary>
    public interface IAnatomicalSite
    {
        #region Properties

        /// <summary>
        /// The name of the anatomical location
        /// </summary>
        [CanBeNull]
        [DataMember]
        SpecificLocation SpecificLocation { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        [CanBeNull]
        [DataMember]
        string Description { get; set; }

        #endregion

        #region Validation

        #endregion
    }
}
