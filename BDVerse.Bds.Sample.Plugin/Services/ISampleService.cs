using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BDVerse.Bds.Sample.Plugin.Models;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Services;

namespace BDVerse.Bds.Sample.Plugin.Services
{

    [Service]
    public interface ISampleService
    {                
        Task<IEnumerable<Sport>> GetSports();

        Task<Sport> ChangeSportGroupSize(Sport sport, int size);
    }
}
