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
  /// This PathologyResultReport object is a composition of the context and content objects that define
  /// a CDA PathologyResultReport document
  /// 
  /// This object is also responsible for providing the factory methods used to instantiate any Pathology Result Report
  /// objects that are required to build a valid Pathology Result Report CDA document
  /// </summary>
  [DataContract]
  [KnownType(typeof(Content))]
  [KnownType(typeof(Context))]
  [KnownType(typeof(CDAContext))]
  [TemplatePackage(DocumentName = "Pathology Result Report", TemplatePackages = ".1(3A) .2(3A) .3(3A) .4(3A)")]
  public class PathologyResultReport : BaseCDAModel
  {
    #region Constants

    private const String HealthIdentifierQualifier = "1.2.36.1.2001.1003.0.";

    #endregion

    #region Properties

    /// <summary>
    /// An ICDAContextPathologyResultReport that contains the CDA context for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public ICDAContextPathologyResultReport CDAContext { get; set; }

    /// <summary>
    /// An IPathologyResultReportContent that contains the content for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public IPathologyResultReportContent SCSContent { get; set; }

    /// <summary>
    /// An IPathologyResultReportContext that contains the context for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public IPathologyResultReportContext SCSContext { get; set; }

    #endregion

    #region Constructors
    /// <summary>
    /// Instantiates an Pathology Result Report model; the status of this CDA document will be 
    /// set to the status passed into this constructor
    /// </summary>
    /// <param name="documentStatus">Document Status</param>
    public PathologyResultReport(DocumentStatus documentStatus)
    {
      DocumentStatus = documentStatus;
    }

    /// <summary>
    /// Instantiates an Pathology Result Report model; the status of this document will be 
    /// set to final
    /// </summary>
    public PathologyResultReport()
      : this(DocumentStatus.Final)
    {
    }
    #endregion

    #region Validation
    /// <summary>
    /// Validates this PathologyResultReport object and its child objects
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
    /// Creates an PathologyResultReport
    /// </summary>
    /// <returns>PathologyResultReport</returns>
    public static PathologyResultReport CreatePathologyResultReport()
    {
      return new PathologyResultReport();
    }

    /// <summary>
    /// Creates an PathologyResultReport
    /// </summary>
    /// <returns>PathologyResultReport</returns>
    public new static SCSModel.Pathology.PathologyTestResult CreatePathologyTestResult()
    {
      return new SCSModel.Pathology.PathologyTestResult();
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
    /// Creates a Result Group
    /// </summary>
    /// <returns>ResultGroup</returns>
    public static SCSModel.Pathology.ResultGroup CreateResultGroup()
    {
      return new SCSModel.Pathology.ResultGroup();
    }

    /// <summary>
    /// Creates a Result
    /// </summary>
    /// <returns>ResultGroup</returns>
    public static SCSModel.Pathology.Result CreateResult()
    {
        return new SCSModel.Pathology.Result();
    }
      

    /// <summary>
    /// Creates an PathologyResultReport
    /// </summary>
    /// <returns>PathologyResultReport</returns>
    public static SCSModel.Pathology.RequestedService CreateRequestedService()
    {
      return new SCSModel.Pathology.RequestedService();
    }

    /// <summary>
    /// Creates a CDA Context
    /// </summary>
    /// <returns>ICDAContextPathologyResultReport</returns>
    public static ICDAContextPathologyResultReport CreateCDAContext()
    {
      return new CDAContext();
    }

    /// <summary>
    /// Creates a SCS Context
    /// </summary>
    /// <returns>IPathologyResultReportContext</returns>
    public static IPathologyResultReportContext CreateSCSContext()
    {
      return new Context();
    }

    /// <summary>
    /// Creates a SCS Content
    /// </summary>
    /// <returns>IPathologyResultReportContent</returns>
    public static IPathologyResultReportContent CreateSCSContent()
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
    /// Create a RelatedDocument  
    /// </summary>
    /// <returns>(RelatedDocument) Participant</returns>
    public static RelatedDocument CreateRelatedDocument()
    {
        return new RelatedDocument();
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

    #region Serialisation Methods

    /// <summary>
    /// This method serializes this model into an XML document and returns this document
    /// </summary>
    /// <returns>XmlDocument</returns>
    public XmlDocument SerializeModel()
    {
      XmlDocument xmlDocument = null;
      var dataContractSerializer = new DataContractSerializer(typeof(PathologyResultReport));

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
    /// This method deserializes the xml document into an PathologyResultReport object
    /// </summary>
    /// <returns>XmlDocument</returns>
    public static PathologyResultReport DeserializeXmlDocument(XmlDocument xmlDocument)
    {
      PathologyResultReport PathologyResultReport = null;

      var dataContractSerializer = new DataContractSerializer(typeof(PathologyResultReport));

      using (var memoryStream = new MemoryStream())
      {
        xmlDocument.Save(memoryStream);

        memoryStream.Position = 0;

        PathologyResultReport = (PathologyResultReport)dataContractSerializer.ReadObject(memoryStream);
      }

      return PathologyResultReport;
    }
    #endregion

    #endregion
  }
}
