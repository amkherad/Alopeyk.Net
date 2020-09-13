---
title: GetPriceLocationInfoDto
categories: [Dto, GetPriceLocationInfoDto]
sort: 3
---

```csharp
public class GetPriceLocationInfoDto
{
    public GetPriceAddressRequestDto Origin { get; set; }
    
    public IEnumerable<GetPriceAddressRequestDto> Destinations { get; set; }
}
```