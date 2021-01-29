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
using System.Runtime.Serialization;

namespace Nehta.VendorLibrary.CDA.Common.Enums
{
    /// <summary>
    /// Street Type
    /// </summary>
    [Serializable]
    [DataContract]
    public enum StreetType
    {
        /// <summary>
        /// Undefined, this is the default value if the enum is left unset.
        /// 
        /// The validation engine uses this to test and assert that the enum has been set (if required)
        /// and is therefore valid.
        /// </summary>
        [EnumMember]
        Undefined,
        
        
        /// <summary>
        /// Access
        /// </summary>
        [EnumMember]
        [Name(Code = "Accs", Name = "Access")]
        Access,


        /// <summary>
        /// Alley
        /// </summary>
        [EnumMember]
        [Name(Code = "Ally", Name = "Alley")]
        Alley,


        /// <summary>
        /// Alleyway
        /// </summary>
        [EnumMember]
        [Name(Code = "Alwy", Name = "Alleyway")]
        Alleyway,


        /// <summary>
        /// Amble
        /// </summary>
        [EnumMember]
        [Name(Code = "Ambl", Name = "Amble")]
        Amble,


        /// <summary>
        /// Anchorage
        /// </summary>
        [EnumMember]
        [Name(Code = "Ancg", Name = "Anchorage")]
        Anchorage,


        /// <summary>
        /// Approach
        /// </summary>
        [EnumMember]
        [Name(Code = "App", Name = "Approach")]
        Approach,


        /// <summary>
        /// Arcade
        /// </summary>
        [EnumMember]
        [Name(Code = "Arc", Name = "Arcade")]
        Arcade,


        /// <summary>
        /// Artery
        /// </summary>
        [EnumMember]
        [Name(Code = "Art", Name = "Artery")]
        Artery,


        /// <summary>
        /// Avenue
        /// </summary>
        [EnumMember]
        [Name(Code = "Ave", Name = "Avenue")]
        Avenue,


        /// <summary>
        /// Basin
        /// </summary>
        [EnumMember]
        [Name(Code = "Basn", Name = "Basin")]
        Basin,


        /// <summary>
        /// Beach
        /// </summary>
        [EnumMember]
        [Name(Code = "Bch", Name = "Beach")]
        Beach,


        /// <summary>
        /// Bend
        /// </summary>
        [EnumMember]
        [Name(Code = "Bend", Name = "Bend")]
        Bend,


        /// <summary>
        /// Block
        /// </summary>
        [EnumMember]
        [Name(Code = "Blk", Name = "Block")]
        Block,


        /// <summary>
        /// Boulevard
        /// </summary>
        [EnumMember]
        [Name(Code = "Bvd", Name = "Boulevard")]
        Boulevard,


        /// <summary>
        /// Brace
        /// </summary>
        [EnumMember]
        [Name(Code = "Brce", Name = "Brace")]
        Brace,


        /// <summary>
        /// Brae
        /// </summary>
        [EnumMember]
        [Name(Code = "Brae", Name = "Brae")]
        Brae,


        /// <summary>
        /// Break
        /// </summary>
        [EnumMember]
        [Name(Code = "Brk", Name = "Break")]
        Break,


        /// <summary>
        /// Bridge
        /// </summary>
        [EnumMember]
        [Name(Code = "Bdge", Name = "Bridge")]
        Bridge,


        /// <summary>
        /// Broadway
        /// </summary>
        [EnumMember]
        [Name(Code = "Bdwy", Name = "Broadway")]
        Broadway,


        /// <summary>
        /// Brow
        /// </summary>
        [EnumMember]
        [Name(Code = "Brow", Name = "Brow")]
        Brow,


        /// <summary>
        /// Bypass
        /// </summary>
        [EnumMember]
        [Name(Code = "Bypa", Name = "Bypass")]
        Bypass,


        /// <summary>
        /// Byway
        /// </summary>
        [EnumMember]
        [Name(Code = "Bywy", Name = "Byway")]
        Byway,


        /// <summary>
        /// Causeway
        /// </summary>
        [EnumMember]
        [Name(Code = "Caus", Name = "Causeway")]
        Causeway,


