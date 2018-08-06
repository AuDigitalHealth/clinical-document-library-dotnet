using System.Linq;
using DigitalHealth.Hl7ToCdaTransformer.Resources;
using DigitalHealth.HL7.Common.DataStructure;
using DigitalHealth.HL7.Common.Message;
using DigitalHealth.HL7.Common.SegmentGroup;

namespace DigitalHealth.Hl7ToCdaTransformer.Services
{
    /// <summary>
    /// Base class for validator functionality.
    /// </summary>
    internal class MessageValidatorBase
    {
        /// <summary>
        /// Validates the patient identification.
        /// </summary>
        /// <param name="hl7GenericMessage">The HL7 generic message.</param>
        /// <exception cref="MessageValidationException">PatientIdentification</exception>
        internal static void ValidatePatientIdentification(HL7GenericMessage hl7GenericMessage)
        {
            if (hl7GenericMessage.PatientIdentification == null)
            {
                throw new MessageValidationException(nameof(hl7GenericMessage.PatientIdentification));
            }
        }

        /// <summary>
        /// Validate the principal result interpreter.
        /// </summary>
        /// <param name="principalResultInterpreter">Principal result interpreter</param>
        /// <exception cref="MessageValidationException"/>
        internal static void ValidatePrincipalResultInterpreter(NDL principalResultInterpreter)
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
        /// <param name="hl7GenericMessage">The HL7 generic message.</param>
        /// <param name="reportData">The report attachment data.</param>
        /// <exception cref="MessageValidationException">Message does not contain an embedded PDF report</exception>
        internal static void ValidateAttachment(HL7GenericMessage hl7GenericMessage, byte[] reportData)
        {
            var hasRpObx = hl7GenericMessage.Order.Last().Observation.Last().Result.SingleOrDefault(s => s.ValueType == "RP" &&
                                                                                                         s.ObservationIdentifier.identifier.ToUpper() == "PDF") != null;
            var hasEdObx = hl7GenericMessage.Order.Last().Observation.Last().Result.SingleOrDefault(s => s.ValueType == "ED" &&
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
        internal static void ValidateOrderingProvider(OrderGroup orderGroup)
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
