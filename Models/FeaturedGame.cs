using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models
{
    public class FeaturedGame
    {
        [JsonProperty("viewers")]
        public int ViewerCount { get; set; }
        [JsonProperty("channels")]
        public int ChannelCount { get; set; }
        [JsonProperty("game")]
        public Game GameObject { get; set; }
    }
}
