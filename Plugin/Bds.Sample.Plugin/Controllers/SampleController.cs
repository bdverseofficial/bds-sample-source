using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bds.Sample.Plugin.Models;
using Bds.Sample.Plugin.Services;
using BDVerse.Bds.Sdk;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bds.Sample.Plugin.Controllers
{
    /// <summary>
    /// our main controller for the app
    /// we defined the base security to be on the User logged limited to the call fron this app with the role USER
    /// </summary>
    [Route("/sample/v1")]
    [ApiExplorerSettings(GroupName = "SAMPLE")]
    [BdsAuthorize(Target = SecurityTarget.User, AppId = "SAMPLE", Roles = Constants.ROLE_USER)]
    public class SampleController : BdsBaseController
    {
        private readonly ISampleService sampleService;

        /// <summary>
        /// The injection is giving us our service
        /// </summary>
        /// <param name="sampleService"></param>
        public SampleController(ISampleService sampleService)
            : base()
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
        /// Update the preferred sport
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateSport/{sportId}")]
        public Task<Member> UpdateSport(string sportId)
        {
            return sampleService.SetPreferredSport(sportId);
        }
    }
}
