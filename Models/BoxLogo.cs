using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Models
{
    public class BoxLogo
    {
        private string _fixedBoxLogo;
        [JsonProperty("template")]
        public string FixedBoxLogo
        {
            get { return Regex.Replace(Regex.Replace(_fixedBoxLogo, "{width}", ConfigurationContext.DEFAULT_MAX_LOGO_WIDTH.ToString()), "{height}", ConfigurationContext.DEFAULT_MAX_LOGO_HEIGHT.ToString()); }
            set { _fixedBoxLogo = value; }
        }

        [JsonProperty("large")]
        public string Large { get; set; }

    }
}
