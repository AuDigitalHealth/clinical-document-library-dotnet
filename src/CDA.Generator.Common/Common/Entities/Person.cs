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
using System.Linq;
using System.Runtime.Serialization;
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.Common.Enums;
using Nehta.VendorLibrary.CDA.Generator.Enums;
using Nehta.VendorLibrary.CDA.SCSModel;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.Common;

namespace Nehta.VendorLibrary.CDA.Common
{
    
    /// <summary>
    /// The Person class contains all the properties that CDA has identified for a person and
    /// implements a variety of interfaces that further define and can be used to constrain the person class
    /// into a CDA specific person instance.
    /// 
    /// The IPerson interface contains the super set of person information
    /// 
    /// The Person object can be cast into an IPersonSubjectOfCare, thus constraining the person class so as it only
    /// exposes those properties that are associated with a subject of care
    /// 
    /// The Person object can be cast into an IPersonDispenser, thus constraining the person class so as it only
    /// exposes those properties that are associated with a dispenser
    /// 
    /// The Person object can be cast into an IPersonPrescriberInstructionRecipient, thus constraining the person class so as it only
    /// exposes those properties that are associated with a prescriber instruction recipient
    /// 
    /// The Person object can be cast into an IPersonName, thus constraining the person class so as it only
    /// exposes those properties that are associated with a person name (a very restrictive view of the person)
    /// 
    /// The Person object can be cast into an IPersonAuthor, thus constraining the person class so as it only
    /// exposes those properties that are associated with an author
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Organisation))]
    [KnownType(typeof(EmploymentOrganisation))]
    [KnownType(typeof(PersonName))]

    internal class Person : IPersonWithRelationship, IPersonWithOrganisation, IPerson, IPersonSubjectOfCare, 
                            IPersonDispenser, IPersonPrescriberInstructionRecipient, IPersonPrescriber, IPersonConsumer,
                            IPersonHealthcareProvider
    {
        #region Properties
        
        /// <summary>
        /// Person Name
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<IPersonName> PersonNames { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateOfBirth { get; set; }

        /// <summary>
        /// A boolean indicating if the date of birth has been calculated from the persons age
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? DateOfBirthCalculatedFromAge { get; set; }

        /// <summary>
        /// Date of birth accuracy indicator
        /// </summary>
        [CanBeNull]
        [DataMember]
        public DateAccuracyIndicator DateOfBirthAccuracyIndicator { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? Age { get; set; }

        /// <summary>
        /// The Unit of Measuse used for the Age : Default is Year
        /// </summary>
        [CanBeNull]
        public AgeUnitOfMeasure? AgeUnitOfMeasure { get; set; }

        /// <summary>
        /// Age accuracy indicator
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Boolean? AgeAccuracyIndicator { get; set; }

        /// <summary>
        /// Birth pularity; the position in relation to their siblings
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? BirthPlurality { get; set; }

        /// <summary>
        /// Birth Order
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Int32? BirthOrder { get; set; }

        /// <summary>
        /// Date of death
        /// </summary>
        [CanBeNull]
        [DataMember]
        public ISO8601DateTime DateOfDeath { get; set; }

        /// <summary>
        /// Date of death accuracy indicator
        /// </summary>
        [CanBeNull]
        [DataMember]
        public DateAccuracyIndicator DateOfDeathAccuracyIndicator { get; set; }

        /// <summary>
        /// Mother's Original Family Name
        /// </summary>
        [CanBeNull]
        public IPersonName MothersOriginalFamilyName { get; set; }

        /// <summary>
        /// Source Of Death Notification
        /// </summary>
        public SourceOfDeathNotification? SourceOfDeathNotification { get; set; } 

        /// <summary> 
        /// Country of birth
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Country CountryOfBirth { get; set; }

        /// <summary>
        /// The Date of birth
        /// </summary>
        [CanBeNull]
        [DataMember]
        public AustralianState StateOfBirth { get; set; }

        /// <summary>
        /// Indigenous status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IndigenousStatus IndigenousStatus { get; set; }

        /// <summary>
        /// Au Indigenous status
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IndigenousStatus AuIndigenousStatus { get; set; }


        /// <summary>
        /// The Entitlements
        /// </summary>
        [CanBeNull]
        [DataMember]
        public List<Entitlement> Entitlements { get; set; }

        /// <summary>
        /// The Qualifications
        /// </summary>
        [CanBeNull]
        [DataMember]
        public String Qualifications { get; set; }

        /// <summary>
        /// The Organisation
        /// </summary>
        [CanBeNull]
        [DataMember]
        public IEmploymentOrganisation Organisation { get; set; }

        /// <summary>
        /// Identifiers
        /// </summary>
        [CanBeNull]
        public List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Relationship to Subject of Care
        /// </summary>
        [CanBeNull]
        [DataMember]
        public RelationshipRoleType? RelationshipToSubjectOfCare { get; set; }

        /// <summary>
        /// Organisation
        /// </summary>
        [CanBeNull]
        [DataMember]
        public Occupation? Occupation { get; set; }

        /// <summary>
        /// Interpreter Required Alert
        /// </summary>
        [CanBeNull]
        [DataMember]
        public InterpreterRequiredAlert InterpreterRequired { get; set; }

        #endregion

        #region Constructors
        internal Person()
        {
            
        }
        #endregion

        #region Validation

        void IPersonWithRelationship.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Identifiers != null)
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
            {
                if (PersonNames != null)
                {
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        /// <summary>
        /// Validates this Person as an IPerson
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonWithOrganisation.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Differs from Spec User may have not have HPI-I 
            if (Identifiers != null)
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(
                        vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
            {
                if (PersonNames != null)
                {
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
                }
            }

            if (Organisation != null)
            {
                Organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this Person as an IPerson
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPerson.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Identifiers != null)
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (PersonNames != null)
            {
                for (var x = 0; x < PersonNames.Count; x++)
                {
                    PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                }
            }
        }

        /// <summary>
        /// Validates this Person as an IPersonSubjectOfCare
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonSubjectOfCare.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
            {
                if (PersonNames != null)
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
            }

            if (Identifiers != null)
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (AgeUnitOfMeasure.HasValue && !Age.HasValue)
            {
               vb.AddValidationMessage(vb.Path + "AgeUnitOfMeasure", null, "AgeUnitOfMeasure can not be specified without an Age");
            }

            if (DateOfBirthAccuracyIndicator != null)
            {
                DateOfBirthAccuracyIndicator.Validate(vb.Path + "DateOfBirthAccuracyIndicator", vb.Messages);
            }

            if (DateOfDeathAccuracyIndicator != null)
            {
                DateOfDeathAccuracyIndicator.Validate(vb.Path + "DateOfDeathAccuracyIndicator", vb.Messages);
            }

            if (MothersOriginalFamilyName != null)
            {
                MothersOriginalFamilyName.Validate(vb.Path + "MothersOriginalFamilyName", vb.Messages);
            }
        }

        /// <summary>
        /// Validates this Person as an IPersonPrescriber
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonPrescriber.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Identifiers", Identifiers))
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(
                        vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
            {
                if (PersonNames != null)
                {
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
                }
            }

            if (Entitlements != null && Entitlements.Any())
            {
              if (Entitlements != null)
              {
                  for (var x = 0; x < Entitlements.Count; x++)
                  {
                    Entitlements[x].Validate
                        (
                        vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages
                        );
                  }
              }
            }
        }

        /// <summary>
        /// Validates this Person as an IPersonDispenser
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonDispenser.ValidateATS(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          if (vb.ArgumentRequiredCheck("Identifiers", Identifiers))
          {
            for (var x = 0; x < Identifiers.Count; x++)
            {
              Identifiers[x].Validate(
                  vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
          {
            if (PersonNames != null)
            {
              for (var x = 0; x < PersonNames.Count; x++)
              {
                PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
              }
            }
          }
        }

        /// <summary>
        /// Validates this Person as an IPersonDispenser
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonDispenser.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (Identifiers != null)
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(
                        vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
            {
                if (PersonNames != null)
                {
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        /// <summary>
        /// Validates this Person as an IPersonPrescriberInstructionRecipient
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonPrescriberInstructionRecipient.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            if (vb.ArgumentRequiredCheck("Identifiers", Identifiers))
            {
               vb.RangeCheck("Identifiers", Identifiers, 1, 1);

                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(
                        vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("PersonNames", PersonNames))
            {
               vb.RangeCheck("PersonNames", PersonNames, 1, 1);

                if (PersonNames != null)
                {
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
                }
            }
        }

        
        /// <summary>
        /// Validates this Person as an IPersonConsumer
        /// </summary>
        /// <param name="path">The path to this object as a string</param>
        /// <param name="messages">the validation messages, these may be added to within this method</param>
        void IPersonConsumer.Validate(string path, List<ValidationMessage> messages)
        {
            var vb = new ValidationBuilder(path, messages);

            // Differs from Spec User may have not have HPI-I 
            if (Identifiers != null)
            {
                for (var x = 0; x < Identifiers.Count; x++)
                {
                    Identifiers[x].Validate(
                        vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
                }
            }

            if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
            {
                if (PersonNames != null)
                {
                    for (var x = 0; x < PersonNames.Count; x++)
                    {
                        PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
                    }
                }
            }

            if (Organisation != null)
            {
                Organisation.Validate(vb.Path + "Organisation", vb.Messages);
            }
        }

      /// <summary>
      /// Validates this Person as an IPersonHealthcareProvider
      /// </summary>
      /// <param name="path">The path to this object as a string</param>
      /// <param IPersonHealthcareProvider="messages">The validation messages, these may be added to within this method</param>
        /// <param name="messages">The validation messages, these may be added to within this method</param>
      void IPersonHealthcareProvider.Validate(string path, List<ValidationMessage> messages)
        {
          var vb = new ValidationBuilder(path, messages);

          // Differs from Spec User may have not have HPI-I 
          if (Identifiers != null)
          {
            for (var x = 0; x < Identifiers.Count; x++)
            {
              Identifiers[x].Validate(
                  vb.Path + string.Format("Identifiers[{0}]", x), vb.Messages);
            }
          }

          if (vb.ArgumentRequiredCheck("PersonName", PersonNames))
          {
            if (PersonNames != null)
            {
              for (var x = 0; x < PersonNames.Count; x++)
              {
                PersonNames[x].Validate(vb.Path + string.Format("PersonName[{0}]", x), vb.Messages);
              }
            }
          }

          if (Entitlements != null && Entitlements.Any())
          {
            if (Entitlements != null)
            {
              for (var x = 0; x < Entitlements.Count; x++)
              {
                Entitlements[x].Validate
                    (
                    vb.Path + string.Format("Entitlements[{0}]", x), vb.Messages
                    );
              }
            }
          }

          if (Organisation != null)
          {
            Organisation.Validate(vb.Path + "Organisation", vb.Messages);
          }
        }

        #endregion
    }
}
