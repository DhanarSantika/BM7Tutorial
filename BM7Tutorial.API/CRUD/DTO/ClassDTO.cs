using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BM7Tutorial.API.CRUD.DTO
{
    public class ClassDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("classCode")]
        public string ClassCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
