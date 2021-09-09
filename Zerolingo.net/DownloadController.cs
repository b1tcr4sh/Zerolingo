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
            Console.WriteLine($"Downloading Chromium/{BrowserFetcher.DefaultRevision}:");
            BrowserFetcher fetcher = new BrowserFetcher();

            fetcher.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DisplayDownloadProgress);

            await fetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
        }
        static void DisplayDownloadProgress(object sender, DownloadProgressChangedEventArgs e) {
            // Console.WriteLine(e);

            Console.Write("\rDownloaded {0} of {1} bytes. {2} % complete...",
            e.BytesReceived,
            e.TotalBytesToReceive,
            e.ProgressPercentage);
        }
    }
}