        /// <summary>
        /// Centre
        /// </summary>
        [EnumMember]
        [Name(Code = "Ctr", Name = "Centre")]
        Centre,


        /// <summary>
        /// Centreway
        /// </summary>
        [EnumMember]
        [Name(Code = "Cnwy", Name = "Centreway")]
        Centreway,


        /// <summary>
        /// Chase
        /// </summary>
        [EnumMember]
        [Name(Code = "Ch", Name = "Chase")]
        Chase,


        /// <summary>
        /// Circle
        /// </summary>
        [EnumMember]
        [Name(Code = "Cir", Name = "Circle")]
        Circle,


        /// <summary>
        /// Circlet
        /// </summary>
        [EnumMember]
        [Name(Code = "Clt", Name = "Circlet")]
        Circlet,


        /// <summary>
        /// Circuit
        /// </summary>
        [EnumMember]
        [Name(Code = "Cct", Name = "Circuit")]
        Circuit,


        /// <summary>
        /// Circus
        /// </summary>
        [EnumMember]
        [Name(Code = "Crcs", Name = "Circus")]
        Circus,


        /// <summary>
        /// Close
        /// </summary>
        [EnumMember]
        [Name(Code = "Cl", Name = "Close")]
        Close,


        /// <summary>
        /// Colonnade
        /// </summary>
        [EnumMember]
        [Name(Code = "Clde", Name = "Colonnade")]
        Colonnade,


        /// <summary>
        /// Common
        /// </summary>
        [EnumMember]
        [Name(Code = "Cmmn", Name = "Common")]
        Common,


        /// <summary>
        /// Concourse
        /// </summary>
        [EnumMember]
        [Name(Code = "Con", Name = "Concourse")]
        Concourse,


        /// <summary>
        /// Copse
        /// </summary>
        [EnumMember]
        [Name(Code = "Cps", Name = "Copse")]
        Copse,


        /// <summary>
        /// Corner 
        /// The code "Crn" is specified in the Australian Standards document.  However, most of the other street type codes come 
        /// from the Australia Post site and this is the only value that is different. The code for Corner from Australia Post is
        /// "Cnr"
        /// </summary>
        [EnumMember]
        [Name(Code = "Crn", Name = "Corner")]
        Corner,


        /// <summary>
        /// Corso
        /// </summary>
        [EnumMember]
        [Name(Code = "Cso", Name = "Corso")]
        Corso,


        /// <summary>
        /// Court
        /// </summary>
        [EnumMember]
        [Name(Code = "Ct", Name = "Court")]
        Court,


        /// <summary>
        /// Courtyard
        /// </summary>
        [EnumMember]
        [Name(Code = "Ctyd", Name = "Courtyard")]
        Courtyard,


        /// <summary>
        /// Cove
        /// </summary>
        [EnumMember]
        [Name(Code = "Cove", Name = "Cove")]
        Cove,


        /// <summary>
        /// Crescent
        /// </summary>
        [EnumMember]
        [Name(Code = "Cres", Name = "Crescent")]
        Crescent,


        /// <summary>
        /// Crest
        /// </summary>
        [EnumMember]
        [Name(Code = "Crst", Name = "Crest")]
        Crest,


        /// <summary>
        /// Cross
        /// </summary>
        [EnumMember]
        [Name(Code = "Crss", Name = "Cross")]
        Cross,


        /// <summary>
        /// Crossing
        /// </summary>
        [EnumMember]
        [Name(Code = "Crsg", Name = "Crossing")]
        Crossing,


        /// <summary>
        /// Crossroad
        /// </summary>
        [EnumMember]
        [Name(Code = "Crd", Name = "Crossroad")]
        Crossroad,


        /// <summary>
        /// Crossway
        /// </summary>
        [EnumMember]
        [Name(Code = "Cowy", Name = "Crossway")]
        Crossway,


        /// <summary>
        /// Cruiseway
        /// </summary>
        [EnumMember]
        [Name(Code = "Cuwy", Name = "Cruiseway")]
        Cruiseway,


