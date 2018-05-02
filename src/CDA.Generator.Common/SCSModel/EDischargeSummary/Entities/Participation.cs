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
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that extend the Participation class for discharge summary.
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Participation))]
    [KnownType(typeof(Participant))]
    internal class Participation : Common.Participation, IParticipationFacility, IParticipationResponsibleHealthProfessional, IParticipationHealthProfessional, IParticipationOtherParticipant, IParticipationNominatedPrimaryHealthCareProvider
    {
        #region Properties

        IFacility IParticipationFacility.Participant { get; set; }
        IResponsibleHealthProfessional IParticipationResponsibleHealthProfessional.Participant { get; set; }
        IHealthPersonOrOrganisation IParticipationHealthProfessional.Participant { get; set; }
        IOtherParticipant IParticipationOtherParticipant.Participant { get; set; }
        INominatedPrimaryHealthCareProvider IParticipationNominatedPrimaryHealthCareProvider.Participant { get; set; }

        #endregion

        #region Constructors
        internal Participation()
        {
         }
        #endregion

        #region Validation

        void IParticipationFacility.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Role", Role);

            if (Role != null)
            {
                Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            var participant = ((IParticipationFacility)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationResponsibleHealthProfessional.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Role", Role);

            if (Role != null)
            {
              Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationResponsibleHealthProfessional)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationHealthProfessional.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Role", Role);

            if (Role != null)
            {
              Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationHealthProfessional)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationNominatedPrimaryHealthCareProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Role", Role);

            if (Role != null)
            {
              Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationNominatedPrimaryHealthCareProvider)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationOtherParticipant.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("RoleType", RoleType);
            vb.ArgumentRequiredCheck("Role", Role);

            if (Role != null)
            {
                Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationOtherParticipant)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        #endregion
    }
}
