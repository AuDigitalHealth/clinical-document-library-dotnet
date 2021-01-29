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
  /// Physical Measurement Document Type
    /// </summary>
    public enum PhysicalMeasurementsDocumentType
    {
      /// <summary>
      /// Consumer Entered Measurements
      /// </summary>
      [Name(Name = "Consumer Entered Measurements", CodeSystem = "NCTIS", Code = "100.16870", Identifier = "1.2.36.1.2001.1001.100.1002.208", Version = "1.0", Title = "Consumer Entered Measurements")]
      ConsumerEnteredMeasurements,

      /// <summary>
      /// Healthcare Provider Entered Measurements
      /// </summary>
      [Name(Name = "Healthcare Provider Entered Measurements", CodeSystem = "NCTIS", Code = "100.16871", Identifier = "1.2.36.1.2001.1001.100.1002.209", Version = "1.0", Title = "Healthcare Provider Entered Measurements")]
      HealthcareProviderEnteredMeasurements,

      /// <summary>
      /// Physical Measurements View
      /// </summary>
      [Name(Name = "Physical Measurements View", CodeSystem = "NCTIS", Code = "100.16872", Identifier = "1.2.36.1.2001.1001.100.1002.210", Version = "1.0", Title = "Physical Measurements View")]
      PhysicalMeasurementsView,
    }
}
