---
title: InsertOrderLocationInfoDto
categories: [Dto, InsertOrderLocationInfoDto]
sort: 3
---

```csharp
public class InsertOrderLocationInfoDto
{
    public InsertOrderAddressRequestDto Origin { get; set; }
    
    public IEnumerable<InsertOrderAddressRequestDto> Destinations { get; set; }
}
```