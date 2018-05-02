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
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;
using System.Runtime.Serialization;

namespace CDA.Generator.Common.SCSModel.ConsumerAchievements.Entities
{
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up an Achievement.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Achievement
    {
        #region Properties

        /// <summary>
        /// An Achievement
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AchievementTopic { get; set; }

        /// <summary>
        /// Achievement Description
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String AchievementDescription { get; set; }

        /// <summary>
        /// InformationProvider
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationInformationProvider InformationProvider { get; set; }

        /// <summary>
        /// The Achievement Date
        /// </summary>
        [CanBeNull]
        public ISO8601DateTime AchievementDate { get; set; }

        #endregion

        #region Constructors
        internal Achievement()
        {
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validates the Achievement
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("AchievementDate", AchievementDate);

            if (vb.ArgumentRequiredCheck("InformationProvider", InformationProvider))
            {
               InformationProvider.Validate(vb.Path + "InformationProvider", vb.Messages);
            }
        }

        #endregion
    }
}
