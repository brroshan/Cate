using System.Net;
using System.Net.Http;
using Cate.Http.Content;
using Cate.Http.Core;
using static Cate.Http.Handlers.MockMessageHandler;

namespace Cate.Http.Test
{
    public static class TestUtils
    {
        public static HttpResponseMessage RespondWithJson(this ICateHttp cate,
                                                        string content)
        {
            var response = new HttpResponseMessage { Content = new JsonBody(content) };
            MockHandler(cate).FakeResponse = response;
            return response;
        }

        public static HttpResponseMessage RespondWithXml(
            this ICateHttp cate, string content)
        {
            var response = new HttpResponseMessage { Content = new XmlBody(content) };
            MockHandler(cate).FakeResponse = response;
            return response;
        }

        public static HttpResponseMessage AndStatusCode(this HttpResponseMessage response, HttpStatusCode sc)
        {
            response.StatusCode = sc;
            return response;
        }
    }
}
