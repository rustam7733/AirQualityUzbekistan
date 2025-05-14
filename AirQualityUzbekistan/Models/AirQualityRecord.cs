namespace AirQualityUzbekistan.Models
{
    public class AirQualityRecord
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime Timestamp { get; set; }

        // Pollution
        public int AQIUS { get; set; }
        public string MainPollutantUS { get; set; }
        public int AQICN { get; set; }
        public string MainPollutantCN { get; set; }

        // Weather
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int Temperature { get; set; }
        public int WindDirection { get; set; }
        public double WindSpeed { get; set; }
    }
}
