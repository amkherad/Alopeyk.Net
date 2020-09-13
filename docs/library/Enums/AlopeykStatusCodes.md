---
title: AlopeykStatusCodes
category: Enums
---

```csharp
public enum AlopeykStatusCodes
{
    Success = 0,
    Fail = Failure,
    
    UnknownError = Failure,
    
    Failure = -1,
    
    ForbiddenError = 403,
    InvalidRequestError = 400,
    OrderNotFoundError = 404,
    OrderCancelledError = 406
}
```