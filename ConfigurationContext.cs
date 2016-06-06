using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP
{
    public class ConfigurationContext
    {
        public static string BASE_URL;
        public static int FEATURED_GAME_COUNT;

        static ConfigurationContext()
        {
            BASE_URL = "https://api.twitch.tv/kraken/";
            FEATURED_GAME_COUNT = 30;
        }
    }
}
