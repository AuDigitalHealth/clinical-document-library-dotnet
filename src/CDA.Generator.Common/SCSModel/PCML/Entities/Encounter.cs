using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CDA.Generator.Common.SCSModel.Interfaces;
using JetBrains.Annotations;
using Nehta.VendorLibrary.CDA.SCSModel.Common;

namespace Nehta.VendorLibrary.CDA.SCSModel.PCML.Entities
{/// <summary>
    /// Encounter
    /// </summary>
    [Serializable]
    [DataContract]
    [KnownType(typeof(Participation))]
    [KnownType(typeof(CodableText))]
    public class Encounter
    {
        /// <summary>
        /// The Duration of the EncounterPeriod
        /// </summary>
        [CanBeNull]
        [DataMember]
        public CdaInterval EncounterPeriod { get; set; }


        /// <summary>
        /// Participation Healthcare Facility
        /// </summary>
        [CanBeNull]
        public IParticipationHealthcareFacility HealthcareFacility { get; set; }
    }
}
