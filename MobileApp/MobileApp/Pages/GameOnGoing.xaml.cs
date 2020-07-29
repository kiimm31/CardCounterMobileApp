using MobileApp.Models;
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
    public partial class GameOnGoing : ContentPage
    {
        private GameDetailsViewModel _history { get; set; }
        private readonly List<PlayerProfile> _players;
        private readonly string _p1Name;
        private readonly string _p2Name;
        private readonly string _p3Name;
        private readonly string _p4Name;
        private readonly float _rate;

        public GameOnGoing(List<PlayerProfile> ListOfPlayers, float rate)
        {
            InitializeComponent();
            _history = new GameDetailsViewModel();
            _players = ListOfPlayers;
            _p1Name = _players.FirstOrDefault(x => x.PlayerID == 1).Name;
            _p2Name = _players.FirstOrDefault(x => x.PlayerID == 2).Name;
            _p3Name = _players.FirstOrDefault(x => x.PlayerID == 3).Name;
            _p4Name = _players.FirstOrDefault(x => x.PlayerID == 4).Name;

            _rate = rate;

            p1TotalCard.Text = "0";
            p2TotalCard.Text = "0";
            p3TotalCard.Text = "0";
            p4TotalCard.Text = "0";
            loadMe();
        }

        private void loadMe()
        {
            p1Name.Text = _p1Name;
            p2Name.Text = _p2Name;
            p3Name.Text = _p3Name;
            p4Name.Text = _p4Name;

            p1Value.Placeholder = $"{_p1Name}'s Cards";
            p2Value.Placeholder = $"{_p2Name}'s Cards";
            p3Value.Placeholder = $"{_p3Name}'s Cards";
            p4Value.Placeholder = $"{_p4Name}'s Cards";

            lblP1Name.Text = _p1Name;
            lblP2Name.Text = _p2Name;
            lblP3Name.Text = _p3Name;
            lblP4Name.Text = _p4Name;

            p1Value.Text = "";
            p2Value.Text = "";
            p3Value.Text = "";
            p4Value.Text = "";
        }

        private async void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                Models.GameDetail gd = new Models.GameDetail()
                {
                    p1Cards = int.Parse(p1Value.Text),
                    p2Cards = int.Parse(p2Value.Text),
                    p3Cards = int.Parse(p3Value.Text),
                    p4Cards = int.Parse(p4Value.Text)
                };
                bool added = _history.addGame(gd);
                if (added)
                {
                    reloadTable(gd);
                    loadMe();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Card Error", "Please check card inputs", "Ok");
                return;
            }
        }

        private void reloadTable(GameDetail gd)
        {
            p1TotalCard.Text = (int.Parse(p1TotalCard?.Text ?? "0") + gd.p1Cards).ToString();
            p2TotalCard.Text = (int.Parse(p2TotalCard?.Text ?? "0") + gd.p2Cards).ToString();
            p3TotalCard.Text = (int.Parse(p3TotalCard?.Text ?? "0") + gd.p3Cards).ToString();
            p4TotalCard.Text = (int.Parse(p4TotalCard?.Text ?? "0") + gd.p4Cards).ToString();
        }

        private async void btnEndGame_Clicked(object sender, EventArgs e)
        {
            var endgame = await DisplayAlert("End Game", "Do You want to end the game?", "Yes", "No");
            if (endgame)
            {
                await Navigation.PushAsync(new GameResult(_players,_history, _rate));
            }
            return;
        }

        private async void btnViewHistory_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameHistory(_history, _players));
        }

        private void pValue_Focused(object sender, FocusEventArgs e)
        {
        }
    }
}