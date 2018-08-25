using Newtonsoft.Json;
using System;

namespace MedicalEmergency.Domain.Entities
{
    public class Entity
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Updated { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Active { get; set; }
    }
}
