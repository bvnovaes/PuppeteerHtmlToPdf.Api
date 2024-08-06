using Microsoft.AspNetCore.Mvc;
using PuppeteerHtmlToPdf.Api.Core.Interfaces;

namespace PuppeteerHtmlToPdf.Api.Controllers
{
    [ApiController]
    [Route("/v1/htmlparapdf")]
    public class HtmlToPdfController(IHtmlToPdfService htmlToPdfService) : ControllerBase
    {
        private readonly IHtmlToPdfService _htmlToPdfService = htmlToPdfService;

        [HttpPost]
        [Route("converter")]
        public async Task<IActionResult> ConvertHtmlToPdf([FromBody] string htmlContent)
        {
            try
            {
                var pdfBytes = await _htmlToPdfService.ConvertHtmlToPdfAsync(htmlContent);

                return File(pdfBytes, "application/pdf");
            }
            catch
            {
                return Problem(htmlContent);
            }
        }
    }
}
