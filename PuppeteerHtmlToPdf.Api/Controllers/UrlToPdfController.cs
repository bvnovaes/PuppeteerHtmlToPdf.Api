using Microsoft.AspNetCore.Mvc;
using PuppeteerHtmlToPdf.Api.Core.Interfaces;

namespace PuppeteerHtmlToPdf.Api.Controllers
{
    [ApiController]
    [Route("/v1/urlparapdf")]
    public class UrlToPdfController(IUrlToPdfService urlToPdfService) : ControllerBase
    {
        private readonly IUrlToPdfService _urlToPdfService = urlToPdfService;

        [HttpPost]
        [Route("converter")]
        public async Task<IActionResult> ConvertUrlToPdf([FromBody] string htmlContent)
        {
            try
            {
                var pdfBytes = await _urlToPdfService.ConvertUrlToPdfAsync(htmlContent);

                return File(pdfBytes, "application/pdf");
            }
            catch
            {
                return Problem(htmlContent);
            }
        }
    }
}
