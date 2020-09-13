using System.IO;
using Newtonsoft.Json;

namespace Alopeyk.Net.JsonNet
{
    public class AlopeykJsonNetJsonSerializer : IJsonSerializer
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Serialize(
            object @object
        )
        {
            return JsonConvert.SerializeObject(@object, Settings);
        }

        public void Serialize(
            Stream stream,
            object @object
        )
        {
            var result = JsonConvert.SerializeObject(@object, Settings);

            using (var sw = new StreamWriter(stream))
            {
                sw.Write(result);
            }
        }

        public T Deserialize<T>(
            Stream stream
        )
        {
            string @string;
            using (var sr = new StreamReader(stream))
            {
                @string = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(@string, Settings);
        }

        public T Deserialize<T>(
            string @string
        )
        {
            return JsonConvert.DeserializeObject<T>(@string, Settings);
        }
    }
}