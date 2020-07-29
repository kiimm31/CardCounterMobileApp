using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameHistory : ContentPage
    {
        private readonly GameDetailsViewModel _history;
        private readonly List<PlayerProfile> _players;

        public GameHistory(GameDetailsViewModel history, List<PlayerProfile> players)
        {
            InitializeComponent();
            _history = history;
            _players = players;
            loadMe();
            this.BindingContext = _history;
        }

        private void loadMe()
        {
            p1Name.Text = _players.FirstOrDefault(x => x.PlayerID == 1).Name;
            p2Name.Text = _players.FirstOrDefault(x => x.PlayerID == 2).Name;
            p3Name.Text = _players.FirstOrDefault(x => x.PlayerID == 3).Name;
            p4Name.Text = _players.FirstOrDefault(x => x.PlayerID == 4).Name;

            listViewHistory.ItemsSource = _history.Games;

        }
    }
}