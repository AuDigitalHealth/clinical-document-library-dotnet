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
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.CDAModel.Entities;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;

namespace Nehta.VendorLibrary.CDA.CDAModel
{
    
    /// <summary>
    /// This class defines a CDA context, and implements interfaces that further constrain the context into
    /// a specific CDA implementation.
    /// 
    /// The ICDAContextPrescriptionRecord constrains the context so as it only exposes properties that are valid
    /// for an PrescriptionRecord document
    /// </summary>
    [Serializable]
    [DataContract]
  public class CDAContext : ICDAContextPCEHRPrescriptionRecord, ICDAContextPCEHRDispenseRecord, ICDAContextSharedHealthSummary, ICDAContextEReferral, 
        ICDAContextEventSummary, ICDAContextPrescriptionRequestContent, ICDAContextDispenseRecord, ICDAContextAcdCustodianRecord,ICDAContextSpecialistLetter, ICDAContextEPrescription, 
        ICDAContextConsumerEnteredHealthSummary, ICDAContextConsumerEnteredNotes, ICDAContextConsolidatedView, ICDAContextMedicareOverview, ICDAContextPrescriptionAndDispenseView,
        ICDAContextConsumerEnteredAchievements, ICDAContextPhysicalMeasurements, ICDAContextNSWHealthCheckAssessment, ICDAContextPersonalHealthObservation, ICDAContextConsumerQuestionnaire,
        ICDAContextBirthDetailsRecord, ICDAContextChildHealthCheckScheduleView, ICDAContextObservationViewDocument, ICDAContextPathologyResultView, ICDAContextPathologyResultReport, ICDAContextDiagnosticImagingReport,
        ICDAContextAdvanceCareInformation, ICDAContextPathologyReportWithStructuredContent, ICDAContextServiceReferral, ICDAContextPCML
  {
        #region Properties

        /// <summary>
        /// The version for this CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Version { get; set; }

        /// <summary>
        /// PCEHR Prescription Record Instance Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier DocumentId { get; set; }

        /// <summary>
        /// Document Title
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Identifier of Original Prescription (Prescription Identifier)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ParentDocument> ParentDocuments { get; set; }

        /// <summary>
        /// The CDA set Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier SetId { get; set; }

        /// <summary>
        /// The authenticator of the CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IParticipationLegalAuthenticator> Authenticators { get; set; }

        /// <summary>
        /// The legal authenticator of the CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationLegalAuthenticator LegalAuthenticator { get; set; }

        /// <summary>
        /// The custodian for this CDA document
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationCustodian Custodian { get; set; }

        /// <summary>
        /// A list of recipients
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IParticipationInformationRecipient> InformationRecipients { get; set; }
        #endregion
        
        #region Constructors
        internal CDAContext()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this CDA Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void ValidateWithMandatoryLegalAuthenticator(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("LegalAuthenticator", LegalAuthenticator))
            {
                LegalAuthenticator.Validate(vb.Path + "LegalAuthenticator", vb.Messages);
            }

            if (InformationRecipients != null)
            {
                for (var x = 0; x < InformationRecipients.Count; x++)
                {
                    InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("Custodian", Custodian))
            {
                if (Custodian != null) Custodian.Validate(vb.Path + "Custodian", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this CDA Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (LegalAuthenticator != null)  
            {
                LegalAuthenticator.Validate(vb.Path + "LegalAuthenticator", vb.Messages);
            }

            if (InformationRecipients != null)
            {
              for (var x = 0; x < InformationRecipients.Count; x++)
              {
                InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
              }
            }

            if (vb.ArgumentRequiredCheck("Custodian", Custodian))
            {
              if (Custodian != null) Custodian.Validate(vb.Path + "Custodian", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this CDA Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextDispenseRecord.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (LegalAuthenticator != null)
          {
            LegalAuthenticator.ValidateATS(vb.Path + "LegalAuthenticator", vb.Messages);
          }

          if (InformationRecipients != null)
          {
            for (var x = 0; x < InformationRecipients.Count; x++)
            {
              InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("Custodian", Custodian))
          {
            if (Custodian != null) Custodian.ValidateATS(vb.Path + "Custodian", vb.Messages);
          }
        }

        /// <summary>
        /// Validates this CDA Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextEPrescription.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (LegalAuthenticator != null)
          {
            LegalAuthenticator.ValidateATS(vb.Path + "LegalAuthenticator", vb.Messages);
          }

          if (InformationRecipients != null)
          {
            for (var x = 0; x < InformationRecipients.Count; x++)
            {
              InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("Custodian", Custodian))
          {
            if (Custodian != null) Custodian.ValidateATS(vb.Path + "Custodian", vb.Messages);
          }
        }

        /// <summary>
        /// Validates this CDA Context for a ICDAContextPhysicalMeasurements
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextPhysicalMeasurements.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (LegalAuthenticator != null) LegalAuthenticator.Validate(vb.Path + "LegalAuthenticator", vb.Messages);

          if (InformationRecipients != null)
          {
            for (var x = 0; x < InformationRecipients.Count; x++)
            {
              InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("Custodian", Custodian))
          {
            if (Custodian != null) Custodian.Validate(vb.Path + "Custodian", vb.Messages);

            // Check for PAI-O
            if (Custodian.Participant.Organisation != null && Custodian.Participant.Organisation.Identifiers != null)
            {
              var foundIdentifier = false;
              foreach (var identifiers in Custodian.Participant.Organisation.Identifiers)
              {
                if (identifiers.AssigningAuthorityName == HealthIdentifierType.PAIO.GetAttributeValue<NameAttribute, string>(x => x.Code))
                {
                  foundIdentifier = true;
                }
              }

              if (!foundIdentifier)
              {
                 vb.AddValidationMessage(vb.PathName, null, "At least one PAI-O Required");
              }
            }
          }
        }

        /// <summary>
        /// Validates this CDA Context
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextMedicareOverview.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (LegalAuthenticator != null) LegalAuthenticator.Validate(vb.Path + "LegalAuthenticator", vb.Messages);

            if (InformationRecipients != null)
            {
              for (var x = 0; x < InformationRecipients.Count; x++)
              {
                InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
              }
            }

            if (vb.ArgumentRequiredCheck("Custodian", Custodian))
            {
              if (Custodian != null) Custodian.Validate(vb.Path + "Custodian", vb.Messages);

              // Check for PAI-O
              if (Custodian.Participant.Organisation != null && Custodian.Participant.Organisation.Identifiers != null)
              {
                var foundIdentifier = false;
                foreach (var identifiers in Custodian.Participant.Organisation.Identifiers)
                {
                  if (identifiers.AssigningAuthorityName == HealthIdentifierType.PAIO.GetAttributeValue<NameAttribute, string>(x => x.Code))
                  {
                    foundIdentifier = true;
                  }
                }

                if (!foundIdentifier)
                {
                  vb.AddValidationMessage(vb.PathName, null, "At leat one PAI-O Required");
                }
              }
            }
        }

        /// <summary>
        /// Validates this CDA Context for a ICDAContextPrescriptionAndDispenseView
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextPrescriptionAndDispenseView.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (LegalAuthenticator != null) LegalAuthenticator.Validate(vb.Path + "LegalAuthenticator", vb.Messages);

          if (InformationRecipients != null)
          {
            for (var x = 0; x < InformationRecipients.Count; x++)
            {
              InformationRecipients[x].Validate(vb.Path + string.Format("InformationRecipients[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("Custodian", Custodian))
          {
            if (Custodian != null) Custodian.Validate(vb.Path + "Custodian", vb.Messages);

            // Check for PAI-O
            if (Custodian.Participant.Organisation != null && Custodian.Participant.Organisation.Identifiers != null)
              if (!Custodian.Participant.Organisation.Identifiers.Select(identifiers => identifiers.AssigningAuthorityName).Contains(HealthIdentifierType.PAIO.GetAttributeValue<NameAttribute, string>(x => x.Code)))
              {
                vb.AddValidationMessage(vb.PathName, null, "At leat one PAI-O Required");
              }
          }
        }

        /// <summary>
        /// Validates this CDA Context for a ICDAContextPrescriptionRecord
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextPCEHRPrescriptionRecord.Validate(string path, List<ValidationMessage> messages)
        {
          ValidateWithParentDocument(path, CDADocumentType.PrescriptionRecord, messages);
        }

        /// <summary>
        /// Validates this CDA Context for a ICDAContextDispenseRecord
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void ICDAContextPCEHRDispenseRecord.Validate(string path, List<ValidationMessage> messages)
        {
          ValidateWithParentDocument(path, CDADocumentType.DispenseRecord, messages);
        }

        /// <summary>
        /// Validates a CDA Context document with a parent document
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="documentType">The parent document type</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void ValidateWithParentDocument(string path, CDADocumentType documentType, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("LegalAuthenticator", LegalAuthenticator))
          {
            LegalAuthenticator.Validate(vb.Path + "LegalAuthenticator", vb.Messages);
          }

          if (vb.ArgumentRequiredCheck("ParentDocuments", ParentDocuments))
          {
            for (var x = 0; x < ParentDocuments.Count; x++)
            {
              ParentDocuments[x].Validate(vb.Path + string.Format("ParentDocuments[{0}]", x), vb.Messages);

              if ((ParentDocuments[x].ReleatedDocumentType == ReleatedDocumentType.Transform))
              {
                if (ParentDocuments[x].DocumentType.HasValue && ParentDocuments[x].DocumentType != documentType)
                  vb.AddValidationMessage(vb.PathName, null, string.Format("ParentDocuments of type 'XFRM' must have a document type of {0}", documentType.ToString()));

                if (ParentDocuments[x].SetId != null && SetId != null && (SetId.Extension == ParentDocuments[x].SetId.Extension && SetId.Root == ParentDocuments[x].SetId.Root))
                  vb.AddValidationMessage(vb.PathName, null, "ParentDocuments of type 'XFRM' SHALL contain a new value for setId.");
              }
              else
              {

                if (ParentDocuments[x].DocumentType.HasValue && ParentDocuments[x].DocumentType != documentType)
                  vb.AddValidationMessage(vb.PathName, null, string.Format("ParentDocuments of type 'RPLC' must have a document type of {0}", documentType.ToString()));

                if (ParentDocuments[x].SetId != null && SetId != null && (SetId.Extension != ParentDocuments[x].SetId.Extension || SetId.Root != ParentDocuments[x].SetId.Root))
                  vb.AddValidationMessage(vb.PathName, null, "ParentDocuments of type 'RPLC' the ClinicalDocument/versionNumber SHALL match the setId of the current document");

                if (ParentDocuments[x].VersionNumber != null && !Version.IsNullOrEmptyWhitespace() && Version == ParentDocuments[x].VersionNumber)
                  vb.AddValidationMessage(vb.PathName, null, "ParentDocuments of type 'RPLC' SHALL contain an incremented value of ClinicalDocument/VersionNumber");
              }
            }

            if (ParentDocuments.Any() && ParentDocuments.Count > 1)
            {
              if (ParentDocuments.Count > 2)
              {
                vb.AddValidationMessage(vb.PathName, null, "ParentDocument can only have the following combinations 'XFRM', 'XFRM' and 'RPLC'");
              }

              if (ParentDocuments.Count == 2)
              {
                if (!(ParentDocuments.Any(u => u.ReleatedDocumentType == ReleatedDocumentType.Transform) || ParentDocuments.Any(u => u.ReleatedDocumentType == ReleatedDocumentType.Replace)))
                {
                  vb.AddValidationMessage(vb.PathName, null, "ParentDocument can only have the following combinations 'XFRM', 'XFRM' and 'RPLC'");
                }
              }

              if (ParentDocuments.Count == 1)
              {
                if (!(ParentDocuments.Any(u => u.ReleatedDocumentType == ReleatedDocumentType.Transform)))
                {
                  vb.AddValidationMessage(vb.PathName, null, "ParentDocument can only have the following combinations 'XFRM', 'XFRM' and 'RPLC'");
                }
              }
            }
          }

          if (vb.ArgumentRequiredCheck("Custodian", Custodian))
          {
            if (Custodian != null) Custodian.Validate(vb.Path + "Custodian", vb.Messages);
          }

          vb.ArgumentRequiredCheck("DocumentId", DocumentId);
        }

        #endregion
    }
}
