using System;
using BethanysPieShop.Filters;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShop.Models;
using BethanysPieShop.Utility;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace BethanysPieShop.Controllers
{
    //[RequireHeader]
    [ServiceFilter(typeof(TimerAction))]
    //[TimerAction]
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _memoryCache;

        public HomeController(IPieRepository pieRepository, IStringLocalizer<HomeController> stringLocalizer, ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _pieRepository = pieRepository;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        //[ResponseCache(Duration = 30)]
        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        //[ResponseCache(Duration = 30, VaryByHeader = "User-Agent")]
        [ResponseCache(CacheProfileName = "Default")]
        public ViewResult Index()
        {
            //Serilog
            _logger.LogDebug("Loading home page");
            
            //Application Insights
            TelemetryClient tc = new TelemetryClient();
            tc.TrackPageView(new PageViewTelemetry("Insights: Bethany's Home page loaded") { Timestamp = DateTime.UtcNow });
            tc.TrackEvent("HomeControllerLoad");

            //Logic for action method
            //ViewBag.PageTitle = _stringLocalizer["PageTitle"];

            //var homeViewModel = new HomeViewModel
            //{
            //    PiesOfTheWeek = _pieRepository.PiesOfTheWeek
            //};

            //caching change for IMemoryCache
            List<Pie> piesOfTheWeekCached = null;

            if (!_memoryCache.TryGetValue(CacheEntryConstants.PiesOfTheWeek, out piesOfTheWeekCached))
            {
                piesOfTheWeekCached = _pieRepository.PiesOfTheWeek.ToList();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
                cacheEntryOptions.RegisterPostEvictionCallback(FillCacheAgain, this);

                _memoryCache.Set(CacheEntryConstants.PiesOfTheWeek, piesOfTheWeekCached, cacheEntryOptions);
            }

            //piesOfTheWeekCached = _memoryCache.GetOrCreate(CacheEntryConstants.PiesOfTheWeek, entry =>
            //{
            //    entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            //    entry.Priority = CacheItemPriority.High;
            //    return _pieRepository.PiesOfTheWeek.ToList();
            //});

            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = piesOfTheWeekCached
            };

            return View(homeViewModel);
        }

        private void FillCacheAgain(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogInformation(LogEventIds.LoadHomepage, "Cache was cleared: reason " + reason.ToString());
        }

        public IActionResult TestUrl()
        {
            // Generates /Pie/Details/1		
            var url =
                Url.Action("Details", "Pie", new { id = 1 });
            //return Content(url);
            return RedirectToAction("Index");
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //Logging
            _logger.LogInformation(LogEventIds.ChangeLanguage, "Language changed to {0}", culture);

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}