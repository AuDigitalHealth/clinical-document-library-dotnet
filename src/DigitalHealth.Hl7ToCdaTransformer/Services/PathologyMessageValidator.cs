using System;
using System.Linq;
using DigitalHealth.Hl7ToCdaTransformer.Models;
using DigitalHealth.Hl7ToCdaTransformer.Resources;
using DigitalHealth.HL7.Common.DataStructure;
using DigitalHealth.HL7.Common.Message;
using DigitalHealth.HL7.Common.Segment;
using DigitalHealth.HL7.Common.SegmentGroup;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    /// <summary>
    /// Validator for HL7 V2 Pathology Message
    /// </summary>
    internal class PathologyMessageValidator
    {
        /// <summary>
        /// Validates the specified HL7 generic path message.
        /// </summary>
        /// <param name="hl7GenericPathMessage">The HL7 generic path message.</param>
        /// <param name="metadata">The metadata instance providing required information.</param>
        /// <param name="reportData">The pathology report data, if not included in the path message.</param>
        /// <exception cref="MessageValidationException">hl7GenericPathMessage</exception>
        public void Validate(HL7GenericPathMessage hl7GenericPathMessage, PathologyMetadata metadata, byte[] reportData)
        {
            if (hl7GenericPathMessage == null)
            {
                throw new MessageValidationException(nameof(hl7GenericPathMessage));
            }

            if (metadata == null)
            {
                throw new MessageValidationException(nameof(metadata));
            }

            ValidatePatientIdentification(hl7GenericPathMessage);

            ValidateOrder(hl7GenericPathMessage, metadata);

            ValidateAttachment(hl7GenericPathMessage, reportData);

            ValidateMetadata(metadata);
        }

        /// <summary>
        /// Validate metadata instance.
        /// </summary>
        /// <param name="metadata">Metadata instance to validate.</param>
        private static void ValidateMetadata(PathologyMetadata metadata)
        {
            if (metadata.ReportIdentifier == null)
                throw new ArgumentNullException(nameof(metadata.ReportIdentifier));

            if (string.IsNullOrEmpty(metadata.ReportIdentifier.Root))
                throw new ArgumentNullException(nameof(metadata.ReportIdentifier.Root));
            
            if (string.IsNullOrEmpty(metadata.ReportIdentifier.Extension))
                throw new ArgumentNullException(nameof(metadata.ReportIdentifier.Extension));

            if (metadata.RequesterOrderIdentifier == null)
                throw new ArgumentNullException(nameof(metadata.RequesterOrderIdentifier));

            if (string.IsNullOrEmpty(metadata.RequesterOrderIdentifier.Root))
                throw new ArgumentNullException(nameof(metadata.RequesterOrderIdentifier.Root));

            if (string.IsNullOrEmpty(metadata.RequesterOrderIdentifier.Extension))
                throw new ArgumentNullException(nameof(metadata.RequesterOrderIdentifier.Extension));

            //Required has to be HPI-O
            if (string.IsNullOrEmpty(metadata.AuthorOrganisationHpio))
                throw new ArgumentNullException(nameof(metadata.AuthorOrganisationHpio));

            if (!metadata.AuthorOrganisationHpio.StartsWith("80036"))
                throw new ArgumentException(nameof(metadata.AuthorOrganisationHpio));

            //Required
            if (metadata.ReportingPathologist == null)
                throw new ArgumentNullException(nameof(metadata.ReportingPathologist));

            if (metadata.ReportingPathologist.Address == null || !metadata.ReportingPathologist.Address.Any())
                throw new ArgumentNullException(nameof(metadata.ReportingPathologist.Address));

            if (string.IsNullOrEmpty(metadata.ReportingPathologist.OrganisationHpio))
                throw new ArgumentNullException(nameof(metadata.ReportingPathologist.OrganisationHpio));

            if (!metadata.ReportingPathologist.OrganisationHpio.StartsWith("80036"))
                throw new ArgumentException(nameof(metadata.ReportingPathologist.OrganisationHpio));

            //Role SHOULD have a value chosen from 1220.0 - ANZSCO - Australian and New Zealand Standard Classification of Occupations, First Edition, Revision 1[ABS2009]
            Occupation role;
            if (!Enum.TryParse(metadata.ReportingPathologist.Role.ToString(), out role))
                throw new ArgumentException(nameof(metadata.ReportingPathologist.Role));

            if (metadata.ReportingPathologist.Address.Any(y => y.AustralianAddress == null))
                throw new ArgumentNullException(nameof(metadata.ReportingPathologist.Address));

            if (metadata.ReportingPathologist.Address.Any(y => y.AddressPurpose != AddressPurpose.Business))
                throw new ArgumentException(nameof(metadata.ReportingPathologist.ContactDetails));

            if (!metadata.ReportingPathologist.ContactDetails.All(x => x.Usage != null && x.Usage.Any(y => y == ElectronicCommunicationUsage.WorkPlace)))
                throw new ArgumentException(nameof(metadata.ReportingPathologist.ContactDetails));
        }

        /// <summary>
        /// Validates the patient identification.
        /// </summary>
        /// <param name="hl7GenericPathMessage">The HL7 generic path message.</param>
        /// <exception cref="MessageValidationException">PatientIdentification</exception>
        private static void ValidatePatientIdentification(HL7GenericPathMessage hl7GenericPathMessage)
        {
            if (hl7GenericPathMessage.PatientIdentification == null)
            {
                throw new MessageValidationException(nameof(hl7GenericPathMessage.PatientIdentification));
            }
        }

        /// <summary>
        /// Validates the order.
        /// </summary>
        /// <param name="hl7GenericPathMessage">The HL7 generic path message.</param>
        /// <param name="metadata">Pathology metadata instance.</param>
        /// <exception cref="MessageValidationException">
        /// Order
        /// or
        /// Observation
        /// or
        /// ObservationsReportID
        /// </exception>
        private static void ValidateOrder(HL7GenericPathMessage hl7GenericPathMessage, PathologyMetadata metadata)
        {
            if (hl7GenericPathMessage.Order?.First().Observation?.First().ObservationsReportID?.PrincipalResultInterpreter?
                    .name?.assigningauthority?.namespaceID != TransformerConstants.Aushic
                && string.IsNullOrEmpty(metadata.ReportingPathologist.Hpii))
            {
                throw new MessageValidationException("ReportingPathologist.Hpii must be set in the metadata, if not provided in OBR-32 of the HL7 V2 message.");
            }

            if (hl7GenericPathMessage.Order == null || hl7GenericPathMessage.Order.Length == 0)
            {
                throw new MessageValidationException(nameof(hl7GenericPathMessage.Order));
            }

            foreach (OrderGroup orderGroup in hl7GenericPathMessage.Order)
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

                        if (obrSegment.UniversalServiceID == null || string.IsNullOrEmpty(obrSegment.UniversalServiceID.alternatetext ?? obrSegment.UniversalServiceID.text))
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

        /// <summary>
        /// Validate the principal result interpreter.
        /// </summary>
        /// <param name="principalResultInterpreter">Principal result interpreter</param>
        /// <exception cref="MessageValidationException"/>
        private static void ValidatePrincipalResultInterpreter(NDL principalResultInterpreter)
        {
            if (principalResultInterpreter?.name == null)
            {
                throw new MessageValidationException("Message missing PrincipalResultInterpreter");
            }

            if (string.IsNullOrWhiteSpace(principalResultInterpreter.name.familyname))
            {
                throw new MessageValidationException(ResponseStrings.HealthProviderNameNotSupplied);
            }

            if (principalResultInterpreter.name.assigningauthority?.universalID == null)
            {
                throw new MessageValidationException("Message missing PrincipalResultInterpreter universalID");
            }
        }

        /// <summary>
        /// Validates the attachment.
        /// </summary>
        /// <param name="hl7GenericPathMessage">The HL7 generic path message.</param>
        /// <param name="reportData">The report attachment data.</param>
        /// <exception cref="MessageValidationException">Message does not contain an embedded PDF report</exception>
        private static void ValidateAttachment(HL7GenericPathMessage hl7GenericPathMessage, byte[] reportData)
        {
            var hasRpObx = hl7GenericPathMessage.Order.Last().Observation.Last().Result.SingleOrDefault(s => s.ValueType == "RP" &&
                                                                                                             s.ObservationIdentifier.identifier.ToUpper() == "PDF") != null;
            var hasEdObx = hl7GenericPathMessage.Order.Last().Observation.Last().Result.SingleOrDefault(s => s.ValueType == "ED" &&
                                                                                                             s.ObservationIdentifier.identifier.ToUpper() == "PDF") != null;
            if (hasRpObx && reportData == null)
            {
                throw new MessageValidationException("reportData must be provided for messages containing an RP type attachment");
            }
            if (hasEdObx && reportData != null)
            {
                throw new MessageValidationException("reportData cannot be provided for messages containing an ED type attachment");
            }
        }

        /// <summary>
        /// Validates the ordering provider.
        /// </summary>
        /// <param name="orderGroup">The order group.</param>
        /// <exception cref="MessageValidationException">
        /// Message must contain a valid Ordering Provider
        /// or
        /// Message contains more than one Ordering Provider
        /// </exception>
        private static void ValidateOrderingProvider(OrderGroup orderGroup)
        {
            var observations = orderGroup.Observation.Where(o => o.ObservationsReportID.UniversalServiceID.identifier != TransformerConstants.ReportText).ToArray();

            if (observations.Any(i => string.IsNullOrEmpty(i.ObservationsReportID.OrderingProvider?.IDnumberST) || i.ObservationsReportID.OrderingProvider.familylastname == null))
            {
                throw new MessageValidationException("Message must contain a valid Ordering Provider");
            }
            
            ObservationGroup[] distinctOrderingProviders = observations.GroupBy(
                i => $"{i.ObservationsReportID.OrderingProvider.IDnumberST} {i.ObservationsReportID.OrderingProvider.familylastname.familyname} {i.ObservationsReportID.OrderingProvider.givenname} {i.ObservationsReportID.OrderingProvider.prefix}",
                (key, group) => group.First()).ToArray();

            if (distinctOrderingProviders.Length > 1)
            {
                throw new MessageValidationException("Message contains more than one Ordering Provider");
            }
        }
    }
}