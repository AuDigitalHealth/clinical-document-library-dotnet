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
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    
    /// <summary>
    /// This class is designed to encapsulate the properties within a CDA document that make up 
    /// the detail within an electronic communication section.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ElectronicCommunicationDetail
    {
        private String _address;

        #region Properties
        /// <summary>
        /// The medium associated with this electronic communiqué, E.g. Telephone
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ElectronicCommunicationMedium Medium { get; set; }

        /// <summary>
        /// The usage associated with this electronic communiqué, E.g. Home
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<ElectronicCommunicationUsage> Usage { get; set; }

        /// <summary>
        /// Address, E.g. the email address or phone number etc, as appropriate with the chosen Medium
        /// 
        /// If the address usage is Telephone, this property also ensures that the address starts with "tel:"
        /// as the NEHTA style sheet expects telephone numbers to be prefixed with "tel:"
        /// </summary>
        [DataMember]
        public String Address
        {
            get
            {
	            // Begin the switch.
	            switch (Medium)
	            {
	                case ElectronicCommunicationMedium.Telephone:
                    case ElectronicCommunicationMedium.Mobile:
	                case ElectronicCommunicationMedium.Page:
	                    {
                            return "tel:" + _address;	                        
	                    }
                    case ElectronicCommunicationMedium.Fax:
                        {
                            return "fax:" + _address;
                        }
                    case ElectronicCommunicationMedium.Email:
                        {
                            return "mailto:" + _address;
                        }
                    case ElectronicCommunicationMedium.NFS:
                    {
                        return "nfs:" + _address;
                    }
                    case ElectronicCommunicationMedium.Telnet:
                    {
                        return "telnet:" + _address;
                    }
                    case ElectronicCommunicationMedium.HTTP:
                    {
                        return "http:" + _address;
                    }
                    case ElectronicCommunicationMedium.MLLP:
                    {
                        return "mllp:" + _address;
                    }
                    case ElectronicCommunicationMedium.Modem:
                    {
                        return "modem:" + _address;
                    }
                    default :
                    return ElectronicCommunicationMedium.Modem + " "  + _address;
	            }
            }
            set
            {
                _address = value;
            }
        }

        /// <summary>
        /// The narrative entry for this item
        /// </summary>
        public String Narrative
        {
            get
            {
                return _address ;
            }
        }
        #endregion

        #region Constructors
        internal ElectronicCommunicationDetail()
        {
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validates this electronic communication detail
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages to date, these may be added to within this method</param>
        public void Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);
            vb.ArgumentRequiredCheck("Address", Address);

            if (vb.ArgumentRequiredCheck("Usage", Usage))
            {
                vb.RangeCheck("Usage", Usage, 1, Int32.MaxValue);
            }
        }

        #endregion
    }
}