        /// <summary>
        /// Cul-De-Sac
        /// </summary>
        [EnumMember]
        [Name(Code = "Cds", Name = "Cul-De-Sac")]
        CulDeSac,


        /// <summary>
        /// Cutting
        /// </summary>
        [EnumMember]
        [Name(Code = "Cttg", Name = "Cutting")]
        Cutting,


        /// <summary>
        /// Dale
        /// </summary>
        [EnumMember]
        [Name(Code = "Dale", Name = "Dale")]
        Dale,


        /// <summary>
        /// Dell
        /// </summary>
        [EnumMember]
        [Name(Code = "Dell", Name = "Dell")]
        Dell,


        /// <summary>
        /// Deviation
        /// </summary>
        [EnumMember]
        [Name(Code = "Devn", Name = "Deviation")]
        Deviation,


        /// <summary>
        /// Dip
        /// </summary>
        [EnumMember]
        [Name(Code = "Dip", Name = "Dip")]
        Dip,


        /// <summary>
        /// Distributor
        /// </summary>
        [EnumMember]
        [Name(Code = "Dstr", Name = "Distributor")]
        Distributor,


        /// <summary>
        /// Drive
        /// </summary>
        [EnumMember]
        [Name(Code = "Dr", Name = "Drive")]
        Drive,


        /// <summary>
        /// Driveway
        /// </summary>
        [EnumMember]
        [Name(Code = "Drwy", Name = "Driveway")]
        Driveway,


        /// <summary>
        /// Edge
        /// </summary>
        [EnumMember]
        [Name(Code = "Edge", Name = "Edge")]
        Edge,


        /// <summary>
        /// Elbow
        /// </summary>
        [EnumMember]
        [Name(Code = "Elb", Name = "Elbow")]
        Elbow,


        /// <summary>
        /// End
        /// </summary>
        [EnumMember]
        [Name(Code = "End", Name = "End")]
        End,


        /// <summary>
        /// Entrance
        /// </summary>
        [EnumMember]
        [Name(Code = "Ent", Name = "Entrance")]
        Entrance,


        /// <summary>
        /// Esplanade
        /// </summary>
        [EnumMember]
        [Name(Code = "Esp", Name = "Esplanade")]
        Esplanade,


        /// <summary>
        /// Estate
        /// </summary>
        [EnumMember]
        [Name(Code = "Est", Name = "Estate")]
        Estate,


        /// <summary>
        /// Expressway
        /// </summary>
        [EnumMember]
        [Name(Code = "Exp", Name = "Expressway")]
        Expressway,


        /// <summary>
        /// Extension
        /// </summary>
        [EnumMember]
        [Name(Code = "Extn", Name = "Extension")]
        Extension,


        /// <summary>
        /// Fairway
        /// </summary>
        [EnumMember]
        [Name(Code = "Fawy", Name = "Fairway")]
        Fairway,


        /// <summary>
        /// Fire Track
        /// </summary>
        [EnumMember]
        [Name(Code = "Ftrk", Name = "Fire Track")]
        FireTrack,


        /// <summary>
        /// Firetrail
        /// </summary>
        [EnumMember]
        [Name(Code = "Fitr", Name = "Firetrail")]
        Firetrail,


        /// <summary>
        /// Flat
        /// </summary>
        [EnumMember]
        [Name(Code = "Flat", Name = "Flat")]
        Flat,


        /// <summary>
        /// Follow
        /// </summary>
        [EnumMember]
        [Name(Code = "Folw", Name = "Follow")]
        Follow,


        /// <summary>
        /// Footway
        /// </summary>
        [EnumMember]
        [Name(Code = "Ftwy", Name = "Footway")]
        Footway,


        /// <summary>
        /// Foreshore
        /// </summary>
        [EnumMember]
        [Name(Code = "Fshr", Name = "Foreshore")]
        Foreshore,


        /// <summary>
        /// Formation
        /// </summary>
        [EnumMember]
        [Name(Code = "Form", Name = "Formation")]
        Formation,


