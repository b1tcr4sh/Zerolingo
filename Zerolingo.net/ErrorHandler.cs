using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace Zerolingo {
    public class ErrorHandler {
        Browser browserMember;
        Page pageMember;

        public delegate void ApplicationExitTriggerEventHandler(object sender, ApplicationExitTriggerEventArgs e);

        public event ApplicationExitTriggerEventHandler ApplicationExitTrigger; 

        public void RegisterEvents(Browser browser, Page page) {
            browserMember = browser;
            pageMember = page;

            browser.Disconnected += new EventHandler(BrowserDisconnected);
            browser.Closed += new EventHandler(BrowserDisconnected);

            page.Error += new EventHandler<ErrorEventArgs>(PageError);
            page.PageError += new EventHandler<PageErrorEventArgs>(PageException);
            page.RequestFailed += new EventHandler<RequestEventArgs>(ConnectionError);

            ApplicationExitTrigger += new ApplicationExitTriggerEventHandler(ExitApplication);
        }
        public async void BrowserDisconnected(Object sender, EventArgs e) {
            Console.WriteLine("Chromium instance was disconnected or closed.  This likely means that it crashed, if this was unintended, please try running the program again.");

            if (browserMember.Process != null) {
                Console.WriteLine("Disposing Chromium Process...");
                await browserMember.DisposeAsync();
                
            }
            ApplicationExitTrigger.Invoke(this, new ApplicationExitTriggerEventArgs(-1));
        }
        public async void PageError(object sender, ErrorEventArgs e) {
            Console.WriteLine("The tab encountered an error: {0}", e.Error);

            await pageMember.DisposeAsync();
        }
        public async void ConnectionError(object sender, RequestEventArgs e) {
            // Console.WriteLine("A request sent by the page failed to connect: {0}: {1}", e.Request.Response.Status, e.Request.Response.StatusText);

            await pageMember.DisposeAsync();
        }
        public void PageException(object sender, PageErrorEventArgs e) {
            Console.WriteLine("The current page has encountered an exception: {0}.  Not terminating process...", e.Message);
        }
        public void ExitApplication(object sender, ApplicationExitTriggerEventArgs e) {
            Console.WriteLine("Exiting Process...");
            
            Environment.Exit(e.ExitCode);
        }


        public class ApplicationExitTriggerEventArgs {
            public ApplicationExitTriggerEventArgs(int exitCode) { exitCode = ExitCode; }
            public int ExitCode { get; }
        }
    }
}