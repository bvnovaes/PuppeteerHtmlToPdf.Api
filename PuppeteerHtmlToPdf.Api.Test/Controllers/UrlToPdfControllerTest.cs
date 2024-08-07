using Microsoft.AspNetCore.Mvc;
using Moq;
using PuppeteerHtmlToPdf.Api.Controllers;
using PuppeteerHtmlToPdf.Api.Core.Interfaces;

public class UrlToPdfControllerTests
{
    private readonly Mock<IUrlToPdfService> _mockUrlToPdfService;
    private readonly UrlToPdfController _controller;

    public UrlToPdfControllerTests()
    {
        _mockUrlToPdfService = new Mock<IUrlToPdfService>();
        _controller = new UrlToPdfController(_mockUrlToPdfService.Object);
    }

    [Fact]
    public async Task ConvertUrlToPdf_ReturnsFileResult_WhenHtmlContentIsValid()
    {
        // Arrange
        var url = "https://www.google.com";
        var pdfBytes = new byte[] { 1, 2, 3, 4, 5 };
        _mockUrlToPdfService.Setup(service => service.ConvertUrlToPdfAsync(url)).ReturnsAsync(pdfBytes);

        // Act
        var result = await _controller.ConvertUrlToPdf(url);

        // Assert
        var fileResult = Assert.IsType<FileContentResult>(result);
        Assert.Equal("application/pdf", fileResult.ContentType);
        Assert.Equal(pdfBytes, fileResult.FileContents);
    }

    [Fact]
    public async Task ConvertUrlToPdf_ReturnsProblem_WhenExceptionIsThrown()
    {
        // Arrange
        var htmlContent = "https://www.google.com";
        _mockUrlToPdfService.Setup(service => service.ConvertUrlToPdfAsync(htmlContent)).ThrowsAsync(new System.Exception("Erro de conversão"));

        // Act
        var result = await _controller.ConvertUrlToPdf(htmlContent);

        // Assert
        var problemResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, problemResult.StatusCode);
    }
}