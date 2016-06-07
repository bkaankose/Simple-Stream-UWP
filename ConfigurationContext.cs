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
        public static double DEFAULT_MAX_LOGO_WIDTH;
        public static double DEFAULT_MAX_LOGO_HEIGHT;

        static ConfigurationContext()
        {
            BASE_URL = "https://api.twitch.tv/kraken/";
            FEATURED_GAME_COUNT = 30;
            DEFAULT_MAX_LOGO_WIDTH = 225;
            DEFAULT_MAX_LOGO_HEIGHT = 365;
        }
    }
}
