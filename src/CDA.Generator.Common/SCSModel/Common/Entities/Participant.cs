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
using CDA.Generator.Common.SCSModel.Interfaces;
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;


namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Person))]
    [KnownType(typeof(Organisation))]
    [KnownType(typeof(Participant))]
    [KnownType(typeof(Address))]
    [KnownType(typeof(AustralianAddress))]
    [KnownType(typeof(InternationalAddress))]
    [KnownType(typeof(DischargeSummary.Participant))]                                                                                                   
    internal class Participant : IParticipant, IPatientNominatedContacts, IAcdCustodian, IInformationRecipient, ISubjectOfCare, 
        ILegalAuthenticator, IUsualGP, ICustodian, IReferee, IReferrer, IAddressee, IServiceProvider, 
        IPrescriberInstructionRecipient, IDispenser, IPrescriber, IPrescriberOrganisation, IDispenserOrganisation,
        IAuthorWithRelationship, IServiceRequester,  IInformationProviderHealthcareProvider, IAuthorPerson,
        IAuthorHealthcareProvider, IHealthcareFacility, IReportingPathologist, IUploadAuthoriser, IPathologyServiceRequester,
        IAuthoriser, IAuthorisee, ISubject, IReportingRadiologist, IRequester, IPersonOrOrganisation
    {
        [CanBeNull]
        [DataMember]
        private Guid? _UniqueIdentifier;
        /// <summary>
        /// This identifier is used to associated participants through the document.
        /// </summary>
        public Guid UniqueIdentifier
        {
            get
            {
                if (_UniqueIdentifier == null)
                {
                    _UniqueIdentifier = Guid.NewGuid();
                }

                return _UniqueIdentifier.Value;
            }
            set 
            { 
                 _UniqueIdentifier = value;
            }
        }

        [CanBeNull]
        [DataMember]
        public DateTime? DispenseEventDateTime { get; set; }

        [DataMember]
        public List<IAddress> Addresses { get; set; }

        [DataMember]
        List<IAddressAustralian> IAcdCustodian.Addresses { get; set; }

        [DataMember]
        public IAddress Address { get; set; }

        [DataMember]
        public List<ElectronicCommunicationDetail> ElectronicCommunicationDetails { get; set; }

        [DataMember]
        public ElectronicCommunicationDetail ElectronicCommunicationDetail { get; set; }

        [DataMember]
        public List<Entitlement> Entitlements { get; set; }

        public ParticipationPeriod DateTimeAuthenticatedParticipationPeriod { get; set; }

        [DataMember]
        public IPersonWithOrganisation Person { get; set; }

        [DataMember]
        IPersonHealthcareProvider IServiceProvider.Person { get; set; }

        [DataMember]
        IEmploymentOrganisation IUploadAuthoriser.Organisation { get; set; }

        [DataMember]
        IPersonSubjectOfCare ISubjectOfCare.Person { get; set; }

        [DataMember]
        IPerson IAuthorWithRelationship.Person { get; set; }

        [DataMember]
        IPersonPrescriber IPrescriber.Person { get; set; }

        [DataMember]
        IPersonDispenser IDispenser.Person { get; set; }

        [DataMember]
        IPersonWithRelationship IPatientNominatedContacts.Person { get; set; }

        [DataMember]
        IPersonConsumer IAcdCustodian.Person { get; set; }

        [DataMember]
        IPerson IPathologyServiceRequester.Person { get; set; }

        [DataMember]
        IPersonWithOrganisation IAuthoriser.Person { get; set; }

        [DataMember]
        IOrganisation IAuthorisee.Organisation { get; set; }

        [DataMember]
        IPerson ISubject.Person { get; set; }

        [DataMember]
        IPersonWithOrganisation IReportingRadiologist.Person { get; set; }

        [DataMember]
        IPersonWithOrganisation IRequester.Person { get; set; }

        [DataMember]
        IPerson IUploadAuthoriser.Person { get; set; }

        [DataMember]
        IPersonWithOrganisation IReportingPathologist.Person { get; set; }

        [DataMember]
        IPersonPrescriberInstructionRecipient IPrescriberInstructionRecipient.Person { get; set; }

        [DataMember]
        IPerson ILegalAuthenticator.Person { get; set; }

        [DataMember]
        IPerson IServiceRequester.Person { get; set; }

        [DataMember]
        IPerson IInformationRecipient.Person { get; set; }

        [DataMember]
        IPerson IInformationProvider.Person { get; set; }

        [DataMember]
        IPerson IAuthorPerson.Person { get; set; }

        [DataMember]
        IPersonHealthcareProvider IAuthorHealthcareProvider.Person { get; set; }

        [DataMember]
        IOrganisation IParticipant.Organisation { get; set; }

        [DataMember]
        IOrganisationName ILegalAuthenticator.Organisation { get; set; }

        [DataMember]
        public ISO8601DateTime DateTimeAuthenticated { get; set; }

        [DataMember]
        public IOrganisation Organisation { get; set; }

         /// <summary>
        /// The organisation name
        /// </summary>
        [DataMember]
        IOrganisationName IInformationRecipient.Organisation { get; set; }

        /// <summary>
        /// The organisation name
        /// </summary>
        [DataMember]
        IOrganisationName ICustodian.Organisation { get; set; }

        /// <summary>
        /// Recipient type, E.g. Primary or Secondary
        /// </summary>
        [DataMember]
        public RecipientType RecipientType { get; set; }

        /// <summary>
        /// Relationship to Subject of Care
        /// </summary>
        [DataMember]
        public ICodableText RelationshipToSubjectOfCare { get; set; }

        [DataMember]
        public string Qualifications { get; set; }

        #region Constructors
        internal Participant()
        {
        }
        #endregion

        #region Validation

        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            if (Addresses != null)
            {
                for (var x = 0; x < Addresses.Count; x++)
                {
                    Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", Person))
            {
                if (Person != null) Person.Validate(vb.Path + "Person", vb.Messages);
            }

            if (RelationshipToSubjectOfCare != null)
            {
                RelationshipToSubjectOfCare.Validate(vb.Path + "RelationshipToSubjectOfCare", vb.Messages);
            }
        }

        void IAuthorPerson.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var castedContent = (IAuthorPerson)this;

            // optional fields as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            if (castedContent.Addresses != null)
            {
              for (var x = 0; x < castedContent.Addresses.Count; x++)
                {
                  castedContent.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            if (castedContent.ElectronicCommunicationDetails != null)
            {
              for (var x = 0; x < castedContent.ElectronicCommunicationDetails.Count; x++)
                {
                  castedContent.ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", castedContent.Person))
            {
              if (castedContent.Person != null)
              {
                castedContent.Person.Validate(vb.Path + "Person", vb.Messages);

                  vb.ArgumentRequiredCheck("Person.PersonNames", castedContent.Person.PersonNames);
              }
            }
        }

        void IAuthorWithRelationship.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var castedContent = (IAuthorWithRelationship)this;

          if (castedContent.Addresses != null)
          {
            for (var x = 0; x < castedContent.Addresses.Count; x++)
            {
              castedContent.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
            }
          }

          if (castedContent.ElectronicCommunicationDetails != null)
          {
            for (var x = 0; x < castedContent.ElectronicCommunicationDetails.Count; x++)
            {
              castedContent.ElectronicCommunicationDetails[x].Validate
                  (
                      vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                  );
            }
          }

          if (vb.ArgumentRequiredCheck("Person", castedContent.Person))
          {
            if (castedContent.Person != null) castedContent.Person.Validate(vb.Path + "Person", vb.Messages);
          }

          if (castedContent.RelationshipToSubjectOfCare != null)
          {
            castedContent.RelationshipToSubjectOfCare.Validate(vb.Path + "RelationshipToSubjectOfCare", vb.Messages);
          }
        }

        void IUsualGP.Validate(string path, List<ValidationMessage> messages)
        {
            var iUsualGpParticipation = (IUsualGP)this;

            var person = iUsualGpParticipation.Person;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / Organisation structure
            if ((person == null && iUsualGpParticipation.Organisation == null) || (person != null && iUsualGpParticipation.Organisation != null))
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
                if (iUsualGpParticipation.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", iUsualGpParticipation.Organisation))
                    {
                        iUsualGpParticipation.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
                    }
               }
            }

            // Optional field as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            vb.ArgumentRequiredCheck(path + "Addresses", iUsualGpParticipation.Addresses);
            if (iUsualGpParticipation.Addresses != null)
            {
                for (var x = 0; x < iUsualGpParticipation.Addresses.Count; x++)
                {
                    iUsualGpParticipation.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            vb.ArgumentRequiredCheck(path + "ElectronicCommunicationDetails", ElectronicCommunicationDetails);
            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }
        }

        void IReferee.Validate(string path, List<ValidationMessage> messages)
        {
            var iRefereeParticipation = (IReferee) this;

            var person = iRefereeParticipation.Person;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / iRefereeParticipation structure
            if ((person == null && iRefereeParticipation.Organisation == null) || (person != null && iRefereeParticipation.Organisation != null))
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

                    if (vb.ArgumentRequiredCheck("Person.Organisation", person.Organisation))
                    {
                        person.Organisation.Validate(vb.Path + person.Organisation, vb.Messages);
                    }
                }

                // An Organisation
                if (iRefereeParticipation.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", iRefereeParticipation.Organisation))
                    {
                        iRefereeParticipation.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
                    }
                }
            }

            if (iRefereeParticipation.Addresses != null)
            {
                for (var x = 0; x < iRefereeParticipation.Addresses.Count; x++)
                {
                    iRefereeParticipation.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }
        }

        void IReferrer.Validate(string path, List<ValidationMessage> messages)
        {
            var iRefereeParticipation = (IReferrer)this;

            var person = iRefereeParticipation.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            if (vb.ArgumentRequiredCheck("Addresses", iRefereeParticipation.Addresses))
            {
                if (iRefereeParticipation.Addresses != null)
                    for (var x = 0; x < iRefereeParticipation.Addresses.Count; x++)
                    {
                        iRefereeParticipation.Addresses[x].Validate
                            (
                                vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                            );
                    }
            }

            vb.ArgumentRequiredCheck(path + "ElectronicCommunicationDetails", ElectronicCommunicationDetails);
            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null) person.Validate(vb.Path + "Person", vb.Messages);
            }
        }

        void IAuthor.Validate(string path, List<ValidationMessage> messages)
        {
            var person = ((IAuthor)this).Person;

            var vb = new ValidationBuilder(path, messages);

            // This is made optional as it is validated at the SCSContext level
            if (Addresses != null)
            {
                for (var x = 0; x < Addresses.Count; x++)
                {
                    if (Addresses[x].InternationalAddress != null || Addresses[x].AustralianAddress == null)
                    {
                        vb.AddValidationMessage(vb.Path + string.Format("Addresses[{0}]", x), null, "Australian address required");
                    }

                    Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            // This is made optional as it is validated at the SCSContext level
            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                }
            }

            // Entitlements
            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate(vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
                }
            }

            // Person organisation is made optional as it is validated at the SCSContext level
            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null)
                {
                    person.Validate(vb.Path + "Person", vb.Messages);

                    if (person.Organisation != null)
                        person.Organisation.Validate(vb.Path + "Person.Organisation", vb.Messages);
                }
            }
        }

        void ISubjectOfCare.Validate(string path, List<ValidationMessage> messages)
        {
            var subjectOfCare = (ISubjectOfCare)this;

            var vb = new ValidationBuilder(path, messages);
           
            if (subjectOfCare.Addresses != null)
            {
                if (subjectOfCare.Addresses != null)
                    for (var x = 0; x < subjectOfCare.Addresses.Count; x++)
                    {
                        subjectOfCare.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                }
            }

            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate(
                        vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("Person", subjectOfCare.Person))
            {
                if (subjectOfCare.Person != null) subjectOfCare.Person.Validate(vb.Path + "Person", vb.Messages);
            }
        }
 
        void IPrescriber.Validate(string path, List<ValidationMessage> messages)
        {
            var iPrescriber = (IPrescriber)this;

            var person = iPrescriber.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            if (iPrescriber.Addresses != null)
            {
                for (var x = 0; x < iPrescriber.Addresses.Count; x++)
                {
                    if (iPrescriber.Addresses[x] != null)
                    {
                      if (iPrescriber.Addresses[x].InternationalAddress != null)
                      {
                        vb.AddValidationMessage(vb.PathName, null, "IPrescriber Address shall be instantiated as an Australian Address"); 
                      }

                      if (iPrescriber.Addresses[x].AddressPurpose != AddressPurpose.Business)
                      {
                        vb.AddValidationMessage(vb.PathName, null, "IPrescriber Address shall have an AddressPurpose of Business");
                      }

                      iPrescriber.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (iPrescriber.ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < iPrescriber.ElectronicCommunicationDetails.Count; x++)
                {
                    iPrescriber.ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null)
                {
                    person.Validate(vb.Path + "Person", vb.Messages);
                }
            }
        }

        void IDispenser.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var iDispenser = (IDispenser)this;

          var person = iDispenser.Person;

          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Addresses", iDispenser.Addresses))
          {
            vb.RangeCheck("Addresses", iDispenser.Addresses, 1, 1);

            for (var x = 0; x < iDispenser.Addresses.Count; x++)
            {
              if (iDispenser.Addresses[x].InternationalAddress != null)
              {
                vb.AddValidationMessage(vb.PathName, null, "iDispenser Address shall be instantiated as an Australian Address");
              }

              if (iDispenser.Addresses[x] != null)
              {
                iDispenser.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
              }
            }
          }

          if (vb.ArgumentRequiredCheck("ElectronicCommunicationDetails", iDispenser.ElectronicCommunicationDetails))
          {
            for (var x = 0; x < iDispenser.ElectronicCommunicationDetails.Count; x++)
            {
              iDispenser.ElectronicCommunicationDetails[x].Validate
                  (
                      vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                  );
            }
          }

          if (vb.ArgumentRequiredCheck("Person", person))
          {
            if (person != null) person.ValidateATS(vb.Path + "Person", vb.Messages);
          }
        }

        void IDispenser.Validate(string path, List<ValidationMessage> messages)
        {
            var iDispenser = (IDispenser)this;

            var person = iDispenser.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            if (iDispenser.Addresses != null)
            {
                for (var x = 0; x < iDispenser.Addresses.Count; x++)
                {

                  if (iDispenser.Addresses[x].InternationalAddress != null)
                  {
                    vb.AddValidationMessage(vb.PathName, null, "iDispenser Address shall be instantiated as an Australian Address");
                  }

                  if (iDispenser.Addresses[x] != null)
                  {
                      iDispenser.Addresses[x].Validate(
                          vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                  }
                }
            }

            if (iDispenser.ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < iDispenser.ElectronicCommunicationDetails.Count; x++)
                {
                    iDispenser.ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null) person.Validate(vb.Path + "Person", vb.Messages);
            }
        }

        void IPrescriberOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var iPrescriberOrganisation = (IPrescriberOrganisation)this;

            var organisation = iPrescriberOrganisation.Organisation;

            var vb = new ValidationBuilder(path, messages);

            if (iPrescriberOrganisation.Addresses != null && iPrescriberOrganisation.Addresses.Any())
            {
                for (var x = 0; x < iPrescriberOrganisation.Addresses.Count; x++)
                {
                    if (iPrescriberOrganisation.Addresses[x] != null)
                    {
                      if (iPrescriberOrganisation.Addresses[x].InternationalAddress != null)
                        {
                           vb.AddValidationMessage(vb.PathName, null, "iPrescriberOrganisation Address shall be instantiated as an Australian Address");
                        }

                      if (iPrescriberOrganisation.Addresses[x].AddressPurpose != AddressPurpose.Business)
                      {
                        vb.AddValidationMessage(vb.PathName, null, "iPrescriberOrganisation Address shall be instantiated with an Address Purpose of Business");
                      }

                        iPrescriberOrganisation.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (iPrescriberOrganisation.ElectronicCommunicationDetails != null && iPrescriberOrganisation.ElectronicCommunicationDetails.Any())
            {
                if (iPrescriberOrganisation.ElectronicCommunicationDetails != null)
                    for (var x = 0; x < iPrescriberOrganisation.ElectronicCommunicationDetails.Count; x++)
                    {
                        iPrescriberOrganisation.ElectronicCommunicationDetails[x].Validate
                            (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                            );
                    }
            }

            if (vb.ArgumentRequiredCheck("Organisation", organisation))
            {
                if (organisation != null) organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }

            if (Entitlements != null)
            {
              for (var x = 0; x < Entitlements.Count; x++)
              {
                Entitlements[x].Validate(vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
              }
            }
        }

        void IDispenserOrganisation.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          var iDispenserOrganisation = (IDispenserOrganisation)this;

          var organisation = iDispenserOrganisation.Organisation;

          if (vb.ArgumentRequiredCheck("Addresses", iDispenserOrganisation.Addresses))
          {
            vb.RangeCheck("Addresses", iDispenserOrganisation.Addresses, 1, 1);

            for (var x = 0; x < iDispenserOrganisation.Addresses.Count; x++)
            {
              if (iDispenserOrganisation.Addresses[x].InternationalAddress != null)
              {
                vb.AddValidationMessage(vb.PathName, null, "Address shall be instantiated as an Australian Address");
              }

              if (iDispenserOrganisation.Addresses[x].AddressPurpose != AddressPurpose.Business)
              {
                vb.AddValidationMessage(vb.PathName, null, "Address shall be instantiated with an Address Purpose of Business");
              }

              if (iDispenserOrganisation.Addresses[x] != null)
              {
                iDispenserOrganisation.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
              }
            }
          }

          if (vb.ArgumentRequiredCheck("ElectronicCommunicationDetails", iDispenserOrganisation.ElectronicCommunicationDetails))
          {
            if (iDispenserOrganisation.ElectronicCommunicationDetails != null)
              for (var x = 0; x < iDispenserOrganisation.ElectronicCommunicationDetails.Count; x++)
              {
                iDispenserOrganisation.ElectronicCommunicationDetails[x].Validate
                    (
                    vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                    );
              }
          }

          if (Entitlements != null)
          {
            for (var x = 0; x < Entitlements.Count; x++)
            {
              Entitlements[x].Validate(vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("Organisation", organisation))
          {
            if (organisation != null)
            {
               organisation.Validate(vb.Path + "Organisation", vb.Messages);

               vb.ArgumentRequiredCheck("Organisation.Identifiers", organisation.Identifiers);
            }
          }
        }

       void IDispenserOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var iDispenserOrganisation = (IDispenserOrganisation)this;

            var organisation = iDispenserOrganisation.Organisation;

            var vb = new ValidationBuilder(path, messages);

            if (iDispenserOrganisation.Addresses != null)
            {
                for (var x = 0; x < iDispenserOrganisation.Addresses.Count; x++)
                {
                  if (iDispenserOrganisation.Addresses[x].InternationalAddress != null)
                  {
                     vb.AddValidationMessage(vb.PathName, null, "Address shall be instantiated as an Australian Address");
                  }

                  if (iDispenserOrganisation.Addresses[x].AddressPurpose != AddressPurpose.Business)
                  {
                    vb.AddValidationMessage(vb.PathName, null, "Address shall be instantiated with an Address Purpose of Business");
                  }

                  if (iDispenserOrganisation.Addresses[x] != null)
                  {
                      iDispenserOrganisation.Addresses[x].Validate(
                          vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                  }
                }
            }

            if (iDispenserOrganisation.ElectronicCommunicationDetails != null)
            {
                if (iDispenserOrganisation.ElectronicCommunicationDetails != null)
                    for (var x = 0; x < iDispenserOrganisation.ElectronicCommunicationDetails.Count; x++)
                    {
                        iDispenserOrganisation.ElectronicCommunicationDetails[x].Validate
                            (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                            );
                    }
            }

            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate(vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("Organisation", organisation))
            {
                if (organisation != null) organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }

        void IPathologyServiceRequester.Validate(string path, List<ValidationMessage> messages)
        {
           var person = ((IPathologyServiceRequester)this).Person;
           var addresses = ((IPathologyServiceRequester)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPII
            // vb.ArgumentRequiredCheck(path + "HPII", HPII);

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                  person.Validate(vb.Path + "Person", vb.Messages);
            }

            // Addresses
            if (addresses != null)
            {
                for (var x = 0; x < addresses.Count; x++)
                {
                    if (addresses[x] != null)
                    {
                        addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate
                    (
                            vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages
                    );
                }
            }

            // Electronic Communication Details
            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    if (ElectronicCommunicationDetails[x] != null)
                    {
                        ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        void IServiceRequester.Validate(string path, List<ValidationMessage> messages)
        {
          var serviceRequester = (IServiceRequester)this;

          var person = ((IServiceRequester)this).Person;
          var addresses = ((IServiceRequester)this).Addresses;

          var vb = new ValidationBuilder(path, messages);

          // optional fields as organisations may not have a HPII
          // vb.ArgumentRequiredCheck(path + "HPII", HPII);

          if (person != null)
          {
            person.Validate(vb.Path + "Person", vb.Messages);
          }

          // An Organisation
          if (serviceRequester.Organisation != null)
          {
            serviceRequester.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
          }

          // Addresses
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

          // Electronic Communication Details
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

        void IServiceProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var serviceProvider = (IServiceProvider)this;

            var person = ((IServiceProvider)this).Person;
            var addresses = ((IServiceProvider)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / Organisation structure
            if ((person == null && serviceProvider.Organisation == null) || (person != null && serviceProvider.Organisation != null))
            {
                vb.AddValidationMessage(vb.PathName, null, "A Person or a Organisation must be provided");
            }
            else
            {
                // A Person
                if (person != null)
                {
                    // optional fields as organisations may not have a HPII
                    // vb.ArgumentRequiredCheck(path + "HPII", HPII);

                    if (vb.ArgumentRequiredCheck("Person", person))
                    {
                        person.Validate(vb.Path + "Person", vb.Messages);
                    }

                    // ReSharper disable once AssignNullToNotNullAttribute
                    if ((new StackTrace()).GetFrames().Any(t => t.GetMethod().Name.Contains(CDADocumentType.EventSummary.ToString())))
                    {
                        if (serviceProvider.Person != null)
                            vb.ArgumentRequiredCheck("ServiceProvider.Person.Organisation", serviceProvider.Person.Organisation);
                    }

                    vb.ArgumentRequiredCheck("Addresses", addresses);
                }

                // An Organisation
                if (serviceProvider.Organisation != null)
                {
                    serviceProvider.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
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

        void IPrescriberInstructionRecipient.Validate(string path, List<ValidationMessage> messages)
        {
            var iPrescriberInstructionRecipient = (IPrescriberInstructionRecipient)this;

            var person = iPrescriberInstructionRecipient.Person;

            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Addresses", iPrescriberInstructionRecipient.Addresses))
            {
               vb.RangeCheck("Addresses", iPrescriberInstructionRecipient.Addresses, 1, 1);

                for (var x = 0; x < iPrescriberInstructionRecipient.Addresses.Count; x++)
                {
                    if (iPrescriberInstructionRecipient.Addresses[x] != null)
                    {
                        iPrescriberInstructionRecipient.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (iPrescriberInstructionRecipient.ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < iPrescriberInstructionRecipient.ElectronicCommunicationDetails.Count; x++)
                {
                    iPrescriberInstructionRecipient.ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                if (person != null) person.Validate(vb.Path + "Person", vb.Messages);
            }
        }

        void ILegalAuthenticator.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var iLegalAuthenticator = (ILegalAuthenticator)this;

            var person = iLegalAuthenticator.Person;

            if (vb.ArgumentRequiredCheck("DateTimeAuthored", DateTimeAuthenticated))
            {
                if (DateTimeAuthenticated.PrecisionIndicator == ISO8601DateTime.Precision.Day ||
                    DateTimeAuthenticated.PrecisionIndicator == ISO8601DateTime.Precision.Month ||
                    DateTimeAuthenticated.PrecisionIndicator == ISO8601DateTime.Precision.Year)
                {
                    vb.AddValidationMessage("DateTimeAuthenticated", null, "The time@value SHALL include both a time and a date.");
                }
            }

             if (person != null)
             {
                 person.Validate(vb.Path + "Person", vb.Messages);
             }

            if (iLegalAuthenticator.Addresses != null)
            {
                for (var x = 0; x < iLegalAuthenticator.Addresses.Count; x++)
                {
                    iLegalAuthenticator.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            if (iLegalAuthenticator.ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < iLegalAuthenticator.ElectronicCommunicationDetails.Count; x++)
                {
                    iLegalAuthenticator.ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                }
            }

            if (iLegalAuthenticator.Organisation != null)
            {
                 iLegalAuthenticator.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }

        void IPatientNominatedContacts.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var nominatedContacts = (IPatientNominatedContacts)this;
            var person = nominatedContacts.Person;

            // Check for correct Person / Organisation structure
            if ((person == null && nominatedContacts.Organisation == null) || (person != null && nominatedContacts.Organisation != null))
            {
                vb.AddValidationMessage(vb.PathName, null, "A Person or a Organisation must be provided");
            }
            else
            {
                // A Person
                if (person != null)
                {
                    // optional fields as organisations may not have a HPII
                    // vb.ArgumentRequiredCheck(path + "HPII", HPII);

                    if (vb.ArgumentRequiredCheck("Person", person))
                    {
                        person.Validate(vb.Path + "Person", vb.Messages);
                    }

                }

                // An Organisation
                if (nominatedContacts.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", nominatedContacts.Organisation))
                    {
                        nominatedContacts.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
                    }
                }
            }


            if (Addresses != null)
            {
                for (var x = 0; x < Addresses.Count; x++)
                {
                    Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
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
        }

        void IAcdCustodian.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var acdCustodian = (IAcdCustodian)this;
            var person = acdCustodian.Person;

            // Check for correct Person / Organisation structure
            if ((person == null && acdCustodian.Organisation == null) || (person != null && acdCustodian.Organisation != null))
            {
                vb.AddValidationMessage(vb.PathName, null, "A Person or a Organisation must be provided");
            }
            else
            {
                // A Person
                if (person != null)
                {
                    // optional fields as organisations may not have a HPII
                    // vb.ArgumentRequiredCheck(path + "HPII", HPII);

                    if (vb.ArgumentRequiredCheck("Person", person))
                    {
                        person.Validate(vb.Path + "Person", vb.Messages);
                        vb.ArgumentRequiredCheck("Addresses", acdCustodian.Addresses);
                    }

                }

                // An Organisation
                if (acdCustodian.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", acdCustodian.Organisation))
                    {
                        acdCustodian.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
                    }
                }
            }

            if (acdCustodian.Addresses != null)
            {
                for (var x = 0; x < acdCustodian.Addresses.Count; x++)
                {
                    acdCustodian.Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
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
        }

        void ICustodian.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var iCustodian = (ICustodian)this;

            if (ElectronicCommunicationDetail != null)
            {
                ElectronicCommunicationDetail.Validate(vb.Path + "ElectronicCommunicationDetail", vb.Messages);
            }

            if (Address != null)
            {
                Address.Validate(vb.Path + "Address", vb.Messages);
            }

            if (iCustodian.Organisation != null)
            {
                iCustodian.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }

        void IInformationRecipient.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var iInformationRecipient = (IInformationRecipient)this;

            vb.ArgumentRequiredCheck("RecipientType", iInformationRecipient.RecipientType);
            
            if (Addresses != null)
            {
                for (var x = 0; x < Addresses.Count; x++)
                {
                   Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
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

            if (iInformationRecipient.Organisation != null)
            {
                iInformationRecipient.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }

            if (iInformationRecipient.Person != null)
            {
                iInformationRecipient.Person.Validate(vb.Path + "Person", vb.Messages);
            }
        }

        void IInformationProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var nominatedContacts = (IInformationProvider)this;
            var person = nominatedContacts.Person;

            // A Person
            if (person != null)
            {
                // optional fields as organisations may not have a HPII
                // vb.ArgumentRequiredCheck(path + "HPII", HPII);

                if (vb.ArgumentRequiredCheck("Person", person))
                {
                    person.Validate(vb.Path + "Person", vb.Messages);
                    
                    // Check that person names exist 
                    vb.ArgumentRequiredCheck("Person.PersonNames", person.PersonNames);
                }
            }

            if (Addresses != null)
            {
                for (var x = 0; x < Addresses.Count; x++)
                {
                    Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
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
        }

        void IInformationProviderHealthcareProvider.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var nominatedContacts = (IInformationProviderHealthcareProvider)this;
            var person = nominatedContacts.Person;

            // A Person
            if (person != null)
            {
                // optional fields as organisations may not have a HPII
                // vb.ArgumentRequiredCheck(path + "HPII", HPII);

                if (vb.ArgumentRequiredCheck("Person", person))
                {
                    person.Validate(vb.Path + "Person", vb.Messages);
                    
                    // Check that person names exist 
                    vb.ArgumentRequiredCheck("Person.PersonNames", person.PersonNames);

                    // Check that the Identifiers exist 
                    vb.ArgumentRequiredCheck("Person.Identifiers", person.Identifiers);
                }
            }

            if (Addresses != null)
            {
                for (var x = 0; x < Addresses.Count; x++)
                {
                    Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                }
            }
        }

        void IAuthorHealthcareProvider.Validate(string path, List<ValidationMessage> messages)
        {
          var person = ((IAuthorHealthcareProvider)this).Person;

          var vb = new ValidationBuilder(path, messages);

          // This is made optional as it is validated at the SCSContext level
          if (Addresses != null)
          {
            for (var x = 0; x < Addresses.Count; x++)
            {
              if (Addresses[x].InternationalAddress != null || Addresses[x].AustralianAddress == null)
                vb.AddValidationMessage(vb.Path + string.Format("Addresses[{0}]", x), null, "Australian address required.");

              Addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
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

          if (Entitlements != null)
          {
              for (var x = 0; x < Entitlements.Count; x++)
              {
                  Entitlements[x].Validate(
                      vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
              }
          }

          // Person organisation is made optional as it is validated at the SCSContext level
          if (vb.ArgumentRequiredCheck("Person", person))
          {
            if (person != null)
            {
              person.Validate(vb.Path + "Person", vb.Messages);

              if (person.Organisation != null)
              {
                person.Organisation.Validate(vb.Path + "Person.Organisation", vb.Messages);
              }
            }
          }
        }

        void IHealthcareFacility.Validate(string path, List<ValidationMessage> messages)
        {
          var healthcareFacility = (IHealthcareFacility)this;

          var organisation = healthcareFacility.Organisation;

          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Addresses",healthcareFacility.Addresses))
          {
            for (var x = 0; x < healthcareFacility.Addresses.Count; x++)
            {
              if (healthcareFacility.Addresses[x].InternationalAddress != null)
              {
                vb.AddValidationMessage(vb.PathName, null, "Address shall be instantiated as an Australian Address");
              }

              if (healthcareFacility.Addresses[x] != null)
              {
                healthcareFacility.Addresses[x].Validate( vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
              }
            }
          }

          if (healthcareFacility.ElectronicCommunicationDetails != null)
          {
              for (var x = 0; x < healthcareFacility.ElectronicCommunicationDetails.Count; x++)
              {
                healthcareFacility.ElectronicCommunicationDetails[x].Validate
                    (
                    vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                    );
              }
          }

          if (vb.ArgumentRequiredCheck("Organisation", organisation))
          {
            if (organisation != null) organisation.Validate(vb.Path + "Organisation", vb.Messages);
          }
        }

        void IUploadAuthoriser.Validate(string path, List<ValidationMessage> messages)
        {
          var reportingPathologist = (IUploadAuthoriser)this;

          var organisation = reportingPathologist.Organisation;
          var person = reportingPathologist.Person;

          var vb = new ValidationBuilder(path, messages);

          // optional fields as organisations may not have a HPIO
          // vb.ArgumentRequiredCheck(path + "HPIO", HPIO);

          if (vb.ArgumentRequiredCheck("Addresses", reportingPathologist.Addresses))
          {
            if (reportingPathologist.Addresses != null)
              for (var x = 0; x < reportingPathologist.Addresses.Count; x++)
              {
                reportingPathologist.Addresses[x].Validate
                    (
                        vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                    );
              }
          }

          if (vb.ArgumentRequiredCheck("ElectronicCommunicationDetails", ElectronicCommunicationDetails))
          {
            for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
            {
              ElectronicCommunicationDetails[x].Validate
                  (
                      vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                  );
            }
          }

          if (vb.ArgumentRequiredCheck("Person", person))
          {
            person.Validate(vb.Path + "Person", vb.Messages);
          }

          if (organisation != null)
          {
            organisation.Validate(vb.Path + "Organisation", vb.Messages);
          }
        }

        void IAddressee.Validate(string path, List<ValidationMessage> messages)
        {
            var iAddresseeParticipation = (IAddressee)this;
            var person = iAddresseeParticipation.Person;
            var addresses = ((IAddressee)this).Addresses;

            var vb = new ValidationBuilder(path, messages);

            // Check for correct Person / Organisation structure
            if ((person == null && iAddresseeParticipation.Organisation == null) || (person != null && iAddresseeParticipation.Organisation != null))
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
                if (iAddresseeParticipation.Organisation != null)
                {
                    if (vb.ArgumentRequiredCheck("Organisation", iAddresseeParticipation.Organisation))
                    {
                        iAddresseeParticipation.Organisation.Validate(vb.Path + "Organisation", vb.Messages);
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

        void IReportingPathologist.Validate(string path, List<ValidationMessage> messages)
        {
            var reportingPathologist = (IReportingPathologist)this;

            var person = reportingPathologist.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPIO
            // vb.ArgumentRequiredCheck(path + "HPIO", HPIO);

            if (vb.ArgumentRequiredCheck("Addresses", reportingPathologist.Addresses))
            {
                if (reportingPathologist.Addresses != null)
                    for (var x = 0; x < reportingPathologist.Addresses.Count; x++)
                    {
                        reportingPathologist.Addresses[x].Validate
                        (
                            vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                        );
                    }
            }

            if (vb.ArgumentRequiredCheck("ElectronicCommunicationDetails", ElectronicCommunicationDetails))
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                    (
                       vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                    );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                person.Validate(vb.Path + "Person", vb.Messages);

                vb.ArgumentRequiredCheck("Person.PersonNames", person.PersonNames);

                vb.ArgumentRequiredCheck("person.Organisation", person.Organisation);
            }

            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate
                    (
                       vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages
                    );
                }
            }
        }

        void IReportingRadiologist.Validate(string path, List<ValidationMessage> messages)
        {
            var reportingRadiologist = (IReportingRadiologist)this;

            var person = reportingRadiologist.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPIO
            // vb.ArgumentRequiredCheck(path + "HPIO", HPIO);

            if (reportingRadiologist.Addresses != null)
            {
                if (reportingRadiologist.Addresses != null)
                    for (var x = 0; x < reportingRadiologist.Addresses.Count; x++)
                    {
                        reportingRadiologist.Addresses[x].Validate
                        (
                            vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                        );
                    }
            }

            if ( ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                    (
                       vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                    );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                person.Validate(vb.Path + "Person", vb.Messages);

                if (person.PersonNames != null)
                    vb.RangeCheck("person.PersonNames", person.PersonNames, 1, 1);

                vb.ArgumentRequiredCheck("person.Organisation", person.Organisation);

            }

            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate
                    (
                       vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages
                    );
                }
            }
        }

        void IRequester.Validate(string path, List<ValidationMessage> messages)
        {
            var iAddresseeParticipation = (IRequester)this;
            var person = iAddresseeParticipation.Person;
            var addresses = iAddresseeParticipation.Addresses;

            var vb = new ValidationBuilder(path, messages);

            // Person
            if (vb.ArgumentRequiredCheck("Person", person))
            {
                person.Validate(vb.Path + "Person", vb.Messages);

                vb.ArgumentRequiredCheck("Person.Organisation", person.Organisation);
            }

            if (addresses != null)
            {
                for (var x = 0; x < addresses.Count; x++)
                {
                    if (addresses[x] != null)
                    {
                        addresses[x].Validate(vb.Path + string.Format("Addresses[{0}]", x), vb.Messages);
                    }
                }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    if (ElectronicCommunicationDetails[x] != null)
                    {
                        ElectronicCommunicationDetails[x].Validate(vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        #region Authoriser

        void IAuthoriser.Validate(string path, List<ValidationMessage> messages)
        {
            var authoriser = (IAuthoriser)this;

            var person = authoriser.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPIO
            // vb.ArgumentRequiredCheck(path + "HPIO", HPIO);

            // Addresses
            if (authoriser.Addresses != null)
            {
                for (var x = 0; x < authoriser.Addresses.Count; x++)
                {
                    authoriser.Addresses[x].Validate
                        (
                            vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                        );
                }
            }

            // Electronic Communication Details
            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            // Entitlements
            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate(
                        vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
                }
            }

            // Person
            if (vb.ArgumentRequiredCheck("Person", person))
            {
                person.Validate(vb.Path + "Person", vb.Messages);

                // Person Organisation
                if (vb.ArgumentRequiredCheck("Person.Organisation", person.Organisation))
                    person.Organisation.Validate(vb.Path + "Person.Organisation", vb.Messages);
            }
        }

        void IAuthorisee.Validate(string path, List<ValidationMessage> messages)
        {
            var authorisee = (IAuthorisee)this;

            var organisation = authorisee.Organisation;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPIO
            // vb.ArgumentRequiredCheck(path + "HPIO", HPIO);

            if (authorisee.Addresses != null)
            {
                    for (var x = 0; x < authorisee.Addresses.Count; x++)
                    {
                        authorisee.Addresses[x].Validate
                            (
                                vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                            );
                    }
            }

            if (ElectronicCommunicationDetails != null)
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (Entitlements != null)
            {
                for (var x = 0; x < Entitlements.Count; x++)
                {
                    Entitlements[x].Validate(
                        vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("organisation", organisation))
            {
                organisation.Validate(vb.Path + "organisation", vb.Messages);
            }
        }

        void ISubject.Validate(string path, List<ValidationMessage> messages)
        {
            var subject = (ISubject)this;

            var organisation = subject.Organisation;
            var person = subject.Person;

            var vb = new ValidationBuilder(path, messages);

            // optional fields as organisations may not have a HPIO
            // vb.ArgumentRequiredCheck(path + "HPIO", HPIO);

            if (vb.ArgumentRequiredCheck("Addresses", subject.Addresses))
            {
                if (subject.Addresses != null)
                    for (var x = 0; x < subject.Addresses.Count; x++)
                    {
                        subject.Addresses[x].Validate
                            (
                                vb.Path + string.Format("Addresses[{0}]", x), vb.Messages
                            );
                    }
            }

            if (vb.ArgumentRequiredCheck(path + "ElectronicCommunicationDetails", ElectronicCommunicationDetails))
            {
                for (var x = 0; x < ElectronicCommunicationDetails.Count; x++)
                {
                    ElectronicCommunicationDetails[x].Validate
                        (
                            vb.Path + string.Format("ElectronicCommunicationDetails[{0}]", x), vb.Messages
                        );
                }
            }

            if (vb.ArgumentRequiredCheck("Person", person))
            {
                person.Validate(vb.Path + "Person", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Organisation", organisation))
            {
                if (organisation != null) organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }


        void IPersonOrOrganisation.Validate(string path, List<ValidationMessage> messages)
        {

        }
        
        #endregion

        #endregion
    }
}
