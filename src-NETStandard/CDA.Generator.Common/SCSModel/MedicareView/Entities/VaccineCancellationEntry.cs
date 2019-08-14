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
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.Common;

namespace CDA.Generator.Common.SCSModel.MedicareOverview.Entities
{
  /// <summary>
  /// Models a Vaccine Cancellation Entry for Medciare Overview  
  /// </summary>
  public class VaccineCancellationEntry
  {
     #region Properties

     /// <summary>
     /// Vaccine Type
     /// </summary>
     [CanBeNull]
     [DataMember]
     public ICodableText VaccineType { get; set; }

     /// <summary>
     /// A list of Medicare Antigen Code's
     /// </summary>
     [CanBeNull]
     [DataMember]
     public List<ICodableText> MedicareAntigenCode { get; set; }

     /// <summary>
     /// Vaccine Dose Number
     /// </summary>
     [CanBeNull]
     [DataMember]
     public int? VaccineDoseNumber { get; set; }

     /// <summary>
     /// Date Vaccination Cancelled
     /// </summary>
     [CanBeNull]
     [DataMember]
     public ISO8601DateTime DateVaccinationCancelled { get; set; }

    #endregion

      #region Constructors
      internal VaccineCancellationEntry()
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


        if (vb.ArgumentRequiredCheck("VaccineType", VaccineType))
        {
          VaccineType.Validate("VaccineType", messages);
        }

        if (MedicareAntigenCode != null)
        {
          for (var x = 0; x < MedicareAntigenCode.Count; x++)
            MedicareAntigenCode[x].Validate(vb.Path + string.Format("MedicareAntigenCode[{0}]", x), vb.Messages);
        }
      }

      #endregion

  }
}
