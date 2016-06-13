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
using Microsoft.Practices.Unity;
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
                //if (Debugger.IsAttached)
                    //throw;

                response.ExceptionContainer = ex;
                response.Data = null;
            }

            return response;
        }

        #endregion

        public async Task<ObservableCollection<FeaturedGame>> GetFeaturedChannels()
        {
            retry:
            var response =  await FetchDataFromService($"games/top?limit={ConfigurationContext.FEATURED_GAME_COUNT}");
            if(response.IsSuccess)
            {
                dynamic d = JObject.Parse(response.Data.ToString());
                response.Data = d.top;
                return ParseResponse<ObservableCollection<FeaturedGame>>(response);
            }
            else
            {
                var res = await App.Current.Container.Resolve<IPageDialogService>().DisplayConfirmationDialogAsync($"We got an error:\n{response.ExceptionContainer.Message}\nDo you want me to try again?", "Ops");
                if (res)
                    goto retry;
                else
                    App.Current.Exit();

                return null;
            }
        }

        public async Task<ObservableCollection<StreamInformation>> GetGameDetails(string gameName)
        {
            retry:
            var response = await FetchDataFromService($"streams?game={gameName}");
            if(response.IsSuccess)
            {
                dynamic d = JObject.Parse(response.Data.ToString());
                response.Data = d.streams;
                return ParseResponse<ObservableCollection<StreamInformation>>(response);
            }
            else
            {
                var res = await App.Current.Container.Resolve<IPageDialogService>().DisplayConfirmationDialogAsync($"We got an error:\n{response.ExceptionContainer.Message}\nDo you want me to try again?", "Ops");
                if (res)
                    goto retry;
                else
                    App.Current.Exit();

                return null;
            }
        }

        public async Task<TwitchToken> GetStreamToken(string channelName)
        {
            retry:
            string tokenResponse = string.Empty;
            try
            {
                using (var newHttpClient = new HttpClient())
                {
                    tokenResponse = await newHttpClient.GetStringAsync($"http://api.twitch.tv/api/channels/{channelName}/access_token");
                }
            }
            catch (Exception ex)
            {
                var res = await App.Current.Container.Resolve<IPageDialogService>().DisplayConfirmationDialogAsync($"We got an error:\n{ex.Message}\nDo you want me to try again?", "Ops");
                if (res)
                    goto retry;
                else
                    App.Current.Exit();

                return null;
            }
            
            return JsonConvert.DeserializeObject<TwitchToken>(tokenResponse);
        }
        public async Task<string> FetchStreamURL(string channelName)
        {
            try
            {
                var token = await GetStreamToken(channelName);
                if (token == null)
                {
                    App.Current.Exit();
                    return null;
                }
                else
                    return $"http://usher.justin.tv/api/channel/hls/{channelName.ToLower()}.m3u8?token={WebUtility.UrlEncode(token.ValidToken)}&sig={token.Sig}&allow_source=true";
            }
            catch (Exception)
            {
                App.Current.Exit();
                return null;
            }
        }
    }
}
