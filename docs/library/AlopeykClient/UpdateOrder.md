---
title: UpdateOrder
category: AlopeykClient
tags: 
sort: 7
---

## UpdateOrder
> For order editions and updates, transport types of the same group can be changed to each other.  
> This means that the transport types of motor orders can not be changed into transport types belonging to car or cargo group.
> 
> motor group => (“motorbike”, “motor_taxi”)  
> car group => (“car”, “car_taxi”)  
> cargo group => (“cargo”, “cargo_s”)  
> If an order has a return policy and courier has started the trip back the origin address.  
> The has_return parameter cannot be turned off. In order to edit the order details, this endpoint can be called:


Task<**BaseResponseDto<[UpdateOrderResponseDto]({{site.libraryurl}}/Dtos/UpdateOrder/UpdateOrderResponseDto.html)>**> UpdateOrder (  
&nbsp;&nbsp;&nbsp;&nbsp;[UpdateOrderRequestDto]({{site.libraryurl}}/Dtos/UpdateOrder/UpdateOrderRequestDto.html) request,  
&nbsp;&nbsp;&nbsp;&nbsp;**CancellationToken** cancellationToken  
)
