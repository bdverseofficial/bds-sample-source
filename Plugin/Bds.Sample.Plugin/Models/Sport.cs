using System;
using Bds.Sample.Plugin.Enums;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;
using Newtonsoft.Json;

namespace Bds.Sample.Plugin.Models
{
    [BdsObject("SAMPLE.Sport", DisplayPath = nameof(Name), FullTextSearch = Feature.Enable )]
    public class Sport : BdsEntity
    {
        [JsonProperty]
        [Field(SearchFullText = Feature.Enable)]
        public string Name
        {
            get => Get<string>(nameof(Name));
            set => Set(nameof(Name), value);
        }

        [JsonProperty]
        [Field(SearchFullText = Feature.Enable)]
        public LocalizedString LocalName
        {
            get => Get<LocalizedString>(nameof(LocalName));
            set => Set(nameof(LocalName), value);
        }

        [Field(SearchFullTextGroup = Feature.Enable)]
        [JsonProperty]
        public Reference<SportCategory> Category
        {
            get => Get<Reference<SportCategory>>(nameof(Category));            
            set => Set(nameof(Category), value);
        } 

        [Field(SearchFullTextFacet = Feature.Enable, SearchFullTextFacetId = "sample.sport.groupType")]
        [JsonProperty]
        public GroupType GroupType
        {
            get => Get<GroupType>(nameof(GroupType));            
            set => Set(nameof(GroupType), value);            
        }  
    }
}
