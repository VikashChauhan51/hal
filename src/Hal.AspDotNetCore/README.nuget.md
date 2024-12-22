# VHal.AspNetCore

VHal.AspNetCore provides ASP.NET Core extensions for implementing Hypertext Application Language (HAL) in web APIs. This package integrates seamlessly with Hal.Core to offer a comprehensive solution for HAL-compliant API development.

## Features

- **ASP.NET Core Integration**: Smooth integration with ASP.NET Core, simplifying the creation of HAL resources.
- **Request Handling**: Efficient handling of requests and responses within the HAL framework.
- **Resource Link Management**: Streamlined link management for resource collections and individual resources.

## Installation

Install the package via NuGet:

```bash
dotnet add package VHal.AspNetCore
```

## Usage

Here's how to use VHal.AspNetCore in your ASP.NET Core application:

```csharp
using VHal.AspNetCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILinkGenerator linkGenerator;

    public WeatherForecastController(ILinkGenerator linkGenerator)
    {
        this.linkGenerator = linkGenerator;
    }

    [HttpGet]
    public IActionResult GetWeatherForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();

        var response = new ResourceCollectionBuilder<WeatherForecast, string>(forecast, "meta")
            .AddLink(linkBuilder =>
                linkBuilder
                .SetRel("self")
                .SetMethod(HttpVerbs.Get)
                .SetHref(linkGenerator.GenerateUri("GetWeatherForecastWithMeta", new { }))
                .Build())
            .AddLink(linkBuilder =>
                linkBuilder
                .SetRel("all")
                .SetMethod(HttpVerbs.Get)
                .SetHref(linkGenerator.GenerateUri("GetWeatherForecast", new { }))
                .Build())
            .Build();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetWeatherForecastById(int id)
    {
        var item = new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        );

        var response = new ResourceBuilder<WeatherForecast>(item)
            .AddLink("self", linkGenerator.GenerateUri("GetLowest", new { }), HttpVerbs.Get)
            .AddLink("all", linkGenerator.GenerateUri("GetWeatherForecast", new { }), HttpVerbs.Get)
            .Build();

        return Ok(response);
    }
}
```


## License

This project is licensed under the MIT License.

