using Simple_Stream_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Stream_UWP.Models;
using System.Collections.ObjectModel;
using Windows.Media.Streaming.Adaptive;

namespace Simple_Stream_UWP.Services
{
    public class TwitchRepository : ITwitchRepository
    {
        #region Cache Properties

        private ObservableCollection<FeaturedGame> FeaturedGames;
        private ObservableCollection<StreamInformation> LatestLoadedGameObjects;

        #endregion
        internal ITwitchService _twitchService;

        public TwitchRepository(ITwitchService twitchService)
        {
            _twitchService = twitchService;
        }
        public async Task<ObservableCollection<FeaturedGame>> GetFeaturedGames()
        {
            if (FeaturedGames == null)
                FeaturedGames = await _twitchService.GetFeaturedChannels();

            return FeaturedGames;
        }

        public async Task ReloadFeaturedGames()
        {
            FeaturedGames = await _twitchService.GetFeaturedChannels();
        }

        public ObservableCollection<StreamInformation> GetLatestLoadedGames()
        {
            return LatestLoadedGameObjects;
        }

        public async Task<ObservableCollection<StreamInformation>> GetGameDetails(string gameName)
        {
            return await _twitchService.GetGameDetails(gameName);
        }

        public async Task<AdaptiveMediaSourceCreationResult> FetchStreamHLS(string channelName)
        {
            var streamUrl = await _twitchService.FetchStreamURL(channelName);
            if (string.IsNullOrEmpty(streamUrl))
                App.Current.Exit();

            return await AdaptiveMediaSource.CreateFromUriAsync(new Uri(streamUrl));
        }

        public void SetLatestLoadedGames(ObservableCollection<StreamInformation> games)
        {
            LatestLoadedGameObjects = games;
        }
    }
}
