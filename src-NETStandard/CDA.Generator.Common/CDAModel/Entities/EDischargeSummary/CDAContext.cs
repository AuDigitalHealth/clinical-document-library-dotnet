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


namespace Nehta.VendorLibrary.CDA.SCSModel.DischargeSummary
{
    /// <summary>
    /// This class defines a CDA context, and implements interfaces that further constrain the context into
    /// a Discharge CDA implementation.
    /// </summary>
    public class CDAContext : CDAModel.CDAContext, ICDAContextEDischargeSummary
    {
        #region Constructors
        internal CDAContext()
        {
        }
        #endregion
    }
}

