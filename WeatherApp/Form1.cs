using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            LoadWeatherData();
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 900000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadWeatherData();
        }

        private void LoadWeatherData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=54.36&longitude=18.64&current=temperature_2m,relative_humidity_2m,precipitation");
            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            JObject weatherData = JObject.Parse(result);

            var currentWeather = weatherData["current"];
            var temperature = currentWeather["temperature_2m"];
            var humidity = currentWeather["relative_humidity_2m"];
            var precipitation = currentWeather["precipitation"];

            Temperature.Text = $"Temperatura: {temperature} °C";
            Humidity.Text = $"Wilgotnoœæ: {humidity} %";
            Precipitation.Text = $"Opady: {precipitation} mm";
        }
    }
}
