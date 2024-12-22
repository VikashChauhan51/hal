# Hal.Core

Hal.Core is a robust library designed to streamline the implementation of Hypertext Application Language (HAL) in .NET applications. It provides a foundation for creating HAL-compliant APIs, enhancing the readability and maintainability of your code.

## Features

- **Resource Building**: Simplifies the creation of HAL resources with embedded links.
- **Link Generation**: Easily generate and manage hyperlinks within your resources.
- **Collection Handling**: Manage collections of resources effectively with built-in utilities.
- **Extensibility**: Customizable to fit various use cases.

## Installation

Install the package via NuGet:

```bash
dotnet add package Hal.Core
```

## Usage

Here's a basic example of how to use Hal.Core in your .NET application:

```csharp
using Hal.Core;

var forecast = Enumerable.Range(1, 5).Select(index =>
    new WeatherForecast
    (
        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        Random.Shared.Next(-20, 55),
        summaries[Random.Shared.Next(summaries.Length)]
    ))
    .ToArray();

var response = new ResourceCollectionBuilder<WeatherForecast, string>(forecast, "meta")
    .AddLink(linkGenerator
    .SetRouteName("GetWeatherForecast")
    .Build())
    .Build();
```
