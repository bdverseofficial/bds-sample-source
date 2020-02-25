using System;
using System.Threading.Tasks;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models.Metadata;
using BDVerse.Bds.Sdk.Services;
using BDVerser.Bds.Sample.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BDVerser.Bds.Sample.Plugin
{
    [Plugin(Id = PLUGIN_ID, Name = "Sample Plugin", NeedLicence = false)]
    [Dependency(PluginId = "CRM")]
    [Dependency(PluginId = "PIM")]
    [Dependency(PluginId = "CS")]
    [ClientApplication(Id = APP_ID, Name = APP_NAME, UserTypes = USER_TYPES, Roles = USER_ROLES)]
    public class SamplePlugin : BdsPlugin
    {
        public const string PLUGIN_ID = "SAMPLE";
        public const string APP_ID = "SAMPLE";
        public const string APP_NAME = "Sample App";
        public const string USER_TYPES = "CS.B2CCustomer";
        public const string USER_ROLES = "";
        public const string API_USERNAME = "SAMPLEAPI";
        public const string API_USER_DISPLAYNAME = "SAMPLE API User";
        public const string API_USER_PASSWORD = "P@ssw0rd!";        

        public override Task InitClientApplication(IServiceProvider services, string appId)
        {
            if (appId == APP_ID)
            {
                var csService = services.GetRequiredService<ISampleService>();
                return csService.InitClientApplication();
            }
            return Task.CompletedTask;
        }
    }
}
