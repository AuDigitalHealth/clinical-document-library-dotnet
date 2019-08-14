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
    /// Country
    /// </summary>
    [Serializable]
    [DataContract]
    public enum Country
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
        /// Adelie Land (France)
        /// </summary>
        [EnumMember]
        [Name(Code = "1601", Name = "Adelie Land (France)")]
        AdelieLandFrance,

        /// <summary>
        /// Afghanistan
        /// </summary>
        [EnumMember]
        [Name(Code = "7201", Name = "Afghanistan")]
        Afghanistan,

        /// <summary>
        /// Aland Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "2408", Name = "Aland Islands")]
        AlandIslands,

        /// <summary>
        /// Albania
        /// </summary>
        [EnumMember]
        [Name(Code = "3201", Name = "Albania")]
        Albania,

        /// <summary>
        /// Algeria
        /// </summary>
        [EnumMember]
        [Name(Code = "4101", Name = "Algeria")]
        Algeria,

        /// <summary>
        /// Andorra
        /// </summary>
        [EnumMember]
        [Name(Code = "3101", Name = "Andorra")]
        Andorra,

        /// <summary>
        /// Angola
        /// </summary>
        [EnumMember]
        [Name(Code = "9201", Name = "Angola")]
        Angola,

        /// <summary>
        /// Anguilla
        /// </summary>
        [EnumMember]
        [Name(Code = "8401", Name = "Anguilla")]
        Anguilla,

        /// <summary>
        /// Antigua and Barbuda
        /// </summary>
        [EnumMember]
        [Name(Code = "8402", Name = "Antigua and Barbuda")]
        AntiguaAndBarbuda,

        /// <summary>
        /// Argentina
        /// </summary>
        [EnumMember]
        [Name(Code = "8201", Name = "Argentina")]
        Argentina,

        /// <summary>
        /// Argentinian Antarctic Territory
        /// </summary>
        [EnumMember]
        [Name(Code = "1602", Name = "Argentinian Antarctic Territory")]
        ArgentinianAntarcticTerritory,

        /// <summary>
        /// Armenia
        /// </summary>
        [EnumMember]
        [Name(Code = "7202", Name = "Armenia")]
        Armenia,

        /// <summary>
        /// Aruba
        /// </summary>
        [EnumMember]
        [Name(Code = "8403", Name = "Aruba")]
        Aruba,

        /// <summary>
        /// Australia
        /// </summary>
        [EnumMember]
        [Name(Code = "AU", Name = "Australia")]
        Australia,

        /// <summary>
        /// Australian Antarctic Territory
        /// </summary>
        [EnumMember]
        [Name(Code = "1603", Name = "Australian Antarctic Territory")]
        AustralianAntarcticTerritory,

        /// <summary>
        /// Australian External Territories, nec
        /// </summary>
        [EnumMember]
        [Name(Code = "1199", Name = "Australian External Territories, nec")]
        AustralianExternalTerritoriesNec,

        /// <summary>
        /// Austria
        /// </summary>
        [EnumMember]
        [Name(Code = "2301", Name = "Austria")]
        Austria,

        /// <summary>
        /// Azerbaijan
        /// </summary>
        [EnumMember]
        [Name(Code = "7203", Name = "Azerbaijan")]
        Azerbaijan,

        /// <summary>
        /// Bahamas
        /// </summary>
        [EnumMember]
        [Name(Code = "8404", Name = "Bahamas")]
        Bahamas,

        /// <summary>
        /// Bahrain
        /// </summary>
        [EnumMember]
        [Name(Code = "4201", Name = "Bahrain")]
        Bahrain,

        /// <summary>
        /// Bangladesh
        /// </summary>
        [EnumMember]
        [Name(Code = "7101", Name = "Bangladesh")]
        Bangladesh,

        /// <summary>
        /// Barbados
        /// </summary>
        [EnumMember]
        [Name(Code = "8405", Name = "Barbados")]
        Barbados,

        /// <summary>
        /// Belarus
        /// </summary>
        [EnumMember]
        [Name(Code = "3301", Name = "Belarus")]
        Belarus,

        /// <summary>
        /// Belgium
        /// </summary>
        [EnumMember]
        [Name(Code = "2302", Name = "Belgium")]
        Belgium,

        /// <summary>
        /// Belize
        /// </summary>
        [EnumMember]
        [Name(Code = "8301", Name = "Belize")]
        Belize,

        /// <summary>
        /// Benin
        /// </summary>
        [EnumMember]
        [Name(Code = "9101", Name = "Benin")]
        Benin,

        /// <summary>
        /// Bermuda
        /// </summary>
        [EnumMember]
        [Name(Code = "8101", Name = "Bermuda")]
        Bermuda,

        /// <summary>
        /// Bhutan
        /// </summary>
        [EnumMember]
        [Name(Code = "7102", Name = "Bhutan")]
        Bhutan,

        /// <summary>
        /// Bolivia
        /// </summary>
        [EnumMember]
        [Name(Code = "8202", Name = "Bolivia")]
        Bolivia,

        /// <summary>
        /// Bosnia and Herzegovina
        /// </summary>
        [EnumMember]
        [Name(Code = "3202", Name = "Bosnia and Herzegovina")]
        BosniaAndHerzegovina,

        /// <summary>
        /// Botswana
        /// </summary>
        [EnumMember]
        [Name(Code = "9202", Name = "Botswana")]
        Botswana,

        /// <summary>
        /// Brazil
        /// </summary>
        [EnumMember]
        [Name(Code = "8203", Name = "Brazil")]
        Brazil,

        /// <summary>
        /// British Antarctic Territory
        /// </summary>
        [EnumMember]
        [Name(Code = "1604", Name = "British Antarctic Territory")]
        BritishAntarcticTerritory,

        /// <summary>
        /// Brunei Darussalam
        /// </summary>
        [EnumMember]
        [Name(Code = "5201", Name = "Brunei Darussalam")]
        BruneiDarussalam,

        /// <summary>
        /// Bulgaria
        /// </summary>
        [EnumMember]
        [Name(Code = "3203", Name = "Bulgaria")]
        Bulgaria,

        /// <summary>
        /// Burkina Faso
        /// </summary>
        [EnumMember]
        [Name(Code = "9102", Name = "Burkina Faso")]
        BurkinaFaso,

        /// <summary>
        /// Burma (Myanmar)
        /// </summary>
        [EnumMember]
        [Name(Code = "5101", Name = "Burma (Myanmar)")]
        BurmaMyanmar,

        /// <summary>
        /// Burundi
        /// </summary>
        [EnumMember]
        [Name(Code = "9203", Name = "Burundi")]
        Burundi,

        /// <summary>
        /// Cambodia
        /// </summary>
        [EnumMember]
        [Name(Code = "5102", Name = "Cambodia")]
        Cambodia,

        /// <summary>
        /// Cameroon
        /// </summary>
        [EnumMember]
        [Name(Code = "9103", Name = "Cameroon")]
        Cameroon,

        /// <summary>
        /// Canada
        /// </summary>
        [EnumMember]
        [Name(Code = "8102", Name = "Canada")]
        Canada,

        /// <summary>
        /// Cape Verde
        /// </summary>
        [EnumMember]
        [Name(Code = "9104", Name = "Cape Verde")]
        CapeVerde,

        /// <summary>
        /// Cayman Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "8406", Name = "Cayman Islands")]
        CaymanIslands,

        /// <summary>
        /// Central African Republic
        /// </summary>
        [EnumMember]
        [Name(Code = "9105", Name = "Central African Republic")]
        CentralAfricanRepublic,

        /// <summary>
        /// Chad
        /// </summary>
        [EnumMember]
        [Name(Code = "9106", Name = "Chad")]
        Chad,

        /// <summary>
        /// Chile
        /// </summary>
        [EnumMember]
        [Name(Code = "8204", Name = "Chile")]
        Chile,

        /// <summary>
        /// Chilean Antarctic Territory
        /// </summary>
        [EnumMember]
        [Name(Code = "1605", Name = "Chilean Antarctic Territory")]
        ChileanAntarcticTerritory,

        /// <summary>
        /// China (excludes SARs and Taiwan)
        /// </summary>
        [EnumMember]
        [Name(Code = "6101", Name = "China (excludes SARs and Taiwan)")]
        ChinaExcludesSARsAndTaiwan,

        /// <summary>
        /// Colombia
        /// </summary>
        [EnumMember]
        [Name(Code = "8205", Name = "Colombia")]
        Colombia,

        /// <summary>
        /// Comoros
        /// </summary>
        [EnumMember]
        [Name(Code = "9204", Name = "Comoros")]
        Comoros,

        /// <summary>
        /// Congo
        /// </summary>
        [EnumMember]
        [Name(Code = "9107", Name = "Congo")]
        Congo,

        /// <summary>
        /// Congo, Democratic Republic of
        /// </summary>
        [EnumMember]
        [Name(Code = "9108", Name = "Congo, Democratic Republic of")]
        CongoDemocraticRepublicOf,

        /// <summary>
        /// Cook Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "1501", Name = "Cook Islands")]
        CookIslands,

        /// <summary>
        /// Costa Rica
        /// </summary>
        [EnumMember]
        [Name(Code = "8302", Name = "Costa Rica")]
        CostaRica,

        /// <summary>
        /// Cote d'Ivoire
        /// </summary>
        [EnumMember]
        [Name(Code = "9111", Name = "Cote d'Ivoire")]
        CotedIvoire,

        /// <summary>
        /// Croatia
        /// </summary>
        [EnumMember]
        [Name(Code = "3204", Name = "Croatia")]
        Croatia,

        /// <summary>
        /// Cuba
        /// </summary>
        [EnumMember]
        [Name(Code = "8407", Name = "Cuba")]
        Cuba,

        /// <summary>
        /// Cyprus
        /// </summary>
        [EnumMember]
        [Name(Code = "3205", Name = "Cyprus")]
        Cyprus,

        /// <summary>
        /// Czech Republic
        /// </summary>
        [EnumMember]
        [Name(Code = "3302", Name = "Czech Republic")]
        CzechRepublic,

        /// <summary>
        /// Denmark
        /// </summary>
        [EnumMember]
        [Name(Code = "2401", Name = "Denmark")]
        Denmark,

        /// <summary>
        /// Djibouti
        /// </summary>
        [EnumMember]
        [Name(Code = "9205", Name = "Djibouti")]
        Djibouti,

        /// <summary>
        /// Dominica
        /// </summary>
        [EnumMember]
        [Name(Code = "8408", Name = "Dominica")]
        Dominica,

        /// <summary>
        /// Dominican Republic
        /// </summary>
        [EnumMember]
        [Name(Code = "8411", Name = "Dominican Republic")]
        DominicanRepublic,

        /// <summary>
        /// East Timor
        /// </summary>
        [EnumMember]
        [Name(Code = "5206", Name = "East Timor")]
        EastTimor,

        /// <summary>
        /// Ecuador
        /// </summary>
        [EnumMember]
        [Name(Code = "8206", Name = "Ecuador")]
        Ecuador,

        /// <summary>
        /// Egypt
        /// </summary>
        [EnumMember]
        [Name(Code = "4102", Name = "Egypt")]
        Egypt,

        /// <summary>
        /// El Salvador
        /// </summary>
        [EnumMember]
        [Name(Code = "8303", Name = "El Salvador")]
        ElSalvador,

        /// <summary>
        /// England
        /// </summary>
        [EnumMember]
        [Name(Code = "2102", Name = "England")]
        England,

        /// <summary>
        /// Equatorial Guinea
        /// </summary>
        [EnumMember]
        [Name(Code = "9112", Name = "Equatorial Guinea")]
        EquatorialGuinea,

        /// <summary>
        /// Eritrea
        /// </summary>
        [EnumMember]
        [Name(Code = "9206", Name = "Eritrea")]
        Eritrea,

        /// <summary>
        /// Estonia
        /// </summary>
        [EnumMember]
        [Name(Code = "3303", Name = "Estonia")]
        Estonia,

        /// <summary>
        /// Ethiopia
        /// </summary>
        [EnumMember]
        [Name(Code = "9207", Name = "Ethiopia")]
        Ethiopia,

        /// <summary>
        /// Falkland Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "8207", Name = "Falkland Islands")]
        FalklandIslands,

        /// <summary>
        /// Faroe Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "2402", Name = "Faroe Islands")]
        FaroeIslands,

        /// <summary>
        /// Fiji
        /// </summary>
        [EnumMember]
        [Name(Code = "1502", Name = "Fiji")]
        Fiji,

        /// <summary>
        /// Finland
        /// </summary>
        [EnumMember]
        [Name(Code = "2403", Name = "Finland")]
        Finland,

        /// <summary>
        /// Former Yugoslav Republic of Macedonia (FYROM)
        /// </summary>
        [EnumMember]
        [Name(Code = "3206", Name = "Former Yugoslav Republic of Macedonia (FYROM)")]
        FormerYugoslavRepublicOfMacedoniaFYROM,

        /// <summary>
        /// France
        /// </summary>
        [EnumMember]
        [Name(Code = "2303", Name = "France")]
        France,

        /// <summary>
        /// French Guiana
        /// </summary>
        [EnumMember]
        [Name(Code = "8208", Name = "French Guiana")]
        FrenchGuiana,

        /// <summary>
        /// French Polynesia
        /// </summary>
        [EnumMember]
        [Name(Code = "1503", Name = "French Polynesia")]
        FrenchPolynesia,

        /// <summary>
        /// Gabon
        /// </summary>
        [EnumMember]
        [Name(Code = "9113", Name = "Gabon")]
        Gabon,

        /// <summary>
        /// Gambia
        /// </summary>
        [EnumMember]
        [Name(Code = "9114", Name = "Gambia")]
        Gambia,

        /// <summary>
        /// Gaza Strip and West Bank
        /// </summary>
        [EnumMember]
        [Name(Code = "4202", Name = "Gaza Strip and West Bank")]
        GazaStripAndWestBank,

        /// <summary>
        /// Georgia
        /// </summary>
        [EnumMember]
        [Name(Code = "7204", Name = "Georgia")]
        Georgia,

        /// <summary>
        /// Germany
        /// </summary>
        [EnumMember]
        [Name(Code = "2304", Name = "Germany")]
        Germany,

        /// <summary>
        /// Ghana
        /// </summary>
        [EnumMember]
        [Name(Code = "9115", Name = "Ghana")]
        Ghana,

        /// <summary>
        /// Gibraltar
        /// </summary>
        [EnumMember]
        [Name(Code = "3102", Name = "Gibraltar")]
        Gibraltar,

        /// <summary>
        /// Greece
        /// </summary>
        [EnumMember]
        [Name(Code = "3207", Name = "Greece")]
        Greece,

        /// <summary>
        /// Greenland
        /// </summary>
        [EnumMember]
        [Name(Code = "2404", Name = "Greenland")]
        Greenland,

        /// <summary>
        /// Grenada
        /// </summary>
        [EnumMember]
        [Name(Code = "8412", Name = "Grenada")]
        Grenada,

        /// <summary>
        /// Guadeloupe
        /// </summary>
        [EnumMember]
        [Name(Code = "8413", Name = "Guadeloupe")]
        Guadeloupe,

        /// <summary>
        /// Guam
        /// </summary>
        [EnumMember]
        [Name(Code = "1401", Name = "Guam")]
        Guam,

        /// <summary>
        /// Guatemala
        /// </summary>
        [EnumMember]
        [Name(Code = "8304", Name = "Guatemala")]
        Guatemala,

        /// <summary>
        /// Guernsey
        /// </summary>
        [EnumMember]
        [Name(Code = "2107", Name = "Guernsey")]
        Guernsey,

        /// <summary>
        /// Guinea
        /// </summary>
        [EnumMember]
        [Name(Code = "9116", Name = "Guinea")]
        Guinea,

        /// <summary>
        /// Guinea-Bissau
        /// </summary>
        [EnumMember]
        [Name(Code = "9117", Name = "Guinea-Bissau")]
        GuineaBissau,

        /// <summary>
        /// Guyana
        /// </summary>
        [EnumMember]
        [Name(Code = "8211", Name = "Guyana")]
        Guyana,

        /// <summary>
        /// Haiti
        /// </summary>
        [EnumMember]
        [Name(Code = "8414", Name = "Haiti")]
        Haiti,

        /// <summary>
        /// Holy See
        /// </summary>
        [EnumMember]
        [Name(Code = "3103", Name = "Holy See")]
        HolySee,

        /// <summary>
        /// Honduras
        /// </summary>
        [EnumMember]
        [Name(Code = "8305", Name = "Honduras")]
        Honduras,

        /// <summary>
        /// Hong Kong (SAR of China)
        /// </summary>
        [EnumMember]
        [Name(Code = "6102", Name = "Hong Kong (SAR of China)")]
        HongKongSAROfChina,

        /// <summary>
        /// Hungary
        /// </summary>
        [EnumMember]
        [Name(Code = "3304", Name = "Hungary")]
        Hungary,

        /// <summary>
        /// Iceland
        /// </summary>
        [EnumMember]
        [Name(Code = "2405", Name = "Iceland")]
        Iceland,

        /// <summary>
        /// India
        /// </summary>
        [EnumMember]
        [Name(Code = "7103", Name = "India")]
        India,

        /// <summary>
        /// Indonesia
        /// </summary>
        [EnumMember]
        [Name(Code = "5202", Name = "Indonesia")]
        Indonesia,

        /// <summary>
        /// Iran
        /// </summary>
        [EnumMember]
        [Name(Code = "4203", Name = "Iran")]
        Iran,

        /// <summary>
        /// Iraq
        /// </summary>
        [EnumMember]
        [Name(Code = "4204", Name = "Iraq")]
        Iraq,

        /// <summary>
        /// Ireland
        /// </summary>
        [EnumMember]
        [Name(Code = "2201", Name = "Ireland")]
        Ireland,

        /// <summary>
        /// Isle of Man
        /// </summary>
        [EnumMember]
        [Name(Code = "2103", Name = "Isle of Man")]
        IsleOfMan,

        /// <summary>
        /// Israel
        /// </summary>
        [EnumMember]
        [Name(Code = "4205", Name = "Israel")]
        Israel,

        /// <summary>
        /// Italy
        /// </summary>
        [EnumMember]
        [Name(Code = "3104", Name = "Italy")]
        Italy,

        /// <summary>
        /// Jamaica
        /// </summary>
        [EnumMember]
        [Name(Code = "8415", Name = "Jamaica")]
        Jamaica,

        /// <summary>
        /// Japan
        /// </summary>
        [EnumMember]
        [Name(Code = "6201", Name = "Japan")]
        Japan,

        /// <summary>
        /// Jersey
        /// </summary>
        [EnumMember]
        [Name(Code = "2108", Name = "Jersey")]
        Jersey,

        /// <summary>
        /// Jordan
        /// </summary>
        [EnumMember]
        [Name(Code = "4206", Name = "Jordan")]
        Jordan,

        /// <summary>
        /// Kazakhstan
        /// </summary>
        [EnumMember]
        [Name(Code = "7205", Name = "Kazakhstan")]
        Kazakhstan,

        /// <summary>
        /// Kenya
        /// </summary>
        [EnumMember]
        [Name(Code = "9208", Name = "Kenya")]
        Kenya,

        /// <summary>
        /// Kiribati
        /// </summary>
        [EnumMember]
        [Name(Code = "1402", Name = "Kiribati")]
        Kiribati,

        /// <summary>
        /// Korea, Democratic People's Republic of (North)
        /// </summary>
        [EnumMember]
        [Name(Code = "6202", Name = "Korea, Democratic People's Republic of (North)")]
        KoreaDemocraticPeoplesRepublicOfNorth,

        /// <summary>
        /// Korea, Republic of (South)
        /// </summary>
        [EnumMember]
        [Name(Code = "6203", Name = "Korea, Republic of (South)")]
        KoreaRepublicOfSouth,

        /// <summary>
        /// Kosovo
        /// </summary>
        [EnumMember]
        [Name(Code = "3216", Name = "Kosovo")]
        Kosovo,

        /// <summary>
        /// Kuwait
        /// </summary>
        [EnumMember]
        [Name(Code = "4207", Name = "Kuwait")]
        Kuwait,

        /// <summary>
        /// Kyrgyzstan
        /// </summary>
        [EnumMember]
        [Name(Code = "7206", Name = "Kyrgyzstan")]
        Kyrgyzstan,

        /// <summary>
        /// Laos
        /// </summary>
        [EnumMember]
        [Name(Code = "5103", Name = "Laos")]
        Laos,

        /// <summary>
        /// Latvia
        /// </summary>
        [EnumMember]
        [Name(Code = "3305", Name = "Latvia")]
        Latvia,

        /// <summary>
        /// Lebanon
        /// </summary>
        [EnumMember]
        [Name(Code = "4208", Name = "Lebanon")]
        Lebanon,

        /// <summary>
        /// Lesotho
        /// </summary>
        [EnumMember]
        [Name(Code = "9211", Name = "Lesotho")]
        Lesotho,

        /// <summary>
        /// Liberia
        /// </summary>
        [EnumMember]
        [Name(Code = "9118", Name = "Liberia")]
        Liberia,

        /// <summary>
        /// Libya
        /// </summary>
        [EnumMember]
        [Name(Code = "4103", Name = "Libya")]
        Libya,

        /// <summary>
        /// Liechtenstein
        /// </summary>
        [EnumMember]
        [Name(Code = "2305", Name = "Liechtenstein")]
        Liechtenstein,

        /// <summary>
        /// Lithuania
        /// </summary>
        [EnumMember]
        [Name(Code = "3306", Name = "Lithuania")]
        Lithuania,

        /// <summary>
        /// Luxembourg
        /// </summary>
        [EnumMember]
        [Name(Code = "2306", Name = "Luxembourg")]
        Luxembourg,

        /// <summary>
        /// Macau (SAR of China)
        /// </summary>
        [EnumMember]
        [Name(Code = "6103", Name = "Macau (SAR of China)")]
        MacauSAROfChina,

        /// <summary>
        /// Madagascar
        /// </summary>
        [EnumMember]
        [Name(Code = "9212", Name = "Madagascar")]
        Madagascar,

        /// <summary>
        /// Malawi
        /// </summary>
        [EnumMember]
        [Name(Code = "9213", Name = "Malawi")]
        Malawi,

        /// <summary>
        /// Malaysia
        /// </summary>
        [EnumMember]
        [Name(Code = "5203", Name = "Malaysia")]
        Malaysia,

        /// <summary>
        /// Maldives
        /// </summary>
        [EnumMember]
        [Name(Code = "7104", Name = "Maldives")]
        Maldives,

        /// <summary>
        /// Mali
        /// </summary>
        [EnumMember]
        [Name(Code = "9121", Name = "Mali")]
        Mali,

        /// <summary>
        /// Malta
        /// </summary>
        [EnumMember]
        [Name(Code = "3105", Name = "Malta")]
        Malta,

        /// <summary>
        /// Marshall Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "1403", Name = "Marshall Islands")]
        MarshallIslands,

        /// <summary>
        /// Martinique
        /// </summary>
        [EnumMember]
        [Name(Code = "8416", Name = "Martinique")]
        Martinique,

        /// <summary>
        /// Mauritania
        /// </summary>
        [EnumMember]
        [Name(Code = "9122", Name = "Mauritania")]
        Mauritania,

        /// <summary>
        /// Mauritius
        /// </summary>
        [EnumMember]
        [Name(Code = "9214", Name = "Mauritius")]
        Mauritius,

        /// <summary>
        /// Mayotte
        /// </summary>
        [EnumMember]
        [Name(Code = "9215", Name = "Mayotte")]
        Mayotte,

        /// <summary>
        /// Mexico
        /// </summary>
        [EnumMember]
        [Name(Code = "8306", Name = "Mexico")]
        Mexico,

        /// <summary>
        /// Micronesia, Federated States of
        /// </summary>
        [EnumMember]
        [Name(Code = "1404", Name = "Micronesia, Federated States of")]
        MicronesiaFederatedStatesOf,

        /// <summary>
        /// Moldova
        /// </summary>
        [EnumMember]
        [Name(Code = "3208", Name = "Moldova")]
        Moldova,

        /// <summary>
        /// Monaco
        /// </summary>
        [EnumMember]
        [Name(Code = "2307", Name = "Monaco")]
        Monaco,

        /// <summary>
        /// Mongolia
        /// </summary>
        [EnumMember]
        [Name(Code = "6104", Name = "Mongolia")]
        Mongolia,

        /// <summary>
        /// Montenegro
        /// </summary>
        [EnumMember]
        [Name(Code = "3214", Name = "Montenegro")]
        Montenegro,

        /// <summary>
        /// Montserrat
        /// </summary>
        [EnumMember]
        [Name(Code = "8417", Name = "Montserrat")]
        Montserrat,

        /// <summary>
        /// Morocco
        /// </summary>
        [EnumMember]
        [Name(Code = "4104", Name = "Morocco")]
        Morocco,

        /// <summary>
        /// Mozambique
        /// </summary>
        [EnumMember]
        [Name(Code = "9216", Name = "Mozambique")]
        Mozambique,

        /// <summary>
        /// Namibia
        /// </summary>
        [EnumMember]
        [Name(Code = "9217", Name = "Namibia")]
        Namibia,

        /// <summary>
        /// Nauru
        /// </summary>
        [EnumMember]
        [Name(Code = "1405", Name = "Nauru")]
        Nauru,

        /// <summary>
        /// Nepal
        /// </summary>
        [EnumMember]
        [Name(Code = "7105", Name = "Nepal")]
        Nepal,

        /// <summary>
        /// Netherlands
        /// </summary>
        [EnumMember]
        [Name(Code = "2308", Name = "Netherlands")]
        Netherlands,

        /// <summary>
        /// Netherlands Antilles
        /// </summary>
        [EnumMember]
        [Name(Code = "8418", Name = "Netherlands Antilles")]
        NetherlandsAntilles,

        /// <summary>
        /// New Caledonia
        /// </summary>
        [EnumMember]
        [Name(Code = "1301", Name = "New Caledonia")]
        NewCaledonia,

        /// <summary>
        /// New Zealand
        /// </summary>
        [EnumMember]
        [Name(Code = "1201", Name = "New Zealand")]
        NewZealand,

        /// <summary>
        /// Nicaragua
        /// </summary>
        [EnumMember]
        [Name(Code = "8307", Name = "Nicaragua")]
        Nicaragua,

        /// <summary>
        /// Niger
        /// </summary>
        [EnumMember]
        [Name(Code = "9123", Name = "Niger")]
        Niger,

        /// <summary>
        /// Nigeria
        /// </summary>
        [EnumMember]
        [Name(Code = "9124", Name = "Nigeria")]
        Nigeria,

        /// <summary>
        /// Niue
        /// </summary>
        [EnumMember]
        [Name(Code = "1504", Name = "Niue")]
        Niue,

        /// <summary>
        /// Norfolk Island
        /// </summary>
        [EnumMember]
        [Name(Code = "1102", Name = "Norfolk Island")]
        NorfolkIsland,

        /// <summary>
        /// Northern Ireland
        /// </summary>
        [EnumMember]
        [Name(Code = "2104", Name = "Northern Ireland")]
        NorthernIreland,

        /// <summary>
        /// Northern Mariana Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "1406", Name = "Northern Mariana Islands")]
        NorthernMarianaIslands,

        /// <summary>
        /// Norway
        /// </summary>
        [EnumMember]
        [Name(Code = "2406", Name = "Norway")]
        Norway,

        /// <summary>
        /// Oman
        /// </summary>
        [EnumMember]
        [Name(Code = "4211", Name = "Oman")]
        Oman,

        /// <summary>
        /// Pakistan
        /// </summary>
        [EnumMember]
        [Name(Code = "7106", Name = "Pakistan")]
        Pakistan,

        /// <summary>
        /// Palau
        /// </summary>
        [EnumMember]
        [Name(Code = "1407", Name = "Palau")]
        Palau,

        /// <summary>
        /// Panama
        /// </summary>
        [EnumMember]
        [Name(Code = "8308", Name = "Panama")]
        Panama,

        /// <summary>
        /// Papua New Guinea
        /// </summary>
        [EnumMember]
        [Name(Code = "1302", Name = "Papua New Guinea")]
        PapuaNewGuinea,

        /// <summary>
        /// Paraguay
        /// </summary>
        [EnumMember]
        [Name(Code = "8212", Name = "Paraguay")]
        Paraguay,

        /// <summary>
        /// Peru
        /// </summary>
        [EnumMember]
        [Name(Code = "8213", Name = "Peru")]
        Peru,

        /// <summary>
        /// Philippines
        /// </summary>
        [EnumMember]
        [Name(Code = "5204", Name = "Philippines")]
        Philippines,

        /// <summary>
        /// Pitcairn Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "1513", Name = "Pitcairn Islands")]
        PitcairnIslands,

        /// <summary>
        /// Poland
        /// </summary>
        [EnumMember]
        [Name(Code = "3307", Name = "Poland")]
        Poland,

        /// <summary>
        /// Polynesia (excludes Hawaii), nec
        /// </summary>
        [EnumMember]
        [Name(Code = "1599", Name = "Polynesia (excludes Hawaii), nec")]
        PolynesiaExcludesHawaiiNec,

        /// <summary>
        /// Portugal
        /// </summary>
        [EnumMember]
        [Name(Code = "3106", Name = "Portugal")]
        Portugal,

        /// <summary>
        /// Puerto Rico
        /// </summary>
        [EnumMember]
        [Name(Code = "8421", Name = "Puerto Rico")]
        PuertoRico,

        /// <summary>
        /// Qatar
        /// </summary>
        [EnumMember]
        [Name(Code = "4212", Name = "Qatar")]
        Qatar,

        /// <summary>
        /// Queen Maud Land (Norway)
        /// </summary>
        [EnumMember]
        [Name(Code = "1606", Name = "Queen Maud Land (Norway)")]
        QueenMaudLandNorway,

        /// <summary>
        /// Reunion
        /// </summary>
        [EnumMember]
        [Name(Code = "9218", Name = "Reunion")]
        Reunion,

        /// <summary>
        /// Romania
        /// </summary>
        [EnumMember]
        [Name(Code = "3211", Name = "Romania")]
        Romania,

        /// <summary>
        /// Ross Dependency (New Zealand)
        /// </summary>
        [EnumMember]
        [Name(Code = "1607", Name = "Ross Dependency (New Zealand)")]
        RossDependencyNewZealand,

        /// <summary>
        /// Russian Federation
        /// </summary>
        [EnumMember]
        [Name(Code = "3308", Name = "Russian Federation")]
        RussianFederation,

        /// <summary>
        /// Rwanda
        /// </summary>
        [EnumMember]
        [Name(Code = "9221", Name = "Rwanda")]
        Rwanda,

        /// <summary>
        /// Samoa
        /// </summary>
        [EnumMember]
        [Name(Code = "1505", Name = "Samoa")]
        Samoa,

        /// <summary>
        /// Samoa, American
        /// </summary>
        [EnumMember]
        [Name(Code = "1506", Name = "Samoa, American")]
        SamoaAmerican,

        /// <summary>
        /// San Marino
        /// </summary>
        [EnumMember]
        [Name(Code = "3107", Name = "San Marino")]
        SanMarino,

        /// <summary>
        /// Sao Tome and Principe
        /// </summary>
        [EnumMember]
        [Name(Code = "9125", Name = "Sao Tome and Principe")]
        SaoTomeAndPrincipe,

        /// <summary>
        /// Saudi Arabia
        /// </summary>
        [EnumMember]
        [Name(Code = "4213", Name = "Saudi Arabia")]
        SaudiArabia,

        /// <summary>
        /// Scotland
        /// </summary>
        [EnumMember]
        [Name(Code = "2105", Name = "Scotland")]
        Scotland,

        /// <summary>
        /// Senegal
        /// </summary>
        [EnumMember]
        [Name(Code = "9126", Name = "Senegal")]
        Senegal,

        /// <summary>
        /// Serbia
        /// </summary>
        [EnumMember]
        [Name(Code = "3215", Name = "Serbia")]
        Serbia,

        /// <summary>
        /// Seychelles
        /// </summary>
        [EnumMember]
        [Name(Code = "9223", Name = "Seychelles")]
        Seychelles,

        /// <summary>
        /// Sierra Leone
        /// </summary>
        [EnumMember]
        [Name(Code = "9127", Name = "Sierra Leone")]
        SierraLeone,

        /// <summary>
        /// Singapore
        /// </summary>
        [EnumMember]
        [Name(Code = "5205", Name = "Singapore")]
        Singapore,

        /// <summary>
        /// Slovakia
        /// </summary>
        [EnumMember]
        [Name(Code = "3311", Name = "Slovakia")]
        Slovakia,

        /// <summary>
        /// Slovenia
        /// </summary>
        [EnumMember]
        [Name(Code = "3212", Name = "Slovenia")]
        Slovenia,

        /// <summary>
        /// Solomon Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "1303", Name = "Solomon Islands")]
        SolomonIslands,

        /// <summary>
        /// Somalia
        /// </summary>
        [EnumMember]
        [Name(Code = "9224", Name = "Somalia")]
        Somalia,

        /// <summary>
        /// South Africa
        /// </summary>
        [EnumMember]
        [Name(Code = "9225", Name = "South Africa")]
        SouthAfrica,

        /// <summary>
        /// South America, nec
        /// </summary>
        [EnumMember]
        [Name(Code = "8299", Name = "South America, nec")]
        SouthAmericaNec,

        /// <summary>
        /// Southern and East Africa, nec
        /// </summary>
        [EnumMember]
        [Name(Code = "9299", Name = "Southern and East Africa, nec")]
        SouthernAndEastAfricaNec,

        /// <summary>
        /// Spain
        /// </summary>
        [EnumMember]
        [Name(Code = "3108", Name = "Spain")]
        Spain,

        /// <summary>
        /// Spanish North Africa
        /// </summary>
        [EnumMember]
        [Name(Code = "4108", Name = "Spanish North Africa")]
        SpanishNorthAfrica,

        /// <summary>
        /// Sri Lanka
        /// </summary>
        [EnumMember]
        [Name(Code = "7107", Name = "Sri Lanka")]
        SriLanka,

        /// <summary>
        /// St Barthelemy
        /// </summary>
        [EnumMember]
        [Name(Code = "8431", Name = "St Barthelemy")]
        StBarthelemy,

        /// <summary>
        /// St Helena
        /// </summary>
        [EnumMember]
        [Name(Code = "9222", Name = "St Helena")]
        StHelena,

        /// <summary>
        /// St Kitts and Nevis
        /// </summary>
        [EnumMember]
        [Name(Code = "8422", Name = "St Kitts and Nevis")]
        StKittsAndNevis,

        /// <summary>
        /// St Lucia
        /// </summary>
        [EnumMember]
        [Name(Code = "8423", Name = "St Lucia")]
        StLucia,

        /// <summary>
        /// St Martin (French part)
        /// </summary>
        [EnumMember]
        [Name(Code = "8432", Name = "St Martin (French part)")]
        StMartinFrenchPart,

        /// <summary>
        /// St Pierre and Miquelon
        /// </summary>
        [EnumMember]
        [Name(Code = "8103", Name = "St Pierre and Miquelon")]
        StPierreAndMiquelon,

        /// <summary>
        /// St Vincent and the Grenadines
        /// </summary>
        [EnumMember]
        [Name(Code = "8424", Name = "St Vincent and the Grenadines")]
        StVincentAndTheGrenadines,

        /// <summary>
        /// Sudan
        /// </summary>
        [EnumMember]
        [Name(Code = "4105", Name = "Sudan")]
        Sudan,

        /// <summary>
        /// Suriname
        /// </summary>
        [EnumMember]
        [Name(Code = "8214", Name = "Suriname")]
        Suriname,

        /// <summary>
        /// Swaziland
        /// </summary>
        [EnumMember]
        [Name(Code = "9226", Name = "Swaziland")]
        Swaziland,

        /// <summary>
        /// Sweden
        /// </summary>
        [EnumMember]
        [Name(Code = "2407", Name = "Sweden")]
        Sweden,

        /// <summary>
        /// Switzerland
        /// </summary>
        [EnumMember]
        [Name(Code = "2311", Name = "Switzerland")]
        Switzerland,

        /// <summary>
        /// Syria
        /// </summary>
        [EnumMember]
        [Name(Code = "4214", Name = "Syria")]
        Syria,

        /// <summary>
        /// Taiwan
        /// </summary>
        [EnumMember]
        [Name(Code = "6105", Name = "Taiwan")]
        Taiwan,

        /// <summary>
        /// Tajikistan
        /// </summary>
        [EnumMember]
        [Name(Code = "7207", Name = "Tajikistan")]
        Tajikistan,

        /// <summary>
        /// Tanzania
        /// </summary>
        [EnumMember]
        [Name(Code = "9227", Name = "Tanzania")]
        Tanzania,

        /// <summary>
        /// Thailand
        /// </summary>
        [EnumMember]
        [Name(Code = "5104", Name = "Thailand")]
        Thailand,

        /// <summary>
        /// Togo
        /// </summary>
        [EnumMember]
        [Name(Code = "9128", Name = "Togo")]
        Togo,

        /// <summary>
        /// Tokelau
        /// </summary>
        [EnumMember]
        [Name(Code = "1507", Name = "Tokelau")]
        Tokelau,

        /// <summary>
        /// Tonga
        /// </summary>
        [EnumMember]
        [Name(Code = "1508", Name = "Tonga")]
        Tonga,

        /// <summary>
        /// Trinidad and Tobago
        /// </summary>
        [EnumMember]
        [Name(Code = "8425", Name = "Trinidad and Tobago")]
        TrinidadAndTobago,

        /// <summary>
        /// Tunisia
        /// </summary>
        [EnumMember]
        [Name(Code = "4106", Name = "Tunisia")]
        Tunisia,

        /// <summary>
        /// Turkey
        /// </summary>
        [EnumMember]
        [Name(Code = "4215", Name = "Turkey")]
        Turkey,

        /// <summary>
        /// Turkmenistan
        /// </summary>
        [EnumMember]
        [Name(Code = "7208", Name = "Turkmenistan")]
        Turkmenistan,

        /// <summary>
        /// Turks and Caicos Islands
        /// </summary>
        [EnumMember]
        [Name(Code = "8426", Name = "Turks and Caicos Islands")]
        TurksAndCaicosIslands,

        /// <summary>
        /// Tuvalu
        /// </summary>
        [EnumMember]
        [Name(Code = "1511", Name = "Tuvalu")]
        Tuvalu,

        /// <summary>
        /// Uganda
        /// </summary>
        [EnumMember]
        [Name(Code = "9228", Name = "Uganda")]
        Uganda,

        /// <summary>
        /// Ukraine
        /// </summary>
        [EnumMember]
        [Name(Code = "3312", Name = "Ukraine")]
        Ukraine,

        /// <summary>
        /// United Arab Emirates
        /// </summary>
        [EnumMember]
        [Name(Code = "4216", Name = "United Arab Emirates")]
        UnitedArabEmirates,

        /// <summary>
        /// United States of America
        /// </summary>
        [EnumMember]
        [Name(Code = "8104", Name = "United States of America")]
        UnitedStatesOfAmerica,

        /// <summary>
        /// Uruguay
        /// </summary>
        [EnumMember]
        [Name(Code = "8215", Name = "Uruguay")]
        Uruguay,

        /// <summary>
        /// Uzbekistan
        /// </summary>
        [EnumMember]
        [Name(Code = "7211", Name = "Uzbekistan")]
        Uzbekistan,

        /// <summary>
        /// Vanuatu
        /// </summary>
        [EnumMember]
        [Name(Code = "1304", Name = "Vanuatu")]
        Vanuatu,

        /// <summary>
        /// Venezuela
        /// </summary>
        [EnumMember]
        [Name(Code = "8216", Name = "Venezuela")]
        Venezuela,

        /// <summary>
        /// Vietnam
        /// </summary>
        [EnumMember]
        [Name(Code = "5105", Name = "Vietnam")]
        Vietnam,

        /// <summary>
        /// Virgin Islands, British
        /// </summary>
        [EnumMember]
        [Name(Code = "8427", Name = "Virgin Islands, British")]
        VirginIslandsBritish,

        /// <summary>
        /// Virgin Islands, United States
        /// </summary>
        [EnumMember]
        [Name(Code = "8428", Name = "Virgin Islands, United States")]
        VirginIslandsUnitedStates,

        /// <summary>
        /// Wales
        /// </summary>
        [EnumMember]
        [Name(Code = "2106", Name = "Wales")]
        Wales,

        /// <summary>
        /// Wallis and Futuna
        /// </summary>
        [EnumMember]
        [Name(Code = "1512", Name = "Wallis and Futuna")]
        WallisAndFutuna,

        /// <summary>
        /// Western Sahara
        /// </summary>
        [EnumMember]
        [Name(Code = "4107", Name = "Western Sahara")]
        WesternSahara,

        /// <summary>
        /// Yemen
        /// </summary>
        [EnumMember]
        [Name(Code = "4217", Name = "Yemen")]
        Yemen,

        /// <summary>
        /// Zambia
        /// </summary>
        [EnumMember]
        [Name(Code = "9231", Name = "Zambia")]
        Zambia,

        /// <summary>
        /// Zimbabwe
        /// </summary>
        [EnumMember]
        [Name(Code = "9232", Name = "Zimbabwe")]
        Zimbabwe,
   
    }
}
