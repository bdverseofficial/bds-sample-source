using System.Threading.Tasks;
using BDVerse.Bds.Sdk;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Controllers;
using BDVerse.Bds.Sample.Plugin;
using BDVerse.Bds.Sample.Plugin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Models.Services;
using BDVerse.Bds.Sample.Plugin.Models;

namespace BDVerse.Bds.Sample.Plugin.Controllers
{
    /// <summary>
    /// Sample controller
    /// The route must omit the "/api/", this will be managed by BDS server
    /// ApiExplorerSettings is allowing to declare this controller to be part of the plugin
    /// BdsAuthorize define the global rules for authorization: 
    /// - The authorization is based on the User logged
    /// - The user must be granted on the App with the role "USER"
    /// </summary>
    [Route("/sample/v1")]
    [ApiExplorerSettings(GroupName = SamplePlugin.PLUGIN_ID)]
    [BdsAuthorize(Target = SecurityTarget.User, AppId = SamplePlugin.APP_ID, Roles = Constants.ROLE_USER)]
    public class SampleController : BdsBaseController
    {
        private readonly ISampleService sampleService;

        /// <summary>
        /// Constructor of the controller, we get by injection the ISampleService
        /// </summary>
        /// <param name="sampleService"></param>
        public SampleController(ISampleService sampleService)
        {
            this.sampleService = sampleService;            
        }

        /// <summary>
        /// Our first operation available for all
        /// </summary>
        /// <returns></returns>
        [Route("helloworld")]
        [HttpGet]
        [AllowAnonymous]
        public Task<string> HelloWorld()
        {
            return sampleService.HelloWorld();
        }    

        /// <summary>
        /// Define the request expected
        /// </summary>
        public class RegisterRequest
        {
            public Member Member { get; set; }

            public string Password { get; set; }
        }

        /// <summary>
        /// Register a member
        /// Can be replaced by using directly /api/cs/v1/b2ccustomer/registerCustomer in the CS plugin, in that case Member must inherit B2CCustomer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<Member> Register([FromBody] RegisterRequest request)
        {
            if (request == null) throw new ApiException(System.Net.HttpStatusCode.BadRequest, "REQUESTEMPTY", "Request is empty");            
            return await sampleService.Register(request.Member, request.Password);
        }              
    }
}