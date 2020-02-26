using System.Threading.Tasks;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Services;

namespace BDVerser.Bds.Sample.Plugin.Services
{
    /// <summary>
    /// The main sample service, this one is registered automaticly with the ServiceAttribute on all interface with the ServiceAtribute
    /// </summary>
    [Service]
    public class SampleService : ISampleService
    {
        private readonly IUserService userService;

        /// <summary>
        /// Constructor of the service, we get by injection the BDS IUserService
        /// </summary>
        /// <param name="userService"></param>        
        public SampleService(IUserService userService) 
        {
            this.userService = userService;
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
    }
}