using Alopeyk.Net.Enums;

namespace Alopeyk.Net.Dto
{
    /// <summary>
    /// Base response model for every API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponseDto<T>
    {   
        /// <summary>
        /// Currently you can send up to 100 requests every minute. You will be able to check the current quotas on every response header.
        /// </summary>
        public string MinuteRateLimitIdentifier { get; set; }
        
        /// <summary>
        /// Currently you can send up to 100 requests every minute. You will be able to check the current quotas on every response header.
        /// </summary>
        public int MinuteRateLimitLimit { get; set; }
        
        /// <summary>
        /// Currently you can send up to 100 requests every minute. You will be able to check the current quotas on every response header.
        /// </summary>
        public int MinuteRateLimitRemaining { get; set; }
        
        
        /// <summary>
        /// Currently you can send up to 43200 requests every day. You will be able to check the current quotas on every response header.
        /// </summary>
        public string DailyRateLimitIdentifier { get; set; }
        
        /// <summary>
        /// Currently you can send up to 43200 requests every day. You will be able to check the current quotas on every response header.
        /// </summary>
        public int DailyRateLimitLimit { get; set; }
        
        /// <summary>
        /// Currently you can send up to 43200 requests every day. You will be able to check the current quotas on every response header.
        /// </summary>
        public int DailyRateLimitRemaining { get; set; }
        
        
        public AlopeykStatusCodes Status { get; set; }
        
        public string Message { get; set; }
        
        public T Object { get; set; } 
    }
}