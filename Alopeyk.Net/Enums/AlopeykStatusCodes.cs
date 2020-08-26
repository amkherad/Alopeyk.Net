namespace Alopeyk.Net.Enums
{
    public enum AlopeykStatusCodes
    {
        Success = 0,
        Failure = -1,
        
        UnknownError = Failure,
        
        ForbiddenError = 403,
        InvalidRequestError = 400,
        OrderNotFoundError = 404,
        OrderCancelledError = 406
    }
}