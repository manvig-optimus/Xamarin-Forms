using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem
{
    [JsonObject]
    public class DriveFile
    {
        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        [JsonProperty("files")]
        public List<File> Files { get; set; }
    }
}
