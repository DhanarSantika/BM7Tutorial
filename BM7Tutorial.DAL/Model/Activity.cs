using Newtonsoft.Json;
using Nexus.Base.CosmosDBRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BM7Tutorial.DAL.Model
{
    public class Activity : ModelBase
    {
        [JsonProperty("classId")]
        public string ClassId { get; set; }

        [JsonProperty("activityName")]
        public string ActivityName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
