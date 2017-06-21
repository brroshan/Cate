using System.IO;
using Newtonsoft.Json;

namespace Cate.Http.Serializers
{
    public class JsonNETSerializer : ISerializer
    {
        public object Settings { get; set; }

        public T Deserialize<T>(string s) => JsonConvert.DeserializeObject<T>(s);

        public T Deserialize<T>(Stream s)
        {
            using (var sr = new StreamReader(s))
            using (var tr = new JsonTextReader(sr)) {
                return JsonSerializer
                    .Create(Settings as JsonSerializerSettings).Deserialize<T>(tr);
            }
        }

        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Settings as JsonSerializerSettings);
        }
    }
}