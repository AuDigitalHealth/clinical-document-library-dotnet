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
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Pathology
{
  /// <summary>
  /// This class encapsulates the Result Group
  /// </summary>
  public class ResultGroup
  {
    /// <summary>
    /// Pathology Test Result GroupName
    /// </summary>
    [CanBeNull]
    [DataMember]
    public ICodableText PathologyTestResultGroupName { get; set; }

    /// <summary>
    /// Result
    /// </summary>
    [CanBeNull]
    [DataMember]
    public IList<Result> Result { get; set; }

    #region Constructors
    internal ResultGroup()
    {
            
    }
    #endregion
    
    /// <summary>
    /// Validate the Content for this Result Group
    /// </summary>
    /// <param name="path">The path to this object as a string</param>
    /// <param name="messages">the validation messages, these may be added to within this method</param>
    public void Validate(string path, List<ValidationMessage> messages)
    {
      var vb = new ValidationBuilder(path, messages);

      if (PathologyTestResultGroupName != null)
      {
         PathologyTestResultGroupName.Validate(vb.Path + "PathologyTestResultGroupName", vb.Messages);
      }

      if (Result != null)
      {
        for (var x = 0; x < Result.Count; x++)
        {
           Result[x].Validate(vb.Path + string.Format("Result[{0}]", x), vb.Messages);
        }     
      }


    }
  }
}
