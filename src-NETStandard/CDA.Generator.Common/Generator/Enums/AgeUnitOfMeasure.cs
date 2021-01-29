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
    public enum AgeUnitOfMeasure
    {
        /// <summary>
        /// year  
        /// </summary>
        [Name(Name = "year", Code = "a", Comment = "y")]
        Year,

        /// <summary>
        /// day     
        /// </summary>
        [Name(Name = "day", Code = "d", Comment = "d")]
        Day,

        /// <summary>
        /// week   
        /// </summary>
        [Name(Name = "week", Code = "wk", Comment = "w")]
        Week,

        /// <summary>
        /// month 
        /// </summary>
        [Name(Name = "month", Code = "mo", Comment = "m")]
        Month
    }
}
