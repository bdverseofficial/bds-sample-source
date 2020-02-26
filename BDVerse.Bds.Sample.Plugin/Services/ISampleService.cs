using System;
using System.Threading.Tasks;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Services;

namespace BDVerse.Bds.Sample.Plugin.Services
{

    [Service]
    public interface ISampleService
    {
        Task InitClientApplication();

        Task<string> HelloWorld();

        Task<SearchEntityResponse> SearchSport(SearchFullTextRequest request);
    }
}
