using System.Threading.Tasks;
using BDVerse.Bds.Sdk;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Controllers;
using BDVerse.Bds.Sample.Plugin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sample.Plugin.Models;
using System.Collections.Generic;
using BDVerse.Bds.Sdk.Services;

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
        private readonly IServerService server;

        /// <summary>
        /// Constructor of the controller, we get by injection the ISampleService
        /// </summary>
        /// <param name="sampleService"></param>
        public SampleController(ISampleService sampleService, IServerService server)
        {
            this.sampleService = sampleService;
            this.server = server;
        }

        /// <summary>
        /// Our first operation available for all
        /// </summary>
        /// <returns></returns>
        [Route("helloworld")]
        [HttpGet]
        [AllowAnonymous]
        public string HelloWorld()
        {
            return "Hello World From the BDS Sample Plugin";
        }

        /// <summary>
        /// Load all sports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        [AllowAnonymous]
        public Task<IEnumerable<Sport>> GetSports()
        {
            return sampleService.GetSports();
        }

        /// <summary>
        /// Update the size of a sport group
        /// </summary>
        /// <param name="sportId"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateSize/{sportId}/{size}")]  
        [AllowAnonymous]
        public async Task<Sport> UpdateSize(string sportId, int size)
        {
            var sport = await server.Entity.GetById<Sport>(sportId);
            if (sport == null) throw new ApiException(System.Net.HttpStatusCode.NotFound, "SPORT_NOT_FOUND", $"Sport with id {sportId} was not found");
            return await sampleService.ChangeSportGroupSize(sport, size);
        }      
    }
}