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
    /// 10.22 SNOMED CT-AU: Pregnancy Indicator
    /// </summary>
    public enum PregnancyIndicator
    {
      /// <summary>
      /// Lightly clothed/underwear
      /// </summary>
      [Name(Name = "Patient currently pregnant (finding)", CodeSystem = "SNOMED", Code = "77386006")]
      PatientCurrentlyPregnant,

      /// <summary>
      /// Not pregnant (finding)
      /// </summary>
      [Name(Name = "Not pregnant (finding)", CodeSystem = "SNOMED", Code = "60001007")]
      NotPregnant,
 }
}
