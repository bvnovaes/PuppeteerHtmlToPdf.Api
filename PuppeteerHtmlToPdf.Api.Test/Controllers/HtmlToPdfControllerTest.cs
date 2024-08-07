using Microsoft.AspNetCore.Mvc;
using Moq;
using PuppeteerHtmlToPdf.Api.Controllers;
using PuppeteerHtmlToPdf.Api.Core.Interfaces;

public class HtmlToPdfControllerTests
{
    private readonly Mock<IHtmlToPdfService> _mockHtmlToPdfService;
    private readonly HtmlToPdfController _controller;

    public HtmlToPdfControllerTests()
    {
        _mockHtmlToPdfService = new Mock<IHtmlToPdfService>();
        _controller = new HtmlToPdfController(_mockHtmlToPdfService.Object);
    }

    [Fact]
    public async Task ConvertHtmlToPdf_ReturnsFileResult_WhenHtmlContentIsValid()
    {
        // Arrange
        var htmlContent = "<html><body>Hello, World!</body></html>";
        var pdfBytes = new byte[] { 1, 2, 3, 4, 5 };
        _mockHtmlToPdfService.Setup(service => service.ConvertHtmlToPdfAsync(htmlContent))
                             .ReturnsAsync(pdfBytes);

        // Act
        var result = await _controller.ConvertHtmlToPdf(htmlContent);

        // Assert
        var fileResult = Assert.IsType<FileContentResult>(result);
        Assert.Equal("application/pdf", fileResult.ContentType);
        Assert.Equal(pdfBytes, fileResult.FileContents);
    }

    [Fact]
    public async Task ConvertHtmlToPdf_ReturnsProblem_WhenExceptionIsThrown()
    {
        // Arrange
        var htmlContent = "<html><body>Hello, World!</body></html>";
        _mockHtmlToPdfService.Setup(service => service.ConvertHtmlToPdfAsync(htmlContent))
                             .ThrowsAsync(new System.Exception("Conversion error"));

        // Act
        var result = await _controller.ConvertHtmlToPdf(htmlContent);

        // Assert
        var problemResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, problemResult.StatusCode);
    }
}