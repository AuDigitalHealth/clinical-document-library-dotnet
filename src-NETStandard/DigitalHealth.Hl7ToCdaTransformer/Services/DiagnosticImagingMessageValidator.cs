using System;
using System.Linq;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.Hl7ToCdaTransformer.Resources;
using DigitalHealth.HL7.Common.Message;
using DigitalHealth.HL7.Common.Segment;
using DigitalHealth.HL7.Common.SegmentGroup;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    /// <summary>
    /// Validator for HL7 V2 Diagnostic Imaging Message
    /// </summary>
    internal class DiagnosticImagingMessageValidator : MessageValidatorBase
    {
        /// <summary>
        /// Validates the specified HL7 generic diagnostic imaging message.
        /// </summary>
        /// <param name="hl7GenericMessage">The HL7 generic diagnostic imaging message.</param>
        /// <param name="metadata">The metadata instance providing required information.</param>
        /// <param name="reportData">The diagnostic imaging report data, if not included in the path message.</param>
        /// <exception cref="MessageValidationException">hl7GenericMessage</exception>
        public void Validate(HL7GenericMessage hl7GenericMessage, DiagnosticImagingMetadata metadata, byte[] reportData)
        {
            if (hl7GenericMessage == null)
            {
                throw new MessageValidationException(nameof(hl7GenericMessage));
            }

            if (metadata == null)
            {
                throw new MessageValidationException(nameof(metadata));
            }

            ValidatePatientIdentification(hl7GenericMessage);

            ValidateOrder(hl7GenericMessage, metadata);

            ValidateAttachment(hl7GenericMessage, reportData);

            ValidateMetadata(metadata);
        }

        /// <summary>
        /// Validate metadata instance.
        /// </summary>
        /// <param name="metadata">Metadata instance to validate.</param>
        internal static void ValidateMetadata(DiagnosticImagingMetadata metadata)
        {
            if (metadata.AccessionNumber == null)
                throw new ArgumentNullException(nameof(metadata.AccessionNumber));

            if (String.IsNullOrEmpty(metadata.AccessionNumber.Root))
                throw new ArgumentNullException(nameof(metadata.AccessionNumber.Root));

            if (String.IsNullOrEmpty(metadata.AccessionNumber.Extension))
                throw new ArgumentNullException(nameof(metadata.AccessionNumber.Extension));

            if (metadata.RequesterOrderIdentifier == null)
                throw new ArgumentNullException(nameof(metadata.RequesterOrderIdentifier));

            if (String.IsNullOrEmpty(metadata.RequesterOrderIdentifier.Root))
                throw new ArgumentNullException(nameof(metadata.RequesterOrderIdentifier.Root));

            if (String.IsNullOrEmpty(metadata.RequesterOrderIdentifier.Extension))
                throw new ArgumentNullException(nameof(metadata.RequesterOrderIdentifier.Extension));

            //Required has to be HPI-O
            if (String.IsNullOrEmpty(metadata.AuthorOrganisationHpio))
                throw new ArgumentNullException(nameof(metadata.AuthorOrganisationHpio));

            if (!metadata.AuthorOrganisationHpio.StartsWith("80036"))
                throw new ArgumentException(nameof(metadata.AuthorOrganisationHpio));

            //Required
            if (metadata.ReportingRadiologist == null)
                throw new ArgumentNullException(nameof(metadata.ReportingRadiologist));

            if (metadata.ReportingRadiologist.Address == null || !metadata.ReportingRadiologist.Address.Any())
                throw new ArgumentNullException(nameof(metadata.ReportingRadiologist.Address));

            if (String.IsNullOrEmpty(metadata.ReportingRadiologist.OrganisationHpio))
                throw new ArgumentNullException(nameof(metadata.ReportingRadiologist.OrganisationHpio));

            if (!metadata.ReportingRadiologist.OrganisationHpio.StartsWith("80036"))
                throw new ArgumentException(nameof(metadata.ReportingRadiologist.OrganisationHpio));

            //Role SHOULD have a value chosen from 1220.0 - ANZSCO - Australian and New Zealand Standard Classification of Occupations, First Edition, Revision 1[ABS2009]
            Occupation role;
            if (!Enum.TryParse(metadata.ReportingRadiologist.Role.ToString(), out role))
                throw new ArgumentException(nameof(metadata.ReportingRadiologist.Role));

            if (metadata.ReportingRadiologist.Address.Any(y => y.AustralianAddress == null))
                throw new ArgumentNullException(nameof(metadata.ReportingRadiologist.Address));

            if (metadata.ReportingRadiologist.Address.Any(y => y.AddressPurpose != AddressPurpose.Business))
                throw new ArgumentException(nameof(metadata.ReportingRadiologist.ContactDetails));

            if (!metadata.ReportingRadiologist.ContactDetails.All(x => x.Usage != null && x.Usage.Any(y => y == ElectronicCommunicationUsage.WorkPlace)))
                throw new ArgumentException(nameof(metadata.ReportingRadiologist.ContactDetails));
        }

        /// <summary>
        /// Validates the order.
        /// </summary>
        /// <param name="hl7GenericMessage">The HL7 generic message.</param>
        /// <param name="metadata">Diagnostic Imaging metadata instance.</param>
        /// <exception cref="MessageValidationException">
        /// Order
        /// or
        /// Observation
        /// or
        /// ObservationsReportID
        /// </exception>
        internal static void ValidateOrder(HL7GenericMessage hl7GenericMessage, DiagnosticImagingMetadata metadata)
        {
            if (hl7GenericMessage.Order?.First().Observation?.First().ObservationsReportID?.PrincipalResultInterpreter?
                    .name?.assigningauthority?.namespaceID != TransformerConstants.Aushic
                && String.IsNullOrEmpty(metadata.ReportingRadiologist.Hpii))
            {
                throw new MessageValidationException("ReportingRadiologist.Hpii must be set in the metadata, if not provided in OBR-32 of the HL7 V2 message.");
            }

            if (hl7GenericMessage.Order == null || hl7GenericMessage.Order.Length == 0)
            {
                throw new MessageValidationException(nameof(hl7GenericMessage.Order));
            }

            foreach (OrderGroup orderGroup in hl7GenericMessage.Order)
            {
                if (orderGroup.Observation == null || orderGroup.Observation.Length == 0)
                {
                    throw new MessageValidationException(nameof(orderGroup.Observation));
                }

                foreach (ObservationGroup observationGroup in orderGroup.Observation)
                {
                    OBR obrSegment = observationGroup.ObservationsReportID;

                    if (observationGroup.ObservationsReportID == null)
                    {
                        throw new MessageValidationException(nameof(observationGroup.ObservationsReportID));
                    }

                    if (obrSegment.UniversalServiceID?.identifier == null)
                    {
                        throw new MessageValidationException("Message OBR contains no UniversalServiceID.identifier");
                    }

                    if (obrSegment.UniversalServiceID.identifier != TransformerConstants.ReportText)
                    {
                        if (obrSegment.ResultsRptStatusChngDateTime == null || !obrSegment.ResultsRptStatusChngDateTime.Any(r => r.TimestampValue.HasValue))
                        {
                            throw new MessageValidationException(ConstantsResource.InvalidResultsDateTime);
                        }

                        if (obrSegment.UniversalServiceID == null || String.IsNullOrEmpty(obrSegment.UniversalServiceID.alternatetext ?? obrSegment.UniversalServiceID.text))
                        {
                            throw new MessageValidationException(ConstantsResource.NoTestResultNameSupplied);
                        }

                        if (obrSegment.ObservationDateTime?.TimestampValue == null)
                        {
                            throw new MessageValidationException(ConstantsResource.InvalidObservationDateTime);
                        }

                        Hl7V3ResultStatus hl7V3ResultStatus;
                        if (!EnumHelper.TryGetEnumValue<Hl7V3ResultStatus, NameAttribute>(attribute => attribute.Code == obrSegment.ResultStatus, out hl7V3ResultStatus))
                        {
                            throw new MessageValidationException(ConstantsResource.IncorrectTestResultStatusSupplied);
                        }

                        if (obrSegment.UniversalServiceID.text == null && obrSegment.UniversalServiceID.alternatetext == null)
                        {
                            throw new MessageValidationException("Message OBR contains no UniversalServiceID value");
                        }

                        ValidatePrincipalResultInterpreter(obrSegment.PrincipalResultInterpreter);
                    }
                }

                ValidateOrderingProvider(orderGroup);
            }
        }
    }
}
