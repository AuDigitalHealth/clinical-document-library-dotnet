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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    
    /// <summary>
    /// The Organization class contains all the properties that CDA has identified for an organization and
    /// implements the IOrganisation interface
    /// </summary>
    [Serializable]
    [DataContract]
    internal class Organisation : IOrganisation, IOrganisationName
    {
        #region Properties

        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        public List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// The organization name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// The department of interest, within the organization 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Department { get; set; }

        /// <summary>
        /// The name usage for this organization, E.g. Legal, External etc
        /// </summary>
        [CanBeNull]
        [DataMember]
        public OrganisationNameUsage? NameUsage { get; set; }

        #endregion

        #region Constructors
        internal Organisation()
        {
            
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this Organisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck(path + "Name", Name);

            if(NameUsage.HasValue)
            {
                if (NameUsage != null) vb.NoMatchCheck("NameUsage", NameUsage.Value, Enums.OrganisationNameUsage.Undefined);
            }

        }

        /// <summary>
        /// Validates this Organisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IOrganisationName.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var organisationName = ((IOrganisationName)this);

            if (organisationName.Identifiers != null)
            {
                // Validate each Identifier
                for (var x = 0; x < organisationName.Identifiers.Count; x++)
                {
                    if (organisationName.Identifiers[x] != null)
                    {
                        organisationName.Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        /// <summary>
        /// Validates this Organisation
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Validate Identifiers
            // optional fields as organisations may not have a HPIO
            // vb.ArgumentRequiredCheck("Identifiers", Identifiers)

            if (Identifiers != null)
            {
                // Validate each Identifier
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    if (Identifiers[x] != null)
                    {
                        Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                    }
                }
            }

            vb.ArgumentRequiredCheck("Name", Name);

            if(NameUsage.HasValue)
            {
                if (NameUsage != null) vb.NoMatchCheck("NameUsage", NameUsage.Value, OrganisationNameUsage.Undefined);
            }
        }

        #endregion
    }
}
