using System.IO;

namespace Alopeyk.Net
{
    public interface IJsonSerializer
    {
        string Serialize(
            object @object
        );

        void Serialize(
            Stream stream,
            object @object
        );

        T Deserialize<T>(
            Stream stream
        );

        T Deserialize<T>(
            string @string
        );
    }
}