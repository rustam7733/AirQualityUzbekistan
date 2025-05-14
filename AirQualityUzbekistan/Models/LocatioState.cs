namespace AirQualityUzbekistan.Models
{
    public class LocationState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<LocationCity> Cities { get; set; }
    }
}
