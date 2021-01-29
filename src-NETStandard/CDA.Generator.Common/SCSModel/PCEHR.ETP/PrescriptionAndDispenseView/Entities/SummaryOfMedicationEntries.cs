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
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The SummaryOfMedicationEntries class contains all the properties that CDA has identified for 
    /// a Summary Of Medication Entries item
    /// 
    /// Please use the SummaryOfMedicationEntries() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class SummaryOfMedicationEntries
    {
      #region Properties

      /// <summary>
      /// Therapeutic Good Identification
      /// </summary>
      [CanBeNull]
      public ICodableText TherapeuticGoodId { get; set; }

      /// <summary>
      /// DateTime Prescription Written
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateTimeOfEarliestDispenseEvent { get; set; }

      /// <summary>
      /// DateTime Prescription Written
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateTimePrescriptionWritten { get; set; }

      /// <summary>
      /// DateTime of Latest Dispense Event
      /// </summary>
      [CanBeNull]
      public ISO8601DateTime DateTimeOfLatestDispenseEvent { get; set; }

      /// <summary>
      /// Total Number of Known Supplies
      /// </summary>
      [CanBeNull]
      public int? TotalNumberOfKnownSupplies { get; set; }

      /// <summary>
      /// Maximum Number of Permitted Supplies
      /// </summary>
      [CanBeNull]
      public int? MaximumNumberOfPermittedSupplies { get; set; }

      #endregion

      #region Constructors
      internal SummaryOfMedicationEntries()
      {
      }
      #endregion

      #region Validation
      /// <summary>
      /// Validates this Summary Of Medication Entries
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (vb.ArgumentRequiredCheck("TherapeuticGoodId", TherapeuticGoodId))
        {
          TherapeuticGoodId.Validate(vb.Path + "TherapeuticGoodId", vb.Messages);
        }

      }
      #endregion

    }
}