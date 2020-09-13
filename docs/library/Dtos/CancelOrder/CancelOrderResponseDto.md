---
title: CancelOrderResponseDto
categories: [Dto, CancelOrderResponseDto]
sort: 2
---

```csharp
public class CancelOrderResponseDto
{
    public int Id { get; set; }
    
    public AlopeykOrderStates Status { get; set; }
    
    public int? CourierId { get; set; }
    
    public int CustomerId { get; set; }
    
    public ResourceDescriptorDto Signature { get; set; }
    
    public string OrderToken { get; set; }
    
    public decimal? NPrice { get; set; }
    
    public decimal? Subsidy { get; set; }
    
    public string SignedBy { get; set; }
    
    public decimal? FinalPrice { get; set; }
}
```