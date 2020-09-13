---
title: GetLocationSuggestions
category: AlopeykClient
tags: 
sort: 2
---

## GetLocationSuggestions
>This endpoint retrieves suggestions by search input.  
>The result will be an array of suggestions. Each one includes the region and the name of the retrieved place, and offers coordinates for that item.


Task<**BaseResponseDto<[GetLocationSuggestionsResponseDto](/library/Dtos/GetLocationSuggestions/GetLocationSuggestionsResponseDto.html)>**> GetLocationSuggestions (  
&nbsp;&nbsp;&nbsp;&nbsp;[GetLocationSuggestionsRequestDto](/library/Dtos/GetLocationSuggestions/GetLocationSuggestionsRequestDto.html) request,  
&nbsp;&nbsp;&nbsp;&nbsp;***CancellationToken*** cancellationToken  
)
