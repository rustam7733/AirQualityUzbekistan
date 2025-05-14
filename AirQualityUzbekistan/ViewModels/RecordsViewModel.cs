using AirQualityUzbekistan.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirQualityUzbekistan.ViewModels
{
    public class RecordsViewModel
    {
        // Список всех записей после фильтрации
        public List<AirQualityRecord> FilteredRecords { get; set; } = new();

        // Список штатов и городов для фильтрации
        public List<SelectListItem> StateList { get; set; } = new();
        public List<SelectListItem> CityList { get; set; } = new();

        // Текущие выбранные значения
        public string SelectedState { get; set; }
        public string SelectedCity { get; set; }

        // Поля, доступные для отображения на графике
        public List<string> AvailableFields { get; set; } = new()
        {
            "AQIUS", "AQICN", "Temperature", "Humidity", "Pressure", "WindSpeed"
        };

        // Выбранные пользователем поля для отображения на графике
        public List<string> SelectedFields { get; set; } = new();
    }
}