        /// <summary>
        /// Freeway
        /// </summary>
        [EnumMember]
        [Name(Code = "Fwy", Name = "Freeway")]
        Freeway,


        /// <summary>
        /// Front
        /// </summary>
        [EnumMember]
        [Name(Code = "Frnt", Name = "Front")]
        Front,


        /// <summary>
        /// Frontage
        /// </summary>
        [EnumMember]
        [Name(Code = "Frtg", Name = "Frontage")]
        Frontage,


        /// <summary>
        /// Gap
        /// </summary>
        [EnumMember]
        [Name(Code = "Gap", Name = "Gap")]
        Gap,


        /// <summary>
        /// Garden
        /// </summary>
        [EnumMember]
        [Name(Code = "Gdn", Name = "Garden")]
        Garden,


        /// <summary>
        /// Gardens
        /// </summary>
        [EnumMember]
        [Name(Code = "Gdns", Name = "Gardens")]
        Gardens,


        /// <summary>
        /// Gate
        /// </summary>
        [EnumMember]
        [Name(Code = "Gte", Name = "Gate")]
        Gate,


        /// <summary>
        /// Gates
        /// </summary>
        [EnumMember]
        [Name(Code = "Gtes", Name = "Gates")]
        Gates,


        /// <summary>
        /// Glade
        /// </summary>
        [EnumMember]
        [Name(Code = "Gld", Name = "Glade")]
        Glade,


        /// <summary>
        /// Glen
        /// </summary>
        [EnumMember]
        [Name(Code = "Glen", Name = "Glen")]
        Glen,


        /// <summary>
        /// Grange
        /// </summary>
        [EnumMember]
        [Name(Code = "Gra", Name = "Grange")]
        Grange,


        /// <summary>
        /// Green
        /// </summary>
        [EnumMember]
        [Name(Code = "Grn", Name = "Green")]
        Green,


        /// <summary>
        /// Ground
        /// </summary>
        [EnumMember]
        [Name(Code = "Grnd", Name = "Ground")]
        Ground,


        /// <summary>
        /// Grove
        /// </summary>
        [EnumMember]
        [Name(Code = "Gr", Name = "Grove")]
        Grove,


        /// <summary>
        /// Gully
        /// </summary>
        [EnumMember]
        [Name(Code = "Gly", Name = "Gully")]
        Gully,


        /// <summary>
        /// Heights
        /// </summary>
        [EnumMember]
        [Name(Code = "Hts", Name = "Heights")]
        Heights,


        /// <summary>
        /// Highroad
        /// </summary>
        [EnumMember]
        [Name(Code = "Hrd", Name = "Highroad")]
        Highroad,


        /// <summary>
        /// Highway
        /// </summary>
        [EnumMember]
        [Name(Code = "Hwy", Name = "Highway")]
        Highway,


        /// <summary>
        /// Hill
        /// </summary>
        [EnumMember]
        [Name(Code = "Hill", Name = "Hill")]
        Hill,


        /// <summary>
        /// Interchange
        /// </summary>
        [EnumMember]
        [Name(Code = "Intg", Name = "Interchange")]
        Interchange,


        /// <summary>
        /// Intersection
        /// </summary>
        [EnumMember]
        [Name(Code = "Intn", Name = "Intersection")]
        Intersection,


        /// <summary>
        /// Junction
        /// </summary>
        [EnumMember]
        [Name(Code = "Jnc", Name = "Junction")]
        Junction,


        /// <summary>
        /// Key
        /// </summary>
        [EnumMember]
        [Name(Code = "Key", Name = "Key")]
        Key,


        /// <summary>
        /// Landing
        /// </summary>
        [EnumMember]
        [Name(Code = "Ldg", Name = "Landing")]
        Landing,


        /// <summary>
        /// Lane
        /// </summary>
        [EnumMember]
        [Name(Code = "Lane", Name = "Lane")]
        Lane,


        /// <summary>
        /// Laneway
        /// </summary>
        [EnumMember]
        [Name(Code = "Lnwy", Name = "Laneway")]
        Laneway,


