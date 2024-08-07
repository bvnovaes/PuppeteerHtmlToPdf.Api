using Moq;
using PuppeteerHtmlToPdf.Api.Core.Interfaces;

namespace PuppeteerHtmlToPdf.Api.Test.Infrastructure.Services
{
    public class HtmlToPdfServiceTest
    {
        [Fact]
        public async Task ConvertUrlToPdf_ShouldReturnPdfBytes()
        {
            // Arrange
            var mockConverter = new Mock<IHtmlToPdfService>();
            var expectedPdfBytes = new byte[] { 1, 2, 3 };
            var htmlContent = "<html><body><h1>Hello, world!</h1></body></html>";

            mockConverter.Setup(c => c.ConvertHtmlToPdfAsync(It.IsAny<string>())).ReturnsAsync(expectedPdfBytes);

            // Act
            var htmlConverter = mockConverter.Object;
            var pdfBytes = await htmlConverter.ConvertHtmlToPdfAsync(htmlContent);

            // Assert
            Assert.NotNull(pdfBytes);
            Assert.Equal(expectedPdfBytes, pdfBytes);
        }
    }
}
