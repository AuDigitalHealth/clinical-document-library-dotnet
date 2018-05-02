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
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// a Requested Service  
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestedService
    {
        #region Properties

        /// <summary>
        /// Requested Service Description
        /// </summary>
        [CanBeNull] 
        [DataMember]
        public ICodableText RequestedServiceDescription { get; set; }

        /// <summary>
        /// Service Scheduled
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime ServiceScheduled { get; set; }

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
        /// Subject of Care Instruction Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string SubjectOfCareInstructionDescription { get; set; }

        /// <summary>
        /// Requested Service DateTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime RequestedServiceDateTime { get; set; }

        /// <summary>
        ///  The Service Provider Participation
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Provide a custom Narrative 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public StrucDocText CustomNarrativeRequestedService { get; set; }

        #endregion

        #region Constructors
        internal RequestedService()
        {

        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this RequestedService
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("RequestedServiceDateTime", RequestedServiceDateTime);

           if (ServiceCommencementWindow != null)
           {
                ServiceCommencementWindow.Validate(vb.Path + "ServiceCommencementWindow", vb.Messages);
           }

//            var choice1 = new Dictionary<string, object>
//                {
//                    { "ServiceCommencementWindow", ServiceCommencementWindow },
//                    { "ServiceScheduled", ServiceScheduled }
//                };
//
//            vb.ChoiceCheck(choice1);
//            
            if (vb.ArgumentRequiredCheck("RequestedServiceDescription", RequestedServiceDescription))
            {
                if (RequestedServiceDescription != null)
                {
                    RequestedServiceDescription.ValidateMandatory(vb.Path + "RequestedServiceDescription", vb.Messages);
                }
            }

            vb.ArgumentRequiredCheck("ServiceBookingStatus", ServiceBookingStatus);

            if (ServiceProvider != null)
            {
                ServiceProvider.Validate(vb.Path + "ServiceProvider", vb.Messages);
            }
        }

        #endregion
    }
}
