using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Nexus.Base.CosmosDBRepository;
using System;

namespace BM7Tutorial.DAL
{
    public class Class : ModelBase
    {
        [JsonProperty("classCode")]
        public string ClassCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
