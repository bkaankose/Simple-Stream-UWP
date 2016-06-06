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

        static ConfigurationContext()
        {
            BASE_URL = "http://api.twitch.tv/api/";
        }
    }
}
