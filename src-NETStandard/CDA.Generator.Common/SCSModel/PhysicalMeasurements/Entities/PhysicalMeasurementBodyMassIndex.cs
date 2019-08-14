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
    /// This class is designed to encapsulate the properties within a CDA document that make up an Physical Measurement Body Mass Index
    /// </summary>
    [Serializable]
    [DataContract]
    public class PhysicalMeasurementBodyMassIndex
    {
        #region Properties

        /// <summary>
        /// Body Mass Index
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Quantity BodyMassIndex { get; set; }

        /// <summary>
        /// Body Mass Index Normal Status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public HL7ObservationInterpretationNormality? BodyMassIndexNormalStatus { get; set; }

        /// <summary>
        /// Body Mass Index Reference Range Details
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ReferenceRangeDetails> BodyMassIndexReferenceRangeDetails { get; set; }

        /// <summary>
        /// Comment (BMI Observation Note)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// Method (BMI Entry Method)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText Method { get; set; }

        /// <summary>
        /// Formula (BMI Calculation Formula)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string Formula { get; set; }

        /// <summary>
        /// Device
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IInformationProviderCollection InformationProvider { get; set; }

        /// <summary>
        /// Body Mass Index DateTime
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime BodyMassIndexDateTime { get; set; }

        /// <summary>
        /// Body Mass Index Instance Identifier
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Identifier BodyMassIndexInstanceIdentifier { get; set; }

        #endregion

        #region Constructors

        internal PhysicalMeasurementBodyMassIndex()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this PhysicalMeasurementBodyMassIndex item
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("BodyMassIndex", BodyMassIndex))
          {
             BodyMassIndex.Validate(vb.Path + "BodyMassIndex", vb.Messages);
          }

          if (BodyMassIndexReferenceRangeDetails != null)
          {
            for (var x = 0; x < BodyMassIndexReferenceRangeDetails.Count; x++)
            {
              BodyMassIndexReferenceRangeDetails[x].Validate(vb.Path + string.Format("BodyMassIndexReferenceRangeDetails[{0}]", x), vb.Messages);
            }
          }

          if (Method != null)
          {
            Method.Validate(vb.Path + "Method", vb.Messages);
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

          vb.ArgumentRequiredCheck("BodyMassIndexDateTime", BodyMassIndexDateTime);

          if (vb.ArgumentRequiredCheck("BodyMassIndexInstanceIdentifier", BodyMassIndexInstanceIdentifier))
          {
            BodyMassIndexInstanceIdentifier.Validate(vb.Path + "BodyMassIndexInstanceIdentifier", vb.Messages);
          }
        }

        #endregion
    }
}