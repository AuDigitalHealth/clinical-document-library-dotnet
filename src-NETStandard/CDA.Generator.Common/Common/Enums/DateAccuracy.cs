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
    /// Date Accuracy
    /// </summary>
    [Serializable]
    [DataContract]
    public enum DateAccuracy
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        [EnumMember]
        //[Name(Code = "Undefined", Name = "Undefined")]
        [Name(Code = "Undefined")]
        Undefined,

        /// <summary>
        /// Accurate Date
        /// </summary>
        [EnumMember]
        //[Name(Code = "AAA", Name = "Accurate date")]
        [Name(Code = "AAA")]
        AAA,   

        /// <summary>
        /// Estimated Date
        /// </summary>
        [EnumMember]
        //[Name(Code = "EEE", Name = "Estimated date")]
        [Name(Code = "EEE")]
        EEE,    

        /// <summary>
        /// Unknown Date
        /// </summary>
        [EnumMember]
        //[Name(Code = "UUU", Name = "Unknown date")]
        [Name(Code = "UUU")]
        UUU,   
        
        /// <summary>
        /// Acurrate Day,      Acurrate Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AAE", Name = "Accurate day and month, estimated year")]
        [Name(Code = "AAE")]
        AAE,
    
        /// <summary>
        /// Acurrate Day,      Estimate Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AEA", Name = "Accurate day, estimated month, accurate year")]
        [Name(Code = "AEA")]
        AEA, 

        /// <summary>
        /// Accurate Day,      Accurate Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AAU", Name = "Accurate day and month, unknown year")]
        [Name(Code = "AAU")]
        AAU,

        /// <summary>
        /// Accurate Day,      Unknown Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AUA", Name = "Accurate day, unknown month, accurate year")]
        [Name(Code = "AUA")]
        AUA,

        /// <summary>
        /// Accurate Day,      Estimated Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AEE", Name = "Accurate day, estimated month and year")]
        [Name(Code = "AEE")]
        AEE,

        /// <summary>
        /// Accurate Day,      Unknown Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AUU", Name = "Accurate day, unknown month and year")]
        [Name(Code = "AUU")]
        AUU,

        /// <summary>
        /// Accurate Day,      Estimated Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AEU", Name = "Accurate day, estimated month, unknown year")]
        [Name(Code = "AEU")]
        AEU,

        /// <summary>
        /// Accurate Day,      Unknown Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "AUE", Name = "Accurate day, unknown month")]
        [Name(Code = "AUE")]
        AUE,

        /// <summary>
        /// Estimated Day,      Estimated Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EEA", Name = "Estimated day and month, accurate year")]
        [Name(Code = "EEA")]
        EEA,

        /// <summary>
        /// Estimated Day,      Accurate Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EEA", Name = "Estimated day and year, accurate month")]
        [Name(Code = "EAE")]
        EAE,

        /// <summary>
        /// Estimated Day,      Estimated Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EEU", Name = "Estimated day and month, unknown year")]
        [Name(Code = "EEU")]
        EEU,

        /// <summary>
        /// Estimated Day,      Unknown Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EUE", Name = "Estimated day, unknown month, estimated year")]
        [Name(Code = "EUE")]
        EUE,

        /// <summary>
        /// Estimated Day,      Accurate Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EAA", Name = "Estimated day, accurate month and year")]
        [Name(Code = "EAA")]
        EAA,

        /// <summary>
        /// Estimated Day,      Unknown Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EUU", Name = "Estimated day, unknown month and year")]
        [Name(Code = "EUU")]
        EUU,

        /// <summary>
        /// Estimated Day,      Accurate Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EAU", Name = "Estimated day, accurate month, unknown year")]
        [Name(Code = "EAU")]
        EAU,

        /// <summary>
        /// Estimated Day,      Unknown Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "EUA", Name = "Estimated day, unknown month, accurate year")]
        [Name(Code = "EUA")]
        EUA,

        /// <summary>
        /// Unknown Day,      Unknown Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UUA", Name = "Unknown day and month, accurate year")]
        [Name(Code = "UUA")]
        UUA,

        /// <summary>
        /// Unknown Day,      Accurate Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UAU", Name = "Unknown day, accurate month, unknown year")]
        [Name(Code = "UAU")]
        UAU,

        /// <summary>
        /// Unknown Day,      Unknown Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UUE", Name = "")]
        [Name(Code = "UUE")]
        UUE,

        /// <summary>
        /// Unknown Day,      Estimated Month,          Unknown Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UEU", Name = "Unknown day, estimated month, unknown year")]
        [Name(Code = "UEU")]
        UEU,

        /// <summary>
        /// Unknown Day,      Accurate Month,         Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UAA", Name = "Unknown day, accurate month and year")]
        [Name(Code = "UAA")]
        UAA,

        /// <summary>
        /// Unknown Day,      Estimated Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UEE", Name = "Unknown day, estimated month and year")]
        [Name(Code = "UEE")]
        UEE,

        /// <summary>
        /// Unknown Day,      Accurate Month,          Estimated Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UAE", Name = "Unknown day, accurate month, estimated year")]
        [Name(Code = "UEA")]
        UAE,

        /// <summary>
        /// Unknown Day,      Estimated Month,          Accurate Year
        /// </summary>
        [EnumMember]
        //[Name(Code = "UEA", Name = "Unknown day, estimated month, accurate year")]
        [Name(Code = "UEA")]
        UEA
    }
}
