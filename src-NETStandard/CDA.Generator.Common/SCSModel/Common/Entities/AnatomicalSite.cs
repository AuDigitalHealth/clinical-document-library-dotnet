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
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common.Entities;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// an anatomical site
    /// </summary>
    [Serializable]
    [DataContract]
    public class AnatomicalSite : IAnatomicalSite, IAnatomicalSiteExtended
    {
        #region Properties

        /// <summary>
        /// The name of the anatomical location
        /// </summary>
        [DataMember]
        public SpecificLocation SpecificLocation { get; set; }

        /// <summary>
        /// The description
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// The Image
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ExternalData> Images { get; set; }

        #endregion

        #region Constructors
        internal AnatomicalSite()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates this Anatomical Site
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if(SpecificLocation != null)
            {
                SpecificLocation.Validate(vb.Path + "AnatomicalLocation", messages);

                if (SpecificLocation.NameOfLocation != null && !SpecificLocation.NameOfLocation.HasCodeSystem)
                {
                    if (SpecificLocation.Side != null)
                    {
                        vb.AddValidationMessage(vb.PathName, null, "Can not have only a NameOfLocation (original text) and a Side");
                    }
                }
            }

            if (!Description.IsNullOrEmptyWhitespace())
            {
                if (SpecificLocation != null && SpecificLocation.Side != null)
                {
                    vb.AddValidationMessage(vb.PathName, null, "Can not have a description (original text) and a Side");
                }
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            if ((new StackTrace()).GetFrames().Any(t => t.GetMethod().Name.Contains(CDADocumentType.EventSummary.ToString())))
            {
                // Only for EventSummary at this point eventually the if statement above will be removed
                var choice1 = new Dictionary<string, object>
                {
                    { "AnatomicalLocation", SpecificLocation },
                    { "Description", Description }
                };

                vb.ChoiceCheck(choice1);
            };

            if(Images != null && Images.Any())
            {
                foreach (var image in Images)
                {
                    if (image != null)
                    {
                        if (
                            image.ExternalDataMediaType == MediaType.GIF ||
                            image.ExternalDataMediaType == MediaType.JPEG ||
                            image.ExternalDataMediaType == MediaType.PNG
                            )
                        {
                             image.Validate(vb.Path + "Images", messages);
                        } else
                        {
                            vb.AddValidationMessage(vb.PathName, null, "The image must be of a type GIF, JPEG or a PNG");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validates this IAnatomicalSiteExtended Site
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        void IAnatomicalSiteExtended.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (SpecificLocation != null)
            {
                SpecificLocation.Validate(vb.Path + "AnatomicalLocation", messages);

                var choice1 = new Dictionary<string, object>()
                {
                    { "AnatomicalLocation.NameOfLocation", SpecificLocation.NameOfLocation },
                    { "Description", Description }
                };

                var choice2 = new Dictionary<string, object>()
                {
                    { "AnatomicalLocation.Side", SpecificLocation.Side },
                    { "Description", Description }
                };

                vb.ChoiceCheck(choice1);
                vb.ChoiceCheck(choice2);
            }
        }
     

        #endregion
    }
}