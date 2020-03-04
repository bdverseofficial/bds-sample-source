using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bds.Sample.Plugin.Models;
using BDVerse.Bds.Sdk.Attributes;
using BDVerse.Bds.Sdk.Models;
using BDVerse.Bds.Sdk.Services;

namespace Bds.Sample.Plugin.Services
{
    [Service]
    public class SampleService : ISampleService
    {
        private readonly IServerService server;

        /// <summary>
        /// By injection received the server service
        /// </summary>
        /// <param name="server"></param>
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
        /// Set the preferred sport to the current user
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns></returns>
        public async Task<Member> SetPreferredSport(string sportId) 
        {
            var me = server.Context.Identity.User?.Cast<Member>();
            var sport = (await server.Entity.GetById<Sport>(sportId)).ToReferenceOrDefault();
            if (me != null && sport != null)
            {
                return await server.Entity.UpdateAndSave(me, (m) => { m.PreferredSport = sport; return m; });
            }
            return null;
        }
    }
}
