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
    /// Unit of measure for time.
    /// </summary>
    public enum TimeUnitOfMeasure
    {
        /// <summary>
        /// Minute    
        /// </summary>
        [Name(Name = "minute", Code = "min")]
        Minute,

        /// <summary>
        /// Hour    
        /// </summary>
        [Name(Name = "hour", Code = "h")]
        Hour,

        /// <summary>
        /// Day     
        /// </summary>
        [Name(Name = "day", Code = "d")]
        Day,

        /// <summary>
        /// Tropical Year  
        /// </summary>
        [Name(Name = "tropical year", Code = "a_t")]
        TropicalYear,

        /// <summary>
        /// Mean Julian year  
        /// </summary>
        [Name(Name = "mean Julian year", Code = "a_j")]
        MeanJulianYear,

        /// <summary>
        /// Mean Gregorian year  
        /// </summary>
        [Name(Name = "mean Gregorian year", Code = "a_g")]
        MeanGregorianYear,

        /// <summary>
        /// Year  
        /// </summary>
        [Name(Name = "year", Code = "a")]
        Year,

        /// <summary>
        /// Week   
        /// </summary>
        [Name(Name = "week", Code = "wk")]
        Week,

        /// <summary>
        /// Synodal Month    
        /// </summary>
        [Name(Name = "synodal month", Code = "mo_s")]
        SynodalMonth,

        /// <summary>
        /// Mean Julian Month
        /// </summary>
        [Name(Name = "mean Julian month", Code = "mo_j")]
        MeanJulianMonth,

        /// <summary>
        /// Mean Gregorian month 
        /// </summary>
        [Name(Name = "mean Gregorian month", Code = "mo_g")]
        MeanGregorianMonth,
        /// <summary>
        /// Month 
        /// </summary>
        [Name(Name = "month", Code = "mo")]
        Month,
    }
}
