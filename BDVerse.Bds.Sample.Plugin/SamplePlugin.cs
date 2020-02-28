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
    /// - Declare a new client application
    /// </summary>
    [Plugin(Id = PLUGIN_ID, Name = "Sample Plugin", NeedLicence = false, VersionStr = "1.0.0.0")]    
    [ClientApplication(Id = APP_ID, Name = APP_NAME, UserTypes = USER_TYPES)]
    public class SamplePlugin : BdsPlugin
    {
        /*
            Define some overall const for the plugin
        */
        public const string PLUGIN_ID = "SAMPLE";
        
        /*
            For the Client Application
        */
        public const string APP_ID = "SAMPLE";
        public const string APP_NAME = "Sample App";
        public const string USER_TYPES = "SAMPLE.Member";
        public const string USER_ROLES = "";                
    }
}
