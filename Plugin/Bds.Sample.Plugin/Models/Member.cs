using System;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using Newtonsoft.Json;

namespace Bds.Sample.Plugin.Models
{
    [BdsObject("SAMPLE.Member")]
    public class Member : User
    {
        public Member()
            : base()
        {
            this.SyncLoginWithEmail = true;
        }

        [JsonProperty]
        public Reference<Sport> PreferredSport
        {
            get => Get<Reference<Sport>>(nameof(PreferredSport));
            set => Set(nameof(PreferredSport), value);
        }
    }
}
