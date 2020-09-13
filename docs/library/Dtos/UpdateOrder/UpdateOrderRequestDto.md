---
title: UpdateOrderRequestDto
categories: [Dto, UpdateOrderRequestDto]
sort: 1
---

```csharp
public class UpdateOrderRequestDto
{
    public string OrderId { get; set; }


    public AlopeykTransportTypes TransportType { get; set; }

    public bool HasReturn { get; set; }

    public bool Cashed { get; set; }

    public bool Credit { get; set; }

    public bool PayAtDest { get; set; }

    public int Delay { get; set; }
}
```