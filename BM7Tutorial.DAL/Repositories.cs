using Microsoft.Azure.Documents.Client;
using Nexus.Base.CosmosDBRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BM7Tutorial.DAL
{
    public class Repositories
    {
        public class ClassRepository : DocumentDBRepository<Class>
        {
            public ClassRepository(DocumentClient client) :
                base(databaseId: "Course", cosmosDBClient: client, partitionProperties: "ClassCode",
                    eventGridEndPoint: "", eventGridKey: "")
            { }
        }
    }
}
