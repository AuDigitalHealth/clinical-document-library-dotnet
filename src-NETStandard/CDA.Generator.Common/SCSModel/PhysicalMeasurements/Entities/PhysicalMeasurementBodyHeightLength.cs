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
    /// This class is designed to encapsulate the properties within a CDA document that make up an Physical Measurement Body Weight
    /// </summary>
    [Serializable]
    [DataContract]
    public class PhysicalMeasurementBodyHeightLength
    {
        #region Properties

        /// <summary>
        /// Weight
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity HeightLength { get; set; }

        /// <summary>
        /// Weight Normal Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public HL7ObservationInterpretationNormality? HeightLengthNormalStatus { get; set; }

        /// <summary>
        /// Weight Reference Range Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ReferenceRangeDetails> HeightLengthReferenceRangeDetails { get; set; }

        /// <summary>
        /// Comment (Measurement Comment) 
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Position { get; set; }

        /// <summary>
        /// Confounding Factor
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<string> ConfoundingFactor { get; set; }

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
        /// Body Weight Date Time
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime BodyHeightLengthDateTime { get; set; }

        /// <summary>
        /// Body Weight Instance Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier BodyHeightLengthInstanceIdentifier { get; set; }

        ///// <summary>
        ///// Detailed Clinical Model Identifier
        ///// </summary>
        //[CanBeNull]
        //[DataMember]
        //public Identifier DetailedClinicalModelIdentifier { get; set; }

        #endregion

        #region Constructors

        internal PhysicalMeasurementBodyHeightLength()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this PhysicalMeasurementBodyHeightLength item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("HeightLength", HeightLength))
            {
               HeightLength.Validate(vb.Path + "HeightLength", vb.Messages);
            }

            if (HeightLengthReferenceRangeDetails != null)
            {
              for (var x = 0; x < HeightLengthReferenceRangeDetails.Count; x++)
              {
                 HeightLengthReferenceRangeDetails[x].Validate(vb.Path + string.Format("HeightLengthReferenceRangeDetails[{0}]", x), vb.Messages);

                 if (HeightLengthReferenceRangeDetails[x].ReferenceRangeMeaning != null && HeightLengthReferenceRangeDetails[x].ReferenceRangeMeaning.Code != "260395002" && HeightLengthReferenceRangeDetails[x].ReferenceRangeMeaning.DisplayName != "normal range" &&
                     HeightLengthReferenceRangeDetails[x].ReferenceRangeMeaning.CodeSystemCode != CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(d => d.Code) &&
                     HeightLengthReferenceRangeDetails[x].ReferenceRangeMeaning.CodeSystemName != CodingSystem.SNOMED.GetAttributeValue<NameAttribute, string>(d => d.Name))
                 {
                   vb.AddValidationMessage(path + ".HeightLengthReferenceRangeDetails", null, "Height/Length Range Meaning must have a SNOMED CT code of 260395002 with a display name of 'normal range'");
                 }
              }
            }

            //if (ConfoundingFactor != null)
            //{
              // Leave this in incase the type changes back to CD
              //for (var x = 0; x < ConfoundingFactor.Count; x++)
              //{
              //   ConfoundingFactor[x].Validate(vb.Path + string.Format("ConfoundingFactor[{0}]", x), vb.Messages);
              //}
            //}

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

            vb.ArgumentRequiredCheck("BodyHeightLengthDateTime", BodyHeightLengthDateTime);

            if (vb.ArgumentRequiredCheck("BodyHeightLengthInstanceIdentifier", BodyHeightLengthInstanceIdentifier))
            {
               BodyHeightLengthInstanceIdentifier.Validate(vb.Path + "BodyHeightLengthInstanceIdentifier", vb.Messages);
            }
        }

        #endregion
    }
}