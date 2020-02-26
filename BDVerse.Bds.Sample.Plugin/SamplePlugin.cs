using System;
using System.Threading.Tasks;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models.Metadata;
using BDVerse.Bds.Sdk.Services;
using BDVerse.Bds.Sample.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BDVerse.Bds.Sample.Plugin
{
    /// <summary>
    /// The Sample Plugin:
    /// - Create a new plugin definition
    /// - Declare to be dependant on 3 other plugins
    /// - Declare a new client application
    /// </summary>
    [Plugin(Id = PLUGIN_ID, Name = "Sample Plugin", NeedLicence = false, VersionStr = "1.0.0.0")]
    [Dependency(PluginId = "CRM")]    
    [ClientApplication(Id = APP_ID, Name = APP_NAME, UserTypes = USER_TYPES, Roles = USER_ROLES)]
    public class SamplePlugin : BdsPlugin
    {
        /*
            Define some overall const for the plugin
        */

        public const string PLUGIN_ID = "SAMPLE";
        public const string APP_ID = "SAMPLE";
        public const string APP_NAME = "Sample App";
        public const string USER_TYPES = "SAMPLE.Member";
        public const string USER_ROLES = "";
        public const string API_USERNAME = "SAMPLEAPI";
        public const string API_USER_DISPLAYNAME = "SAMPLE API User";
        public const string API_USER_PASSWORD = "P@ssw0rd!";                

        /// <summary>
        /// Intercept the Init Client Application events, check if this is our app and create a new API User
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
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
