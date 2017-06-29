using System.Text;

namespace Cate.Http.Content
{
    public class JsonBody : StringBody
    {
        public JsonBody(string content) : base(content, Encoding.UTF8, "application/json") { }
    }
}
