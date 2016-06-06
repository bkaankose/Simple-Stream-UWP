using Simple_Stream_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Interfaces
{
    public interface ITwitchRepository
    {
        Task<ObservableCollection<Game>> GetFeaturedGames();
    }
}
