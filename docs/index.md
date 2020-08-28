---
title: Alopeyk.Net
---

# Alopeyk.Net Documentation
Alopeyk.Net is a simple wrapper for [alopeyk](https://alopeyk.com/)'s API for .NET with a full async implementation.

* It compliants to [docs.alopeyk.com](https://docs.alopeyk.com/) as of august 2020.
* Comments are copied from the original documentation.

## Installation
* Packages are available on [nuget.org](https://www.nuget.org/packages/Alopeyk.Net/), installation instructions available on
nuget's website.
```bash
dotnet add package Alopeyk.Net
```
* If you'd like to have the code instead of a pre-build library, you could use git's [submodule](https://git-scm.com/book/en/v2/Git-Tools-Submodules) ability.

TL;DR -
Just type this command where you want to include the submodule:
```bash
git submodule add git@github.com:amkherad/Alopeyk.Net.git --name Alopeyk.Net
```

### How to use

### Integration with Asp.Net


### AlopeykClient
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