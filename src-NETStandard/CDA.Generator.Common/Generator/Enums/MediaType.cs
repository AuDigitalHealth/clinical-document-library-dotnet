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
    /// Media Types - hl7-v2,gif,jpeg,pdf,png,bmp,tiff,rtf,x-rtf,richtext,plain,html,vnd.openxmlformats-officedocument.wordprocessingml.document (eg docx)
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// The undefined enumeration is used to determine if a value has been explicitly set.
        /// </summary>
        Undefined,

        /// <summary>
        /// HL7
        /// </summary>
        [Name(Name = "application/hl7-v2")]
        HL7,

        /// <summary>
        /// JPEG is a commonly used method of lossy compression for digital photography (image). 
        /// </summary>
        [Name(Name = "image/jpeg")]
        JPEG,

        /// <summary>
        /// Graphics Interchange Format (GIF)
        /// </summary>
        [Name(Name = "image/gif")]
        GIF,

        /// <summary>
        /// Portable Network Graphic (PNG)
        /// </summary>
        [Name(Name = "image/png")]
        PNG,

        /// <summary>
        /// Tag Image File Format (TIFF)
        /// </summary>
        [Name(Name = "image/tiff")]
        TIFF,

        /// <summary>
        /// Bitmap 
        /// </summary>
        [Name(Name = "image/bmp")]
        BMP,

        /// <summary>
        /// Plain text
        /// </summary>
        [Name(Name = "text/plain")]
        TXT,

        /// <summary>
        /// Adobe Portable Document Format
        /// </summary>
        [Name(Name = "application/pdf")]
        PDF,

        /// <summary>
        /// HTML 
        /// </summary>
        [Name(Name = "text/html")]
        HTML,

        /// <summary>
        /// Rich Text Format
        /// </summary>
        [Name(Name = "application/rtf")]
        RTF,

        /// <summary>
        /// X-Rich Text Format
        /// </summary>
        [Name(Name = "application/x-rtf")]
        XRTF,

        /// <summary>
        /// Rich Text Plain
        /// </summary>
        [Name(Name = "text/richtext")]
        RICHTEXT,

        /// <summary>
        /// DocX format
        /// </summary>
        [Name(Name = "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        DOCX,
    }
}

