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
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Nehta.HL7.CDA;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.Common.Entities
{
    /// <summary>
    /// This class represents a file. The file can either be embedded or referenced.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ExternalData
    {
        #region Properties
        private string _id;
        /// <summary>
        /// The ID associated with this external data
        /// </summary>
        [CanBeNull]
        [DataMember]
        public string ID
        {
          get
          {
            if (_id == null)
            {
              var guid = Guid.NewGuid();
              while (true)
              {
                var firstCharacter = guid.ToString()[0];
                if (Char.IsLetter(firstCharacter))
                {
                  break;
                }

                guid = Guid.NewGuid();
              }

              _id = guid.ToString();
            }

            return _id;
          }
          set
          {
            _id = value;
          }
        }

        [CanBeNull]
        private Byte[] _digestValue;

        /// <summary>
        /// The digest value associated with this external data
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Byte[] DigestValue
        {
            get
            {
                if (_digestValue == null)
                {
                    if (_digestCheckAlgorithm.HasValue && _digestCheckAlgorithm.Value == CDA.Common.Enums.DigestCheckAlgorithm.SHA256)
                    {
                        DigestCheckAlgorithm = CDA.Common.Enums.DigestCheckAlgorithm.SHA256;
                    } else
                    {
                        //Set the digest algorithm, and let the digest algorithm property generate the digest value.
                        DigestCheckAlgorithm = CDA.Common.Enums.DigestCheckAlgorithm.SHA1;
                    }
  
                }

                return _digestValue;
            }
            set
            {
                _digestValue = value;
            }
        }

        private DigestCheckAlgorithm? _digestCheckAlgorithm { get; set; }
        /// <summary>
        /// The digest algorithm associated with this external data
        /// </summary>
        [CanBeNull]
        [DataMember]
        public DigestCheckAlgorithm? DigestCheckAlgorithm
        {
            get
            {
                return _digestCheckAlgorithm;
            }
            set
            {
                _digestCheckAlgorithm = value;

                if (!Path.IsNullOrEmptyWhitespace() || ByteArrayInput != null)
                {
                    //Convert the file into a byte array.
                    byte[] bytes = null;

                    if (!Path.IsNullOrEmptyWhitespace() && File.Exists(Path))
                    {
                        //load the file reference at the path
                        using (var fileStream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                        {
                            // Read the source file into a byte array.
                            bytes = new byte[fileStream.Length];

                            var numBytesToRead = (int)fileStream.Length;
                            var numBytesRead = 0;
                            while (numBytesToRead > 0)
                            {
                                // Read may return anything from 0 to numBytesToRead.
                                var bytesRead = fileStream.Read(bytes, numBytesRead, numBytesToRead);

                                // Break when the end of the file is reached.
                                if (bytesRead == 0)
                                {
                                    break;
                                }

                                numBytesRead += bytesRead;
                                numBytesToRead -= bytesRead;
                            }
                        }
                    }

                    if (ByteArrayInput != null)
                    {
                      if (ByteArrayInput.ByteArray != null) bytes = ByteArrayInput.ByteArray;
                    }

                    if (bytes != null)
                    {
                      try
                      {
                        if (_digestCheckAlgorithm == CDA.Common.Enums.DigestCheckAlgorithm.SHA256)
                          _digestValue = CalculateSHA256(bytes);
                        else
                          //Calculate the digest value from the byte array representing the file
                          _digestValue = CalculateSHA1(bytes);
                      }
                      catch
                      {
                        //Assume the file was not found and ignore this exception
                      }
                    }
                }
            }
        }

        /// <summary>
        /// A file name with the full extension
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Path { get; set; }

        /// <summary>
        /// A Byte Array Input for an external attachment
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ByteArrayInput ByteArrayInput { get; set; }

        [CanBeNull]
        private MediaType? _mediaType;
        /// <summary>
        /// A file MediaType
        /// </summary>
        [CanBeNull]
        public MediaType? ExternalDataMediaType
        {
            get
            {
                return _mediaType;
            }
            set
            {
                _mediaType = value;
            }
        }

        [CanBeNull]
        private FileStorageType? _fileStorageType { get; set; }

        /// <summary>
        /// FileStorageType Mode, eg embed or reference
        /// </summary>
        [CanBeNull]
        [DataMember]
        public FileStorageType? FileStorageType  
        {
          get
          {
            return Generator.Enums.FileStorageType.Reference;

            // EMBEDED Reference is not supported 
            //  if (_fileStorageType == null)
            //  {
            //     return Generator.Enums.FileStorageType.Reference;
            //  }
            //  else
            //  {
            //    return _fileStorageType;
            //  }
            //}
            //set { _fileStorageType = value; }
          }
        }

        /// <summary>
        /// The caption associated with this file (if applicable)
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Caption { get; set; }
        #endregion

        #region Constructors
        internal ExternalData()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this procedure
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("ExternalDataMediaType", ExternalDataMediaType);

            vb.ArgumentRequiredCheck("ID", ID);

            if ((!Path.IsNullOrEmptyWhitespace() && ByteArrayInput != null) || (Path.IsNullOrEmptyWhitespace() && ByteArrayInput == null))
            {
              vb.AddValidationMessage("FileName", null, "The Path and Base64Input are Mutually exclusive, please pass the file by File path or Base64");
            }

            if (!Path.IsNullOrEmptyWhitespace())
            {
                if (!File.Exists(Path))
                    vb.AddValidationMessage("FileName", Path, Path + " does not exist");

                var fileName = System.IO.Path.GetFileName(Path);
                if (fileName != null && fileName.Contains(" "))
                {
                    vb.AddValidationMessage("FileName", Path, "Can not contain a character with a space");
                }
            }

            if (ByteArrayInput != null)
            {
              vb.ArgumentRequiredCheck("Base64Input.ByteArray", ByteArrayInput.ByteArray);
              vb.ArgumentRequiredCheck("ExternalDataMediaType", ByteArrayInput.FileName);
            }

            vb.ArgumentRequiredCheck("FileStorageType", FileStorageType);

            if (ExternalDataMediaType == MediaType.TXT || ExternalDataMediaType == MediaType.PDF)
            {
                vb.ArgumentRequiredCheck("Caption", Caption);
            }
        }

        /// <summary>
        /// Validates this procedure
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void ValidateNoCaption(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            vb.ArgumentRequiredCheck("ExternalDataMediaType", ExternalDataMediaType);

            vb.ArgumentRequiredCheck("ID", ID);

            if ((!Path.IsNullOrEmptyWhitespace() && ByteArrayInput != null) || (Path.IsNullOrEmptyWhitespace() && ByteArrayInput == null))
            {
                vb.AddValidationMessage("FileName", null, "The Path and Base64Input are Mutually exclusive, please pass the file by File path or Base64");
            }

            if (!Path.IsNullOrEmptyWhitespace())
            {
                if (!File.Exists(Path))
                    vb.AddValidationMessage("FileName", Path, Path + " does not Exist");
            }

            if (ByteArrayInput != null)
            {
                vb.ArgumentRequiredCheck("Base64Input.ByteArray", ByteArrayInput.ByteArray);
                vb.ArgumentRequiredCheck("ExternalDataMediaType", ByteArrayInput.FileName);
            }

            vb.ArgumentRequiredCheck("FileStorageType", FileStorageType);

            if (!Caption.IsNullOrEmptyWhitespace())
            {
                vb.AddValidationMessage("External Data - Caption", Caption, "Please note the caption in this instance is populated by the 'Report Description' field, so please do not populate this field");
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Converts the external data that this object references into a Base64 String
        /// </summary>
        /// <returns>A Base64 string representing the external object</returns>
        public String ConvertToBase64String()
        {
            String base64String = null;

            if (!Path.IsNullOrEmptyWhitespace())
            {
                using (var fileStream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var buffer = new byte[fileStream.Length];

                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    fileStream.Close();

                    base64String = Convert.ToBase64String(buffer);
                }
            }
            else if (ByteArrayInput != null && ByteArrayInput.ByteArray != null)
            {
                    base64String = Convert.ToBase64String(ByteArrayInput.ByteArray);
            }

            return base64String;
        }

        /// <summary>
        /// Converts this external data into an HL7 StrucDocRenderMultiMedia object, providing
        /// the narrative entry for the referenced external data.
        /// </summary>
        /// <returns>StrucDocRenderMultiMedia</returns>
        public StrucDocRenderMultiMedia ConvertToStrucDocRenderMultiMedia()
        {
            //Returns a StrucDocRenderMultiMedia with a caption if the caption text on this external data object has been set.
            return new StrucDocRenderMultiMedia
                       {
                           referencedObject = ID,
                           caption = !Caption.IsNullOrEmptyWhitespace() ?  new StrucDocCaption
                                                                           {
                                                                              Text = new [] { Caption }
                                                                           } : null
                       };
        }
        
        /// <summary>
        /// Generates a hash value for a byte array.
        /// </summary>
        /// <param name="content">The byte array to generate the hash from.</param>
        /// <returns>Generated hash value.</returns>
        private static byte[] CalculateSHA1(byte[] content)
        {
            var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            return sha1CryptoServiceProvider.ComputeHash(content);
        }

        /// <summary>
        /// Generates a hash value for a byte array.
        /// </summary>
        /// <param name="content">The byte array to generate the hash from.</param>
        /// <returns>Generated hash value.</returns>
        private static byte[] CalculateSHA256(byte[] content)
        {
            var sha1CryptoServiceProvider = new SHA256CryptoServiceProvider();
            return sha1CryptoServiceProvider.ComputeHash(content);
        }
        
        #endregion
    }

    /// <summary>
    /// Allow ByteArrayInput input for External data
    /// </summary>
    public class ByteArrayInput
    {
        /// <summary>
        /// ByteArray, The assumption is the user will  manually copy the image file to the output directory.
        /// </summary>
        [CanBeNull]
        [DataMember]
        public byte[] ByteArray { get; set; }

        /// <summary>
        ///name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String FileName { get; set; }
    }
}

