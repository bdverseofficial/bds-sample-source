using System;
using System.Threading.Tasks;
using BDVerse.Bds.Sdk.Attributes;

namespace BDVerser.Bds.Sample.Plugin.Services
{

    [Service]
    public interface ISampleService
    {
        Task InitClientApplication();

        Task<string> HelloWorld();
    }
}
