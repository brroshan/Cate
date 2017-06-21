using System.IO;

namespace Cate.Http.Serializers
{
    public interface ISerializer
    {
        object Settings { get; set; }

        T Deserialize<T>(string s);
        T Deserialize<T>(Stream s);
        string Serialize<T>(T data);
    }
}