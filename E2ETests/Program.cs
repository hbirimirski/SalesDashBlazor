using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace E2ETests
{
    class Program
    {
        public static async Task Main()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://www.microsoft.com");
            // other actions...
        }
    }
}
