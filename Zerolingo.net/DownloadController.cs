using System;
using System.Threading.Tasks;
using System.Net;
using Zerolingo;
using PuppeteerSharp;

namespace Zerolingo 
{
    static class DownloadController 
    {
        public static async Task DownloadDefaultAsync() {
            Console.WriteLine($"Downloading Chromium/{BrowserFetcher.DefaultRevision}");
            BrowserFetcher fetcher = new BrowserFetcher();

            await fetcher.DownloadAsync(BrowserFetcher.DefaultRevision);

            fetcher.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DisplayDownloadProgress);
        }
        static void DisplayDownloadProgress(object sender,DownloadProgressChangedEventArgs e) {
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
            (string)e.UserState,
            e.BytesReceived,
            e.TotalBytesToReceive,
            e.ProgressPercentage);
        }
    }
}