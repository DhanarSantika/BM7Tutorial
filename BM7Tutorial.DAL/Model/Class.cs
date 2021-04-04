using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace BM7Tutorial.DAL
{
    public class Class
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("classCode")]
        public string ClassCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
