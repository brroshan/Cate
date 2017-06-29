using System.Text;

namespace Cate.Http.Content
{
    public class XmlBody : StringBody
    {
        public XmlBody(string content) : base(content, Encoding.UTF8, "application/xml")
        { }
    }
}
