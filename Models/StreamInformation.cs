﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models
{
    public class StreamInformation
    {
        [JsonProperty("_id")]
        public object StreamId { get; set; }
        [JsonProperty("game")]
        public string GameName { get; set; }
        [JsonProperty("viewers")]
        public int ViewerCount { get; set; }
        [JsonProperty("created_at")]
        public string OnlineSince { get; set; }
        [JsonProperty("average_fps")]
        public double AverageFPS { get; set; }
        [JsonProperty("delay")]
        public int StreamDelay { get; set; }
        [JsonProperty("preview")]
        public Logo Logo { get; set; }
        [JsonProperty("channel")]
        public Channel Channel { get; set; }
    }
}