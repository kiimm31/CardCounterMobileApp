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
    public partial class GameResult : ContentPage
    {
        private List<PlayerProfile> _profiles;
        private readonly GameDetailsViewModel _history;
        private Color LosingColor => new Color(100,0,0);
        private Color WinningColor => new Color(0,100,0);
        private readonly float _rate;
        public GameResult(List<PlayerProfile> profiles, GameDetailsViewModel history, float rate)
        {
            InitializeComponent();
            _profiles = profiles;
            _history = history;
            _rate = rate;
            calculateTotalCards();

            loadMe();
        }

        private void calculateTotalCards()
        {
            _profiles.FirstOrDefault(y => y.PlayerID == 1).TotalCards = _history.Games.Sum(x => x.p1Cards);
            _profiles.FirstOrDefault(y => y.PlayerID == 2).TotalCards = _history.Games.Sum(x => x.p2Cards);
            _profiles.FirstOrDefault(y => y.PlayerID == 3).TotalCards = _history.Games.Sum(x => x.p3Cards);
            _profiles.FirstOrDefault(y => y.PlayerID == 4).TotalCards = _history.Games.Sum(x => x.p4Cards);

            foreach (PlayerProfile player in _profiles)
            {
                player.Winnings = 0;
                foreach (PlayerProfile playerLoop in _profiles.Where(x => x.PlayerID != player.PlayerID))
                {
                    player.Winnings += playerLoop.TotalCards - player.TotalCards;
                }
            }
        }

        private void loadMe()
        {
            p1Name.Text = _profiles.FirstOrDefault(x => x.PlayerID == 1).Name;
            p2Name.Text = _profiles.FirstOrDefault(x => x.PlayerID == 2).Name;
            p3Name.Text = _profiles.FirstOrDefault(x => x.PlayerID == 3).Name;
            p4Name.Text = _profiles.FirstOrDefault(x => x.PlayerID == 4).Name;

            p1Value.Text = _profiles.FirstOrDefault(x => x.PlayerID == 1).TotalCards.ToString();
            p2Value.Text = _profiles.FirstOrDefault(x => x.PlayerID == 2).TotalCards.ToString();
            p3Value.Text = _profiles.FirstOrDefault(x => x.PlayerID == 3).TotalCards.ToString();
            p4Value.Text = _profiles.FirstOrDefault(x => x.PlayerID == 4).TotalCards.ToString();

            p1Profits.Text = (_profiles.FirstOrDefault(x => x.PlayerID == 1).Winnings * _rate).ToString();
            p2Profits.Text = (_profiles.FirstOrDefault(x => x.PlayerID == 2).Winnings * _rate).ToString();
            p3Profits.Text = (_profiles.FirstOrDefault(x => x.PlayerID == 3).Winnings * _rate).ToString();
            p4Profits.Text = (_profiles.FirstOrDefault(x => x.PlayerID == 4).Winnings * _rate).ToString();

            p1Profits.TextColor = float.Parse(p1Profits.Text) >= 0 ? WinningColor : LosingColor;
            p2Profits.TextColor = float.Parse(p2Profits.Text) >= 0 ? WinningColor : LosingColor;
            p3Profits.TextColor = float.Parse(p3Profits.Text) >= 0 ? WinningColor : LosingColor;
            p4Profits.TextColor = float.Parse(p4Profits.Text) >= 0 ? WinningColor : LosingColor;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameConfig());
        }
    }
}