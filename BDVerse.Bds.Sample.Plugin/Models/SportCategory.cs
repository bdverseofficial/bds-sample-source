using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;

namespace BDVerse.Bds.Sample.Plugin.Models
{
    /// <summary>
    /// Define a specific entity for managing the category of sports
    /// The purpose of the mepty class is the hidden from parent, to reuse fully the base object group, without poluting those data
    /// </summary>
    [BdsObject("SAMPLE.SportCategory", ViewName = "Sport Categories", CacheType = CacheType.Long, HiddenFromParent = Feature.Enable)]
    public class SportCategory : Group
    {
    }
}
