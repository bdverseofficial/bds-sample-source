using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDVerse.Bds.Sample.Plugin.Models
{    
    /// <summary>
    /// Define a new entity for storing the member that are connecting    
    /// </summary>
    [BdsObject("SAMPLE.Member", CacheType = CacheType.Long, ViewName = "Sample Members")]
    public class Member : User
    {
        public Member()
            : base()
        {
            this.SyncLoginWithEmail = true;            
        }

        [JsonProperty]
        public Reference<Sport> PreferedSport
        {
            get
            {
                return Get<Reference<Sport>>(nameof(PreferedSport));
            }
            set
            {
                Set(nameof(PreferedSport), value);
            }
        }

        [JsonProperty]
        public Personal Personnal
        {
            get
            {
                return Get<Personal>(nameof(Personnal));
            }
            set
            {
                Set(nameof(Personnal), value);
            }
        }

        [JsonProperty]
        public Professional Professional
        {
            get
            {
                return Get<Professional>(nameof(Professional));
            }
            set
            {
                Set(nameof(Professional), value);
            }
        }
    }

    [BdsObject("SAMPLE.Personal")]
    public class Personal : BdsObject
    {       
        [Field(Inline = Feature.Enable)]
        [JsonProperty]
        public Phone MobilePhone
        {
            get
            {
                return Get<Phone>(nameof(MobilePhone));
            }
            set
            {
                Set(nameof(MobilePhone), value);
            }
        }        
    }

    [BdsObject("SAMPLE.Professional")]
    public class Professional : BdsObject
    {
        [JsonProperty]
        public string CompagnyName
        {
            get
            {
                return Get<string>(nameof(CompagnyName));
            }
            set
            {
                Set(nameof(CompagnyName), value);
            }
        }

        [JsonProperty]
        public string JobTitle
        {
            get
            {
                return Get<string>(nameof(JobTitle));
            }
            set
            {
                Set(nameof(JobTitle), value);
            }
        }
    }
}
