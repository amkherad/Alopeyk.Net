---
title: GetLiveMapLink
category: AlopeykClient
tags: 
sort: 12
---

## GetLiveMapLink
> Once you successfully have created an order, you will be able to watch the courier on a live map.  
> At Sandbox environment, courier location is static and order status will change every 30 seconds.
> But at Production environment, the courier location and order status based on reality will be change.
> 
> You can access tracking URL (tracking_url) trough Webhook data.
> Even you can manually create this URL by concatenation Order Token (‘order_token’) which is accessible in Order details method and the tracking base URL
> 
> https://sandbox-tracking.alopeyk.com/#/<order_token>
> 
> For Orders which their status are new, you can’t access tracking URL
> For customizing some elements, you can pass these variables on a URL by GET method


Task<**BaseResponseDto< string >**> GetLiveMapLink (  
&nbsp;&nbsp;&nbsp;&nbsp;[GetLiveMapLinkRequestDto]({{site.libraryurl}}/Dtos/GetLiveMapLink/GetLiveMapLinkRequestDto.html) request,  
&nbsp;&nbsp;&nbsp;&nbsp;**CancellationToken** cancellationToken  
)
