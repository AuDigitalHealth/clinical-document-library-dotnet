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


using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models a Vaccine Cancellation Entry for Medciare Overview  
  /// </summary>
  public class VaccineCancellationReason
  {
     #region Properties

     /// <summary>
     /// Vaccine Type
     /// </summary>
     [CanBeNull]
     [DataMember]
      public VaccineCancellationReasonTypeValues? VaccineType { get; set; }

     /// <summary>
     /// Vaccine Type NullFlavours
     /// </summary>
     [CanBeNull]
     [DataMember]
     public NullFlavour? VaccineTypeNullFlavour { get; set; }

     /// <summary>
     /// Vaccine Cancellation Reason Period
     /// </summary>
     [CanBeNull]
     [DataMember]
     public CdaInterval VaccineCancellationReasonPeriod { get; set; }

     /// <summary>
     /// Vaccine Cancellation Reason Comment
     /// </summary>
     [CanBeNull]
     [DataMember]
     public string VaccineCancellationReasonComment { get; set; }


      #endregion

      #region Constructors
     internal VaccineCancellationReason()
      {
      }
      #endregion

      #region Validation

      /// <summary>
      /// Validate the SCS Content for the Vaccine Cancellation Entry Component
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param name="messages">the validation messages, these may be added to within this method</param>
      public void Validate(string path, List<ValidationMessage> messages)
      {
        var vb = new ValidationBuilder(path, messages);

        if (VaccineType.HasValue && VaccineTypeNullFlavour.HasValue)
        {
          vb.AddValidationMessage(vb.Path + "VaccineType or VaccineTypeNullFlavour", null, "lease specify only one VaccineType or VaccineTypeNullFlavour");
        }

        if (!VaccineType.HasValue && !VaccineTypeNullFlavour.HasValue)
        {
          vb.AddValidationMessage(vb.Path + "VaccineType or VaccineTypeNullFlavour", null, "Please specify only one VaccineType or VaccineTypeNullFlavour");
        }

        if (vb.ArgumentRequiredCheck("VaccineCancellationReasonPeriod", VaccineCancellationReasonPeriod))
        {
           VaccineCancellationReasonPeriod.Validate("VaccineCancellationReasonPeriod", messages);
        }

        vb.ArgumentRequiredCheck("VaccineCancellationReasonComment", VaccineCancellationReasonComment);
      }

      #endregion

  }
}
