using System;
using System.Runtime.Serialization;

namespace Alopeyk.Net
{
    public class AlopeykException : Exception
    {
        public string RemoteResponse { get; set; }
        
        public AlopeykException()
            : base("An exception occured when trying to call an alopeyk remote service.")
        {
        }

        protected AlopeykException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        {
        }

        public AlopeykException(
            string message
        )
            : base(message)
        {
        }

        public AlopeykException(
            string message,
            Exception innerException
        )
            : base(message, innerException)
        {
        }
    }
}