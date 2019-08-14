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
using JetBrains.Annotations;

namespace Nehta.VendorLibrary.CDA.Common
{
    /// <summary>
    /// OID (Object Identifier)
    /// 
    /// This attribute is used to specify an OID and a free text Identifier value.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All)]
    public class OIDAttribute : Attribute
    {
        /// <summary>
        /// The OID (typically a CDA OID)
        /// </summary>
        [CanBeNull]
        public String OID { get; set; }

        /// <summary>
        /// The free text Identifier
        /// </summary>
        [CanBeNull]
        public String Identifier { get; set; }

        #region Constructors
        internal OIDAttribute()
        {

        }

        /// <summary>
        /// OID Attribute
        /// </summary>
        /// <param name="identifier">The free text identifier</param>
        /// <param name="oid">The OID (typically a OID code)</param>
        internal OIDAttribute(String oid, String identifier)
        {
            OID = oid;
            Identifier = identifier;
        }

        /// <summary>
        /// OID Attribute
        /// </summary>
        /// <param name="oid">The OID (typically a oid OID)</param>
        internal OIDAttribute(String oid)
        {
            OID = oid;
        }
        #endregion
    }
}
