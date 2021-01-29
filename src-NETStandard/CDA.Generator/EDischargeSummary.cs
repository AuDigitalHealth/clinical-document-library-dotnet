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
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using CDA.Generator.Common.Common.Attributes;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This eDischargeSummary object is a composition of the context and content objects that define
    /// a CDA eDischargeSummary document
    /// 
    /// This object is also responsible for providing the factory methods used to instantiate any E-DischargeSummary
    /// objects that are required to build a valid E-DischargeSummary CDA document
    /// </summary>
    [DataContract]
    [KnownType(typeof(Content))]
    [KnownType(typeof(Context))]
    [KnownType(typeof(CDAContext))]
    [KnownType(typeof(BaseCDAModel))]
    [TemplatePackage(DocumentName = "Discharge Summary", TemplatePackages = ".18 (1A) .19 (1B) .20 (2) .21 (3A) .22 (3B) .23 (1A) .24 (1B) .25 (2) .26 (3A) .27 (3B)")]
    public class EDischargeSummary : BaseCDAModel
    {
        #region Constants

        private const String HEALTH_IDENTIFIER_QUALIFIER = "1.2.36.1.2001.1003.0.";

        #endregion

        #region Properties
        /// <summary>
        /// An ICDAContextEDischargeSummary that contains the CDA context for this e-DischargeSummary record
        /// </summary>
        [DataMember]
        public ICDAContextEDischargeSummary CDAContext { get; set; }

        /// <summary>
        /// An IEDischargeSummaryContent that contains the content for this e-DischargeSummary record
        /// </summary>
        [DataMember]
        public IEDischargeSummaryContent SCSContent { get; set; }

        /// <summary>
        /// An IDispenseRecordContext that contains the context for this e-DischargeSummary record
        /// </summary>
        [DataMember]
        public IEDischargeSummaryContext SCSContext { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiates an eDichargeSummary model; the status of this CDA document will be 
        /// set to the status passed into this constructor
        /// </summary>
        /// <param name="documentStatus">Document Status</param>
        public EDischargeSummary(DocumentStatus documentStatus)
        {
            DocumentStatus = documentStatus;
        }

        /// <summary>
        /// Instantiates an eDischargeSummary model; the status of this document will be 
        /// set to final
        /// </summary>
        public EDischargeSummary() : this(DocumentStatus.Final)
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this eDischargeSummary object and its child objects
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("CDAContext", CDAContext))
               CDAContext.Validate(vb.Path + "CDAContext", vb.Messages);
 
            if (vb.ArgumentRequiredCheck("SCSContext", SCSContext))
                SCSContext.Validate(vb.Path + "SCSContext", vb.Messages);

            if (vb.ArgumentRequiredCheck("SCSContent", SCSContent))
                SCSContent.Validate(vb.Path + "SCSContent", vb.Messages);

        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates an EDischargeSummary
        /// </summary>
        /// <returns>An EDischargeSummary Object</returns>
        public static EDischargeSummary CreateEDischargeSummary()
        {
            return new EDischargeSummary();
        }

        /// <summary>
        /// Creates a CDA Context
        /// </summary>
        /// <returns>(ICDAContextEDischargeSummary) Context</returns>
        public static ICDAContextEDischargeSummary CreateCDAContext()
        {
            return new CDAContext();
        }

        /// <summary>
        /// Creates a SCS Context
        /// </summary>
        /// <returns>(IEDischargeSummaryContext) Context</returns>
        public static IEDischargeSummaryContext CreateSCSContext()
        {
            return new Context();
        }

        /// <summary>
        /// Create a Facility
        /// </summary>
        /// <returns>(IParticipationFacility) Facility</returns>
        public static IParticipationFacility CreateFacility()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a facility Participant 
        /// </summary>
        /// <returns>(IFacility) facility</returns>
        public static IFacility CreateParticipantForFacility()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a Nominated Primary HealthCare Provider
        /// </summary>
        /// <returns>INominatedPrimaryHealthCareProvider</returns>
        public static INominatedPrimaryHealthCareProvider CreateParticipantForNominatedPrimaryHealthProvider()
        {
            return new Participant();
        }
        /// <summary>
        /// Creates an imaging examination result
        /// </summary>
        /// <returns>(IImagingExaminationResult) Imaging Examination Result Discharge Summary</returns>
        public new static IImagingExaminationResult CreateImagingExaminationResult()
        {
            return new ImagingExaminationResult();
        }

        /// <summary>
        /// Creates a participation constrained down to an IParticipationSubjectOfCare
        /// </summary>
        /// <returns>(IParticipationSubjectOfCare) Participation</returns>
        public static new IParticipationSubjectOfCare CreateSubjectOfCare()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates a participation constrained down to an ISubjectOfCare
        /// </summary>
        /// <returns>(ISubjectOfCare) Participation</returns>
        public static new ISubjectOfCare CreateParticipantForSubjectOfCare()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a SCS Content
        /// </summary>
        /// <returns>(IEDischargeSummaryContent) Content</returns>
        public static IEDischargeSummaryContent CreateSCSContent()
        {
            return new Content();
        }

        /// <summary>
        /// Creates a Diagnostic Investigation
        /// </summary>
        /// <returns>(IDiagnosticInvestigationsDischargeSummary) DiagnosticInvestigations</returns>
        public new static IDiagnosticInvestigationsDischargeSummary CreateDiagnosticInvestigations()
        {
            return new DiagnosticInvestigations();
        }

        /// <summary>
        /// Creates an Event
        /// </summary>
        /// <returns>An Event</returns>
        public static Event CreateEvent()
        {
            return new Event();
        }

        /// <summary>
        /// Creates an Encounter
        /// </summary>
        /// <returns>An Encounter</returns>
        public static Encounter CreateEncounter()
        {
            return new Encounter();
        }

        /// <summary>
        /// Creates an Health Professional Participation Object
        /// </summary>
        /// <returns>(IParticipationResponsibleHealthProfessional) Participation</returns>
        public static IParticipationResponsibleHealthProfessional CreateResponsibleHealthProfessional()
        {
            return new Participation();
        }

         /// <summary>
        /// Creates an Health Person Or Organisation
        /// </summary>
        /// <returns>(IHealthPersonOrOrganisation) IHealthPersonOrOrganisation</returns>
        public static IHealthPersonOrOrganisation CreateHealthPersonOrOrganisation()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates an Other Participant Participation Object
        /// </summary>
        /// <returns>(IParticipationOtherParticipant) Participation</returns>
        public static IParticipationOtherParticipant CreateOtherParticipant()
        {
            return new Participation();
        }

        /// <summary>
        /// Create Participant for a Responsible Health Professional
        /// </summary>
        /// <returns>(IResponsibleHealthProfessional)Participant</returns>
        public static IResponsibleHealthProfessional CreateParticipantForResponsibleHealthProfessional()
        {
            return new Participant();
        }

        /// <summary>
        /// Create Participant for IParticipationHealthProfessional
        /// </summary>
        /// <returns>(IOtherParticipant) Participant</returns>
        public static IOtherParticipant CreateParticipantForOtherParticipant()
        {
            return new Participant();
        }

         /// <summary>
        /// Create Participant For Recommendation Recipient
        /// </summary>
        /// <returns>(IHealthPersonOrOrganisation) Participant</returns>
        public static IHealthPersonOrOrganisation CreateParticipantForRecommendationRecipient()
        {
            return new Participant();
        }

        /// <summary>
        /// Creates a DischargeSummaryProblemDiagnosis
        /// </summary>
        /// <returns> (IDischargeSummaryProblemDiagnosis) ProblemDiagnosis</returns>
        public static IDischargeSummaryProblemDiagnosis CreateProblemDiagnosis()
        {
            return new ProblemDiagnosis();
        }

        /// <summary>
        /// Creates a ProblemDiagnoses This Visit Object
        /// </summary>
        /// <returns>A ProblemDiagnosesThisVisit Object</returns>
        public static ProblemDiagnosesThisVisit CreateProblemDiagnosesThisVisit()
        {
            return new ProblemDiagnosesThisVisit();
        }

        /// <summary>
        /// Creates an IStatement exclusion statement
        /// </summary>
        /// <returns>(IStatement) Statement</returns>
        public new static Statement CreateExclusionStatement()
        {
            return new Statement();
        }

        /// <summary>
        /// Create a Clinical Synopsis for a discharge summary
        /// </summary>
        /// <returns>A ClinicalSynopsis Object</returns>
        public static ClinicalSynopsis CreateClinicalSynopsis()
        {
            return new ClinicalSynopsis();
        }

        /// <summary>
        /// Create a ClinicalIntervention for a discharge summary
        /// </summary>
        /// <returns>A ClinicalIntervention Object</returns>
        public static ClinicalIntervention CreateClinicalIntervention()
        {
            return new ClinicalIntervention();
        }

        /// <summary>
        /// Create an ImagingTest
        /// </summary>
        /// <returns>An ImagingTest Object</returns>
        public static ImagingTest CreateImagingTest()
        {
            return new ImagingTest();
        }

        /// <summary>
        /// Create a View
        /// </summary>
        /// <returns>A View</returns>
        public static View CreateView()
        {
            return new View();
        }

        /// <summary>
        /// Create a Protocol
        /// </summary>
        /// <returns>A Protocol</returns>
        public static Protocol CreateProtocol()
        {
            return new Protocol();
        }

        /// <summary>
        /// Creates Medications for Discharge Summary
        /// </summary>
        /// <returns>(IMedicationsDischargeSummary) Medications</returns>
        public static IMedicationsDischargeSummary CreateMedications()
        {
            return new Medications();
        }

        /// <summary>
        /// Creates Current Medications for Discharge Summary
        /// </summary>
        /// <returns>(CurrentMedications) CurrentMedications</returns>
        public static CurrentMedications CreateCurrentMedications()
        {
            return new CurrentMedications();
        }

        /// <summary>
        /// Create TherapeuticGood for Discharge Summary
        /// </summary>
        /// <returns>(ITherapeuticGood) TherapeuticGood</returns>
        public static ITherapeuticGood CreateTherapeuticGood()
        {
            return new TherapeuticGood();
        }

        /// <summary>
        /// Create CreateMedicationHistory for Discharge Summary
        /// </summary>
        /// <returns>(IMedicalHistory) MedicationHistory</returns>
        public static IMedicationHistory CreateMedicationHistory()
        {
            return new MedicationHistory();
        }

        /// <summary>
        /// Create CreateMedicationHistoryCeased for Discharge Summary  
        /// </summary>
        /// <returns>(IMedicationHistoryCeased) MedicationHistory</returns>
        public static IMedicationHistoryCeased CreateMedicationHistoryCeased()
        {
            return new MedicationHistory();
        }
        
        /// <summary>
        /// Creates CeasedMedications for Discharge Summary
        /// </summary>
        /// <returns>A CeasedMedications Object</returns>
        public static CeasedMedications CreateCeasedMedications()
        {
            return new CeasedMedications();
        }
        
        /// <summary>
        /// Create TherapeuticGood for Discharge Summary TherapeuticGoodCeased
        /// </summary>
        /// <returns>(ITherapeuticGoodCeased) TherapeuticGood</returns>
        public static ITherapeuticGoodCeased CreateTherapeuticGoodCeased()
        {
            return new TherapeuticGood();
        }

        /// <summary>
        /// Create HealthProfile for Discharge Summary 
        /// </summary>
        /// <returns>A HealthProfile Object</returns>
        public static HealthProfile CreateHealthProfile()
        {
            return new HealthProfile();
        }

        /// <summary>
        /// Create Participation Nominated Primary Health Care Provider for Discharge Summary 
        /// </summary>
        /// <returns>(IParticipationNominatedPrimaryHealthCareProvider) Participation</returns>
        public static IParticipationNominatedPrimaryHealthCareProvider CreateNominatedPrimaryHealthCareProvider()
        {
            return new Participation();
        }

        /// <summary>
        /// Create AdverseReaction for Discharge Summary 
        /// </summary>
        /// <returns> (IAdverseReactionDischargeSummary) AdverseReaction </returns>
        public static IAdverseReactionDischargeSummary CreateReactions()
        {
            return new AdverseReaction();
        }

        /// <summary>
        /// Create AdverseReactions for Discharge Summary 
        /// </summary>
        /// <returns>An AdverseReactions Object</returns>
        public static AdverseReactions CreateAdverseReactions()
        {
            return new AdverseReactions();
        }

        /// <summary>
        /// Create Alert for Discharge Summary 
        /// </summary>
        /// <returns>An Alert Object</returns>
        public static Alerts CreateAlerts()
        {
            return new Alerts();
        }


        /// <summary>
        /// Create Alert for Discharge Summary 
        /// </summary>
        /// <returns>An Alert Object</returns>
        public static Alert CreateAlert()
        {
          return new Alert();
        }

        /// <summary>
        /// Create Plan for Discharge Summary 
        /// </summary>
        /// <returns>A Plan Object</returns>
        public static Plan CreatePlan()
        {
            return new Plan();
        }

        /// <summary>
        /// Create Arranged Services for Discharge Summary 
        /// </summary>
        /// <returns>A ArrangedServices Object</returns>
        public static ArrangedServices CreateArrangedServices()
        {
            return new ArrangedServices();
        }
 
        /// <summary>
        /// Create RecommendationsInformationProvided for Discharge Summary 
        /// </summary>
        /// <returns>(RecommendationsInformationProvided) Recommendations Information Provided</returns>
        public static RecommendationsInformationProvided CreateRecommendationsInformationProvided()
        {
            return new RecommendationsInformationProvided();
        }

        /// <summary>
        /// Create Recommendations Provided for Discharge Summary 
        /// </summary>
        /// <returns>A RecommendationsProvided Object</returns>
        public static RecommendationsProvided CreateRecommendationsProvided()
        {
            return new RecommendationsProvided();
        }

        /// <summary>
        /// Creates an Recommendation Recipient Object
        /// </summary>
        /// <returns>(IParticipationHealthProfessional) Participation</returns>
        public static IParticipationHealthProfessional CreateRecommendationRecipient()
        {
            return new Participation();
        }

        /// <summary>
        /// Creates an Information Provided Object
        /// </summary>
        /// <returns>An InformationProvided Object</returns>
        public static InformationProvided CreateInformationProvided()
        {
            return new InformationProvided();
        }

        #endregion

        #region Serialisation Methods
        /// <summary>
        /// This method serializes this model into an XML document and returns this document
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument SerializeModel()
        {
            XmlDocument xmlDocument = null;
            var dataContractSerializer = new DataContractSerializer(typeof(EDischargeSummary));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument = new XmlDocument();

                dataContractSerializer.WriteObject(memoryStream, this);

                memoryStream.Seek(0, SeekOrigin.Begin);

                xmlDocument.Load(memoryStream);
            }

            return xmlDocument;
        }

        /// <summary>
        /// This method deserializes the xml document into an eDischargeSummary object
        /// </summary>
        /// <returns>XmlDocument</returns>
        public static EDischargeSummary DeserializeXmlDocument(XmlDocument xmlDocument)
        {
            EDischargeSummary eDischargeSummary = null;

            var dataContractSerializer = new DataContractSerializer(typeof(EDischargeSummary));

            using (var memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);

                memoryStream.Position = 0;

                eDischargeSummary = (EDischargeSummary)dataContractSerializer.ReadObject(memoryStream);
            }

            return eDischargeSummary;
        }
        #endregion
    }
}
