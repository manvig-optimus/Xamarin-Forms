using DocumentManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem
{
    [JsonObject]
    public class File
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("webViewLink")]
        public string WebViewLink { get; set; }

        [JsonProperty("iconLink")]
        public string IconLink { get; set; }

        [JsonProperty("modifiedTime")]
        public string ModifiedTime { get; set; }

        [JsonProperty("lastModifyingUser")]
        public ModifiedBy LastModifyingUser { get; set; }

        [JsonProperty("permissions")]
        public List<Permission> Permissions { get; set; }
    }
}
