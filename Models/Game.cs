using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models
{
    public class Game
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("_id")]
        public int GameId { get; set; }
        [JsonProperty("logo")]
        public Logo Logo { get; set; }
        [JsonProperty("box")]
        public BoxLogo Box { get; set; }
    }
}
