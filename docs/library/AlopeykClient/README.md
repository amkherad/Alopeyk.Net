---
title: AlopeykClient
category: AlopeykClient
---

## AlopeykClient
You would access APIs through `IAlopeykClient` interface which is implemented in `AlopeykClient` class.

Here's a list of methods available:
1. [GetLocation]({{site.libraryurl}}/AlopeykClient/GetLocation)
1. [GetLocationSuggestions]({{site.libraryurl}}/AlopeykClient/GetLocationSuggestions)
1. [GetPrice]({{site.libraryurl}}/AlopeykClient/GetPrice)
1. [GetPrices]({{site.libraryurl}}/AlopeykClient/GetPrices)
1. [InsertOrder]({{site.libraryurl}}/AlopeykClient/InsertOrder)
1. [GetOrderDetails]({{site.libraryurl}}/AlopeykClient/GetOrderDetails)
1. [UpdateOrder]({{site.libraryurl}}/AlopeykClient/UpdateOrder)
1. [CancelOrder]({{site.libraryurl}}/AlopeykClient/CancelOrder)
1. [RateOrder]({{site.libraryurl}}/AlopeykClient/RateOrder)
1. [AddHiddenDescription]({{site.libraryurl}}/AlopeykClient/AddHiddenDescription)
1. [DeleteHiddenDescription]({{site.libraryurl}}/AlopeykClient/DeleteHiddenDescription)
1. [GetLiveMapLink]({{site.libraryurl}}/AlopeykClient/GetLiveMapLink)


### AlopeykClient takes 5 parameters
1. The first one which is required (i.e. `remoteServiceUri`) is Alopeyks' uri which is `https://api.alopeyk.com/api/`
for `Production` environment, and `https://sandbox-api.alopeyk.com/api/` for `Development`.
2. Second is `token` (required), which is a JWT token for authorization.
3. `httpClient` (optional) takes a *`HttpClient`* to allow you to control it's lifetime so you could use *`IHttpClientFactory`* to create an instance of *`HttpClient`*.
(more available at [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net-core))
4. `jsonSerializer` (required) is taken to allow you to use *`Json.Net`* or *`System.Text.Json`*.
5. `retryHandler` (optional) is used to provide retry capability.
