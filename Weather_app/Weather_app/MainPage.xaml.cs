using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Weather_app
{
    public partial class MainPage : ContentPage
    {
        const string API = "f5378ff95499c7cd8f27fcf69f4bb8d2";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void getWeather_Clicked(object sender, EventArgs e)
        {
            string city = userInput.Text.Trim();
            if(city.Length < 2)
            {

                await DisplayAlert("Error", "City used to be bigger", "Okay :(");
                return;

            }

            HttpClient client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API}&units=metric";
            string response = await client.GetStringAsync(url);

            var json = JObject.Parse(response);
            string temp = json["main"]["temp"].ToString();

            resultLabel.Text = "Погода сейчас: " + temp;

        }

    }
}
