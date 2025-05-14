using System.Net;
using System.Text.Json;
using AirQualityUzbekistan.Models;

namespace AirQualityUzbekistan.Services.AirQualityFolder
{
    public class AirQualityService : Services.AirQualityService.AirQualityService.IAirQualityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "0d25eed9-4e65-4b13-9a68-4b025bef19f6"; // Подставь свой ключ

        public AirQualityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AirQualityRecord> GetAirQualityAsync(string state, string city)
        {
            var country = "Uzbekistan";
            var url = $"https://api.airvisual.com/v2/city?city={city}&state={state}&country={country}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new HttpRequestException("Превышен лимит запросов к API.");
            }

            response.EnsureSuccessStatusCode(); // остальные ошибки

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var data = doc.RootElement.GetProperty("data");

            var pollution = data.GetProperty("current").GetProperty("pollution");
            var weather = data.GetProperty("current").GetProperty("weather");
            var location = data.GetProperty("location").GetProperty("coordinates");

            return new AirQualityRecord
            {
                City = data.GetProperty("city").GetString(),
                State = data.GetProperty("state").GetString(),
                Country = data.GetProperty("country").GetString(),
                Latitude = location[1].GetDouble(),
                Longitude = location[0].GetDouble(),
                Timestamp = DateTime.Parse(pollution.GetProperty("ts").GetString()),

                AQIUS = pollution.GetProperty("aqius").GetInt32(),
                MainPollutantUS = pollution.GetProperty("mainus").GetString(),
                AQICN = pollution.GetProperty("aqicn").GetInt32(),
                MainPollutantCN = pollution.GetProperty("maincn").GetString(),

                Humidity = weather.GetProperty("hu").GetInt32(),
                Pressure = weather.GetProperty("pr").GetInt32(),
                Temperature = weather.GetProperty("tp").GetInt32(),
                WindDirection = weather.GetProperty("wd").GetInt32(),
                WindSpeed = weather.GetProperty("ws").GetDouble(),
            };
        }
    }
}