        /// <summary>
        /// Lees
        /// </summary>
        [EnumMember]
        [Name(Code = "Lees", Name = "Lees")]
        Lees,


        /// <summary>
        /// Line
        /// </summary>
        [EnumMember]
        [Name(Code = "Line", Name = "Line")]
        Line,


        /// <summary>
        /// Link
        /// </summary>
        [EnumMember]
        [Name(Code = "Link", Name = "Link")]
        Link,


        /// <summary>
        /// Little
        /// </summary>
        [EnumMember]
        [Name(Code = "Lt", Name = "Little")]
        Little,


        /// <summary>
        /// Lookout
        /// </summary>
        [EnumMember]
        [Name(Code = "Lkt", Name = "Lookout")]
        Lookout,


        /// <summary>
        /// Loop
        /// </summary>
        [EnumMember]
        [Name(Code = "Loop", Name = "Loop")]
        Loop,


        /// <summary>
        /// Lower
        /// </summary>
        [EnumMember]
        [Name(Code = "Lwr", Name = "Lower")]
        Lower,


        /// <summary>
        /// Mall
        /// </summary>
        [EnumMember]
        [Name(Code = "Mall", Name = "Mall")]
        Mall,


        /// <summary>
        /// Meander
        /// </summary>
        [EnumMember]
        [Name(Code = "Mndr", Name = "Meander")]
        Meander,


        /// <summary>
        /// Mew
        /// </summary>
        [EnumMember]
        [Name(Code = "Mew", Name = "Mew")]
        Mew,


        /// <summary>
        /// Mews
        /// </summary>
        [EnumMember]
        [Name(Code = "Mews", Name = "Mews")]
        Mews,


        /// <summary>
        /// Motorway
        /// </summary>
        [EnumMember]
        [Name(Code = "Mwy", Name = "Motorway")]
        Motorway,


        /// <summary>
        /// Mount
        /// </summary>
        [EnumMember]
        [Name(Code = "Mt", Name = "Mount")]
        Mount,


        /// <summary>
        /// Nook
        /// </summary>
        [EnumMember]
        [Name(Code = "Nook", Name = "Nook")]
        Nook,


        /// <summary>
        /// Outlook
        /// </summary>
        [EnumMember]
        [Name(Code = "Otlk", Name = "Outlook")]
        Outlook,


        /// <summary>
        /// Parade
        /// </summary>
        [EnumMember]
        [Name(Code = "Pde", Name = "Parade")]
        Parade,


        /// <summary>
        /// Park
        /// </summary>
        [EnumMember]
        [Name(Code = "Park", Name = "Park")]
        Park,


        /// <summary>
        /// Parklands
        /// </summary>
        [EnumMember]
        [Name(Code = "Pkld", Name = "Parklands")]
        Parklands,


        /// <summary>
        /// Parkway
        /// </summary>
        [EnumMember]
        [Name(Code = "Pkwy", Name = "Parkway")]
        Parkway,


        /// <summary>
        /// Part
        /// </summary>
        [EnumMember]
        [Name(Code = "Part", Name = "Part")]
        Part,


        /// <summary>
        /// Pass
        /// </summary>
        [EnumMember]
        [Name(Code = "Pass", Name = "Pass")]
        Pass,


        /// <summary>
        /// Path
        /// </summary>
        [EnumMember]
        [Name(Code = "Path", Name = "Path")]
        Path,


        /// <summary>
        /// Pathway
        /// </summary>
        [EnumMember]
        [Name(Code = "Phwy", Name = "Pathway")]
        Pathway,


        /// <summary>
        /// Piazza
        /// </summary>
        [EnumMember]
        [Name(Code = "Piaz", Name = "Piazza")]
        Piazza,


        /// <summary>
        /// Place
        /// </summary>
        [EnumMember]
        [Name(Code = "Pl", Name = "Place")]
        Place,


        /// <summary>
        /// Plateau
        /// </summary>
        [EnumMember]
        [Name(Code = "Plat", Name = "Plateau")]
        Plateau,


        /// <summary>
        /// Plaza
        /// </summary>
        [EnumMember]
        [Name(Code = "Plza", Name = "Plaza")]
        Plaza,


