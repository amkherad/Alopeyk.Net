---
title: InsertOrderAddressResponseDto
categories: [Dto, InsertOrderAddressResponseDto]
sort: 6
---

```csharp
public class InsertOrderAddressResponseDto
{
    public int id { get; set; } 
    public int order_id { get; set; } 
    public int customer_id { get; set; } 
    public object courier_id { get; set; } 
    public double lat { get; set; } 
    public double lng { get; set; } 
    public string type { get; set; } 
    public int priority { get; set; } 
    public string city { get; set; } 
    public string status { get; set; } 
    public string address { get; set; } 
    public string description { get; set; } 
    public string unit { get; set; } 
    public string number { get; set; } 
    public string person_fullname { get; set; } 
    public string person_phone { get; set; } 
    public object signed_by { get; set; } 
    public object distance { get; set; } 
    public object google_distance { get; set; } 
    public object duration { get; set; } 
    public object google_duration { get; set; } 
    public object arrived_at { get; set; } 
    public object handled_at { get; set; } 
    public object arrive_lat { get; set; } 
    public object arrive_lng { get; set; } 
    public object handle_lat { get; set; } 
    public object handle_lng { get; set; } 
    public DateTime created_at { get; set; } 
    public DateTime updated_at { get; set; } 
    public object deleted_at { get; set; } 
    public object signature { get; set; } 
    public string city_fa { get; set; }
}
```