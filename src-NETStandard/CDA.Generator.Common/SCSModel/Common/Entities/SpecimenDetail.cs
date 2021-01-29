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
using System.Linq;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an specimen detail
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(CodableText))]
    public class SpecimenDetail
    {
        #region Properties
        /// <summary>
        /// The specimen tissue type
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText SpecimenTissueType { get; set; }

        /// <summary>
        /// The collection procedure
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText CollectionProcedure { get; set; }

        /// <summary>
        /// The anatomical site
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<AnatomicalSite> AnatomicalSite { get; set; }

        /// <summary>
        /// The physical details
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<PhysicalDetails> PhysicalDetails { get; set; }

        /// <summary>
        /// Physical description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string PhysicalDescription { get; set; }

        // COLLECTION AND HANDLING

        /// <summary>
        /// The sampling pre-conditions
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText SamplingPreconditions { get; set; }

        // HANDLING AND PROCESSING

        /// <summary>
        /// The Date / Time of the collection
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime CollectionDateTime { get; set; }

        /// <summary>
        /// The collection setting
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string CollectionSetting { get; set; }

        /// <summary>
        /// The Date / Time the specimen was received
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime ReceivedDateTime { get; set; }

        // IDENTIFIERS

        /// <summary>
        /// The specimen ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InstanceIdentifier SpecimenIdentifier { get; set; }

        /// <summary>
        /// The parent specimen ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InstanceIdentifier ParentSpecimenIdentifier { get; set; }

        /// <summary>
        /// The container ID
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InstanceIdentifier ContainerIdentifier { get; set; }

        #endregion

        #region Constructors
        internal SpecimenDetail()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this specimen detail
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var validationBuilder = new ValidationBuilder(path, messages);

            validationBuilder.ArgumentRequiredCheck("CollectionDateTime", CollectionDateTime);

            if(AnatomicalSite != null && AnatomicalSite.Any())
            {
                AnatomicalSite.ForEach(anatomicalSite => anatomicalSite.Validate(path + ".AnatomicalSite", messages));
            }

            if (PhysicalDetails != null && PhysicalDetails.Any())
            {
                PhysicalDetails.ForEach(physicalDetail => physicalDetail.Validate(path + ".PhysicalDetails", messages));

                if (PhysicalDetails.Count > 1)
                {
                    validationBuilder.AddValidationMessage(path + ".physicalDetails", null, "Only one physical details item can be specified");
                }
            }

            if (SpecimenTissueType != null)
            {
                SpecimenTissueType.Validate(path + ".SpecimenTissueType", messages);
            }

            if (CollectionProcedure != null)
            {
                CollectionProcedure.Validate(path + ".CollectionProcedure", messages);
            }

            if (SamplingPreconditions != null)
            {
                SamplingPreconditions.Validate(path + ".SamplingPreconditions", messages);
            }

            if (SpecimenIdentifier != null)
            {
                SpecimenIdentifier.Validate(path + ".SpecimenIdentifier", messages);
            }

            if (ParentSpecimenIdentifier != null)
            {
                ParentSpecimenIdentifier.Validate(path + ".ParentSpecimenIdentifier", messages);
            }

            if (ContainerIdentifier != null)
            {
                ContainerIdentifier.Validate(path + ".ContainerIdentifier", messages);
            }
        }

      /// <summary>
      /// Validates this specimen detail with a PIT narrative
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      /// <param name="xPreNarrative">Indicate whether an xPreNarrative is used for this validation</param>
      public void Validate(string path, List<ValidationMessage> messages, Boolean xPreNarrative)
        {
            var validationBuilder = new ValidationBuilder(path, messages);
            Validate(path, messages);

            if (xPreNarrative)
            {
                if (AnatomicalSite != null && AnatomicalSite.Any())
                {
                    foreach (var anatomicalSite in AnatomicalSite)
                    {
                        if (anatomicalSite.Images != null && anatomicalSite.Images.Any())

                            foreach (var images in anatomicalSite.Images)
                            {
                                if (images != null)
                                    validationBuilder.AddValidationMessage(path + ".Images", null, "AnatomicalSite Images can not be included where PathologyTestResult - xPreNarrative is specified");
                            }
                    }
                }

                if (PhysicalDetails != null && PhysicalDetails.Any())
                {
                    foreach (var physicalDetails in PhysicalDetails)
                    {
                        if (physicalDetails.Image != null)
                        {
                            validationBuilder.AddValidationMessage(path + ".Images", null, "PhysicalDetails - Image can not be included where PathologyTestResult - xPreNarrative is specified");
                        }
                    }
                }
            }
        }

        #endregion

    }
}