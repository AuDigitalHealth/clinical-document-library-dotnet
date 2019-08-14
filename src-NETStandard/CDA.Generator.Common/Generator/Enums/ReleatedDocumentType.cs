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
    /// Releated Document Type
    /// </summary>
    public enum ReleatedDocumentType 
    {
        /// <summary>
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
      /// <summary>
      /// Replace
      /// </summary>
      [Name(Name = "Replace", Code = "RPLC")]
      Replace,

      /// <summary>
      /// Transform
      /// </summary>
      [Name(Name = "Transform", Code = "XFRM")]
      Transform
    }
}
