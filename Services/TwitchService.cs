using Simple_Stream_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Simple_Stream_UWP.Models.ServiceModels;
using System.Collections.ObjectModel;
using Simple_Stream_UWP.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Simple_Stream_UWP.Services
{
    public class TwitchService : ITwitchService
    {
        private readonly HttpClient _httpClient;
        public TwitchService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationContext.BASE_URL,UriKind.Absolute);

        }
        #region Helpers
        internal T ParseResponse<T>(ServiceResponse response)
        {
            if (response.IsSuccess)
                try
                {
                    return JsonConvert.DeserializeObject<T>(response.Data.ToString());
                }
                catch
                { // Deserialization error.
                    if (Debugger.IsAttached) throw;
                    return default(T);
                }
            else
                return default(T);
        }
        internal async Task<ServiceResponse> FetchDataFromService(string endpoint)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = await _httpClient.GetStringAsync(endpoint);
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    throw;

                response.ExceptionContainer = ex;
                response.Data = null;
            }

            return response;
        }

        #endregion

        public async Task<ObservableCollection<FeaturedGame>> GetFeaturedChannels()
        {
            var response =  await FetchDataFromService($"games/top?limit={ConfigurationContext.FEATURED_GAME_COUNT}");
            dynamic d = JObject.Parse(response.Data.ToString());
            response.Data = d.top;
            return ParseResponse<ObservableCollection<FeaturedGame>>(response);
        }

        public async Task<ObservableCollection<StreamInformation>> GetGameDetails(string gameName)
        {
            var response = await FetchDataFromService($"streams?game={gameName}");
            dynamic d = JObject.Parse(response.Data.ToString());
            response.Data = d.streams;
            return ParseResponse<ObservableCollection<StreamInformation>>(response);
        }

        public async Task<TwitchToken> GetStreamToken(string channelName)
        {
            string tokenResponse = string.Empty;
            using (var newHttpClient = new HttpClient())
            {
                tokenResponse = await newHttpClient.GetStringAsync($"http://api.twitch.tv/api/channels/{channelName}/access_token");
            }
            return JsonConvert.DeserializeObject<TwitchToken>(tokenResponse);
        }
        public async Task<string> FetchStreamURL(string channelName)
        {
            var token = await GetStreamToken(channelName);
            return $"http://usher.justin.tv/api/channel/hls/{channelName.ToLower()}.m3u8?token={WebUtility.UrlEncode(token.ValidToken)}&sig={token.Sig}&allow_source=true";
        }
    }
}
