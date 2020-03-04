using System;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;

namespace Bds.Sample.Plugin.Models
{
    /// <summary>
    /// We defined a really simple category by leveraging the base group from BDS. Hidden from parents will ensure to not be able to see those records by looking to Group.
    /// </summary>
    [BdsObject("SAMPLE.SportCategory", HiddenFromParent = Feature.Enable)]
    public class SportCategory : Group
    {
    }
}
