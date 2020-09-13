---
title: GetPriceAddressRequestDto
categories: [Dto, GetPriceAddressRequestDto]
sort: 4
---

```csharp
public class GetPriceAddressRequestDto
{
    /// <summary>
    /// The type of the location (origin/destination).
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// The longitude of the location.
    /// </summary>
    public double Longitude { get; set; }
    
    /// <summary>
    /// The latitude of the location.
    /// </summary>
    public double Latitude { get; set; }
}
```