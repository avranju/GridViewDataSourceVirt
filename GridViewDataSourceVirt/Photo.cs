using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewDataSourceVirt
{
    class Photo
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("server")]
        public string Server { get; set; }
        [JsonProperty("farm")]
        public int Farm { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("ispublic")]
        public bool IsPublic { get; set; }
        [JsonProperty("isfriend")]
        public bool IsFriend { get; set; }
        [JsonProperty("isfamily")]
        public bool IsFamily { get; set; }

        [JsonIgnore]
        public Uri Uri
        {
            get
            {
                return new Uri(String.Format("http://farm{0}.staticflickr.com/{1}/{2}_{3}_q.jpg",
                    Farm, Server, Id, Secret));
            }
        }
    }
}
