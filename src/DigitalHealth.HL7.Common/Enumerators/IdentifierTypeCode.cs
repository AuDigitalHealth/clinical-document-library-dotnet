namespace DigitalHealth.HL7.Common.Enumerators
{
    /// <summary>
    /// This enumeration lists the subset of Patient Identifier Type codes that are used by HIPS.
    ///
    /// </summary>
    public enum IdentifierTypeCode
    {
        MR,   // Medical Record Number
        MC,   // Medicare Card Number (with IRN as 11th digit where available)
        DVA,  // Department of Veterans' Affairs (DVA) File Number
        SAUHI, // Enterprise Unique Healthcare Identifier
        DVG, // Department of Veterans’ Affairs (Gold) File Number
        DVO, // Department of Veterans’ Affairs (Orange) File Number
        DVW, // Department of Veterans’ Affairs (White) File Number
        PI, // Internal Patient Identifier
        NI // National Identifier (IHI)
    }

    /// <summary>
    /// This enumeration lists the subset of Patient Identifier Name Type codes that are used by HIPS.
    ///
    /// </summary>
    public enum NameTypeCode
    {
        A, // Alias Name
        B, // Birth Name
        C, // Adopted Name
        D, // Display Name
        I, // Licensing Name
        K, // Artist Name
        L, // Legal Name
        M, // Maiden Name
        N, // Nickname/"Call me" Name/Street Name
        P, // name of Partner/Spouse (retained for backwrd compatibility only)
        R, // Registered Name
        S, // Coded Pseudo-Name to ensure anonymity
        T, // Indigenous/Tribal/Community Name
        U  // Unspecified
    }
}