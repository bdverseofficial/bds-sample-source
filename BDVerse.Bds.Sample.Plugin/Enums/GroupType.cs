using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;

namespace BDVerse.Bds.Sample.Plugin.Enums
{
    /// <summary>
    /// Demonstrate how to define a custom enum
    /// We could have also used a traditionnal enum, but that way, this is allowing other plugin to extend that enum by adding new values.
    /// </summary>
    [Enum(Name = "SAMPLE.GroupType")]
    public class GroupType : EnumValue
    {
        public static readonly GroupType Solo = new GroupType() { Value = 1000001, Code = "SOLO", Label = "Solo" };
        public static readonly GroupType Dual = new GroupType() { Value = 1000002, Code = "DUAL", Label = "Dual" };
        public static readonly GroupType Group = new GroupType() { Value = 1000003, Code = "GROUP", Label = "Group" };
        public static readonly GroupType Team = new GroupType() { Value = 1000004, Code = "TEAM", Label = "Team" };
    }
}
