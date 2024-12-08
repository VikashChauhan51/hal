using Hal.AspDotNetCore;
using Hal.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Sample.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IHalLinkGenerator linkGenerator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHalLinkGenerator linkGenerator)
    {
        _logger = logger;
        this.linkGenerator = linkGenerator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        var forecast= Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        var item =forecast.First();
        var ddd = new Resource<WeatherForecast>(item);

        ddd.AddLink(new Link
        {
            Href = linkGenerator.GenerateUri("GetWeatherForecast", new { }),
            Rel = "self",
            Method = HttpVerbs.Get
        });


        IResourceCollection<WeatherForecast> response = new ResourceCollection<WeatherForecast>(forecast);
        response.AddLink(new Link
        {
            Href = linkGenerator.GenerateUri("GetWeatherForecast", new { }),
            Rel = "self",
            Method = HttpVerbs.Get
        });

        return Ok(ddd);
    }
}
