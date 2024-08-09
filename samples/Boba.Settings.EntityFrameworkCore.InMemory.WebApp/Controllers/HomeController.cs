using Boba.Settings.EntityFrameworkCore.InMemory.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Boba.Settings.EntityFrameworkCore.InMemory.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestSettings _testSettings;
        private readonly IBobaSettingService _settingService;

        public HomeController(
            ILogger<HomeController> logger,
            TestSettings testSettings,
            IBobaSettingService settingService
        )
        {
            _logger = logger;
            _testSettings = testSettings;
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var allSettings = await _settingService.GetAllRegisteredSettingsAsync();
            return View(_testSettings);
        }

        public async Task<IActionResult> Create()
        {
            await _settingService.SaveSettingAsync(
                new TestSettings { DefaultLangId = 1, Enabled = false, DefaultColor = "Blue" }
            );

            return View();
        }

        public async Task<IActionResult> Update()
        {
            await _settingService.SaveSettingAsync(
                new TestSettings { DefaultLangId = 2, Enabled = true, DefaultColor = "Green" }
            );

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
