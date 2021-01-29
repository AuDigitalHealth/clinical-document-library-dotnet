using System;
using JetBrains.Annotations;

namespace CDA.Generator.Common.Common.Attributes
{
  /// <summary>
  /// TemplatePackedgeAttribute - This Attribute Lists the supported template Packages 
  /// </summary>
  /// <summary>
  /// Name Attribute
  /// 
  /// This attribute is used to specify a Code and a free text Name value.
  /// </summary>
  [Serializable]
  [AttributeUsage(AttributeTargets.Class)]
  public class TemplatePackageAttribute : Attribute
  {
        /// <summary>
        /// Extension
        /// </summary>
        [CanBeNull]
        public String DocumentName { get; set; }

        /// <summary>
        /// Template Packages 
        /// </summary>
        [CanBeNull]
        public String TemplatePackages { get; set; }

        #region Constructors

        /// <summary>
        /// Constructer for TemplatePackedgeAttribute
        /// </summary>
        public TemplatePackageAttribute()
        {
        }

        /// <summary>
        /// Constructer for TemplatePackedgeAttribute
        /// </summary>
        public TemplatePackageAttribute(string documentName, string templatePackages)
        {
          this.DocumentName = documentName;
          this.TemplatePackages = templatePackages;
        }

        #endregion
  }
}

