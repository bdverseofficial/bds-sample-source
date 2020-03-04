using System;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using Newtonsoft.Json;

namespace Bds.Sample.Plugin.Models
{
    /// <summary>
    /// Create a Member to be able to sign on our application
    /// </summary>
    [BdsObject("SAMPLE.Member")]
    public class Member : User
    {
        /// <summary>
        /// We enforce the user to use his email as username
        /// </summary>
        public Member()
            : base()
        {
            this.SyncLoginWithEmail = true;
        }

        /// <summary>
        /// We store the prefered sport of an user
        /// </summary>
        /// <value>A reference to a sport</value>
        [JsonProperty]
        public Reference<Sport> PreferredSport
        {
            get => Get<Reference<Sport>>(nameof(PreferredSport));
            set => Set(nameof(PreferredSport), value);
        }
    }
}
