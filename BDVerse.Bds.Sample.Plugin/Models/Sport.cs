using BDVerse.Bds.Sample.Plugin.Enums;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;
using Newtonsoft.Json;

namespace BDVerse.Bds.Sample.Plugin.Models
{
    /// <summary>
    /// Sample for a custom model, could have use the base PIM.Product instead
    /// This is demonstrating a model for full text search
    /// </summary>
    [BdsObject("SAMPLE.Sport", CacheType = CacheType.Long, ViewName = nameof(Sport) + "s", DisplayPath = nameof(Name), FullTextSearch = Feature.Enable)]
    public class Sport : BdsEntity
    {
        [Field(Section = "Identification", Weight = 1000, Search = Feature.Enable, SearchFullText = Feature.Enable)]
        [JsonProperty]
        public string Name
        {
            get
            {
                return Get<string>(nameof(Name));
            }
            set
            {
                Set(nameof(Name), value);
            }
        }

        [Field(Section = "Identification", Weight = 1500, SearchFullTextGroup = Feature.Enable)]
        [JsonProperty]
        public Reference<SportCategory> Category
        {
            get
            {
                return Get<Reference<SportCategory>>(nameof(Category));
            }
            set
            {
                Set(nameof(Category), value);
            }
        }    

        [Field(Section = "Translation", Weight = 7000, SearchFullText = Feature.Enable)]
        [JsonProperty]
        public LocalizedString LocalName
        {
            get
            {
                return Get<LocalizedString>(nameof(LocalName));
            }
            set
            {
                Set(nameof(LocalName), value);
            }
        }

        [Field(Section = "Specifications", Weight = 8000, SearchFullTextFacet = Feature.Enable, SearchFullTextFacetId = "sample.sport.groupType")]
        [JsonProperty]
        public GroupType GroupType
        {
            get
            {
                return Get<GroupType>(nameof(GroupType));
            }
            set
            {
                Set(nameof(GroupType), value);
            }
        }

        [Field(Section = "Specifications", Weight = 8010, SearchFullTextFilter = Feature.Enable)]
        [JsonProperty]
        public int? GroupSize
        {
            get
            {
                return Get<int?>(nameof(GroupSize));
            }
            set
            {
                Set(nameof(GroupSize), value);
            }
        }
    }

}