using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models
{
    public class Channel
    {
        [JsonProperty("mature")]
        public bool? IsAgeRestricted { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("broadcaster_language")]
        public string Language { get; set; }
        [JsonProperty("game")]
        public string GameName { get; set; }
        [JsonProperty("_id")]
        public int ChannelId { get; set; }
        [JsonProperty("display_name")]
        public string ChannelName { get; set; }
        [JsonProperty("partner")]
        public bool IsTwitchPartner { get; set; }
        [JsonProperty("url")]
        public string ProfileUrl { get; set; }
        [JsonProperty("views")]
        public int TotalViews { get; set; }
        [JsonProperty("followers")]
        public int FollowerCont { get; set; }
    }
}
