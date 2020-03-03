using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bds.Sample.Plugin.Models;
using BDVerse.Bds.Sdk.Attributes;

namespace Bds.Sample.Plugin.Services
{
    [Service]
    public interface ISampleService
    {
        Task<Member> SetPreferredSport(string sportId);

        Task<IEnumerable<Sport>> GetSports(); 
    }
}
