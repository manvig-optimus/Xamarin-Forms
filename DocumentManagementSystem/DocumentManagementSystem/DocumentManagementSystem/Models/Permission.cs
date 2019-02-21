using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Models
{
    [JsonObject]
    public class Permission
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("photoLink")]
        public string PhotoLink { get; set; }

        [JsonProperty("deleted")]
        public string Deleted { get; set; }
    }
}
