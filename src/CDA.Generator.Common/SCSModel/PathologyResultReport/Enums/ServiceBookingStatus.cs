using System;
using System.Runtime.Serialization;
namespace Nehta.VendorLibrary.CDA.Common.Enums
{
  /// <summary>
  /// Service Booking Status
  /// </summary>
  [Serializable]
  [DataContract]
  public enum ServiceBookingStatus
  {
    /// <summary>
    /// Event
    /// </summary>
    [EnumMember]
    [Name(Code = "EVN", Name = "Event", Comment = "The entry defines an actual occurrence of an event.")]
    Event,

    /// <summary>
    /// Intent
    /// </summary>
    [EnumMember]
    [Name(Code = "INT", Name = "Intent", Comment = "The entry is intended or planned.")]
    Intent,

    /// <summary>
    /// Appointment
    /// </summary>
    [EnumMember]
    [Name(Code = "APT", Name = "Appointment", Comment = "The entry is planned for a specific time and place.")]
    Appointment,

    /// <summary>
    /// Appointment Request
    /// </summary>
    [EnumMember]
    [Name(Code = "ARQ", Name = "Appointment Request", Comment = "The entry is a request for the booking of an appointment.")]
    AppointmentRequest,

    /// <summary>
    /// Promise
    /// </summary>
    [EnumMember]
    [Name(Code = "PRMS", Name = "Promise", Comment = "A commitment to perform the stated entry.")]
    Promise,

    /// <summary>
    /// Proposal
    /// </summary>
    [EnumMember]
    [Name(Code = "PRP", Name = "Proposal", Comment = "A proposal that the stated entry be performed.")]
    Proposal,

    /// <summary>
    /// Request
    /// </summary>
    [EnumMember]
    [Name(Code = "RQO", Name = "Request", Comment = "A request or order to perform the stated entry.")]
    Request,

    /// <summary>
    /// Definition
    /// </summary>
    [EnumMember]
    [Name(Code = "DEF", Name = "Definition", Comment = "The entry defines a service (master).")]
    Definition,
  }
}

