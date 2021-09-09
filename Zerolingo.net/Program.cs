using System;
using PuppeteerSharp;
using System.Threading.Tasks;
using System.Threading;
using Zerolingo;

namespace Zerolingo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DownloadController.DownloadDefaultAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Timeout = 10000000,
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


            await page.WaitForSelectorAsync("[data-test=have-account]");
            await page.ClickAsync("div._3uMJF");

            Console.WriteLine("Logging in... Please enter your Duolingo login credentials:");
            string[] credentials = passwordManager.CollectCredentials("Duolingo");

            Console.WriteLine("Loading webpage...");
            await page.WaitForSelectorAsync("input._3MNft.fs-exclude");

            
            await page.TypeAsync("[data-test=\"email-input\"]", credentials[0]);
            await page.TypeAsync("[data-test=\"password-input\"]", credentials[1]);

            await page.ClickAsync("button._1rl91._3HhhB._2NolF._275sd._1ZefG._2oW4v");

            // "Continue with Google" button
            await page.WaitForSelectorAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");
            await page.ClickAsync("button._3HhhB._2NolF._275sd._1ZefG._2Dar-._2zhZF");

            page.Popup += new EventHandler<PopupEventArgs>(LoginWithGoogle); // Needs to be awaited somehow to prevent program exiting prematurely

            Thread.Sleep(30000);
            
        }
        static async void LoginWithGoogle(object sender, PopupEventArgs e)
        {
            Console.WriteLine("Popup appeared");
            PasswordManager passwordManager = new PasswordManager();

            Page googlePopup = e.PopupPage;
            await googlePopup.WaitForSelectorAsync("input.whsOnd.zHQkBf");

            Console.WriteLine("Your account was created with Google, so please enter your Google credentials:");
            string[] googleCredentials = passwordManager.CollectCredentials("Google");

            await googlePopup.TypeAsync("[type=\"email\"]", googleCredentials[0]);
            await googlePopup.ClickAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.qIypjc.TrZEUc.lw1w4b");

            // Handle incorrect email/password

            await googlePopup.WaitForSelectorAsync("[type=\"password\"]");
            await googlePopup.TypeAsync("[type=\"password\"]", googleCredentials[1]);
            await googlePopup.ClickAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.qIypjc.TrZEUc.lw1w4b");

            StartStories();
        }
        static async Task StartStories()
        {

        }
    }
}
