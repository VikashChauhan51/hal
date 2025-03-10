using Hal.AspDotNetCore;
using Hal.Core;
using Hal.Core.Builders;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Api.Controllers;
[ApiController]
[Route("[controller]/[Action]")]
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
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();


        var response = new ResourceCollectionBuilder<WeatherForecast>(forecast)
            .AddLink("get", linkGenerator.GenerateLink("GetLowest", new { }), HttpVerbs.Get)
            .AddLink("self", linkGenerator.GenerateLink("GetWeatherForecast", new { }), HttpVerbs.Get)
            .Build();

        return Ok(response);
    }

    [HttpGet(Name = "GetWeatherForecastWithMeta")]
    public IActionResult GetWithMeta()
    {
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();


        var response = new ResourceCollectionBuilder<WeatherForecast, string>(forecast, "meta")
            .AddLink(linkBuilder =>
            linkBuilder
            .SetRel("self")
            .SetMethod(HttpVerbs.Get)
            .SetHref(linkGenerator.GenerateLink("GetWeatherForecastWithMeta", new { }))
            .Build())
            .AddLink(linkBuilder =>
            linkBuilder
            .SetRel("all")
            .SetMethod(HttpVerbs.Get)
            .SetHref(linkGenerator.GenerateLink("GetWeatherForecast", new { }))
            .Build())
            .Build();

        return Ok(response);
    }

    [HttpGet(Name = "GetLowest")]
    public IActionResult GetLowest()
    {
        var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        var item = forecast.MinBy(x => x.TemperatureF);

        var response = new ResourceBuilder<WeatherForecast>(item)
            .AddLink("self", linkGenerator.GenerateLink("GetLowest", new { }), HttpVerbs.Get)
            .AddLink("all", linkGenerator.GenerateLink("GetWeatherForecast", new { }), HttpVerbs.Get)
            .Build();

        return Ok(response);
    }
}
