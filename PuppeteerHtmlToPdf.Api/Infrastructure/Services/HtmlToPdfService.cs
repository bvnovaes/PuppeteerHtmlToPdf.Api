using PuppeteerHtmlToPdf.Api.Core.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace PuppeteerHtmlToPdf.Api.Infrastructure.Services
{

    public class HtmlToPdfService(IBrowserFetcher browserFetcher) : IHtmlToPdfService
    {
        private readonly IBrowserFetcher _browserFetcher = browserFetcher;

        public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
        {
            await _browserFetcher.DownloadAsync();
            using var browser = await PuppeteerSharp.Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            GC.Collect();
            
            return await page.PdfDataAsync(new PdfOptions { Format = PaperFormat.A4 });
        }
    }
}
