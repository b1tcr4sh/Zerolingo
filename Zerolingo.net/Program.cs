using System;
using PuppeteerSharp;
using System.Threading.Tasks;
using Zerolingo;

namespace Zerolingo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });

            Console.WriteLine($"Downloaded Chromium version {await browser.GetVersionAsync()}");

            var page = await browser.NewPageAsync();
            await page.GoToAsync("https://duolingo.com");

            await login(page, browser);
        }
        static async Task login(Page page, Browser browser)
        {
            PasswordManager passwordManager = new PasswordManager();


            ElementHandle loginButton = await page.WaitForSelectorAsync("[data-test=have-account]");

            await page.ClickAsync("div._3uMJF");

            Console.WriteLine("Logging in... Please enter your Duolingo login credentials:");
            string[] credentials = passwordManager.CollectCredentials("Duolingo");

            Console.WriteLine("Loading webpage...");
            await page.WaitForSelectorAsync("input._3MNft.fs-exclude");

            
            await page.TypeAsync("[data-test=\"email-input\"]", credentials[0]);
            await page.TypeAsync("[data-test=\"password-input\"]", credentials[1]);

            await page.ClickAsync("button._1rl91._3HhhB._2NolF._275sd._1ZefG._2oW4v");

            // Sign in with Google
            await page.WaitForSelectorAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");
            await page.ClickAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");

            var pages = await browser.PagesAsync();
            Page googlePopup = pages[pages.Length - 1];
            await googlePopup.WaitForSelectorAsync("input.whsOnd.zHQkBf");

            Console.WriteLine("Your account was created with Google, so please enter your Google credentials:");
            string[] googleCredentials = passwordManager.CollectCredentials("Google");
            await googlePopup.TypeAsync("[type=\"email\"]", googleCredentials[0]);
        }
    }
}
