using System.Text;

namespace Cate.Http.Content
{
    public class StringBody : System.Net.Http.StringContent
    {
        public string Body { get; }

        public StringBody(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType) {
            Body = content;
        }
    }
}
