using AirQualityUzbekistan.Models;

namespace AirQualityUzbekistan.Services.AirQualityService.AirQualityService
{
    public interface IAirQualityService
    {
        Task<AirQualityRecord> GetAirQualityAsync(string state, string city);
    }
}
