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
using JetBrains.Annotations;
using Nehta.VendorLibrary.Common;
using System.Collections.Generic;
using Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary.Interfaces;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(Participation))]
    [KnownType(typeof(CodableText))]
    internal class Context : Common.Context, IEDischargeSummaryContext
    {
        #region Properties

        /// <summary>
        /// The Facility
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IParticipationFacility Facility { get; set; }

        /// <summary>
        /// The CareSetting
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ICodableText CareSetting { get; set; }

        /// <summary>
        /// The HealthEventIdentifications
        /// </summary>
        [CanBeNull]
        [DataMember]
        public HealthEventIdentification HealthEventIdentification { get; set; }

        /// <summary>
        /// Subject Of Care
        /// </summary>
        [CanBeNull]
        [DataMember]
        IParticipationSubjectOfCare IEDischargeSummaryContext.SubjectOfCare { get; set; }

        /// <summary>
        /// Discharge Summary Author
        /// </summary>
        [CanBeNull]
        [DataMember]
        IParticipationDocumentAuthor IEDischargeSummaryContext.Author { get; set; }

        #endregion

        #region Constructors
        internal Context()
        {
        }
        #endregion

        #region Validation

        void IEDischargeSummaryContext.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            var subjectOfCare = ((IEDischargeSummaryContext) this).SubjectOfCare;

            var author = ((IEDischargeSummaryContext)this).Author;

            if (vb.ArgumentRequiredCheck("Author", author))
            {
                if (author != null)
                {
                    author.Validate(vb.Path + "Author", vb.Messages);
                    vb.ArgumentRequiredCheck("AuthorParticipationPeriodOrDateTimeAuthored", author.AuthorParticipationPeriodOrDateTimeAuthored);
                }
            }

            if (vb.ArgumentRequiredCheck("SubjectOfCare", subjectOfCare))
            {
                if (subjectOfCare != null)
                {
                    subjectOfCare.Validate(vb.Path + "SubjectOfCare", vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("Facility", Facility))
            {
                if (Facility != null) Facility.Validate(vb.Path + "Facility", vb.Messages);
            }

            if (HealthEventIdentification != null)
            {
                HealthEventIdentification.Validate(vb.Path + "HealthEventIdentification", vb.Messages);
            }
        }
        #endregion
    }
}