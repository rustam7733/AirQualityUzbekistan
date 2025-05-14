namespace AirQualityUzbekistan.Models
{
    public class LocationCity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LocationStateId { get; set; }
        public LocationState LocationState { get; set; }
    }
}
