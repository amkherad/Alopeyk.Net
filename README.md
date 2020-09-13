# Alopeyk.Net

A strong typed client for https://alopeyk.com/ API with async support.

A more detailed documentation is available at [amkherad.github.io/Alopeyk.Net](https://amkherad.github.io/Alopeyk.Net/)


## Installation
* Packages are available on [nuget.org](https://www.nuget.org/packages/Alopeyk.Net/), installation instructions available on
nugets' website.
    ```bash
    dotnet add package Alopeyk.Net
    ```
* If you'd like to have the code instead of a pre-build library, you could use gits' [submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules) ability.

   Just type this command where you want to include the submodule:
    ```bash
    git submodule add git@github.com:amkherad/Alopeyk.Net.git --name Alopeyk.Net
    ```

### How to use
You just need an instance of `AlopeykClient`, you could register it in your dependency container and inject `IAlopeykClient` interface.
```csharp
services.AddScoped<IAlopeykClient, AlopeykClient>();
```

### Integration with Asp.Net
`Alopeyk.Net.AspNet` was made to allow integration with Asp.Net, you could simply call
`services.AddAlopeyk()`

```csharp
services.AddAlopeyk(config => {
    config.Environment = AlopeykEnvironments.Sandbox;
    config.Timeout = TimeSpan.FromSeconds(20);
    config.Token = "XXX";
    config.JsonSerializer = new AlopeykJsonNetJsonSerializer();
    config.AddRetry()
        .AddDelay(TimeSpan.FromSeconds(5))
        .SetRetryCount(2);
});
```