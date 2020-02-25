using System.Threading.Tasks;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Services;

namespace BDVerser.Bds.Sample.Plugin.Services
{
    [Service]
    public class SampleService : ISampleService
    {
        private readonly IUserService userService;
        
        public SampleService(IUserService userService) 
        {
            this.userService = userService;
        }

        public Task InitClientApplication()
        {            
            return this.userService.CreateSystemApiUser(SamplePlugin.APP_ID, SamplePlugin.API_USERNAME, SamplePlugin.API_USER_DISPLAYNAME, SamplePlugin.API_USER_PASSWORD);
        }

        public Task<string> HelloWorld() 
        {
            return Task.FromResult("Hello World From the BDS Sample Plugin");
        }
    }
}