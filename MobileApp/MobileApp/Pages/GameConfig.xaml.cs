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
    public partial class GameConfig : ContentPage
    {
        public GameConfig()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            List<PlayerProfile> profiles = new List<PlayerProfile>();
            if (!isValid())
            {
                await DisplayAlert("What is your Name?","Please check name inputs","Ok");
                return;
            }
            profiles.Add(new PlayerProfile()
            {
                PlayerID = 1,
                Name = Player1.Text
            });

            profiles.Add(new PlayerProfile()
            {
                PlayerID = 2,
                Name = Player2.Text
            });

            profiles.Add(new PlayerProfile()
            {
                PlayerID = 3,
                Name = Player3.Text
            });

            profiles.Add(new PlayerProfile()
            {
                PlayerID = 4,
                Name = Player4.Text
            });

            float rate = 0;

            float.TryParse(MoneyPerCard.Text, out rate);

            await Navigation.PushAsync(new GameOnGoing(profiles, rate));
        }

        private bool isValid()
        {
            if (!string.IsNullOrWhiteSpace(Player1.Text) &&
                !string.IsNullOrWhiteSpace(Player2.Text) &&
                !string.IsNullOrWhiteSpace(Player3.Text) &&
                !string.IsNullOrWhiteSpace(Player4.Text))
            {
                return true;
            }

            return false;
        }
    }
}