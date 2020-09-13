---
title: GetPriceResponseDto
categories: [Dto, GetPriceResponseDto]
sort: 2
---

```csharp
public class GetPriceResponseDto
{
    /// <summary>
    /// Indicates that the calculation progress has been successful or not.
    /// </summary>
    public AlopeykOrderStates Status { get; set; }
    
    /// <summary>
    /// Total calculated price.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Indicates payment type of the order (payment by credit or cash). Will be true only if you have enough credit on
    /// your account for that order. In the case of credit deficiency, or cashed=true, this attribute will be set to false.
    /// </summary>
    public bool Credit { get; set; }
    
    /// <summary>
    /// Estimated distance between the source and the destination.
    /// </summary>
    public double Distance { get; set; }
    
    /// <summary>
    /// Estimated duration of the path between the source and the destination.
    /// </summary>
    public double Duration { get; set; }
    
    /// <summary>
    /// Your current credit (in Tomans).
    /// </summary>
    public decimal UserCredit { get; set; }
    
    /// <summary>
    /// Calculated price for the order, in case of has_return=true.
    /// </summary>
    public decimal PriceWithReturn { get; set; }
    
    
    public double Delay { get; set; }
    
    public string City { get; set; }
    
    public string CityFa { get; set; }
    
    public AlopeykTransportTypes TransportType { get; set; }
    
    public bool HasReturn { get; set; }
    
    public bool Cached { get; set; }
    
    public int Score { get; set; }
    
    public Dictionary<string, double> ScoreDetail { get; set; }
    
    public decimal FinalPrice { get; set; }
    
    public decimal Discount { get; set; }
    
    public string [] DiscountCoupons { get; set; }
    
    public string[] InvalidDiscountCoupons { get; set; }
    
    public decimal FailedFinalPrice { get; set; }
    
    public decimal FailedDiscount { get; set; }
    
    public string[] FailedDiscountCoupons { get; set; }
    
    public bool Scheduled { get; set; }
}
```