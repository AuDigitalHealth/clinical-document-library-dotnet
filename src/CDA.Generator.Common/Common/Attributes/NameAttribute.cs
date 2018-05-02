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
    /// Name Attribute
    /// 
    /// This attribute is used to specify a Code and a free text Name value.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field)]
    public class NameAttribute : Attribute
    {
        /// <summary>
        /// The free text Name
        /// </summary>
        [CanBeNull]
        public String Name { get; set; }

        /// <summary>
        /// The Code (typically a CDA code)
        /// </summary>
        [CanBeNull]
        public String Code { get; set; }

        /// <summary>
        /// The CodeSystem  
        /// </summary>
        [CanBeNull]
        public String CodeSystem { get; set; }

        /// <summary>
        /// The Identifier for the item
        /// </summary>
        [CanBeNull]
        public String Identifier { get; set; }

        /// <summary>
        /// The Identifier for the item
        /// </summary>
        [CanBeNull]
        public String TemplateIdentifier { get; set; }

        /// <summary>
        /// The Version
        /// </summary>
        [CanBeNull]
        public String Version { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        [CanBeNull]
        public String Comment { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [CanBeNull]
        public String Title { get; set; }

        /// <summary>
        /// Extension
        /// </summary>
        [CanBeNull]
        public String Extension { get; set; }

        /// <summary>
        /// Narrative
        /// </summary>
        [CanBeNull]
        public String Narrative { get; set; }

        #region Constructors
        internal NameAttribute()
        {

        }

        /// <summary>
        /// Name Attribute
        /// </summary>
        /// <param name="name">The free text Name</param>
        /// <param name="code">The Code (typically a CDA code)</param>
        internal NameAttribute(String name, String code)
        {
            Name = name;
            Code = code;
        }

        /// <summary>
        /// Name Attribute
        /// </summary>
        /// <param name="name">The free text Name</param>
        /// <param name="version">the Version (if applicable) asscoated with the element this attribute is applied to</param>
        /// <param name="code">The Code (typically a CDA code)</param>
        internal NameAttribute(String name, String code, String version)
        {
            Name = name;
            Code = code;
            Version = version;
        }

        /// <summary>
        /// Name Attribute
        /// </summary>
        /// <param name="name">The free text Name</param>
        /// <param name="version">the Version (if applicable) associated with the element this attribute is applied to</param>
        /// <param name="comment">Any comments associated with the element this attribute is applied to</param>
        /// <param name="code">The Code (typically a CDA code)</param>
        internal NameAttribute(String name, String code, String version, String comment)
        {
            Name = name;
            Code = code;
            Version = version;
            Comment = comment;
        }

        #endregion
    }
}
