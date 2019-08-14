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
using System.Text;
using System.Security.Cryptography;

namespace Nehta.VendorLibrary.Common
{
    /// <summary>
    /// Helper class for Byte related functions.
    /// </summary>
    public class ByteUtility
    {
        /// <summary>
        /// Convert a byte array into a hexadecimal string.
        /// </summary>
        /// <param name="ba">The byte array to convert.</param>
        /// <returns>A hexadecimal representation of the byte array.</returns>
        public static string BytesToHexString(byte[] ba)
        {
            StringBuilder sb = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Convert a hexadecimal string to a byte array.
        /// </summary>
        /// <param name="hexString">The hexadecimal string to convert.</param>
        /// <returns>The converted byte array.</returns>
        internal static byte[] HexStringToBytes(string hexString)
        {
            byte[] rv = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                rv[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return (rv);
        }

        /// <summary>
        /// Generate a sequence of cryptographically strong random byte values.
        /// </summary>
        /// <param name="numberOfBytes">The number of bytes to generate.</param>
        /// <param name="maxByteValue">The inclusive maximum value allowed in each byte.</param>
        /// <returns>Returns the list of generated random bytes.</returns>
        internal static byte[] GenerateRandomBytes(int numberOfBytes, int maxByteValue)
        {
            Validation.ValidateNumberRange("maxByteValue", 0, 255, maxByteValue);

            int modulusValue = 256 % (maxByteValue + 1);
            int acceptableUpperByteValue = 256 - modulusValue - 1;

            byte[] randomBytes = new byte[numberOfBytes];

            int index = 0;

            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] generated = new byte[1];

            while (index < numberOfBytes)
            {
                rng.GetBytes(generated);
                if (generated[0] <= acceptableUpperByteValue)
                {
                    randomBytes[index] = (byte)((generated[0] + 1) % (maxByteValue + 1));
                    index++;
                }
            }

            return randomBytes;
        }
    }
}
