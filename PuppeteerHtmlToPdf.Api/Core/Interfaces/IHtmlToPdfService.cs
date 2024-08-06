namespace PuppeteerHtmlToPdf.Api.Core.Interfaces
{
    public interface IHtmlToPdfService
    {
        Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent);
    }
}
