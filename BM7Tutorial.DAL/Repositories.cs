using BM7Tutorial.DAL.Model;
using Microsoft.Azure.Documents.Client;
using Nexus.Base.CosmosDBRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BM7Tutorial.DAL
{
    public class Repositories
    {
        private static readonly string C_EventGridEndPoint = Environment.GetEnvironmentVariable("EventGridEndPoint");
        private static readonly string C_EventGridKey = Environment.GetEnvironmentVariable("EventGridKey");

        public class ClassRepository : DocumentDBRepository<Class>
        {
            public ClassRepository(DocumentClient client) :
                base(databaseId: "Course", cosmosDBClient: client,
                    eventGridEndPoint: C_EventGridEndPoint, eventGridKey: C_EventGridKey)
            { }
        }

        public class ActivityRepository : DocumentDBRepository<Activity>
        {
            public ActivityRepository(DocumentClient client) :
                base(databaseId: "Course", cosmosDBClient: client,
                    eventGridEndPoint: C_EventGridEndPoint, eventGridKey: C_EventGridKey)
            { }
        }
    }
}
