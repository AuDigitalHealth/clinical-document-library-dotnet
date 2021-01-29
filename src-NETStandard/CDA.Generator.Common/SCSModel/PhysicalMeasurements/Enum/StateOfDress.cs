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
    /// NCTIS: State of Dress
    /// </summary>
    public enum StateOfDress
    {
      /// <summary>
      /// Lightly clothed/underwear
      /// </summary>
      [Name(Name = "Lightly clothed/underwear", CodeSystem = "NCTIS", Code = "1")]
      LightlyClothedUnderwear,

      /// <summary>
      /// Naked
      /// </summary>
      [Name(Name = "Naked", CodeSystem = "NCTIS", Code = "2")]
      Naked,

      /// <summary>
      /// Fully clothed, including shoes
      /// </summary>
      [Name(Name = "Fully clothed, including shoes", CodeSystem = "NCTIS", Code = "3")]
      FullyClothedIncludingShoes,

      /// <summary>
      /// Nappy/diaper
      /// </summary>
      [Name(Name = "Nappy/diaper", CodeSystem = "NCTIS", Code = "4")]
      NappyDiaper
 }
}
