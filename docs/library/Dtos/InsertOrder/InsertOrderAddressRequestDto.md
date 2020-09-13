---
title: InsertOrderAddressRequestDto
categories: [Dto, InsertOrderAddressRequestDto]
sort: 5
---

```csharp
public class InsertOrderAddressRequestDto
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
    
    /// <summary>
    /// The description of the address which must be of type text.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Exact unit number of the address.
    /// </summary>
    public string Unit { get; set; }
    
    /// <summary>
    /// Exact building number of the destination address.
    /// </summary>
    public string Number { get; set; }
    
    /// <summary>
    /// Full name of the person receiving the package or the person residing at that address.
    /// </summary>
    public string PersonFullName { get; set; }
    
    //Phone number of the person receiving the package or the person residing at that address.
    public string PersonPhone { get; set; }
}
```