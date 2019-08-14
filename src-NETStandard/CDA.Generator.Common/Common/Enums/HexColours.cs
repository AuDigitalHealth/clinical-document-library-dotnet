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
    /// HexColours
    /// </summary>
    [Serializable]
    [DataContract]
    public enum HexColours
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
        /// Aliceblue
        /// </summary>
        [EnumMember]
		[Name(Code = "F0f8ff", Name = "Alice blue")]
        Aliceblue,

        /// <summary>
        /// Antiquewhite
        /// </summary>
        [EnumMember]
		[Name(Code = "Faebd7", Name = "Antique white")]
        Antiquewhite,

        /// <summary>
        /// Aqua
        /// </summary>
        [EnumMember]
		[Name(Code = "00ffff", Name = "Aqua")]
        Aqua,

        /// <summary>
        /// Aquamarine
        /// </summary>
        [EnumMember]
		[Name(Code = "7fffd4", Name = "Aquamarine")]
        Aquamarine,

        /// <summary>
        /// Azure
        /// </summary>
        [EnumMember]
		[Name(Code = "F0ffff", Name = "Azure")]
        Azure,

        /// <summary>
        /// Beige
        /// </summary>
        [EnumMember]
		[Name(Code = "F5f5dc", Name = "Beige")]
        Beige,

        /// <summary>
        /// Bisque
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffe4c4", Name = "Bisque")]
        Bisque,

        /// <summary>
        /// Black
        /// </summary>
        [EnumMember]
		[Name(Code = "000000", Name = "Black")]
        Black,

        /// <summary>
        /// Blanchedalmond
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffebcd", Name = "Blanched almond")]
        Blanchedalmond,

        /// <summary>
        /// Blue
        /// </summary>
        [EnumMember]
		[Name(Code = "0000ff", Name = "Blue")]
        Blue,

        /// <summary>
        /// Bluevoilet
        /// </summary>
        [EnumMember]
		[Name(Code = "8a2be2", Name = "Blue voilet")]
        Bluevoilet,

        /// <summary>
        /// Brown
        /// </summary>
        [EnumMember]
		[Name(Code = "A52a2a", Name = "Brown")]
        Brown,

        /// <summary>
        /// Burlywood
        /// </summary>
        [EnumMember]
		[Name(Code = "Deb887", Name = "Burlywood")]
        Burlywood,

        /// <summary>
        /// Cadetblue
        /// </summary>
        [EnumMember]
		[Name(Code = "5f9ea0", Name = "Cadetblue")]
        Cadetblue,

        /// <summary>
        /// Chocolate
        /// </summary>
        [EnumMember]
		[Name(Code = "D2691e", Name = "Chocolate")]
        Chocolate,

        /// <summary>
        /// Coral
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff7f50", Name = "Coral")]
        Coral,

        /// <summary>
        /// Cornflowerblue
        /// </summary>
        [EnumMember]
		[Name(Code = "6495ed", Name = "Cornflower blue")]
        Cornflowerblue,

        /// <summary>
        /// Cornsilk
        /// </summary>
        [EnumMember]
		[Name(Code = "Fff8dc", Name = "Cornsilk")]
        Cornsilk,

        /// <summary>
        /// Crimson
        /// </summary>
        [EnumMember]
		[Name(Code = "Dc143c", Name = "Crimson")]
        Crimson,

        /// <summary>
        /// Cyan
        /// </summary>
        [EnumMember]
		[Name(Code = "00ffff", Name = "Cyan")]
        Cyan,

        /// <summary>
        /// Darkblue
        /// </summary>
        [EnumMember]
		[Name(Code = "00008b", Name = "Dark blue")]
        Darkblue,

        /// <summary>
        /// Darkcyan
        /// </summary>
        [EnumMember]
		[Name(Code = "008b8b", Name = "Dark cyan")]
        Darkcyan,

        /// <summary>
        /// Darkgoldenrod
        /// </summary>
        [EnumMember]
		[Name(Code = "B8860b", Name = "Dark goldenrod")]
        Darkgoldenrod,

        /// <summary>
        /// Darkgray
        /// </summary>
        [EnumMember]
		[Name(Code = "A9a9a9", Name = "Dark gray")]
        Darkgray,

        /// <summary>
        /// Darkgreen
        /// </summary>
        [EnumMember]
		[Name(Code = "006400", Name = "Dark green")]
        Darkgreen,

        /// <summary>
        /// Darkkhaki
        /// </summary>
        [EnumMember]
		[Name(Code = "Bdb76b", Name = "Dark khaki")]
        Darkkhaki,

        /// <summary>
        /// Darkmagenta
        /// </summary>
        [EnumMember]
		[Name(Code = "8b008b", Name = "Dark magenta")]
        Darkmagenta,

        /// <summary>
        /// Darkolivegreen
        /// </summary>
        [EnumMember]
		[Name(Code = "556b2f", Name = "Dark olive green")]
        Darkolivegreen,

        /// <summary>
        /// Darkorange
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff8c00", Name = "Dark orange")]
        Darkorange,

        /// <summary>
        /// Darkorchid
        /// </summary>
        [EnumMember]
		[Name(Code = "9932cc", Name = "Darko rchid")]
        Darkorchid,

        /// <summary>
        /// Darkred
        /// </summary>
        [EnumMember]
		[Name(Code = "8b0000", Name = "Dark red")]
        Darkred,

        /// <summary>
        /// Darksalmon
        /// </summary>
        [EnumMember]
		[Name(Code = "E9967a", Name = "Dark salmon")]
        Darksalmon,

        /// <summary>
        /// Darkseagreen
        /// </summary>
        [EnumMember]
		[Name(Code = "8fbc8f", Name = "Dark sea green")]
        Darkseagreen,

        /// <summary>
        /// Darkslateblue
        /// </summary>
        [EnumMember]
		[Name(Code = "483d8b", Name = "Dark slate blue")]
        Darkslateblue,

        /// <summary>
        /// Darkslategray
        /// </summary>
        [EnumMember]
		[Name(Code = "2f4f4f", Name = "Dark slate gray")]
        Darkslategray,

        /// <summary>
        /// Darkturquoise
        /// </summary>
        [EnumMember]
		[Name(Code = "00ced1", Name = "Dark turquoise")]
        Darkturquoise,

        /// <summary>
        /// Darkviolet
        /// </summary>
        [EnumMember]
		[Name(Code = "9400d3", Name = "Dark violet")]
        Darkviolet,

        /// <summary>
        /// Deeppink
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff1493", Name = "Deep pink")]
        Deeppink,

        /// <summary>
        /// Deepskyblue
        /// </summary>
        [EnumMember]
		[Name(Code = "00bfff", Name = "Deep sky blue")]
        Deepskyblue,

        /// <summary>
        /// Dimgray
        /// </summary>
        [EnumMember]
		[Name(Code = "696969", Name = "Dim gray")]
        Dimgray,

        /// <summary>
        /// Dodgerblue
        /// </summary>
        [EnumMember]
		[Name(Code = "1e90ff", Name = "Dodger blue")]
        Dodgerblue,

        /// <summary>
        /// Firebrick
        /// </summary>
        [EnumMember]
		[Name(Code = "B22222", Name = "Fire brick")]
        Firebrick,

        /// <summary>
        /// Floralwhite
        /// </summary>
        [EnumMember]
		[Name(Code = "Fffaf0", Name = "Floral white")]
        Floralwhite,

        /// <summary>
        /// Forestgreen
        /// </summary>
        [EnumMember]
		[Name(Code = "228b22", Name = "Forest green")]
        Forestgreen,

        /// <summary>
        /// Fuchsia
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff00ff", Name = "Fuchsia")]
        Fuchsia,

        /// <summary>
        /// Gainsboro
        /// </summary>
        [EnumMember]
		[Name(Code = "Dcdcdc", Name = "Gainsboro")]
        Gainsboro,

        /// <summary>
        /// Ghostwhite
        /// </summary>
        [EnumMember]
		[Name(Code = "F8f8ff", Name = "Ghost white")]
        Ghostwhite,

        /// <summary>
        /// Gold
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffd700", Name = "Gold")]
        Gold,

        /// <summary>
        /// Goldenrod
        /// </summary>
        [EnumMember]
		[Name(Code = "Daa520", Name = "Golden rod")]
        Goldenrod,

        /// <summary>
        /// Gray
        /// </summary>
        [EnumMember]
		[Name(Code = "808080", Name = "Gray")]
        Gray,

        /// <summary>
        /// Green
        /// </summary>
        [EnumMember]
		[Name(Code = "008000", Name = "Green")]
        Green,

        /// <summary>
        /// Greenyellow
        /// </summary>
        [EnumMember]
		[Name(Code = "Adff2f", Name = "Green yellow")]
        Greenyellow,

        /// <summary>
        /// Honeydew
        /// </summary>
        [EnumMember]
		[Name(Code = "F0fff0", Name = "Honeydew")]
        Honeydew,

        /// <summary>
        /// Hotpink
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff69b4", Name = "Hot pink")]
        Hotpink,

        /// <summary>
        /// Indianred
        /// </summary>
        [EnumMember]
		[Name(Code = "Cd5c5c", Name = "Indian red")]
        Indianred,

        /// <summary>
        /// Indigo
        /// </summary>
        [EnumMember]
		[Name(Code = "4b0082", Name = "Indigo")]
        Indigo,

        /// <summary>
        /// Ivory
        /// </summary>
        [EnumMember]
		[Name(Code = "Fffff0", Name = "Ivory")]
        Ivory,

        /// <summary>
        /// Khaki
        /// </summary>
        [EnumMember]
		[Name(Code = "F0e68c", Name = "Khaki")]
        Khaki,

        /// <summary>
        /// Lavendar
        /// </summary>
        [EnumMember]
		[Name(Code = "E6e6fa", Name = "Lavendar")]
        Lavendar,

        /// <summary>
        /// Lavenderblush
        /// </summary>
        [EnumMember]
		[Name(Code = "Fff0f5", Name = "Lavender blush")]
        Lavenderblush,

        /// <summary>
        /// Lawngreen
        /// </summary>
        [EnumMember]
		[Name(Code = "7cfc00", Name = "Lawn green")]
        Lawngreen,

        /// <summary>
        /// Lemonchiffon
        /// </summary>
        [EnumMember]
		[Name(Code = "Fffacd", Name = "Lemon chiffon")]
        Lemonchiffon,

        /// <summary>
        /// Lightblue
        /// </summary>
        [EnumMember]
		[Name(Code = "Add8e6", Name = "Light blue")]
        Lightblue,

        /// <summary>
        /// Lightcoral
        /// </summary>
        [EnumMember]
		[Name(Code = "F08080", Name = "Light coral")]
        Lightcoral,

        /// <summary>
        /// Lightcyan
        /// </summary>
        [EnumMember]
		[Name(Code = "E0ffff", Name = "Light cyan")]
        Lightcyan,

        /// <summary>
        /// Lightgoldenrodyellow
        /// </summary>
        [EnumMember]
		[Name(Code = "Fafad2", Name = "Light goldenrodyellow")]
        Lightgoldenrodyellow,

        /// <summary>
        /// Lightgreen
        /// </summary>
        [EnumMember]
		[Name(Code = "90ee90", Name = "Light green")]
        Lightgreen,

        /// <summary>
        /// Lightgrey
        /// </summary>
        [EnumMember]
		[Name(Code = "D3d3d3", Name = "Light grey")]
        Lightgrey,

        /// <summary>
        /// Lightpink
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffb6c1", Name = "Light pink")]
        Lightpink,

        /// <summary>
        /// Lightsalmon
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffa07a", Name = "Light salmon")]
        Lightsalmon,

        /// <summary>
        /// Lightseagreen
        /// </summary>
        [EnumMember]
		[Name(Code = "20b2aa", Name = "Light seagreen")]
        Lightseagreen,

        /// <summary>
        /// Lightskyblue
        /// </summary>
        [EnumMember]
		[Name(Code = "87cefa", Name = "Light skyblue")]
        Lightskyblue,

        /// <summary>
        /// Lightslategray
        /// </summary>
        [EnumMember]
		[Name(Code = "778899", Name = "Light slategray")]
        Lightslategray,

        /// <summary>
        /// Lightsteelblue
        /// </summary>
        [EnumMember]
		[Name(Code = "B0c4de", Name = "Light steelblue")]
        Lightsteelblue,

        /// <summary>
        /// Lightyellow
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffffe0", Name = "Light yellow")]
        Lightyellow,

        /// <summary>
        /// Lime
        /// </summary>
        [EnumMember]
		[Name(Code = "00ff00", Name = "Lime")]
        Lime,

        /// <summary>
        /// Limegreen
        /// </summary>
        [EnumMember]
		[Name(Code = "32cd32", Name = "Limegreen")]
        Limegreen,

        /// <summary>
        /// Linen
        /// </summary>
        [EnumMember]
		[Name(Code = "Faf0e6", Name = "Linen")]
        Linen,

        /// <summary>
        /// Magenta
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff00ff", Name = "Magenta")]
        Magenta,

        /// <summary>
        /// Maroon
        /// </summary>
        [EnumMember]
		[Name(Code = "800000", Name = "Maroon")]
        Maroon,

        /// <summary>
        /// Mediumaquamarine
        /// </summary>
        [EnumMember]
		[Name(Code = "66cdaa", Name = "Medium aquamarine")]
        Mediumaquamarine,

        /// <summary>
        /// Mediumblue
        /// </summary>
        [EnumMember]
		[Name(Code = "0000cd", Name = "Medium blue")]
        Mediumblue,

        /// <summary>
        /// Mediumorchid
        /// </summary>
        [EnumMember]
		[Name(Code = "Ba55d3", Name = "Medium orchid")]
        Mediumorchid,

        /// <summary>
        /// Mediumpurple
        /// </summary>
        [EnumMember]
		[Name(Code = "9370d8", Name = "Medium purple")]
        Mediumpurple,

        /// <summary>
        /// Mediumseagreen
        /// </summary>
        [EnumMember]
		[Name(Code = "3cb371", Name = "Mediumseagreen")]
        Mediumseagreen,

        /// <summary>
        /// Mediumslateblue
        /// </summary>
        [EnumMember]
		[Name(Code = "7b68ee", Name = "Medium slateblue")]
        Mediumslateblue,

        /// <summary>
        /// Mediumspringgreen
        /// </summary>
        [EnumMember]
		[Name(Code = "00fa9a", Name = "Medium springgreen")]
        Mediumspringgreen,

        /// <summary>
        /// Mediumturquoise
        /// </summary>
        [EnumMember]
		[Name(Code = "48d1cc", Name = "Medium turquoise")]
        Mediumturquoise,

        /// <summary>
        /// Midnightblue
        /// </summary>
        [EnumMember]
		[Name(Code = "191970", Name = "Midnight blue")]
        Midnightblue,

        /// <summary>
        /// Mintcream
        /// </summary>
        [EnumMember]
		[Name(Code = "F5fffa", Name = "Mintcream")]
        Mintcream,

        /// <summary>
        /// Mistyrose
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffe4e1", Name = "Mistyrose")]
        Mistyrose,

        /// <summary>
        /// Moccasin
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffe4b5", Name = "Moccasin")]
        Moccasin,

        /// <summary>
        /// Navajowhite
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffdead", Name = "Navajo white")]
        Navajowhite,

        /// <summary>
        /// Navy
        /// </summary>
        [EnumMember]
		[Name(Code = "000080", Name = "Navy")]
        Navy,

        /// <summary>
        /// Oldlace
        /// </summary>
        [EnumMember]
		[Name(Code = "Fdf5e6", Name = "Oldlace")]
        Oldlace,

        /// <summary>
        /// Olive
        /// </summary>
        [EnumMember]
		[Name(Code = "808000", Name = "Olive")]
        Olive,

        /// <summary>
        /// Olivedrab
        /// </summary>
        [EnumMember]
		[Name(Code = "688e23", Name = "Olive drab")]
        Olivedrab,

        /// <summary>
        /// Orange
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffa500", Name = "Orange")]
        Orange,

        /// <summary>
        /// Orangered
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff4500", Name = "Orange red")]
        Orangered,

        /// <summary>
        /// Orchid
        /// </summary>
        [EnumMember]
		[Name(Code = "Da70d6", Name = "Orchid")]
        Orchid,

        /// <summary>
        /// Palegoldenrod
        /// </summary>
        [EnumMember]
		[Name(Code = "Eee8aa", Name = "Pale goldenrod")]
        Palegoldenrod,

        /// <summary>
        /// Palegreen
        /// </summary>
        [EnumMember]
		[Name(Code = "98fb98", Name = "Pale green")]
        Palegreen,

        /// <summary>
        /// Paleturquoise
        /// </summary>
        [EnumMember]
		[Name(Code = "Afeeee", Name = "Pale turquoise")]
        Paleturquoise,

        /// <summary>
        /// Papayawhip
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffefd5", Name = "Papaya whip")]
        Papayawhip,

        /// <summary>
        /// Peachpuff
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffdab9", Name = "Peach puff")]
        Peachpuff,

        /// <summary>
        /// Peru
        /// </summary>
        [EnumMember]
		[Name(Code = "Cd853f", Name = "Peru")]
        Peru,

        /// <summary>
        /// Pink
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffc0cb", Name = "Pink")]
        Pink,

        /// <summary>
        /// Plum
        /// </summary>
        [EnumMember]
		[Name(Code = "Dda0dd", Name = "Plum")]
        Plum,

        /// <summary>
        /// Powderblue
        /// </summary>
        [EnumMember]
		[Name(Code = "B0e0e6", Name = "Powder blue")]
        Powderblue,

        /// <summary>
        /// Purple
        /// </summary>
        [EnumMember]
		[Name(Code = "800080", Name = "Purple")]
        Purple,

        /// <summary>
        /// Red
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff0000", Name = "Red")]
        Red,

        /// <summary>
        /// Rosybrown
        /// </summary>
        [EnumMember]
		[Name(Code = "Bc8f8f", Name = "Rosy brown")]
        Rosybrown,

        /// <summary>
        /// Royalblue
        /// </summary>
        [EnumMember]
		[Name(Code = "4169e1", Name = "Royal blue")]
        Royalblue,

        /// <summary>
        /// Saddlebrown
        /// </summary>
        [EnumMember]
		[Name(Code = "8b4513", Name = "Saddle brown")]
        Saddlebrown,

        /// <summary>
        /// Salmon
        /// </summary>
        [EnumMember]
		[Name(Code = "Fa8072", Name = "Salmon")]
        Salmon,

        /// <summary>
        /// Sandybrown
        /// </summary>
        [EnumMember]
		[Name(Code = "F4a460", Name = "Sandy brown")]
        Sandybrown,

        /// <summary>
        /// Seagreen
        /// </summary>
        [EnumMember]
		[Name(Code = "2e8b57", Name = "Sea green")]
        Seagreen,

        /// <summary>
        /// Seashell
        /// </summary>
        [EnumMember]
		[Name(Code = "Fff5ee", Name = "Seashell")]
        Seashell,

        /// <summary>
        /// Sienna
        /// </summary>
        [EnumMember]
		[Name(Code = "A0522d", Name = "Sienna")]
        Sienna,

        /// <summary>
        /// Silver
        /// </summary>
        [EnumMember]
		[Name(Code = "C0c0c0", Name = "Silver")]
        Silver,

        /// <summary>
        /// Skyblue
        /// </summary>
        [EnumMember]
		[Name(Code = "87ceeb", Name = "Sky blue")]
        Skyblue,

        /// <summary>
        /// Slateblue
        /// </summary>
        [EnumMember]
		[Name(Code = "6a5acd", Name = "Slate blue")]
        Slateblue,

        /// <summary>
        /// Slategray
        /// </summary>
        [EnumMember]
		[Name(Code = "708090", Name = "Slate gray")]
        Slategray,

        /// <summary>
        /// Snow
        /// </summary>
        [EnumMember]
		[Name(Code = "Fffafa", Name = "Snow")]
        Snow,

        /// <summary>
        /// Springgreen
        /// </summary>
        [EnumMember]
		[Name(Code = "00ff7f", Name = "Spring green")]
        Springgreen,

        /// <summary>
        /// Steelblue
        /// </summary>
        [EnumMember]
		[Name(Code = "4682b4", Name = "Steel blue")]
        Steelblue,

        /// <summary>
        /// Tan
        /// </summary>
        [EnumMember]
		[Name(Code = "D2b48c", Name = "Tan")]
        Tan,

        /// <summary>
        /// Teal
        /// </summary>
        [EnumMember]
		[Name(Code = "008080", Name = "Teal")]
        Teal,

        /// <summary>
        /// Thistle
        /// </summary>
        [EnumMember]
		[Name(Code = "D8bfd8", Name = "Thistle")]
        Thistle,

        /// <summary>
        /// Tomato
        /// </summary>
        [EnumMember]
		[Name(Code = "Ff6347", Name = "Tomato")]
        Tomato,

        /// <summary>
        /// Turquoise
        /// </summary>
        [EnumMember]
		[Name(Code = "40e0d0", Name = "Turquoise")]
        Turquoise,

        /// <summary>
        /// Violet
        /// </summary>
        [EnumMember]
		[Name(Code = "Ee82ee", Name = "Violet")]
        Violet,

        /// <summary>
        /// Wheat
        /// </summary>
        [EnumMember]
		[Name(Code = "F5deb3", Name = "Wheat")]
        Wheat,

        /// <summary>
        /// White
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffffff", Name = "White")]
        White,

        /// <summary>
        /// Whitesmoke
        /// </summary>
        [EnumMember]
		[Name(Code = "F5f5f5", Name = "White smoke")]
        Whitesmoke,

        /// <summary>
        /// Yellow
        /// </summary>
        [EnumMember]
		[Name(Code = "Ffff00", Name = "Yellow")]
        Yellow,

        /// <summary>
        ///        Yellowgreen
        /// </summary>
        [EnumMember]
		[Name(Code = "9acd32", Name = "Yellow green")]
        Yellowgreen
    }
}
