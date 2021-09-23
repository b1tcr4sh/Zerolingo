using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PuppeteerSharp;
using Zerolingo;

namespace Zerolingo
{
    class LoginManager
    {
        public LoginCredentials CollectCredentials(string service)
        {
            LoginCredentials credentials = new LoginCredentials();  

            Console.Write($"{service} Username > ");
            credentials.Username = Console.ReadLine();

            Console.Write($"{service} Password > ");
            credentials.Password = Console.ReadLine();


            return credentials;
        }
        public async Task LoginToDuolingo(Page page) {
            LoginCredentials credentials = CollectCredentials("Duolingo");

            Console.WriteLine("Attempting to Log In...");
            await page.WaitForSelectorAsync("input._3MNft.fs-exclude");


            await page.TypeAsync("[data-test=\"email-input\"]", credentials.Username);
            await page.TypeAsync("[data-test=\"password-input\"]", credentials.Password);

            await page.ClickAsync("button._1rl91._3HhhB._2NolF._275sd._1ZefG._2oW4v");
            
            if (await page.WaitForSelectorAsync("div._1G8OV._14ezr") != null) {
                Console.WriteLine("Incorrect Username or Password Entered. Please try again once the page reloads...");
                
                await page.ReloadAsync(new NavigationOptions {Timeout = 0});
                // await Program.login(page);
            
                await page.WaitForSelectorAsync("[data-test=have-account]");
                await page.ClickAsync("div._3uMJF");
                await LoginToDuolingo(page);
            } else {
                Console.WriteLine("Successfully Logged in!");
            } 


        }
        public async void LoginWithGoogle(object sender, PopupEventArgs e)
        {
            Console.WriteLine("\"Continue With Google\" Popup appeared");
            LoginManager passwordManager = new LoginManager();

            Page googlePopup = e.PopupPage;
            await googlePopup.WaitForSelectorAsync("input.whsOnd.zHQkBf");

            Console.WriteLine("Your account was created with Google, so please enter your Google credentials:");
            LoginCredentials googleCredentials = passwordManager.CollectCredentials("Google");

            await googlePopup.TypeAsync("[type=\"email\"]", googleCredentials.Username);
            await googlePopup.ClickAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.qIypjc.TrZEUc.lw1w4b");

            // TODO: Handle incorrect email/password

            Thread.Sleep(3000);
            await googlePopup.WaitForSelectorAsync("div.Xb9hP");
            await googlePopup.TypeAsync("[type=\"password\"]", googleCredentials.Password);
            await googlePopup.ClickAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.qIypjc.TrZEUc.lw1w4b");

        }
    }
}
