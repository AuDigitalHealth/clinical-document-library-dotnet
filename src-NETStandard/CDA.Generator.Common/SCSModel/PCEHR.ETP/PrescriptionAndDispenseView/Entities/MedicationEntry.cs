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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel
{
    /// <summary>
    /// The MedicationEntry class contains all the properties that CDA has identified for 
    /// a MedicationEntry
    /// 
    /// Please use the MedicationEntries() method on the appropriate parent SCS object to 
    /// instantiate this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class MedicationEntry
    {
      #region Properties

      /// <summary>
      /// Dispense Item
      /// </summary>
      [CanBeNull]
      public IPCEHRDispenseItemView DispenseItem { get; set; }

      /// <summary>
      /// Prescription Item Link (LINK)
      /// </summary>
      [CanBeNull]
      public IPCEHRPrescriptionItemView PrescriptionItem { get; set; }

      #endregion

      #region Constructors
      internal MedicationEntry()
      {
          
      }
      #endregion

      #region Validation
      /// <summary>
      /// Validates this Medication Entry Item
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages to date, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (DispenseItem != null) DispenseItem.Validate(vb.Path + "DispenseItem", vb.Messages);

        if (PrescriptionItem != null) PrescriptionItem.Validate(vb.Path + "PrescriptionItem", vb.Messages);


        if ((DispenseItem != null && PrescriptionItem != null) || (DispenseItem == null && PrescriptionItem == null))
        {
          vb.AddValidationMessage(path + "MedicationEntry", null, "Exactly one 'Prescription Item (MEDICATION INSTRUCTION)' OR exactly one 'Dispense Item (MEDICAITON ACTION)' SHALL be present per instance of the parent choice ('MEDICATION ENTRIES')." );
        }

      }

      #endregion

    }
}