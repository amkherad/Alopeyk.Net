---
title: AlopeykClient
category: AlopeykClient
---

## AlopeykClient
You would access APIs through `IAlopeykClient` interface which is implemented in `AlopeykClient` class.

Here's a list of methods available:
1. [GetLocation](/library/AlopeykClient/GetLocation)
1. [GetLocationSuggestions](/library/AlopeykClient/GetLocationSuggestions)
1. [GetPrice](/library/AlopeykClient/GetPrice)
1. [GetPrices](/library/AlopeykClient/GetPrices)
1. [InsertOrder](/library/AlopeykClient/InsertOrder)
1. [GetOrderDetails](/library/AlopeykClient/GetOrderDetails)
1. [UpdateOrder](/library/AlopeykClient/UpdateOrder)
1. [CancelOrder](/library/AlopeykClient/CancelOrder)
1. [RateOrder](/library/AlopeykClient/RateOrder)
1. [AddHiddenDescription](/library/AlopeykClient/AddHiddenDescription)
1. [DeleteHiddenDescription](/library/AlopeykClient/DeleteHiddenDescription)
1. [GetLiveMapLink](/library/AlopeykClient/GetLiveMapLink)


### AlopeykClient takes 5 parameters
1. The first one which is required (i.e. `remoteServiceUri`) is Alopeyks' uri which is `https://api.alopeyk.com/api/`
for `Production` environment, and `https://sandbox-api.alopeyk.com/api/` for `Development`.
2. Second is `token` (required), which is a JWT token for authorization.
3. `httpClient` (optional) takes a *`HttpClient`* to allow you to control it's lifetime so you could use *`IHttpClientFactory`* to create an instance of *`HttpClient`*.
(more available at [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net-core))
4. `jsonSerializer` (required) is taken to allow you to use *`Json.Net`* or *`System.Text.Json`*.
5. `retryHandler` (optional) is used to provide retry capability.
