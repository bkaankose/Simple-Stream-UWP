using Simple_Stream_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Streaming.Adaptive;

namespace Simple_Stream_UWP.Interfaces
{
    public interface ITwitchRepository
    {
        ObservableCollection<StreamInformation> GetLatestLoadedGames();
        void SetLatestLoadedGames(ObservableCollection<StreamInformation> games);
        Task<ObservableCollection<FeaturedGame>> GetFeaturedGames();
        Task ReloadFeaturedGames();
        Task<ObservableCollection<StreamInformation>> GetGameDetails(string gameName);
        Task<AdaptiveMediaSourceCreationResult> FetchStreamHLS(string channelName);
    }
}
