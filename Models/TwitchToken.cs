using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models
{
    public class TwitchToken
    {
        [JsonProperty("token")]
        public string ValidToken { get; set; }
        [JsonProperty("sig")]
        public string Sig { get; set; }
        [JsonProperty("mobile_restricted")]
        public bool IsMobileRestricted { get; set; }
    }
}