        /// <summary>
        /// Pocket
        /// </summary>
        [EnumMember]
        [Name(Code = "Pkt", Name = "Pocket")]
        Pocket,


        /// <summary>
        /// Point
        /// </summary>
        [EnumMember]
        [Name(Code = "Pnt", Name = "Point")]
        Point,


        /// <summary>
        /// Port
        /// </summary>
        [EnumMember]
        [Name(Code = "Port", Name = "Port")]
        Port,


        /// <summary>
        /// Promenade
        /// </summary>
        [EnumMember]
        [Name(Code = "Prom", Name = "Promenade")]
        Promenade,


        /// <summary>
        /// Quad
        /// </summary>
        [EnumMember]
        [Name(Code = "Quad", Name = "Quad")]
        Quad,


        /// <summary>
        /// Quadrangle
        /// </summary>
        [EnumMember]
        [Name(Code = "Qdgl", Name = "Quadrangle")]
        Quadrangle,


        /// <summary>
        /// Quadrant
        /// </summary>
        [EnumMember]
        [Name(Code = "Qdrt", Name = "Quadrant")]
        Quadrant,


        /// <summary>
        /// Quay
        /// </summary>
        [EnumMember]
        [Name(Code = "Qy", Name = "Quay")]
        Quay,


        /// <summary>
        /// Quays
        /// </summary>
        [EnumMember]
        [Name(Code = "Qys", Name = "Quays")]
        Quays,


        /// <summary>
        /// Ramble
        /// </summary>
        [EnumMember]
        [Name(Code = "Rmbl", Name = "Ramble")]
        Ramble,


        /// <summary>
        /// Ramp
        /// </summary>
        [EnumMember]
        [Name(Code = "Ramp", Name = "Ramp")]
        Ramp,


        /// <summary>
        /// Range
        /// </summary>
        [EnumMember]
        [Name(Code = "Rnge", Name = "Range")]
        Range,


        /// <summary>
        /// Reach
        /// </summary>
        [EnumMember]
        [Name(Code = "Rch", Name = "Reach")]
        Reach,


        /// <summary>
        /// Reserve
        /// </summary>
        [EnumMember]
        [Name(Code = "Res", Name = "Reserve")]
        Reserve,


        /// <summary>
        /// Rest
        /// </summary>
        [EnumMember]
        [Name(Code = "Rest", Name = "Rest")]
        Rest,


        /// <summary>
        /// Retreat
        /// </summary>
        [EnumMember]
        [Name(Code = "Rtt", Name = "Retreat")]
        Retreat,


        /// <summary>
        /// Ride
        /// </summary>
        [EnumMember]
        [Name(Code = "Ride", Name = "Ride")]
        Ride,


        /// <summary>
        /// Ridge
        /// </summary>
        [EnumMember]
        [Name(Code = "Rdge", Name = "Ridge")]
        Ridge,


        /// <summary>
        /// Ridgeway
        /// </summary>
        [EnumMember]
        [Name(Code = "Rgwy", Name = "Ridgeway")]
        Ridgeway,


        /// <summary>
        /// Right Of Way
        /// </summary>
        [EnumMember]
        [Name(Code = "Rowy", Name = "Right Of Way")]
        RightOfWay,


        /// <summary>
        /// Ring
        /// </summary>
        [EnumMember]
        [Name(Code = "Ring", Name = "Ring")]
        Ring,


        /// <summary>
        /// Rise
        /// </summary>
        [EnumMember]
        [Name(Code = "Rise", Name = "Rise")]
        Rise,


        /// <summary>
        /// River
        /// </summary>
        [EnumMember]
        [Name(Code = "Rvr", Name = "River")]
        River,


        /// <summary>
        /// Riverway
        /// </summary>
        [EnumMember]
        [Name(Code = "Rvwy", Name = "Riverway")]
        Riverway,


        /// <summary>
        /// Riviera
        /// </summary>
        [EnumMember]
        [Name(Code = "Rvra", Name = "Riviera")]
        Riviera,


