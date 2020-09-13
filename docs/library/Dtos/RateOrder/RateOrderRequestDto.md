---
title: RateOrderRequestDto
categories: [Dto, RateOrderRequestDto]
sort: 1
---

```csharp
public class RateOrderRequestDto
{
    public string OrderId { get; set; }
    
    /// <summary>
    /// Your Rating on the order. Must be an integer in the range of 1-5.
    /// </summary>
    public int Rate { get; set; }
    
    /// <summary>
    /// Your comment on the order. Must be of type text.
    /// </summary>
    public string Comment { get; set; }
}
```