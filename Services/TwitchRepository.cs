using Simple_Stream_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_Stream_UWP.Models;
using System.Collections.ObjectModel;

namespace Simple_Stream_UWP.Services
{
    public class TwitchRepository : ITwitchRepository
    {
        #region Cache Properties

        private ObservableCollection<FeaturedGame> FeaturedGames;

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

        public async Task<ObservableCollection<StreamInformation>> GetGameDetails(string gameName)
        {
            return await _twitchService.GetGameDetails(gameName);
        }
    }
}
