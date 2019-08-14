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

using Nehta.VendorLibrary.CDA.Common;

namespace Nehta.VendorLibrary.CDA.Generator.Enums
{
    /// <summary>
    /// HL7 Observation Interpretation Normality
    /// </summary>
    public enum HL7ObservationInterpretationNormality
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        Undefined,

        /// <summary>
        /// Abnormal
        /// </summary>
        [Name(Name = "Abnormal", Code = "A")]
        Abnormal,

        /// <summary>
        /// Abnormal Alert
        /// </summary>
        [Name(Name = "Abnormal alert", Code = "AA")]
        AbnormalAlert,

        /// <summary>
        /// High Alert
        /// </summary>
        [Name(Name = "High alert", Code = "HH")]
        HighAlert,

        /// <summary>
        /// Low Alert
        /// </summary>
        [Name(Name = "Low alert", Code = "LL")]
        LowAlert,

        /// <summary>
        /// High
        /// </summary>
        [Name(Name = "High", Code = "H")]
        High,

        /// <summary>
        /// Low
        /// </summary>
        [Name(Name = "Low", Code = "L")]
        Low,

        /// <summary>
        /// Normal
        /// </summary>
        [Name(Name = "Normal", Code = "N")]
        Normal,
    }
}
