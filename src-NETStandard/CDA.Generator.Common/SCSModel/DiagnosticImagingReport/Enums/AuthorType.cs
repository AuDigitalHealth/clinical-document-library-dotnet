using System;
using System.Runtime.Serialization;
 namespace Nehta.VendorLibrary.CDA.Common.Enums
  {

   /// <summary>
   /// Author Type
   /// </summary>
   [Serializable]
   [DataContract]
    public enum AuthorType
    {
       /// <summary>
        /// AuthorNonHealthcareProvider
       /// </summary>
       AuthorNonHealthcareProvider,
       /// <summary>
       /// AuthorHealthcareProvider
       /// </summary>
       AuthorHealthcareProvider,
       /// <summary>
       /// AuthorDevice
       /// </summary>
       AuthorDevice
    }
  }

