using System;
using Bds.Sample.Plugin.Enums;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;
using Newtonsoft.Json;

namespace Bds.Sample.Plugin.Models
{
    /// <summary>
    /// The sport entity. We enable the full text search on this entity
    /// </summary>
    [BdsObject("SAMPLE.Sport", DisplayPath = nameof(Name), FullTextSearch = Feature.Enable)]
    public class Sport : BdsEntity
    {
        /// <summary>
        /// The name of the sport part of the full text search content
        /// </summary>
        /// <value></value>
        [JsonProperty]
        [Field(SearchFullText = Feature.Enable)]
        public string Name
        {
            get => Get<string>(nameof(Name));
            set => Set(nameof(Name), value);
        }

        /// <summary>
        /// The name in multiple language of the sport
        /// </summary>
        /// <value></value>
        [JsonProperty]
        [Field(SearchFullText = Feature.Enable)]
        public LocalizedString LocalName
        {
            get => Get<LocalizedString>(nameof(LocalName));
            set => Set(nameof(LocalName), value);
        }

        /// <summary>
        /// The category of sport, this will be used for displaying a tree view on the side of the search
        /// </summary>
        /// <value></value>
        [Field(SearchFullTextGroup = Feature.Enable)]
        [JsonProperty]
        public Reference<SportCategory> Category
        {
            get => Get<Reference<SportCategory>>(nameof(Category));
            set => Set(nameof(Category), value);
        }

        /// <summary>
        /// The group type will defined a new facet for the search
        /// </summary>
        /// <value></value>
        [Field(SearchFullTextFacet = Feature.Enable, SearchFullTextFacetId = "sample.sport.groupType")]
        [JsonProperty]
        public GroupType GroupType
        {
            get => Get<GroupType>(nameof(GroupType));
            set => Set(nameof(GroupType), value);
        }

        /// <summary>
        /// The group size will be used to filter or sorting the results
        /// </summary>
        /// <value></value>
        [Field(SearchFullTextFilter = Feature.Enable)]
        [JsonProperty]
        public int? GroupSize
        {
            get => Get<int?>(nameof(GroupSize));
            set => Set(nameof(GroupSize), value);
        }
    }
}