        /// <summary>
        /// Road
        /// </summary>
        [EnumMember]
        [Name(Code = "Rd", Name = "Road")]
        Road,


        /// <summary>
        /// Roads
        /// </summary>
        [EnumMember]
        [Name(Code = "Rds", Name = "Roads")]
        Roads,


        /// <summary>
        /// Roadside
        /// </summary>
        [EnumMember]
        [Name(Code = "Rdsd", Name = "Roadside")]
        Roadside,


        /// <summary>
        /// Roadway
        /// </summary>
        [EnumMember]
        [Name(Code = "Rdwy", Name = "Roadway")]
        Roadway,


        /// <summary>
        /// Ronde
        /// </summary>
        [EnumMember]
        [Name(Code = "Rnde", Name = "Ronde")]
        Ronde,


        /// <summary>
        /// Rosebowl
        /// </summary>
        [EnumMember]
        [Name(Code = "Rsbl", Name = "Rosebowl")]
        Rosebowl,


        /// <summary>
        /// Rotary
        /// </summary>
        [EnumMember]
        [Name(Code = "Rty", Name = "Rotary")]
        Rotary,


        /// <summary>
        /// Round
        /// </summary>
        [EnumMember]
        [Name(Code = "Rnd", Name = "Round")]
        Round,


        /// <summary>
        /// Route
        /// </summary>
        [EnumMember]
        [Name(Code = "Rte", Name = "Route")]
        Route,


        /// <summary>
        /// Row
        /// </summary>
        [EnumMember]
        [Name(Code = "Row", Name = "Row")]
        Row,


        /// <summary>
        /// Rue
        /// </summary>
        [EnumMember]
        [Name(Code = "Rue", Name = "Rue")]
        Rue,


        /// <summary>
        /// Run
        /// </summary>
        [EnumMember]
        [Name(Code = "Run", Name = "Run")]
        Run,


        /// <summary>
        /// Service Way
        /// </summary>
        [EnumMember]
        [Name(Code = "Swy", Name = "Service Way")]
        ServiceWay,


        /// <summary>
        /// Siding
        /// </summary>
        [EnumMember]
        [Name(Code = "Sdng", Name = "Siding")]
        Siding,


        /// <summary>
        /// Slope
        /// </summary>
        [EnumMember]
        [Name(Code = "Slpe", Name = "Slope")]
        Slope,


        /// <summary>
        /// Sound
        /// </summary>
        [EnumMember]
        [Name(Code = "Snd", Name = "Sound")]
        Sound,


        /// <summary>
        /// Spur
        /// </summary>
        [EnumMember]
        [Name(Code = "Spur", Name = "Spur")]
        Spur,


        /// <summary>
        /// Square
        /// </summary>
        [EnumMember]
        [Name(Code = "Sq", Name = "Square")]
        Square,


        /// <summary>
        /// Stairs
        /// </summary>
        [EnumMember]
        [Name(Code = "Strs", Name = "Stairs")]
        Stairs,


        /// <summary>
        /// State Highway
        /// </summary>
        [EnumMember]
        [Name(Code = "Shwy", Name = "State Highway")]
        StateHighway,


        /// <summary>
        /// Steps
        /// </summary>
        [EnumMember]
        [Name(Code = "Stps", Name = "Steps")]
        Steps,


        /// <summary>
        /// Strand
        /// </summary>
        [EnumMember]
        [Name(Code = "Stra", Name = "Strand")]
        Strand,


        /// <summary>
        /// Street
        /// </summary>
        [EnumMember]
        [Name(Code = "St", Name = "Street")]
        Street,


        /// <summary>
        /// Strip
        /// </summary>
        [EnumMember]
        [Name(Code = "Strp", Name = "Strip")]
        Strip,


        /// <summary>
        /// Subway
        /// </summary>
        [EnumMember]
        [Name(Code = "Sbwy", Name = "Subway")]
        Subway,


        /// <summary>
        /// Tarn
        /// </summary>
        [EnumMember]
        [Name(Code = "Tarn", Name = "Tarn")]
        Tarn,


