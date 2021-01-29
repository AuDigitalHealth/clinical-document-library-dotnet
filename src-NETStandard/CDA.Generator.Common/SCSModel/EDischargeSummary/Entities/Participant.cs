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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that extend the Participant class for discharge summary.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Common.Participant))]
    [KnownType(typeof(Person))]
    [KnownType(typeof(Address))]
    [KnownType(typeof(Organisation))]
    internal class Participant : Common.Participant, IResponsibleHealthProfessional, IOtherParticipant, IHealthPersonOrOrganisation, INominatedPrimaryHealthCareProvider, IFacility
    {
        List<IAddress> IHealthPersonOrOrganisation.Addresses { get; set; }
        List<IAddress> IFacility.Addresses { get; set; }
        List<IAddress> IResponsibleHealthProfessional.Addresses { get; set; }
        List<IAddress> INominatedPrimaryHealthCareProvider.Addresses { get; set; }
        List<IAddress> IOtherParticipant.Addresses { get; set; }

        IPersonWithOrganisation IResponsibleHealthProfessional.Person { get; set; }
        IPersonWithOrganisation IOtherParticipant.Person { get; set; }
        IPersonWithOrganisation IHealthPersonOrOrganisation.Person { get; set; }
        IPersonWithOrganisation INominatedPrimaryHealthCareProvider.Person { get; set; }

        IOrganisation INominatedPrimaryHealthCareProvider.Organisation { get; set; }
        IOrganisation IHealthPersonOrOrganisation.Organisation { get; set; }
        IOrganisation IFacility.Organisation { get; set; }

        [CanBeNull]
        [DataMember]
        public CdaInterval ParticipationPeriod { get; set; }

        #region Constructors
        internal Participant()
        {
        }
        #endregion

        #region Validation

        void IOtherParticipant.Validate(string path, List<ValidationMessage> messages)
        {
            var person = ((IOtherParticipant)this).Person;
            var addresses = ((IOtherParticipant)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            if (addresses != null)
            {
                for (var x = 0; x < addresses.Count; x++)
                {
                    if (addresses[x] != null)
                    {
                        addresses[x].Validate(
                            vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate(
                        vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null) person.Validate(vb.Path + "Person", vb.Messages);
            }
        }

        void IResponsibleHealthProfessional.Validate(string path, List<ValidationMessage> messages)
        {
            var person = ((IResponsibleHealthProfessional)this).Person;
            var addresses = ((IResponsibleHealthProfessional)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            if (addresses != null)
            {
                for (var x = 0; x < addresses.Count; x++)
                {
                    if (addresses[x] != null)
                    {
                        addresses[x].Validate(
                            vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    if (ElectronicCommunicationDetails[x] != null)
                    {
                        ElectronicCommunicationDetails[x].Validate(
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null)
                {
                    person.Validate(vb.Path + "Person", vb.Messages);

                    // Identifiers has to be checked at this level because it is not required in person level.
                    vb.ArgumentRequiredCheck("Person.Identifiers", person.Identifiers);
                }
            }
        }

        void IHealthPersonOrOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var healthPersonOrOrganisation = (IHealthPersonOrOrganisation)this;
            var person = ((IHealthPersonOrOrganisation)this).Person;
            var addresses = ((IHealthPersonOrOrganisation)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / Organisation structure
            if ((person == null && healthPersonOrOrganisation.Organisation == null) || (person != null && healthPersonOrOrganisation.Organisation != null))
            {
                vb.AddValidationMessage(vb.PathName, null, "A Person or a Organisation must be provided");
            }
            else
            {
                // A Person
                if (person != null)
                {
                    if (vb.ArgumentRequiredCheck("Person", person))
                    {
                        person.Validate(vb.Path + "Person", vb.Messages);
                    }

                    vb.ArgumentRequiredCheck("Addresses", addresses);
                }

                // An Organisation
                if (healthPersonOrOrganisation.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", healthPersonOrOrganisation.Organisation))
                    {
                        healthPersonOrOrganisation.Organisation.Validate("Organisation", vb.Messages);
                    }
                }
            }

            if (addresses != null)
            {
                for (var x = 0; x < addresses.Count; x++)
                {
                    if (addresses[x] != null)
                    {
                        addresses[x].Validate(
                            vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    if (ElectronicCommunicationDetails[x] != null)
                    {
                        ElectronicCommunicationDetails[x].Validate(
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                    }
                }
            }

        }

        void INominatedPrimaryHealthCareProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var nominatedPrimaryHealthCareProvider = (INominatedPrimaryHealthCareProvider)this;
            var person = ((INominatedPrimaryHealthCareProvider)this).Person;
            var addresses = ((INominatedPrimaryHealthCareProvider)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / Organisation structure
            if ((person == null && nominatedPrimaryHealthCareProvider.Organisation == null) || (person != null && nominatedPrimaryHealthCareProvider.Organisation != null))
            {
                vb.AddValidationMessage(vb.PathName, null, "A Person or a Organisation must be provided");
            }
            else
            {
                // A Person
                if (person != null)
                {
                    if (vb.ArgumentRequiredCheck("Person", person))
                    {
                        person.Validate(vb.Path + "Person", vb.Messages);

                        // Identifiers has to be checked at this level because it is not required in person level.
                        vb.ArgumentRequiredCheck("Person.Identifiers", person.Identifiers);
                    }
                }

                // An Organisation
                if (nominatedPrimaryHealthCareProvider.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", nominatedPrimaryHealthCareProvider.Organisation))
                    {
                        nominatedPrimaryHealthCareProvider.Organisation.Validate("Organisation", vb.Messages);
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("addresses", addresses))
            {
                if (addresses != null)
                    for (var x = 0; x < addresses.Count; x++)
                    {
                        if (addresses[x] != null)
                        {
                            addresses[x].Validate(
                                vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                        }
                    }
            }

            if (vb.ArgumentRequiredCheck("ElectronicCommunicationDetails", ElectronicCommunicationDetails))
            {
                if (ElectronicCommunicationDetails != null)
                    for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                    {
                        if (ElectronicCommunicationDetails[x] != null)
                        {
                            ElectronicCommunicationDetails[x].Validate(
                                vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                        }
                    }
            }
        }
        
        void IFacility.Validate(string path, List<ValidationMessage> messages)
        {
            var facility = ((IFacility)this);

            var vb = new ValidationBuilder(path, messages);
            var addresses = ((IFacility)this).Addresses;

            if (vb.ArgumentRequiredCheck("Addresses", addresses))
            {
                if (addresses != null)
                    for (var x = 0; x < addresses.Count; x++)
                    {
                        if (addresses[x] != null)
                        {
                            addresses[x].Validate(
                                vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                        }
                    }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate(
                        vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("Organisation", facility.Organisation))
            {
                if (facility.Organisation != null) facility.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }
        #endregion
    }
}
