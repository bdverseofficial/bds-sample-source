using BDVerse.Bds.Crm.Sdk.Enums;
using BDVerse.Bds.Crm.Sdk.Models;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDVerse.Bds.Sample.Plugin.Models
{
    /// <summary>
    /// Define a new type of Person
    /// </summary>
    [Enum(Name = "CRM.AccountType")]
    public class SampleAccountType : EnumValue
    {
        public static readonly AccountType Member = new AccountType() { Value = 3000001, Code = "Member", Label = "Member" };        
    }

    /// <summary>
    /// Define a new entity for storing the member that are connecting
    /// The group mode is inherited just to ensure unique id in all Account
    /// </summary>
    [BdsObject("SAMPLE.Member", GroupMode = BdsObjectGroupMode.Inherit, CacheType = CacheType.Long, ViewName = "Sample Members")]
    public class Member : Person
    {
        public Member()
            : base()
        {
            this.SyncLoginWithEmail = true;
            this.AccountType = SampleAccountType.Member;
        }
    }
}
