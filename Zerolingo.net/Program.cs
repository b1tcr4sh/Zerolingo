using System;
using PuppeteerSharp;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
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
            if (await page.WaitForSelectorAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF") != null) {
                
                await page.ClickAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");
                await page.WaitForSelectorAsync("div._3E4oM._3jIW4._3iLdv._2d3xe"); // "Learn" Button: To wait for login to complete before attempting to navigate to story.

                await StartStories();
            } else {
                await StartStories();
            }
        }
        
        static async Task StartStories()
        {
            // Navigate to stories page and begin story grinding
            Page storiesPage = await browser.NewPageAsync();
        
            await storiesPage.GoToAsync("https://www.duolingo.com/stories/es-en-buenos-dias?mode=read", new NavigationOptions {Timeout = 0});

            ElementHandle title = await storiesPage.WaitForSelectorAsync("div.saQLX", new WaitForSelectorOptions {Timeout = 0});
            JSHandle titleText = await title.GetPropertyAsync("textContent");   
            Console.WriteLine("Beginning grinding on \"{0}\"", await titleText.JsonValueAsync());

            ElementHandle startButton = await storiesPage.WaitForSelectorAsync("[data-test=\"story-start\"]");
            await startButton.ClickAsync();


            // Story has been entered/started
            ElementHandle continueButton = await storiesPage.WaitForSelectorAsync("[data-test=\"stories-player-continue\"]");

            while (storiesPage.QuerySelectorAsync("[data-test=\"stories-player-continue\"]") != null) {
                await continueButton.ClickAsync();

                if (await storiesPage.QuerySelectorAsync("[data-test=\"stories-choice\"]") != null) {
                    ElementHandle[] choices = await storiesPage.QuerySelectorAllAsync("[data-test=\"stories-choice\"]");

                    foreach (ElementHandle element in choices) {
                        await element.ClickAsync();
                    } 
                } else if (await storiesPage.QuerySelectorAsync("[data-test=\"challenge-tap-token\"]") != null) {
                    ElementHandle[] choices = await storiesPage.QuerySelectorAllAsync("[data-test=\"challenge-tap-token\"]");

                    foreach (ElementHandle element in choices) {
                        await element.ClickAsync();
                    } 
                } else if (await storiesPage.QuerySelectorAsync("[data-test=\"stories-token\"]") != null) {
                    ElementHandle[] tokens = await storiesPage.QuerySelectorAllAsync("[data-test=\"stories-token\"]");
                    Random rng = new Random();

                    while (await storiesPage.QuerySelectorAsync("span._3Y29z._176_d._2jNpf") == null) {
                        ElementHandle[] disabledTokens = await storiesPage.QuerySelectorAllAsync("[disabled=\"\"");

                        rng.Shuffle<ElementHandle>(tokens);
                        foreach (ElementHandle element in tokens) {
                            foreach (ElementHandle disabledToken in disabledTokens) {
                                int index = Array.IndexOf(tokens, disabledToken);
                                tokens.Where(val => val != disabledToken).ToArray();
                            }
                            await element.ClickAsync();
                        }
                    }
                }
            } 
        }
    }
}
