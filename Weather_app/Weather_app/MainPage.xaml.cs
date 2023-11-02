﻿using System;
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

            // Validate the city name.
            if (city.Length < 2)
            {
                await DisplayAlert("Ошибка", "Название города слишком короткое", "ОК :(");
                return;
            }

            // Create an HttpClient object to send HTTP requests.
            HttpClient client = new HttpClient();

            // Build the URL to the OpenWeatherMap API.
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API}&units=metric";

            try
            {
                // Send an HTTP request to the OpenWeatherMap API.
                string response = await client.GetStringAsync(url);

                // Parse the JSON response from the OpenWeatherMap API.
                var json = JObject.Parse(response);

                // Extract and format the weather information 
                string mainWeather = json["weather"][0]["description"].ToString();
                string temperature = json["main"]["temp"].ToString() + "°C";
                string feelsLike = json["main"]["feels_like"].ToString() + "°C";
                string pressure = json["main"]["pressure"].ToString() + " hPa";
                string humidity = json["main"]["humidity"].ToString() + "%";
                string windSpeed = json["wind"]["speed"].ToString() + " м/с";
                string clouds = json["clouds"]["all"].ToString() + "% облачности";
                string cityName = json["name"].ToString();
                string country = json["sys"]["country"].ToString();

                // Display the weather information 
                resultLabel.Text = $"Погода в городе {cityName}, {country}:\n" +
                                  $"Основные условия: {mainWeather}\n" +
                                  $"Температура: {temperature}\n" +
                                  $"Ощущается как: {feelsLike}\n" +
                                  $"Давление: {pressure}\n" +
                                  $"Влажность: {humidity}\n" +
                                  $"Скорость ветра: {windSpeed}\n" +
                                  $"Облачность: {clouds}";
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Ошибка", "Не удалось получить данные о погоде. Проверьте подключение к интернету и название города.", "ОК :(");
            }
        }


    }
}
