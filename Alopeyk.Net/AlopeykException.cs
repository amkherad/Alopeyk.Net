using System;
using System.Runtime.Serialization;

namespace Alopeyk.Net
{
    public class AlopeykException : Exception
    {
        public AlopeykException()
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