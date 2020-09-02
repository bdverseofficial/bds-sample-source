using System;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Attributes;

namespace Bds.Sample.Plugin.Enums
{
    /// <summary>
    /// Demonstrate how to define a custom enum
    /// We could have also used a traditionnal enum, but that way, this is allowing other plugin to extend that enum by adding new values.
    /// </summary>
    [Enum("SAMPLE.GroupType")]
    public class GroupType : EnumValue
    {
        public static readonly GroupType Solo = new GroupType() { Value = 1000001, Code = "SOLO", Label = "Solo" };
        public static readonly GroupType Dual = new GroupType() { Value = 1000002, Code = "DUAL", Label = "Dual" };
        public static readonly GroupType Group = new GroupType() { Value = 1000003, Code = "GROUP", Label = "Group" };
        public static readonly GroupType Team = new GroupType() { Value = 1000004, Code = "TEAM", Label = "Team" };
    }
}
