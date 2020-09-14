---
title: RateOrder
category: AlopeykClient
tags: 
sort: 9
---

## RateOrder
> When an order is in its final status (delivered or returned due to the order’s has_return attribute),
you can call this endpoint, to fill the rate and the comment attributes.


Task<**BaseResponseDto<[RateOrderResponseDto]({{site.libraryurl}}/Dtos/RateOrder/RateOrderResponseDto.html)>**> RateOrder (  
&nbsp;&nbsp;&nbsp;&nbsp;[RateOrderRequestDto]({{site.libraryurl}}/Dtos/RateOrder/RateOrderRequestDto.html) request,  
&nbsp;&nbsp;&nbsp;&nbsp;**CancellationToken** cancellationToken  
)
