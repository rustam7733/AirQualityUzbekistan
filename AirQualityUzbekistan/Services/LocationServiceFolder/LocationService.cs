using AirQualityUzbekistan.Data;
using AirQualityUzbekistan.Models;
using System.Text.Json;

public class LocationService
{
    private readonly ApplicationDbContext _context;

    public LocationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task EnsureStatesAndCitiesAsync()
    {
        if (_context.LocationStates.Any())
            return;

        var stateCityMap = new Dictionary<string, List<string>>
        {
            ["Andijon"] = new() { "Andijon" },
            ["Bukhara"] = new() { "Bukhara", "Kagan" },
            ["Fergana"] = new() { "Beshariq", "Fergana", "Kirguli" },
            ["Jizzax"] = new() { "Jizzax", "Paxtakor" },
            ["Karakalpakstan"] = new() { "Nukus" },
            ["Namangan"] = new() { "Namangan" },
            ["Navoiy"] = new() { },
            ["Qashqadaryo"] = new() { },
            ["Samarqand"] = new() { "Samarqand" },
            ["Sirdaryo"] = new() { "Guliston" },
            ["Surxondaryo"] = new() { "Tirmiz" },
            ["Toshkent"] = new() { "G'azalkent", "Parkent", "Qibray", "Salor", "Sidzhak", "Tuytepa", "Urtaowul", "Yangiobod" },
            ["Toshkent Shahri"] = new() { "Tashkent" },
            ["Xorazm"] = new() { "Pitnak", "Urganch" },
        };

        foreach (var entry in stateCityMap)
        {
            var state = new LocationState { Name = entry.Key };
            _context.LocationStates.Add(state);
            await _context.SaveChangesAsync();

            foreach (var cityName in entry.Value)
            {
                var city = new LocationCity { Name = cityName, LocationStateId = state.Id };
                _context.LocationCities.Add(city);
            }
        }

        await _context.SaveChangesAsync();
    }
}
