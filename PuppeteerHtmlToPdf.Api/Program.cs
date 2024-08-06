using PuppeteerHtmlToPdf.Api.Core.Interfaces;
using PuppeteerHtmlToPdf.Api.Infrastructure.Services;
using PuppeteerSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBrowserFetcher, BrowserFetcher>();
builder.Services.AddScoped<IHtmlToPdfService, HtmlToPdfService>();
builder.Services.AddScoped<IUrlToPdfService, UrlToPdfService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
