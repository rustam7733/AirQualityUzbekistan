using AirQualityUzbekistan.Data;
using AirQualityUzbekistan.Models;
using AirQualityUzbekistan.Services.AirQualityService.AirQualityService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AirQualityUzbekistan.Controllers
{
    public class AirQualityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAirQualityService _airQualityService;

        public AirQualityController(ApplicationDbContext context, IAirQualityService airQualityService)
        {
            _context = context;
            _airQualityService = airQualityService;
        }

        // GET: /AirQuality
        public async Task<IActionResult> Index(string? state, string? city)
        {
            var states = await _context.LocationStates.OrderBy(s => s.Name).ToListAsync();
            var cities = string.IsNullOrEmpty(state)
                ? new List<LocationCity>()
                : await _context.LocationCities
                    .Where(c => c.LocationState.Name == state)
                    .OrderBy(c => c.Name)
                    .ToListAsync();

            var query = _context.AirQualityRecords.AsQueryable();

            if (!string.IsNullOrEmpty(state))
                query = query.Where(r => r.State == state);

            if (!string.IsNullOrEmpty(city))
                query = query.Where(r => r.City == city);

            var records = await query.OrderByDescending(r => r.Timestamp).ToListAsync();

            ViewBag.States = new SelectList(states, "Name", "Name", state);
            ViewBag.Cities = new SelectList(cities, "Name", "Name", city);

            return View(records);
        }

        // GET: /AirQuality/Fetch
        public async Task<IActionResult> Fetch(string state, string city)
        {
            var states = await _context.LocationStates.OrderBy(s => s.Name).ToListAsync();
            var cities = string.IsNullOrEmpty(state)
                ? new List<LocationCity>()
                : await _context.LocationCities
                    .Where(c => c.LocationState.Name == state)
                    .OrderBy(c => c.Name)
                    .ToListAsync();

            ViewBag.States = new SelectList(states, "Name", "Name", state);
            ViewBag.Cities = new SelectList(cities, "Name", "Name", city);

            if (string.IsNullOrEmpty(state) || string.IsNullOrEmpty(city))
            {
                ModelState.AddModelError("", "Укажите и штат, и город");
                return View();
            }

            try
            {
                var record = await _airQualityService.GetAirQualityAsync(state, city);
                return View(record);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка при получении данных: {ex.Message}");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(AirQualityRecord model)
        {
            // Проверка: существует ли запись по городу за последние 24 часа
            var limit = DateTime.UtcNow.AddHours(-24);

            var existing = await _context.AirQualityRecords
                .Where(r => r.City == model.City && r.State == model.State && r.Timestamp >= limit)
                .FirstOrDefaultAsync();

            if (existing != null)
            {
                TempData["Message"] = $"Данные по городу '{model.City}' уже сохранены за последние 24 часа.";
                return RedirectToAction("Fetch", new { state = model.State, city = model.City });
            }

            model.Timestamp = DateTime.UtcNow;
            _context.AirQualityRecords.Add(model);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"Данные по городу '{model.City}' успешно сохранены.";
            return RedirectToAction("Fetch", new { state = model.State, city = model.City });
        }


        // GET: /AirQuality/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.AirQualityRecords.FindAsync(id);
            if (record == null) return NotFound();

            _context.AirQualityRecords.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(string state)
        {
            if (string.IsNullOrEmpty(state))
                return Json(new List<string>());

            var cities = await _context.LocationCities
                .Where(c => c.LocationState.Name == state)
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToListAsync();

            return Json(cities);
        }
    }
}
