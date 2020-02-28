using System;
using System.Collections.Generic;
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
        /// Load all sports from the database.
        /// This is a sample, do not use this in case of large dataset of data
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Sport>> GetSports()
        {
            return server.Entity.LoadAll<Sport>();
        }

        /// <summary>
        /// Update a sport size
        /// </summary>
        /// <param name="sport"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public async Task<Sport> ChangeSportGroupSize(Sport sport, int size)
        {
            if (sport == null) throw new BdsException("SPORT_IS_NULL", "Sport cannot be null to update the size");
            if (size <= 0) throw new BdsException("INVALID_SIZE", "Size must be greater than zero");
            return await server.Entity.UpdateAndSave(sport, s =>
            {
                s.GroupSize = size;
                return s;
            });
        }   
    }
}