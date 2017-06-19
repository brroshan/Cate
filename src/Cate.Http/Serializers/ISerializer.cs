using System.IO;

namespace Cate.Http.Serializers
{
    public interface ISerializer
    {
        T Deserialize<T>(string s);
        T Deserialize<T>(Stream s);
        string Serialize<T>(T data);
    }
}