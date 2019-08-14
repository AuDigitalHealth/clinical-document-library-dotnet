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

namespace CDA.Generator.Common.Common.Time.Enum
{
        /// <summary>
        /// OperationTypes
        /// </summary>
    public enum EventCodes
    {
        /// <summary>
        /// Before meal (from lat. ante cibus)
        /// </summary>
        [Name(Code = "AC", Name = "before meal")]
        BeforeMeal,

        /// <summary>
        /// Before breakfast (from lat. ante cibus matutinus)
        /// </summary>
        [Name(Code = "ACM", Name = "before breakfast")]
        BeforeBreakfast,

        /// <summary>
        /// Before lunch (from lat. ante cibus diurnus)
        /// </summary>
        [Name(Code = "ACD", Name = "before lunch")]
        BeforeLunch,

        /// <summary>
        /// At the hour of sleep.
        /// </summary>
        [Name(Code = "HS", Name = "hour of sleep")]
        HourOfSleep,

        /// <summary>
        /// Between meals (from lat. inter cibus)
        /// </summary>
        [Name(Code = "IC", Name = "between meals")]
        BetweenMeals,

        /// <summary>
        /// Between breakfast and lunch
        /// </summary>
        [Name(Code = "ICM", Name = "between breakfast and lunch")]
        BetweenBreakfastAndLunch,

        /// <summary>
        /// Between lunch and dinner
        /// </summary>
        [Name(Code = "ICD", Name = "between lunch and dinner")]
        BetweenLunchAndAinner,

        /// <summary>
        /// Between dinner and the hour of sleep
        /// </summary>
        [Name(Code = "ICD", Name = "between dinner and the hour of sleep")]
        BetweenDinnerAndTheHourOfSleep,

        /// <summary>
        /// After meal (from lat. post cibus)
        /// </summary>
        [Name(Code = "PC", Name = "after meals")]
        AfterMeals,

        /// <summary>
        /// After breakfast (from lat. post cibus matutinus)
        /// </summary>
        [Name(Code = "PCM", Name = "after breakfast")]
        AfterBreakfast,

        /// <summary>
        /// After lunch (from lat. post cibus diurnus)
        /// </summary>
        [Name(Code = "PCM", Name = "after lunch")]
        AfterLunch,

        /// <summary>
        /// After dinner (from lat. post cibus vespertinus)
        /// </summary>
        [Name(Code = "PCV", Name = "after dinner")]
        AfterDinner,
    }
}
