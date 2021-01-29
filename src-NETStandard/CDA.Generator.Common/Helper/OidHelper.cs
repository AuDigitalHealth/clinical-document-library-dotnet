/*
 * Copyright 2012 NEHTA
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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Nehta.VendorLibrary.CDA.Generator.Helper
{
    /// <summary>
    /// A class to assist in generating oids
    /// </summary>
    public static class OIDHelper
    {
        /// <summary>
        /// The type of ID
        /// </summary>
        public enum IdType
        {
          /// <summary>
          /// The UUID
          /// </summary>
            Uuid,

            /// <summary>
            /// The oid
            /// </summary>
            Oid
        }

        /// <summary>
        /// Accepts a uuid and returns a string as an oid
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static string UuidToOid(string uuid)
        {
            IdType? idType;
            return UuidToOid(uuid, out idType);
        }       

        private static string UuidToOid(string uuid, out IdType? idType)
        {
            idType = null;

            // Example
            // UUID = a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1
            // OID = 2.25.N
            // Where N =  (2^96 * 0x a7b7c3b7) + (2^64 * 0x 463943a9) + (2^32 * 0x 8bb17cb8)) + 0x c91216c1 
            // Correct value  2.25.222935235211552455402395562399683974849

            string answer = uuid;

            // 0 start pos  01234567 9012 4567 9012 456789012345  for SubString
            //string uuid = "a7b7c3b7-4639-43a9-8bb1-7cb8c91216c1";
            // Remove unwanted chars if they exist
            uuid = uuid.Replace("urn:uuid:", "");

            if (Regex.IsMatch(uuid, "^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", RegexOptions.IgnoreCase))
            {
                idType = IdType.Uuid;
                uuid = uuid.Replace("-", "");

                //Convert hex (16) to decimal
                //Add "0" to beginning to make sure positive number returned
                answer = "2.25." + System.Numerics.BigInteger.Parse("0" + uuid, NumberStyles.AllowHexSpecifier);
            }
            else if (Regex.IsMatch(uuid, "^[0-9]+(\\.[0-9]+)+$"))
            {
                idType = IdType.Oid;
            }

            return answer.ToString();
        }
    }
}
