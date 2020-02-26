using System.Threading.Tasks;
using BDVerse.Bds.Sample.Plugin.Models;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Services;
using BDVerse.Bds.Sdk.Services;

namespace BDVerse.Bds.Sample.Plugin.Services
{
    /// <summary>
    /// The main sample service, this one is registered automaticly with the ServiceAttribute on all interface with the ServiceAtribute
    /// </summary>
    [Service]
    public class SampleService : ISampleService
    {
        private readonly IUserService userService;
        private readonly IAppService appService;
        private readonly IBdsObjectMetadataService metadataService;

        /// <summary>
        /// Constructor of the service, we get by injection the BDS IUserService and IAppService
        /// </summary>
        /// <param name="userService"></param>        
        public SampleService(IUserService userService, IAppService appService, IBdsObjectMetadataService metadataService) 
        {
            this.userService = userService;
            this.appService = appService;
            this.metadataService = metadataService;
        }

        /// <summary>
        /// The init client application to create a new System User API able to sign in our application
        /// The token generated for this System User will be used by the client application to override the user rights
        /// All call from the app providing this token will be executed with the rights of that API System User.
        /// You can generate the token from the back office app in the Administration->User menu, Select the API user, on the right side, type the password, select the targeted app and generate an override API Token
        /// </summary>
        /// <returns></returns>
        public Task InitClientApplication()
        {            
            return this.userService.CreateSystemApiUser(SamplePlugin.APP_ID, SamplePlugin.API_USERNAME, SamplePlugin.API_USER_DISPLAYNAME, SamplePlugin.API_USER_PASSWORD);
        }

        public Task<string> HelloWorld() 
        {
            return Task.FromResult("Hello World From the BDS Sample Plugin");
        }

        public async Task<SearchEntityResponse> SearchSport(SearchFullTextRequest request)
        {                    
            var sportEntity = metadataService.GetEntity<Sport>();
            var result = await appService.Search(request, null, sportEntity);
            return result;            
        }
    }
}