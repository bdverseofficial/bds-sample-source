using System;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;

namespace Bds.Sample.Plugin.Models
{
    [BdsObject("SAMPLE.SportCategory", HiddenFromParent = Feature.Enable)]
    public class SportCategory : Group
    {
    }
}
