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
    /// Unit Of Measure
    /// </summary>
    public enum UnitOfMeasure
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        Undefined,

        /// <summary>
        /// Grade 
        /// </summary>
        [Name(Name = "grade", Code = "gon")]
        Grade,

        /// <summary>
        /// Grade 
        /// </summary>
        [Name(Name = "grade", Code = "gon")]
        Gon,

        /// <summary>
        /// Degree 
        /// </summary>
        [Name(Name = "degree", Code = "deg")]
        Degree,

        /// <summary>
        /// Liter  
        /// </summary>
        [Name(Name = "liter ", Code = "L")]
        Liter,

        /// <summary>
        /// liter  
        /// </summary>
        [Name(Name = "liter ", Code = "l")]
        liter,

        /// <summary>
        /// Are   
        /// </summary>
        [Name(Name = "are", Code = "ar")]
        Are,

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

        /// <summary>
        /// Tonne  
        /// </summary>
        [Name(Name = "tonne", Code = "t")]
        Tonne,

        /// <summary>
        /// Bar   
        /// </summary>
        [Name(Name = "bar", Code = "bar")]
        Bar,

        /// <summary>
        /// Unified atomic mass unit 
        /// </summary>
        [Name(Name = "unified atomic mass unit", Code = "u")]
        UnifiedAtomicMassUnit,

        /// <summary>
        /// Electron volt
        /// </summary>
        [Name(Name = "electronvolt", Code = "eV")]
        Electronvolt,

        /// <summary>
        /// Astronomic unit 
        /// </summary>
        [Name(Name = "astronomic unit", Code = "AU")]
        AstronomicUnit,

        /// <summary>
        /// Parsec 
        /// </summary>
        [Name(Name = "parsec", Code = "pc")]
        Parsec,
    }
}
