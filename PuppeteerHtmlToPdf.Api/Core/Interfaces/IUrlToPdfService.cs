namespace PuppeteerHtmlToPdf.Api.Core.Interfaces
{
    public interface IUrlToPdfService
    {
        Task<byte[]> ConvertUrlToPdfAsync(string url);
    }
}
