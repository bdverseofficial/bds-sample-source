using System.Threading.Tasks;
using BDVerse.Bds.Sdk;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Controllers;
using BDVerser.Bds.Sample.Plugin;
using BDVerser.Bds.Sample.Plugin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDVerse.Bds.Sample.Plugin.Controllers
{
    [Route("/sample/v1")]
    [ApiExplorerSettings(GroupName = SamplePlugin.APP_ID)]
    [BdsAuthorize(Target = SecurityTarget.User, AppId = SamplePlugin.APP_ID, Roles = Constants.ROLE_USER)]
    public class SampleController : BdsBaseController
    {
        private readonly ISampleService sampleService;

        public SampleController(ISampleService sampleService)
        {
            this.sampleService = sampleService;            
        }

        [Route("helloworld")]
        [HttpGet]
        [AllowAnonymous]
        public Task<string> HelloWorld()
        {
            return sampleService.HelloWorld();
        }    

    }
}