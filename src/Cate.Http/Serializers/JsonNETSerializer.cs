using System.IO;
using Newtonsoft.Json;

namespace Cate.Http.Serializers
{
    public class JsonNETSerializer : ISerializer
    {
        public T Deserialize<T>(string s) => JsonConvert.DeserializeObject<T>(s);

        public T Deserialize<T>(Stream s)
        {
            using (var sr = new StreamReader(s))
            using (var tr = new JsonTextReader(sr)) {
                return JsonSerializer.CreateDefault().Deserialize<T>(tr);
            }
        }

        public string Serialize<T>(T data) => JsonConvert.SerializeObject(data);
    }
}