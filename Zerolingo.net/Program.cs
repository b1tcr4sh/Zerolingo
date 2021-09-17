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
                Timeout = 0,
                Headless = false
            });

            Console.WriteLine($"Downloaded version {await browser.GetVersionAsync()}");

            var page = await browser.NewPageAsync();
            page.DefaultTimeout = 100000;

            Console.WriteLine("Navigating To https://duolingo.com...  Depending on your connection, this may take a while");
            await page.GoToAsync("https://duolingo.com", new NavigationOptions {Timeout = 0});

            page.Close += new EventHandler(StartStories);

            await login(page, browser);
        }
        static async Task login(Page page, Browser browser)
        {
            LoginManager passwordManager = new LoginManager();
            page.Popup += new EventHandler<PopupEventArgs>(passwordManager.LoginWithGoogle); 


            await page.WaitForSelectorAsync("[data-test=have-account]");
            await page.ClickAsync("div._3uMJF");

            Console.WriteLine("Loading Complete!  Please enter your Duolingo login credentials:");
            string[] credentials = passwordManager.CollectCredentials("Duolingo");

            Console.WriteLine("Logging In...");
            await page.WaitForSelectorAsync("input._3MNft.fs-exclude");

            
            await page.TypeAsync("[data-test=\"email-input\"]", credentials[0]);
            await page.TypeAsync("[data-test=\"password-input\"]", credentials[1]);

            await page.ClickAsync("button._1rl91._3HhhB._2NolF._275sd._1ZefG._2oW4v");

            // "Continue with Google" button
            await page.WaitForSelectorAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");
            await page.ClickAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");


            await page.WaitForSelectorAsync("div._3E4oM._3jIW4._3iLdv");
            await page.CloseAsync();
        }
        
        static async void StartStories(Object sender ,EventArgs e)
        {
            // Navigate to stories page and begin story grinding
            Page storiesPage = await browser.NewPageAsync();
            


            await storiesPage.GoToAsync("https://duolingo.com/stories");
        }
    }
}