        /// <summary>
        /// Terrace
        /// </summary>
        [EnumMember]
        [Name(Code = "Tce", Name = "Terrace")]
        Terrace,


        /// <summary>
        /// Thoroughfare
        /// </summary>
        [EnumMember]
        [Name(Code = "Thor", Name = "Thoroughfare")]
        Thoroughfare,


        /// <summary>
        /// Tollway
        /// </summary>
        [EnumMember]
        [Name(Code = "Tlwy", Name = "Tollway")]
        Tollway,


        /// <summary>
        /// Top
        /// </summary>
        [EnumMember]
        [Name(Code = "Top", Name = "Top")]
        Top,


        /// <summary>
        /// Tor
        /// </summary>
        [EnumMember]
        [Name(Code = "Tor", Name = "Tor")]
        Tor,


        /// <summary>
        /// Towers
        /// </summary>
        [EnumMember]
        [Name(Code = "Twrs", Name = "Towers")]
        Towers,


        /// <summary>
        /// Track
        /// </summary>
        [EnumMember]
        [Name(Code = "Trk", Name = "Track")]
        Track,


        /// <summary>
        /// Trail
        /// </summary>
        [EnumMember]
        [Name(Code = "Trl", Name = "Trail")]
        Trail,


        /// <summary>
        /// Trailer
        /// </summary>
        [EnumMember]
        [Name(Code = "Trlr", Name = "Trailer")]
        Trailer,


        /// <summary>
        /// Triangle
        /// </summary>
        [EnumMember]
        [Name(Code = "Tri", Name = "Triangle")]
        Triangle,


        /// <summary>
        /// Trunkway
        /// </summary>
        [EnumMember]
        [Name(Code = "Tkwy", Name = "Trunkway")]
        Trunkway,


        /// <summary>
        /// Turn
        /// </summary>
        [EnumMember]
        [Name(Code = "Turn", Name = "Turn")]
        Turn,


        /// <summary>
        /// Underpass
        /// </summary>
        [EnumMember]
        [Name(Code = "Upas", Name = "Underpass")]
        Underpass,


        /// <summary>
        /// Upper
        /// </summary>
        [EnumMember]
        [Name(Code = "Upr", Name = "Upper")]
        Upper,


        /// <summary>
        /// Vale
        /// </summary>
        [EnumMember]
        [Name(Code = "Vale", Name = "Vale")]
        Vale,


        /// <summary>
        /// Viaduct
        /// </summary>
        [EnumMember]
        [Name(Code = "Vdct", Name = "Viaduct")]
        Viaduct,


        /// <summary>
        /// View
        /// </summary>
        [EnumMember]
        [Name(Code = "View", Name = "View")]
        View,


        /// <summary>
        /// Villas
        /// </summary>
        [EnumMember]
        [Name(Code = "Vlls", Name = "Villas")]
        Villas,


        /// <summary>
        /// Vista
        /// </summary>
        [EnumMember]
        [Name(Code = "Vsta", Name = "Vista")]
        Vista,


        /// <summary>
        /// Wade
        /// </summary>
        [EnumMember]
        [Name(Code = "Wade", Name = "Wade")]
        Wade,


        /// <summary>
        /// Walk
        /// </summary>
        [EnumMember]
        [Name(Code = "Walk", Name = "Walk")]
        Walk,


        /// <summary>
        /// Walkway
        /// </summary>
        [EnumMember]
        [Name(Code = "Wkwy", Name = "Walkway")]
        Walkway,


        /// <summary>
        /// Way
        /// </summary>
        [EnumMember]
        [Name(Code = "Way", Name = "Way")]
        Way,


        /// <summary>
        /// Wharf
        /// </summary>
        [EnumMember]
        [Name(Code = "Whrf", Name = "Wharf")]
        Wharf,


        /// <summary>
        /// Wynd
        /// </summary>
        [EnumMember]
        [Name(Code = "Wynd", Name = "Wynd")]
        Wynd,


        /// <summary>
        /// Yard
        /// </summary>
        [EnumMember]
        [Name(Code = "Yard", Name = "Yard")]
        Yard


    }
}
