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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// The address class contains both an international and Australia address, the class implements the 
    /// IAddress interface, which in turn inherits the IAddressAustralian and IAddressInternational interfaces
    /// 
    /// This object can be cast into to an IAddressAustralian to restrain and enforce it's usage as an Australian
    /// only address
    /// 
    /// This object can be cast into to an IAddressInternational to restrain and enforce it's usage as an International
    /// only address
    /// 
    /// This object can be cast into an IAddress if both an Australian and an International Address are required.
    /// </summary>
    [Serializable]
    [DataContract]
    internal class Address : IAddress
    {
        #region Properties
        /// <summary>
        ///  An enumeration representing a nullflavor for the No Fixed Address field
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AddressAbsentIndicator? AddressAbsentIndicator { get; set; }

        /// <summary>
        /// An international address
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InternationalAddress InternationalAddress { get; set; }

        /// <summary>
        /// An Australian address
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AustralianAddress AustralianAddress { get; set; }

        /// <summary>
        /// The address purpose, E.g. Business
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AddressPurpose AddressPurpose { get; set; }
        #endregion

        #region Constructors
        internal Address()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this address as an IAddress
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IAddress.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var addressChoice = new Dictionary<string, object>
                                    {
                                        { "AustralianAddress", AustralianAddress },
                                        { "InternationalAddress", InternationalAddress }
                                    };

            if (!AddressAbsentIndicator.HasValue)
            {
              if (vb.ChoiceCheck(addressChoice))
              {
                if (AustralianAddress != null)
                {
                  AustralianAddress.Validate(vb.Path + "AustralianAddress", messages);
                }
                else if (InternationalAddress != null)
                {
                  InternationalAddress.Validate(vb.Path + "InternationalAddress", messages);
                }

                //vb.NoMatchCheck("AddressPurpose", AddressPurpose, AddressPurpose.Undefined);
              }
            }

          if (!(path.ToLowerInvariant().Contains("consumerenterednotes.scscontext.subjectofcare.participant.addresses") 
                    || path.ToLowerInvariant().Contains("consumerenteredhealthsummary.scscontext.subjectofcare.participant.addresses") 
                    || path.ToLowerInvariant().Contains("advancedcaredirective.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("nswhealthcheckassessment.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("personalhealthobservation.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("consumerquestionnaire.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("birthdetailsrecord.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("consumerenteredachievements.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("consumerquestionnaire.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("nswhealthcheckassessment.scscontext.subjectofcare.participant.addresses")
                    || path.ToLowerInvariant().Contains("personalhealthobservation.scscontext.subjectofcare.participant.addresses")
                    )
                )
            {
                if (AddressAbsentIndicator != null)
                {
                    if (AddressAbsentIndicator == CDA.Common.Enums.AddressAbsentIndicator.NotIndicated || AddressAbsentIndicator == CDA.Common.Enums.AddressAbsentIndicator.NotKnown)
                    {
                        vb.AddValidationMessage(vb.PathName, AddressAbsentIndicator.ToString(), "SubjectOfCare.Participant.Address null Flavor can only be 'No Fixed Addresss Indicator' for this document type");
                    }
                }
            }
        }


        /// <summary>
        /// Validates this address as an IAddressAustralian
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IAddressAustralian.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("AustralianAddress", AustralianAddress);
        }

        /// <summary>
        /// Validates this address as an IAddressInterFnational
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IAddressInternational.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("InternationalAddress", InternationalAddress);
        }
        #endregion
    }
}
