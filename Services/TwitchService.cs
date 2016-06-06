using Simple_Stream_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Services
{
    public class TwitchService : ITwitchService
    {
        private HttpClient _httpClient;
        public TwitchService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationContext.BASE_URL,UriKind.Absolute);
        }
    }
}
