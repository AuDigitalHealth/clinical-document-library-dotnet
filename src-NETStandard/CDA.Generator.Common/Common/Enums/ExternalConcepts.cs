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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
  /// <summary>
  /// External Concepts
  /// </summary>
  [Serializable]
  [DataContract]
  public enum ExternalConcepts
  {
    /// <summary>
    /// Individual Medicare Card Number
    /// </summary>
    [EnumMember]
    [Name(Code = "1.2.36.1.2001.1005.36", Name = "Medview prescription item identifier", CodeSystem = "NCTISExternalConcepts")]
    MedviewPrescriptionItemIdentifier,

    /// <summary>
    /// Australian PBS Manufacturer Code
    /// </summary>
    [EnumMember]
    [Name(Code = "1.2.36.1.2001.1005.23", Name = "Australian PBS Manufacturer Code", CodeSystem = "NCTISExternalConcepts")]
    AustralianPBSManufacturerCode,

    /// <summary>
    /// MedView transaction identifier
    /// </summary>
    [EnumMember]
    [Name(Code = "1.2.36.1.2001.1005.35", Name = "Australian PBS Manufacturer Code", CodeSystem = "NCTISExternalConcepts")]
    MedViewTransactionIdentifier,

    /// <summary>
    /// Barwon Prescribe Transaction Identifier 
    /// </summary>
    [EnumMember]
    [Name(Code = "1.2.36.1.2001.1005.28.1.3", Name = "Australian PBS Manufacturer Code", CodeSystem = "NCTISExternalConcepts")]
    BarwonPrescribeTransactionIdentifier 

  }
}
