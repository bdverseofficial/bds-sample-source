using System;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bds.Sample.Plugin
{
    [Plugin(Id = "SAMPLE", Name= "Sample")]
    [ClientApplication(Id = "SAMPLE", Name = "Sample", UserTypes = "SAMPLE.Member")]
    public class SamplePlugin : BdsPlugin
    {
    }
}
