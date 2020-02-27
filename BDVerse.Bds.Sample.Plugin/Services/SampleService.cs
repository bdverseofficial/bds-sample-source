using System;
using System.Threading.Tasks;
using BDVerse.Bds.Sample.Plugin.Models;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Enums;
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
        private readonly IServerService server;        

        /// <summary>
        /// Constructor of the service, we get by injection the BDS IServerService
        /// </summary>
        /// <param name="userService"></param>        
        public SampleService(IServerService server) 
        {
            this.server = server;                     
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
            return server.User.CreateSystemApiUser(SamplePlugin.APP_ID, SamplePlugin.API_USERNAME, SamplePlugin.API_USER_DISPLAYNAME, SamplePlugin.API_USER_PASSWORD);
        }

        /// <summary>
        /// Typical HelloWorld
        /// </summary>
        /// <returns></returns>
        public Task<string> HelloWorld() 
        {
            return Task.FromResult("Hello World From the BDS Sample Plugin");
        }      

        /// <summary>
        /// Register a member by using the default identity provider included in BDS
        /// Add default roles for the app
        /// No activation is requested
        /// </summary>
        /// <param name="member"></param>
        /// <param name="password"></param>        
        /// <returns></returns>
        public async Task<Member> Register(Member member, string password)
        {                        
            if (member != null)
            {
                var application = await server.ClientApplication.GetAppConfig<ClientApplication>();
                if (String.IsNullOrWhiteSpace(member.Email)) throw new BdsException("BAD_REQUEST", "The email can not be null");
                server.User.AddUserRoleToSignIn(member, application.ToReferenceOrDefault());
                member.IdentityProvider = IdentityProviderType.Default;
                member.Password = password;               
                return await server.User.RegisterUser(member, true, null);
            }
            return null;
        }  
    }
}