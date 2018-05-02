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
using CDA.Generator.Common.SCSModel.AdvanceCareInformation.Entities;
using Nehta.VendorLibrary.CDA.CDAModel;
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
  /// This object is also responsible for providing the factory methods used to instantiate any Advance Care Information
  /// objects that are required to build a valid Pathology Result Report CDA document
  /// </summary>
  [DataContract]
  [KnownType(typeof(Content))]
  [KnownType(typeof(Context))]
  [KnownType(typeof(CDAContext))]
  [TemplatePackage(DocumentName = "Advanced Care Information", TemplatePackages = "1.2.36.1.2001.1006.1.226 .1(3A) .2(3A)")]
  public class AdvanceCareInformation : BaseCDAModel
  {
    #region Constants

    private const String HEALTH_IDENTIFIER_QUALIFIER = "1.2.36.1.2001.1003.0.";

    #endregion

    #region Properties

    /// <summary>
    /// An ICDAContextAdvanceCareInformation that contains the CDA context for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public ICDAContextAdvanceCareInformation CDAContext { get; set; }

    /// <summary>
    /// An IAdvanceCareInformationContent that contains the content for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public IAdvanceCareInformationContent SCSContent { get; set; }

    /// <summary>
    /// An IAdvanceCareInformationContext that contains the context for this Pathology Result Report record
    /// </summary>
    [DataMember]
    public IAdvanceCareInformationContext SCSContext { get; set; }

    #endregion

    #region Constructors
    /// <summary>
    /// Instantiates an Pathology Result Report model; the status of this CDA document will be 
    /// set to the status passed into this constructor
    /// </summary>
    /// <param name="documentStatus">Document Status</param>
    public AdvanceCareInformation(DocumentStatus documentStatus)
    {
      DocumentStatus = documentStatus;
    }

    /// <summary>
    /// Instantiates an Pathology Result Report model; the status of this document will be 
    /// set to final
    /// </summary>
    public AdvanceCareInformation() : this(DocumentStatus.Final)  { }

    #endregion

    #region Validation
    /// <summary>
    /// Validates this AdvanceCareInformation object and its child objects
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
    /// Creates an AdvanceCareInformation
    /// </summary>
    /// <returns>AdvanceCareInformation</returns>
    public static AdvanceCareInformation CreateAdvanceCareInformation()
    {
      return new AdvanceCareInformation();
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
    /// <returns>ICDAContextAdvanceCareInformation</returns>
    public static ICDAContextAdvanceCareInformation CreateCDAContext()
    {
      return new CDAContext();
    }

    /// <summary>
    /// Creates a SCS Context
    /// </summary>
    /// <returns>IAdvanceCareInformationContext</returns>
    public static IAdvanceCareInformationContext CreateSCSContext()
    {
      return new Context();
    }

    /// <summary>
    /// Creates a SCS Content
    /// </summary>
    /// <returns>IAdvanceCareInformationContent</returns>
    public static IAdvanceCareInformationContent CreateSCSContent()
    {
      return new Content();
    }

    /// <summary>
    /// Create a RelatedDocument object
    /// </summary>
    /// <returns>RelatedDocument</returns>
    public static IDocumentDetails CreateRelatedDocument()
    {
        return new Information();
    }

    /// <summary>
    /// Create a DocumentProvenance object
    /// </summary>
    /// <returns>DocumentProvenance</returns>
    public static DocumentProvenance CreateDocumentProvenance()
    {
        return new DocumentProvenance();
    }

    #region Serialisation Methods

    /// <summary>
    /// This method serializes this model into an XML document and returns this document
    /// </summary>
    /// <returns>XmlDocument</returns>
    public XmlDocument SerializeModel()
    {
      XmlDocument xmlDocument = null;
      var dataContractSerializer = new DataContractSerializer(typeof(AdvanceCareInformation));

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
    /// This method deserializes the xml document into an AdvanceCareInformation object
    /// </summary>
    /// <returns>XmlDocument</returns>
    public static AdvanceCareInformation DeserializeXmlDocument(XmlDocument xmlDocument)
    {
      AdvanceCareInformation advanceCareInformation = null;

      var dataContractSerializer = new DataContractSerializer(typeof(AdvanceCareInformation));

      using (var memoryStream = new MemoryStream())
      {
        xmlDocument.Save(memoryStream);

        memoryStream.Position = 0;

        advanceCareInformation = (AdvanceCareInformation)dataContractSerializer.ReadObject(memoryStream);
      }

      return advanceCareInformation;
    }
    #endregion

    #endregion
  }
}
