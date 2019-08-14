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
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.CDAModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.ServiceReferral.Interfaces;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up a Service  
    /// </summary>
    [Serializable]
    [DataContract]
    public class Service : ICurrentService, IPendingDiagnosticInvestigation, IRequestedService
    {
        #region Properties

        /// <summary>
        /// Reason For Service
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ReasonForService { get; set; }

        /// <summary>
        /// Reason for Service Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ReasonForServiceDescription { get; set; }

        /// <summary>
        /// Service Category
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ServiceCategory { get; set; }

        /// <summary>
        /// Service Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText ServiceDescription { get; set; }

        /// <summary>
        /// Request Urgency
        /// </summary>
        [DataMember]
        public bool? RequestUrgency { get; set; }

        /// <summary>
        /// Request Urgency Notes
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string RequestUrgencyNotes { get; set; }

        /// <summary>
        /// Service Scheduled
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval ServiceCommencementWindow { get; set; }

        /// <summary>
        /// Service Booking Status
        /// </summary>
        [DataMember]
        public EventTypes ServiceBookingStatus { get; set; }

        /// <summary>
        ///  The Service Provider
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationPersonOrOrganisation ServiceProvider { get; set; }

        /// <summary>
        /// Request Validity Period
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval RequestValidityPeriod { get; set; }

        /// <summary>
        /// DateTime Service Scheduled
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateTimeServiceScheduled { get; set; }

        /// <summary>
        /// Service Comment
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ServiceComment { get; set; }

        /// <summary>
        /// Subject of Care Instruction Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IList<string> SubjectOfCareInstructionDescription { get; set; }

        /// <summary>
        /// Requested Service DateTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime RequestedServiceDateTime { get; set; }

        #endregion

        #region Constructors
        internal Service()
        {

        }
        #endregion

        #region Validation

        void IRequestedService.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ReasonForService != null)
            {
                ReasonForService.Validate(vb.Path + "ReasonForService", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ServiceCategory", ServiceCategory))
            {
                ServiceCategory.Validate(vb.Path + "ServiceCategory", vb.Messages);
            }

            if (ServiceDescription != null)
            {
                ServiceDescription.Validate(vb.Path + "ServiceDescription", vb.Messages);
            }

            if (ServiceCommencementWindow != null)
            {
                ServiceCommencementWindow.Validate(vb.Path + "ServiceCommencementWindow", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ServiceProvider", ServiceProvider))
            {
                ServiceProvider.Validate(vb.Path + "ServiceProvider", vb.Messages);
            }

            if (RequestValidityPeriod != null)
            {
                RequestValidityPeriod.Validate(vb.Path + "RequestValidityPeriod", vb.Messages);
            }

            vb.ArgumentRequiredCheck("RequestedServiceDateTime", RequestedServiceDateTime);
        }

        void ICurrentService.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ServiceBookingStatus != EventTypes.Event)
            {
                vb.AddValidationMessage(vb.PathName, string.Empty, "ServiceBookingStatus for CurrentService needs to be an EVENT");
            }

            if (ServiceCategory != null)
            {
                ServiceCategory.Validate(vb.Path + "ServiceCategory", vb.Messages);
            }

            if (ServiceDescription != null)
            {
                ServiceDescription.Validate(vb.Path + "ServiceDescription", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ServiceProvider", ServiceProvider))
            {
                ServiceProvider.Validate(vb.Path + "ServiceProvider", vb.Messages);
            }

            vb.ArgumentRequiredCheck("RequestedServiceDateTime", RequestedServiceDateTime);
        }

        void IPendingDiagnosticInvestigation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (ReasonForService != null)
            {
                ReasonForService.Validate(vb.Path + "ReasonForService", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("ServiceDescription", ServiceDescription))
            {
                ServiceDescription.Validate(vb.Path + "ServiceDescription", vb.Messages);
            }

            if (ServiceCommencementWindow != null)
            {
                ServiceCommencementWindow.Validate(vb.Path + "ServiceCommencementWindow", vb.Messages);
            }

            vb.ArgumentRequiredCheck("ServiceBookingStatus", ServiceBookingStatus);

            if (ServiceProvider != null)
            {
                ServiceProvider.Validate(vb.Path + "ServiceProvider", vb.Messages);
            }

            vb.ArgumentRequiredCheck("RequestedServiceDateTime", RequestedServiceDateTime);
        }
        
        #endregion
    }
}
