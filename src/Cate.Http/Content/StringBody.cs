using System.Text;
using System.Net.Http;

namespace Cate.Http.Content
{
    public class StringBody : StringContent
    {
        public string Body { get; }

        public StringBody(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType) {
            Body = content;
        }
    }
}
