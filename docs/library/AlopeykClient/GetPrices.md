---
title: GetPrices
category: AlopeykClient
tags: 
sort: 4
---

## GetPrices
> Batch Price  
> This endpoint is the same as Normal Price But the difference is you can calculate up to 15 pairs of Normal Price in one request.


Task<**BaseResponseDto< IEnumerable<[GetPriceResponseDto](/library/Dtos/GetPrice/GetPriceResponseDto.html)> >**> GetPrice (  
&nbsp;&nbsp;&nbsp;&nbsp;IEnumerable<[GetPriceRequestDto](/library/Dtos/GetPrice/GetPriceRequestDto.html)> request,  
&nbsp;&nbsp;&nbsp;&nbsp;**CancellationToken** cancellationToken  
)
