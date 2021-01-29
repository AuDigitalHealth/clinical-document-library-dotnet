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
using CDA.Generator.Common.SCSModel.Entities;
using Nehta.VendorLibrary.CDA.CDAModel;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Pathology;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
  /// <summary>
  /// This DiagnosticImagingReport object is a composition of the context and content objects that define
  /// a CDA DiagnosticImagingReport document
  /// 
  /// This object is also responsible for providing the factory methods used to instantiate any Pathology Result Report
  /// objects that are required to build a valid Pathology Result Report CDA document
  /// </summary>
  [DataContract]
  [KnownType(typeof(Content))]
  [KnownType(typeof(Context))]
  [KnownType(typeof(CDAContext))]
  [TemplatePackage(DocumentName = "PCEHR Diagnostic Imaging", TemplatePackages = ".1(3A) .2(3A) .3(3A) .4(3A)")]
  public class DiagnosticImagingReport : BaseCDAModel
  {
    #region Constants

    private const String HEALTH_IDENTIFIER_QUALIFIER = "1.2.36.1.2001.1003.0.";

    #endregion

    #region Properties

    /// <summary>
    /// An ICDAContextDiagnosticImagingReport that contains the CDA context for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public ICDAContextDiagnosticImagingReport CDAContext { get; set; }

    /// <summary>
    /// An IDiagnosticImagingReportContext that contains the context for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public IDiagnosticImagingReportContext SCSContext { get; set; }

    /// <summary>
    /// An IDiagnosticImagingReportContent that contains the content for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public IDiagnosticImagingReportContent SCSContent { get; set; }

    #endregion

    #region Constructors
    /// <summary>
    /// Instantiates an Pathology Result Report model; the status of this CDA document will be 
    /// set to the status passed into this constructor
    /// </summary>
    /// <param name="documentStatus">Document Status</param>
    public DiagnosticImagingReport(DocumentStatus documentStatus)
    {
      DocumentStatus = documentStatus;
    }

    /// <summary>
    /// Instantiates an Pathology Result Report model; the status of this document will be 
    /// set to final
    /// </summary>
    public DiagnosticImagingReport() : this(DocumentStatus.Final)  { }

    #endregion

    #region Validation
    /// <summary>
    /// Validates this DiagnosticImagingReport object and its child objects
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
    /// Creates an DiagnosticImagingReport
    /// </summary>
    /// <returns>DiagnosticImagingReport</returns>
    public static DiagnosticImagingReport CreateDiagnosticImagingReport()
    {
      return new DiagnosticImagingReport();
    }

    /// <summary>
    /// Creates a Test Specimen Detail
    /// </summary>
    /// <returns>Test Specimen Detail</returns>
    public static TestSpecimenDetail CreateTestSpecimenDetail()
    {
      return new TestSpecimenDetail();
    }
      
    /// <summary>
    /// Creates a CDA Context
    /// </summary>
    /// <returns>ICDAContextDiagnosticImagingReport</returns>
    public static ICDAContextDiagnosticImagingReport CreateCDAContext()
    {
      return new CDAContext();
    }

    /// <summary>
    /// Creates a SCS Context
    /// </summary>
    /// <returns>IDiagnosticImagingReportContext</returns>
    public static IDiagnosticImagingReportContext CreateSCSContext()
    {
      return new Context();
    }

    /// <summary>
    /// Creates a SCS Content
    /// </summary>
    /// <returns>IDiagnosticImagingReportContent</returns>
    public static IDiagnosticImagingReportContent CreateSCSContent()
    {
      return new Content();
    }

    /// <summary>
    /// Creates an Participation Upload Authoriser
    /// </summary>
    /// <returns>Participation Upload Authoriser</returns>
    public static IParticipationUploadAuthoriser CreateUploadAuthoriser()
    {
      return new Participation();
    }

    /// <summary>
    /// Creates an Participation Reporting Pathologist
    /// </summary>
    /// <returns>Participation Reporting Pathologist</returns>
    public static IParticipationReportingPathologist CreateReportingPathologist()
    {
      return new Participation();
    }

    /// <summary>
    /// Creates an UploadAuthoriser
    /// </summary>
    /// <returns>IUploadAuthoriser</returns>
    public static IUploadAuthoriser CreateParticipantUploadAuthoriser()
    {
      return new Participant();
    }

    /// <summary>
    /// Creates an Reporting Pathologist
    /// </summary>
    /// <returns>IReportingPathologist</returns>
    public static IReportingPathologist CreateParticipantForReportingPathologist()
    {
      return new Participant();
    }

    /// <summary>
    /// Create Participation for a IParticipationServiceProvider
    /// </summary>
    /// <returns>(IParticipationServiceRequester) Participation</returns>
    public static IParticipationPathologyServiceRequester CreateServicePathologyRequester()
    {
      return new Participation();
    }

    /// <summary>
    /// Create Participant for a IServiceRequester  
    /// </summary>
    /// <returns>(IServiceRequester) Participant</returns>
    public static IPathologyServiceRequester CreateParticipantForPathologyServiceRequester()
    {
      return new Participant();
    }

    /// <summary>
    /// Create Participant for a IDiagnosticImagingExaminationResult  
    /// </summary>
    /// <returns>(IDiagnosticImagingExaminationResult) ImagingExaminationResult</returns>
    public static IDiagnosticImagingExaminationResult CreateDiagnosticImagingExaminationResult()
    {
        return new ImagingExaminationResult();
    }

    /// <summary>
    /// Create a ExaminationDetails object
    /// </summary>
    /// <returns>ExaminationDetails</returns>
    public static ExaminationDetails CreateExaminationDetails()
    {
        return new ExaminationDetails();
    }

    /// <summary>
    /// Create Participant for a IDiagnosticImageDetails  
    /// </summary>
    /// <returns>(IDiagnosticImageDetails) ImageDetails</returns>
    public static IDiagnosticImageDetails CreateDiagnosticImageDetails()
    {
        return new ImageDetails();
    }

    /// <summary>
    /// Create Participant for a OrderDetails  
    /// </summary>
    /// <returns>OrderDetails</returns>
    public static OrderDetails CreateOrderDetails()
    {
        return new OrderDetails();
    }

    /// <summary>
    /// Create a Document Provenance  
    /// </summary>
    /// <returns>DocumentProvenance</returns>
    public static DocumentDetails CreateDocumentProvenance()
    {
        return new DocumentDetails();
    }

    /// <summary>
    /// Create Participant for a IAnatomicalSiteExtended  
    /// </summary>
    /// <returns>(IAnatomicalSite) AnatomicalSite</returns>
    public static IAnatomicalSiteExtended CreateAnatomicalSiteExtended()
    {
        return new AnatomicalSite();
    }

    /// <summary>
    /// Creates a codableText object that contains and defines a role as defined by an occupation
    /// </summary>
    /// <param name="occupation">The occupation defining the role</param>
    /// <returns>A codable text representing the role</returns>
    public new static ICodableText CreateRole(Occupation occupation)
    {
        return new CodableText
        {
            DisplayName = occupation != Occupation.Undefined ? occupation.GetAttributeValue<NameAttribute, String>(x => x.Name) : String.Empty,
            Code = occupation != Occupation.Undefined ? occupation.GetAttributeValue<NameAttribute, String>(x => x.Code) : String.Empty,
            CodeSystem = CodingSystem.ANZSCORevision1
        };
    }

    /// <summary>
    /// Creates an identifier
    /// </summary>
    /// <param name="extension">extension</param>
    /// <param name="hpio">hpio</param>
    /// <returns>An Identifier Object</returns>
    public static Identifier CreateAccessionNumber(string hpio, string extension)
    {
        Identifier identifier = null;

        identifier = new Identifier
        {
            Extension = extension,
            Root = string.Format("1.2.36.1.2001.1005.53.{0}", hpio),
        };

        return identifier;
    }

    /// <summary>
    /// Creates an identifier
    /// </summary>
    /// <param name="extension">extension</param>
    /// <param name="hpio">hpio</param>
    /// <returns>An Identifier Object</returns>
    public static Identifier CreateRequesterOrderIdentifier(string hpio, string extension)
    {
        Identifier identifier = null;

        identifier = new Identifier
        {
            Extension = extension,
            Root = string.Format("1.2.36.1.2001.1005.52.{0}", hpio),
        };

        return identifier;
    }

    #region Serialisation Methods

    /// <summary>
    /// This method serializes this model into an XML document and returns this document
    /// </summary>
    /// <returns>XmlDocument</returns>
    public XmlDocument SerializeModel()
    {
      XmlDocument xmlDocument = null;
      var dataContractSerializer = new DataContractSerializer(typeof(DiagnosticImagingReport));

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
    /// This method deserializes the xml document into an DiagnosticImagingReport object
    /// </summary>
    /// <returns>XmlDocument</returns>
    public static DiagnosticImagingReport DeserializeXmlDocument(XmlDocument xmlDocument)
    {
      DiagnosticImagingReport DiagnosticImagingReport = null;

      var dataContractSerializer = new DataContractSerializer(typeof(DiagnosticImagingReport));

      using (var memoryStream = new MemoryStream())
      {
        xmlDocument.Save(memoryStream);

        memoryStream.Position = 0;

        DiagnosticImagingReport = (DiagnosticImagingReport)dataContractSerializer.ReadObject(memoryStream);
      }

      return DiagnosticImagingReport;
    }
    #endregion

    #endregion
  }
}
