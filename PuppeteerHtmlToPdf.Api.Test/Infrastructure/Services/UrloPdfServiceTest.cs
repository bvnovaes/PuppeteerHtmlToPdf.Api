using Moq;
using PuppeteerHtmlToPdf.Api.Core.Interfaces;

namespace PuppeteerHtmlToPdf.Api.Test.Infrastructure.Services
{
    public class UrlToPdfServiceTest
    {
        [Fact]
        public async Task ConvertUrlToPdf_ShouldReturnPdfBytes()
        {
            // Arrange
            var mockConverter = new Mock<IUrlToPdfService>();
            var expectedPdfBytes = new byte[] { 1, 2, 3 };
            var htmlContent = "https://www.google.com";

            mockConverter.Setup(c => c.ConvertUrlToPdfAsync(It.IsAny<string>())).ReturnsAsync(expectedPdfBytes);

            // Act
            var htmlConverter = mockConverter.Object;
            var pdfBytes = await htmlConverter.ConvertUrlToPdfAsync(htmlContent);

            // Assert
            Assert.NotNull(pdfBytes);
            Assert.Equal(expectedPdfBytes, pdfBytes);
        }
    }
}
