using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Components
{
    public class SystemStatusPage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://www.pluralsight.com");
            if (response.StatusCode == HttpStatusCode.OK)
                return View(true);
            return View(false);
        }
    }
}
