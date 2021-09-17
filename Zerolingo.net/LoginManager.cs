using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PuppeteerSharp;

namespace Zerolingo
{
    class LoginManager
    {
        public string[] CollectCredentials(string service)
        {
            string[] credentials = new string[2];


            Console.Write($"{service} Username > ");
            credentials[0] = Console.ReadLine();

            Console.Write($"{service} Password > ");
            credentials[1] = Console.ReadLine();


            return credentials;
        }
        public async void LoginWithGoogle(object sender, PopupEventArgs e)
        {
            Console.WriteLine("\"Continue With Google\" Popup appeared");
            LoginManager passwordManager = new LoginManager();

            Page googlePopup = e.PopupPage;
            await googlePopup.WaitForSelectorAsync("input.whsOnd.zHQkBf");

            Console.WriteLine("Your account was created with Google, so please enter your Google credentials:");
            string[] googleCredentials = passwordManager.CollectCredentials("Google");

            await googlePopup.TypeAsync("[type=\"email\"]", googleCredentials[0]);
            await googlePopup.ClickAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.qIypjc.TrZEUc.lw1w4b");

            // TODO: Handle incorrect email/password

            Thread.Sleep(3000);
            await googlePopup.WaitForSelectorAsync("div.Xb9hP");
            await googlePopup.TypeAsync("[type=\"password\"]", googleCredentials[1]);
            await googlePopup.ClickAsync("button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc.qIypjc.TrZEUc.lw1w4b");

        }
    }
}
