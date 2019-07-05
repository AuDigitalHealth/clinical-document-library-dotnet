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
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.Common.Time.Enum;
using CDA.Generator.Common.SCSModel.Interfaces;
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Person))]
    [KnownType(typeof(CodableText))]
    [KnownType(typeof(Participant))]
    [KnownType(typeof(DischargeSummary.Participation))]
    internal class Participation : IParticipation, IParticipationPatientNominatedContact, IParticipationAcdCustodian, 
        IParticipationSubjectOfCare, IParticipationCustodian, IParticipationLegalAuthenticator, 
        IParticipationInformationRecipient, IParticipationReferee, IParticipationUsualGP, IParticipationReferrer, IParticipationAddressee, 
        IParticipationServiceProvider, IParticipationPrescriberInstructionRecipient, IParticipationPrescriber, 
        IParticipationPrescriberOrganisation, IParticipationDispenser, IParticipationConsumerAuthor, IParticipationServiceRequester, IParticipationInformationProviderHealthcareProvider,
        IParticipationInformationProviderNonHealthcareProvider, IParticipationAuthorPerson, IParticipationAuthorHealthcareProvider, IParticipationHealthcareFacility,
        IParticipationAuthoriser, IParticipationReportingPathologist, IParticipationUploadAuthoriser, IParticipationPathologyServiceRequester, IParticipationAuthorisee, 
        IParticipationSubject, IParticipationReportingRadiologist, IParticipationRequester, IParticipationPersonOrOrganisation
    {
        #region Properties

        [DataMember]
        public ICodableText Role { get; set; }

        ParticipationPeriod IParticipationDocumentAuthor.AuthorParticipationPeriodOrDateTimeAuthored { get; set; }

        [DataMember]
        public RoleClassCodeAssociative? RoleClassCodeCode { get; set; }
 
        [DataMember]
        public CdaInterval ParticipationPeriod { get; set; }

        [DataMember]
        public ISO8601DateTime Time { get; set; }

        [DataMember]
        public ISO8601DateTime AuthorParticipationPeriodOrDateTimeAuthored { get; set; }

        [CanBeNull]
        [DataMember]
        public ParticipationPeriod AuthorParticipationPeriod { get; set; }

        [DataMember]
        public ISO8601DateTime ParticipationEndTime { get; set; }

        [DataMember]
        IUploadAuthoriser IParticipationUploadAuthoriser.Participant { get; set; }

        [DataMember]
        IAuthorisee IParticipationAuthorisee.Participant { get; set; }

        [DataMember]
        ISubject IParticipationSubject.Participant { get; set; }

        [DataMember]
        public RoleType RoleType { get; set; }

        [DataMember]
        ISubjectOfCare IParticipationSubjectOfCare.Participant { get; set; }

        [DataMember]
        IAuthoriser IParticipationAuthoriser.Participant { get; set; }

        [DataMember]
        IReportingPathologist IParticipationReportingPathologist.Participant { get; set; }

        [DataMember]
        IAuthorWithRelationship IParticipationConsumerAuthor.Participant { get; set; }

        [DataMember]
        IPrescriber IParticipationPrescriber.Participant { get; set; }

        [DataMember]
        IDispenser IParticipationDispenser.Participant { get; set; }

        [DataMember]
        IDispenserOrganisation IParticipationDispenserOrganisation.Participant { get; set; }

        [DataMember]
        IHealthcareFacility IParticipationHealthcareFacility.Participant { get; set; }

        [DataMember]
        IPrescriberOrganisation IParticipationPrescriberOrganisation.Participant { get; set; }

        [DataMember]
        IPrescriberInstructionRecipient IParticipationPrescriberInstructionRecipient.Participant { get; set; }

        [DataMember]
        IAuthor IParticipationDocumentAuthor.Participant { get; set; }

        [DataMember]
        IReferee IParticipationReferee.Participant { get; set; }

        [DataMember]
        IReferrer IParticipationReferrer.Participant { get; set; }

        [DataMember]
        IUsualGP IParticipationUsualGP.Participant { get; set; }

        [DataMember]
        IAddressee IParticipationAddressee.Participant { get; set; }

        [DataMember]
        IReportingRadiologist IParticipationReportingRadiologist.Participant { get; set; }

        [DataMember]
        IRequester IParticipationRequester.Participant { get; set; }

        [DataMember]
        IServiceProvider IParticipationServiceProvider.Participant { get; set; }

        [DataMember]
        IParticipant IParticipation.Participant { get; set; }

        [DataMember]
        ILegalAuthenticator IParticipationLegalAuthenticator.Participant { get; set; }

        [DataMember]
        IInformationRecipient IParticipationInformationRecipient.Participant { get; set; }

        [DataMember]
        ICustodian IParticipationCustodian.Participant { get; set; }

        [DataMember]
        IInformationProvider IParticipationInformationProvider.Participant { get; set; }

        [DataMember]
        IPatientNominatedContacts IParticipationPatientNominatedContact.Participant { get; set;  }

        [DataMember]
        IAcdCustodian IParticipationAcdCustodian.Participant { get; set; }

        [DataMember]
        IServiceRequester IParticipationServiceRequester.Participant { get; set; }

        [DataMember]
        IPathologyServiceRequester IParticipationPathologyServiceRequester.Participant { get; set; }

        [DataMember]
        IAuthorPerson IParticipationAuthorPerson.Participant { get; set; }

        [DataMember]
        IAuthorHealthcareProvider IParticipationAuthorHealthcareProvider.Participant { get; set; }

        [DataMember]
        public IInformationProviderHealthcareProvider Participant { get; set; }

        [DataMember]
        IPersonOrOrganisation IParticipationPersonOrOrganisation.Participant { get; set; }

        #endregion

        #region Constructors
        internal Participation()
        {
        }
        #endregion

        #region Validation

        void IParticipationDocumentAuthor.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationDocumentAuthor)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationDocumentAuthor.ValidateV2(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var participation = ((IParticipationDocumentAuthor)this);
            var participant = participation.Participant;

            if (vb.ArgumentRequiredCheck("AuthorParticipationPeriodOrDateTimeAuthored", participation.AuthorParticipationPeriodOrDateTimeAuthored))
            {
                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    participation.AuthorParticipationPeriodOrDateTimeAuthored.Validate(vb.Path + "ParticipationPeriod", vb.Messages);

                    if (participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval != null)
                    {
                        if (participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval.Type != IntervalType.LowHigh)
                        {
                            vb.AddValidationMessage(vb.PathName + "AuthorParticipationPeriodOrDateTimeAuthored.Interval", null, "A high element AND a low element is only permitted for Interval");
                        }
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null)
                {
                    participant.Validate(vb.Path + "Participant", vb.Messages);

                    if (participant.Person != null)
                    {
                        vb.ArgumentRequiredCheck("Author.Participant.Person.Organisation", participant.Person.Organisation);
                    }

                    if (participant.Person != null && participant.Person.Organisation != null)
                    {
                        vb.ArgumentRequiredCheck("Addresses", participant.Person.Organisation.Addresses);
                        vb.ArgumentRequiredCheck("ElectronicCommunicationDetails", participant.Person.Organisation.ElectronicCommunicationDetails);
                    }
                }
            }
        }

        void IParticipationDocumentAuthor.ValidateV3(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var participation = (IParticipationDocumentAuthor)this;
            var participant = participation.Participant;

            if (vb.ArgumentRequiredCheck("AuthorParticipationPeriodOrDateTimeAuthored", participation.AuthorParticipationPeriodOrDateTimeAuthored))
            {
                if (participation.AuthorParticipationPeriodOrDateTimeAuthored != null)
                {
                    participation.AuthorParticipationPeriodOrDateTimeAuthored.Validate(vb.Path + "ParticipationPeriod", vb.Messages);

                    if (participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval != null)
                    {
                        if (participation.AuthorParticipationPeriodOrDateTimeAuthored.Interval.Type != IntervalType.LowHigh)
                        {
                            vb.AddValidationMessage(vb.PathName + "AuthorParticipationPeriodOrDateTimeAuthored.Interval", null, "A high element AND a low element is only permitted for Interval");
                        }
                    }
                }
            }

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null)
                {
                    participant.Validate(vb.Path + "Participant", vb.Messages);

                    if (participant.Person != null)
                    {
                        vb.ArgumentRequiredCheck("Author.Participant.Person.Organisation", participant.Person.Organisation);
                    }
                }
            }
        }

        void IParticipationDocumentAuthor.ValidateOptional(string path, bool nullableRole , List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role) && Role != null)
            {
                if (nullableRole)
                {
                    Role.Validate(vb.Path + "Role", vb.Messages);
                } 
                else
                {
                    Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
                }
            }

            // Get participant in this context
            var participant = ((IParticipationDocumentAuthor)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationSubjectOfCare.ValidateV2(string path, List<ValidationMessage> messages)
        {
            var participationSubjectOfCare = this as IParticipationSubjectOfCare;

            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationSubjectOfCare)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null)
                {
                    participant.Validate(vb.Path + "Participant", vb.Messages);
                }
            }

            if (participationSubjectOfCare.Participant != null && participationSubjectOfCare.Participant.Person != null)
            {
                vb.ArgumentRequiredCheck("SubjectOfCare.Participant.Person.IndigenousStatus", participationSubjectOfCare.Participant.Person.IndigenousStatus); 
            }

            participationSubjectOfCare.Validate(path, messages);
        }

        void IParticipationSubjectOfCare.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationSubjectOfCare)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null)
                {
                    CheckSubjectOfCareMedicareIdentifiers(participant, path, messages);

                    vb.ArgumentRequiredCheck("Participant.Addresses", participant.Addresses);
                    participant.Validate(vb.Path + "Participant", vb.Messages);

                    // Move the Gender and Date of Birth validation outside of the person object to cater for newer IG's where these elements are optional
                    var person = participant.Person;

                    if (person != null)
                    {
                        vb.ArgumentRequiredCheck("Gender", person.Gender);
                        vb.ArgumentRequiredCheck("DateOfBirth", person.DateOfBirth);

                        // HIPS: Removed as we do not require all of the documents to supply IndigenousStatus
                        // vb.ArgumentRequiredCheck("IndigenousStatus", person.IndigenousStatus);
                    }
                }
            }
        }

        void IParticipationSubjectOfCare.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participant = ((IParticipationSubjectOfCare)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null)
            {
                CheckSubjectOfCareMedicareIdentifiers(participant, path, messages);
                participant.Validate(vb.Path + "Participant", vb.Messages);
            }

            vb.RangeCheck("Must be only one participant.Addresses", participant.Addresses, 1, 1);

            if (vb.ArgumentRequiredCheck("participant.Person", participant.Person))
            {
              var person = participant.Person;

              if (person != null)
              {
                vb.ArgumentRequiredCheck("Gender", person.Gender);
                vb.ArgumentRequiredCheck("DateOfBirth", person.DateOfBirth);

                if (person.PersonNames != null)
                  vb.RangeCheck("Must be only one participant.Person.PersonName", participant.Person.PersonNames, 1, 1);

                if (person.DateOfDeath != null)
                  vb.AddValidationMessage(vb.PathName, null, "A Date Of Death can not be specified for EPrescription subjectOfCare");

                if (person.CountryOfBirth != Country.Undefined)
                  vb.AddValidationMessage(vb.PathName, null, "A Country Of Birth can not be specified for EPrescription subjectOfCare");

                if (person.StateOfBirth != AustralianState.Undefined)
                  vb.AddValidationMessage(vb.PathName, null, "A State Of Birth can not be specified for EPrescription subjectOfCare");
              }
            }
          }
        }

        void IParticipationSubjectOfCare.ValidateOptional(string path, bool optionalAddress, bool optionalGender, bool optionalDateOfBirth, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participant = ((IParticipationSubjectOfCare)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null)
            {
              CheckSubjectOfCareMedicareIdentifiers(participant, path, messages);

                if (!optionalAddress)
                  vb.ArgumentRequiredCheck("Participant.Addresses", participant.Addresses);

              participant.Validate(vb.Path + "Participant", vb.Messages);
            }

            if (participant != null && participant.Person != null)
            {
              // Move the Gender and Date Of Birth validation outside of the person object to cater for newer IG's where these elements are optional
              var person = (IPersonSubjectOfCare)participant.Person;

                if (!optionalGender)
                vb.ArgumentRequiredCheck("Gender", person.Gender);

                if (!optionalDateOfBirth)
                vb.ArgumentRequiredCheck("DateOfBirth", person.DateOfBirth);
            }
          }
        }

        void IParticipationUsualGP.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Modified 28/1/2016 to allow NullFlavour when Multiple Authors for Pathology and Diagnostic Imaging Uploads
            vb.ArgumentRequiredCheck("Role", Role);
            //if (vb.ArgumentRequiredCheck("Role", Role))
            //{
            //  if (Role != null) Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            //}

            // Get participant in this context
            var participant = ((IParticipationUsualGP)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationReferee.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }
            
            // Get participant in this context
            var participant = ((IParticipationReferee)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationReferrer.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationReferrer)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationPrescriber.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("Time eg.. or otherwies know as Date Time Prescription Written", Time);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }
  
            // Get participant in this context
            var participant = ((IParticipationPrescriber)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null)
                {
                  participant.Validate(vb.Path + "Participant", vb.Messages);

                  if (participant.Person != null)
                  {
                     vb.ArgumentRequiredCheck("participant.Person.Occupation", participant.Person.Occupation);
                  }
                }
            }
        }

        void IParticipationDispenser.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          vb.ArgumentRequiredCheck("Time", Time); 

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null)
            {
              Role.Validate(vb.Path + "Role", vb.Messages);

              if (!(Role.Code == "2515" && Role.CodeSystemCode == "2.16.840.1.113883.13.62" && Role.DisplayName == "Pharmacists"))
                vb.AddValidationMessage(vb.Path + "Role", null, "The Dispenser Role should have a code '2515' and a CodeSystemCode = '2.16.840.1.113883.13.62' and a DisplayName = 'Pharmacists'");
            }
          }

          // Get participant in this context
          var participant = ((IParticipationDispenser)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
            if (participant != null) participant.ValidateATS(vb.Path + "Participant", vb.Messages);
        }

        void IParticipationPrescriber.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          vb.ArgumentRequiredCheck("Time", Time);

          // Get participant in this context
          var participant = ((IParticipationPrescriber)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null)
            {
              participant.Validate(vb.Path + "Participant", vb.Messages);

              if (participant.Addresses != null)
              {
                 vb.RangeCheck("IParticipationPrescriber.Addresses", participant.Addresses, 0, 1);
              }

              if (vb.ArgumentRequiredCheck("IParticipationPrescriber.Person", participant.Person))
              {
                  vb.RangeCheck("ParticipationPrescriber.Person.Identifiers", participant.Person.Identifiers, 1, 1);
                  vb.RangeCheck("ParticipationPrescriber.Person.PersonNames", participant.Person.PersonNames, 1, 1);
              }
            }
          }
        }

        void IParticipationPrescriberOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationPrescriberOrganisation)this).Participant;

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
              if (participant != null)
              {
                participant.Validate(vb.Path + "Participant", vb.Messages);
              }
            }
        }

        void IParticipationPrescriberOrganisation.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participant = ((IParticipationPrescriberOrganisation)this).Participant;

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null)
            {
              participant.Validate(vb.Path + "Participant", vb.Messages);

              if (participant.Organisation != null)
              {
                 vb.RangeCheck("ParticipationPrescriberOrganisation.Organisation.Identifiers", participant.Organisation.Identifiers, 1, 1);
              }

              if (participant.Addresses != null)
              {
                 vb.RangeCheck("ParticipationPrescriberOrganisation.Addresses", participant.Addresses, 1, 1);

                 if (participant.Addresses != null)
                 {
                   foreach (var address in participant.Addresses.Where(address => address.AddressPurpose != AddressPurpose.Business))
                   {
                      vb.AddValidationMessage(path + "Prescriber Organisation", null, "Address - Must have an Address Use of 'Bussiness'");
                   }
                 }
              }

              if (participant.ElectronicCommunicationDetails != null)
              {
                 vb.ArgumentRequiredCheck("ParticipationPrescriberOrganisation.ElectronicCommunicationDetails", participant.ElectronicCommunicationDetails);
              }
            }
          }
        }

        void IParticipationPatientNominatedContact.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationPatientNominatedContact)this).Participant;

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationAcdCustodian.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationAcdCustodian)this).Participant;

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationServiceProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationServiceProvider)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationPathologyServiceRequester.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          // Get participant in this context
          var participant = ((IParticipationPathologyServiceRequester)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationReportingPathologist.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          vb.ArgumentRequiredCheck("ParticipationEndTime", ParticipationEndTime);

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          // Get participant in this context
          var participant = ((IParticipationReportingPathologist)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationReportingRadiologist.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("ParticipationEndTime", ParticipationEndTime);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationReportingRadiologist)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationUploadAuthoriser.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          // Get participant in this context
          var participant = ((IParticipationUploadAuthoriser)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationDispenser.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationDispenser)this).Participant;

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
              if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            vb.ArgumentRequiredCheck("Time", Time);

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationDispenserOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationDispenserOrganisation)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationDispenserOrganisation.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participant = ((IParticipationDispenserOrganisation)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null) participant.ValidateATS(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationPrescriberInstructionRecipient.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationPrescriberInstructionRecipient)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationLegalAuthenticator.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            var participant = ((IParticipationLegalAuthenticator)this).Participant;

            if (Role != null)
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationLegalAuthenticator.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);
          var participant = ((IParticipationLegalAuthenticator)this).Participant;

          if (Role != null)
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          // Get participant in this context
          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null)
            {
              participant.Validate(vb.Path + "Participant", vb.Messages);

              if (vb.ArgumentRequiredCheck("IParticipationLegalAuthenticator.Person", participant.Person))
              {
                if (participant.Person != null)
                if (vb.ArgumentRequiredCheck("participant.Person.Identifiers", participant.Person.Identifiers))
                 vb.RangeCheck("IParticipationLegalAuthenticator.Person.Identifiers", participant.Person.Identifiers, 1, 1);

                if (participant.Person != null)
                 vb.ArgumentRequiredCheck("IParticipationLegalAuthenticator.Person.PersonNames", participant.Person.PersonNames);
              }

              if (vb.ArgumentRequiredCheck("IParticipationLegalAuthenticator.Organisation", participant.Organisation))
              {
                if (participant.Organisation != null)
                vb.ArgumentRequiredCheck("IParticipationLegalAuthenticator.Organisation.Identifiers", participant.Organisation.Identifiers);
              }
            }
          }
        }

        void IParticipationInformationRecipient.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationInformationRecipient)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationCustodian.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participant = ((IParticipationCustodian)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null)
            {
              participant.Validate(vb.Path + "Participant", vb.Messages);

              if (vb.ArgumentRequiredCheck("IParticipationCustodian.Organisation", participant.Organisation))
                if (vb.ArgumentRequiredCheck("IParticipationCustodian.Organisation.Identifiers", participant.Organisation.Identifiers))
                vb.RangeCheck("IParticipationCustodian.Organisation.Identifiers", participant.Organisation.Identifiers, 1, 1);
            }
          }
        }

        void IParticipationCustodian.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationCustodian)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationConsumerAuthor.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationConsumerAuthor)this).Participant;

            vb.ArgumentRequiredCheck("AuthorParticipationPeriodOrDateTimeAuthored", AuthorParticipationPeriodOrDateTimeAuthored);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationInformationProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participation = ((IParticipationInformationProvider)this);

            if (participation.ParticipationPeriod != null) 
            {
               if (participation.ParticipationPeriod.Type != IntervalType.Low)
               {
                 vb.AddValidationMessage(vb.Path + ".ParticipationPeriod", null, "ParticipationPeriod must be instanticated using CdaInterval.CreateLow");
               }
            }

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Participant", participation.Participant))
            {
                participation.Participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationInformationProviderHealthcareProvider.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participation = ((IParticipationInformationProviderHealthcareProvider)this);

          if (participation.ParticipationPeriod != null)
          {
            if (participation.ParticipationPeriod.Type != IntervalType.Low)
            {
              vb.AddValidationMessage(vb.Path + ".ParticipationPeriod", null, "ParticipationPeriod must be instanticated using CdaInterval.CreateLow");
            }
          }

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("Participant", participation.Participant))
          {
            if (participation.Participant != null) participation.Participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationInformationProviderNonHealthcareProvider.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Get participant in this context
          var participation = ((IParticipationInformationProviderNonHealthcareProvider)this);

          if (participation.ParticipationPeriod != null)
          {
            if (participation.ParticipationPeriod.Type != IntervalType.Low)
            {
              vb.AddValidationMessage(vb.Path + ".ParticipationPeriod", null, "ParticipationPeriod must be instanticated using CdaInterval.CreateLow");
            }
          }

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            Role.Validate(vb.Path + "Role", vb.Messages);

            if (Role.Code != "AGNT" || Role.CodeSystemName != "HL7RoleClass" || Role.CodeSystemCode != "2.16.840.1.113883.5.110" || Role.DisplayName !=  "agent")
            {
              vb.AddValidationMessage(vb.Path + ".Role", null, "Role must be of type @code='AGNT', @codeSystemName='HL7RoleClass', @codeSystem='2.16.840.1.113883.5.110', @displayName='agent'");
            }
          }

          if (vb.ArgumentRequiredCheck("Participant", participation.Participant))
          {
            if (participation.Participant != null) participation.Participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationServiceRequester.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          vb.ArgumentRequiredCheck("RoleClassCode", RoleClassCodeCode);

          if (ParticipationPeriod != null)
          {
            ParticipationPeriod.Validate(vb.Path + "ParticipationPeriod", vb.Messages);
          }

          // Get participant in this context
          var participant = ((IParticipationServiceRequester)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationAuthorPerson.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Role
            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null)
                {
                    Role.Validate(vb.Path + "Role", vb.Messages);
                }
            }

            var participation = ((IParticipationAuthorPerson)this);
            var participant = participation.Participant;

            vb.ArgumentRequiredCheck("AuthorParticipationPeriodOrDateTimeAuthored", participation.AuthorParticipationPeriodOrDateTimeAuthored);

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null)
                {
                    participant.Validate(vb.Path + "Participant", vb.Messages);
                }
            }
        }

        void IParticipationAuthorHealthcareProvider.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

            // Modified 20/06/2016: Allows NullFlavour when Multiple Authors for Pathology and Diagnostic Imaging Uploads
            vb.ArgumentRequiredCheck("Role", Role);
            //if (vb.ArgumentRequiredCheck("Role", Role))
            //{
            //  if (Role != null) Role.ValidateMandatory(vb.Path + "Role", vb.Messages);
            //}

            var participation = ((IParticipationAuthorHealthcareProvider)this);
          var participant = participation.Participant;

          //  IPCDOCS-71 - Relax Electronic Communication Details for IParticipationAuthorHealthcareProvider
          //  if (participant != null)
          //  {
          //     // Note : Validate if document != BirthDetails
          //     if (!(new StackTrace()).GetFrames()
          //         .Any(t => t.GetMethod().Name.Contains(CDADocumentType.BirthDetails.ToString())))
          //         {
          //            vb.ArgumentRequiredCheck("Participation.ElectronicCommunicationDetails", participant.ElectronicCommunicationDetails); 
          //         }
          //   }

            vb.ArgumentRequiredCheck("AuthorParticipationPeriodOrDateTimeAuthored", participation.AuthorParticipationPeriodOrDateTimeAuthored);

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
              if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationHealthcareFacility.Validate(string path, bool mandatoryRole, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (mandatoryRole)
          if (vb.ArgumentRequiredCheck("Role", Role))
          {
            if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
          }

          var participation = ((IParticipationHealthcareFacility)this);

          vb.ArgumentRequiredCheck("ParticipationPeriod", participation.ParticipationPeriod);

          // Get participant in this context
          var participant = ((IParticipationHealthcareFacility)this).Participant;

          if (vb.ArgumentRequiredCheck("Participant", participant))
          {
            if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
          }
        }

        void IParticipationAddressee.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Get participant in this context
            var participant = ((IParticipationAddressee)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }
        }

        void IParticipationRequester.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("ParticipationEndTime", ParticipationEndTime);

            // Get participant in this context
            var participant = ((IParticipationRequester)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }
        }

        #region Service Refferal

        /// <summary>
        /// Note : Only basic validation performed universally for this Participant
        ///        Is is up to the user to construct a valid Participant.
        /// </summary>
        void IParticipationPersonOrOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var personOrOrganisation = ((IParticipationPersonOrOrganisation)this).Participant;
            var person = personOrOrganisation.Person;
            var addresses = personOrOrganisation.Addresses;
            var electronicCommunicationDetails = personOrOrganisation.ElectronicCommunicationDetails;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / Organisation structure
            if ((person == null && personOrOrganisation.Organisation == null) || (person != null && personOrOrganisation.Organisation != null))
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
                }

                // An Organisation
                if (personOrOrganisation.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", personOrOrganisation.Organisation))
                    {
                        personOrOrganisation.Organisation.Validate("Organisation", vb.Messages);

                        if (!personOrOrganisation.Qualifications.IsNullOrEmptyWhitespace())
                        {
                            vb.AddValidationMessage(vb.PathName, null, "A qualification can not be provided with an Organisation");
                        }
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

            if (electronicCommunicationDetails != null)
                for (var x = 0; x < electronicCommunicationDetails.Count; x++)
                {
                    if (electronicCommunicationDetails[x] != null)
                    {
                        electronicCommunicationDetails[x].Validate(
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                    }
                }
        }


        #endregion

        #region  Authority to Post

        void IParticipationAuthoriser.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("ParticipationEndTime", ParticipationEndTime);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationAuthoriser)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationAuthorisee.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationAuthorisee)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        void IParticipationSubject.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Role", Role))
            {
                if (Role != null) Role.Validate(vb.Path + "Role", vb.Messages);
            }

            // Get participant in this context
            var participant = ((IParticipationSubject)this).Participant;

            if (vb.ArgumentRequiredCheck("Participant", participant))
            {
                if (participant != null) participant.Validate(vb.Path + "Participant", vb.Messages);
            }
        }

        #endregion

        #endregion

        #region Helper Functions

        private void CheckSubjectOfCareMedicareIdentifiers(ISubjectOfCare subjectOfCare, string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

//            if (subjectOfCare.Person != null && subjectOfCare.Person.Identifiers != null)
//            {
//                var medicareNumberRootValue = MedicareNumberType.MedicareCardNumber.GetAttributeValue<NameAttribute, string>(x => x.Code);
//                foreach (var identifiers in subjectOfCare.Person.Identifiers.Where(identifiers => identifiers.Root == medicareNumberRootValue))
//                {
//                    vb.AddValidationMessage("Subject Of Care - Medicare Card Number", identifiers.Extension, "A Subject Of Care 'Medicare Card Number' Entity Identifier element must be an 11 digit 'Individual Medicare Card Number'");
//                }
//            }
        }
        #endregion

    }
}
