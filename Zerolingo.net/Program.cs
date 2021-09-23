using System;
using PuppeteerSharp;
using System.Threading.Tasks;
using System.Threading;
using Zerolingo;

namespace Zerolingo
{
    class Program
    {
        public static Browser browser;

        static async Task Main(string[] args)
        {
            await DownloadController.DownloadDefaultAsync();

            browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
#if DEBUG
                Headless = false,
#endif
                Timeout = 0
            });

            Console.WriteLine($"Downloaded version {await browser.GetVersionAsync()}");

            var page = await browser.NewPageAsync();
            page.DefaultTimeout = 100000;

            Console.WriteLine("Navigating To https://duolingo.com...  Depending on your connection, this may take a moment");
            await page.GoToAsync("https://duolingo.com", new NavigationOptions {Timeout = 0});

            // page.Close += new EventHandler(StartStories);

            await login(page);
        }
        public static async Task login(Page page)
        {
            LoginManager passwordManager = new LoginManager();
            page.Popup += new EventHandler<PopupEventArgs>(passwordManager.LoginWithGoogle); 


            await page.WaitForSelectorAsync("[data-test=have-account]");
            await page.ClickAsync("div._3uMJF");

            Console.WriteLine("Loading Complete!  Please enter your Duolingo login credentials:");
            await passwordManager.LoginToDuolingo(page);

            // Check for "Continue with Google" button
            if (await page.QuerySelectorAsync("_3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF") != null) {
                // await page.WaitForSelectorAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");

                await page.WaitForSelectorAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");
                await page.ClickAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");
            }


            await page.WaitForSelectorAsync("div._3E4oM._3jIW4._3iLdv");
            // await page.CloseAsync();
            await StartStories();
        }
        
        static async Task StartStories()
        {
            // Navigate to stories page and begin story grinding
            Page storiesPage = await browser.NewPageAsync();
        
            await storiesPage.GoToAsync("https://duolingo.com/stories");
            Console.WriteLine("Arrived at https://duolingo.com/stories");
        }
    }
}
