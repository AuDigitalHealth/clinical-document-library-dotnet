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
using CDA.Generator.Common.SCSModel.PhysicalMeasurements.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an Head Circumference 
    /// </summary>
    [Serializable]
    [DataContract]
    public class HeadCircumference
    {
        #region Properties

        /// <summary>
        /// Name of Location
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText NameOfLocation { get; set; }

        /// <summary>
        /// Circumference
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity Circumference { get; set; }

        /// <summary>
        /// Circumference
        /// </summary>
        [CanBeNull]
        [DataMember]
        public HL7ObservationInterpretationNormality? CircumferenceNormalStatus { get; set; }

        /// <summary>
        /// Circumference Reference Range Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ReferenceRangeDetails> CircumferenceReferenceRangeDetails { get; set; }

        /// <summary>
        /// Comment (Measurement Comment)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// Confounding Factor
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ICodableText> ConfoundingFactor { get; set; }

        /// <summary>
        /// Device
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Device Device { get; set; }

        /// <summary>
        /// Device
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IInformationProviderCollection InformationProvider { get; set; }

        /// <summary>
        /// Body Part Circumference DateTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime BodyPartCircumferenceDateTime { get; set; }

        #endregion

        #region Constructors

        internal HeadCircumference()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this HeadCircumference Item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("NameOfLocation", NameOfLocation))
            {
               NameOfLocation.Validate(vb.Path + "NameOfLocation", vb.Messages);
            }

            if (vb.ArgumentRequiredCheck("Circumference", Circumference))
            {
               Circumference.Validate(vb.Path + "Circumference", vb.Messages);
            }

            if (CircumferenceReferenceRangeDetails != null)
            {
              for (var x = 0; x < CircumferenceReferenceRangeDetails.Count; x++)
              {
                CircumferenceReferenceRangeDetails[x].Validate(vb.Path + string.Format("CircumferenceReferenceRangeDetails[{0}]", x), vb.Messages);
              }
            }

            if (ConfoundingFactor != null)
            {
              for (var x = 0; x < ConfoundingFactor.Count; x++)
              {
                ConfoundingFactor[x].Validate(vb.Path + string.Format("ConfoundingFactor[{0}]", x), vb.Messages);
              }
            }

            vb.ArgumentRequiredCheck("BodyPartCircumferenceDateTime", BodyPartCircumferenceDateTime);

            if (Device != null)
            {
              Device.Validate(vb.Path + "Device", vb.Messages);
            }

            if (InformationProvider != null)
            {
              if (InformationProvider is Device)
              {
                var device = InformationProvider as Device;
                device.Validate(vb.Path + "Device", vb.Messages);
              }

              // Both types are of type Participation so use the Participant to determin the type 
              if (InformationProvider is Participation)
              {
                var informationProviderHealthcareProvider = InformationProvider as IParticipationInformationProviderHealthcareProvider;

                if (informationProviderHealthcareProvider.Participant != null)
                {
                  informationProviderHealthcareProvider.Validate(vb.Path + "IParticipationInformationProviderHealthcareProvider", vb.Messages);
                }

                var informationProviderNonHealthcareProvider = InformationProvider as IParticipationInformationProviderNonHealthcareProvider;

                if (informationProviderNonHealthcareProvider.Participant != null)
                {
                  informationProviderNonHealthcareProvider.Validate(vb.Path + "IParticipationInformationProviderNonHealthcareProvider", vb.Messages);
                }
              }
            }
        }

        #endregion
    }
}