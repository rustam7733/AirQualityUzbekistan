namespace AirQualityUzbekistan.Models
{
    public class AirQualityViewModel
    {
        public List<LocationState> States { get; set; }
        public string SelectedState { get; set; }
        public string SelectedCity { get; set; }
        public List<AirQualityRecord> Records { get; set; } = new();
    }
}
