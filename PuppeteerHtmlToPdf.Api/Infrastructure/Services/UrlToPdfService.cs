using PuppeteerHtmlToPdf.Api.Core.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PuppeteerHtmlToPdf.Api.Infrastructure.Services
{

    public class UrlToPdfService(IBrowserFetcher browserFetcher) : IUrlToPdfService
    {
        private readonly IBrowserFetcher _browserFetcher = browserFetcher;

        public async Task<byte[]> ConvertUrlToPdfAsync(string url)
        {
            await _browserFetcher.DownloadAsync();
            using var browser = await PuppeteerSharp.Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            using var page = await browser.NewPageAsync();
            await page.GoToAsync(url);

            GC.Collect();

            return await page.PdfDataAsync(new PdfOptions { Format = PaperFormat.A4 });
        }
    }
}

