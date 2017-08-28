using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cate.Http.Content;
using Cate.Http.Core;
using Cate.Http.Utils;
using Cate.Tests.Fakes;
using NUnit.Framework;

namespace Cate.Tests
{
    [TestFixture]
    public class UtilsExtensionsTests
    {
        [Test]
        public void Accept_Adds_Json_MediaType_To_Request_Accept_Header()
        {
            var request = new HttpRequestMessage();
            request.Accept(MimeType.Json);

            var expected = new MediaTypeWithQualityHeaderValue("application/json");
            var result =
                request.Headers.Accept.First(m => Equals(m, expected));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Accept_Adds_Xml_MediaType_To_Request_Accept_Header()
        {
            var request = new HttpRequestMessage();
            request.Accept(MimeType.Xml);

            var expected = new MediaTypeWithQualityHeaderValue("application/xml");
            var result =
                request.Headers.Accept.First(m => Equals(m, expected));

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AppendTo_Throws_If_Url_IsNot_Absolute()
        {
            const string url = "/resource";
            Assert.Throws<Exception>(() => url.AppendTo(null));
        }

        [Test]
        public void AppendTo_Throws_If_Url_IsNotNullOrEmpty()
        {
            const string url = "";
            Assert.Throws<Exception>(() => url.AppendTo(null));
        }

        [Test]
        public async Task Deserialize_Returns_Default_T_When_Response_Is_Null()
        {
            var result = await Extensions.Deserialize<FakeData>(null);
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task
            Deserialize_Returns_Default_T_When_Response_Is_Null_2()
        {
            const int expected = 0;
            var result = await Extensions.Deserialize<int>(null);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void
            Deserialize_Returns_Default_T_When_Response_StatusCode_Is_Not_Success()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict);
            var result = Extensions.Deserialize<FakeData>(response).Result;
            Assert.AreEqual(result, null);
        }

        [Test]
        public void
            Deserialize_Throws_CateHttpException_For_Unsupported_MediaType()
        {
            var response =
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content =
                        new StringBody("",
                            Encoding.UTF8,
                            "application/unsupported-media"),
                    RequestMessage =
                        new HttpRequestMessage
                        {
                            RequestUri =
                                new Uri("http://fake.url.com")
                        }
                };

            Assert.That(() => Extensions.Deserialize<FakeData>(response),
                Throws.TypeOf<CateHttpException>());
        }

        [Test]
        public void
            IsJon_Returns_False_When_Response_Content_Headers_ContentType_MediaType_Contains_Json()
        {
            const bool expected = false;

            var response =
                new HttpResponseMessage
                {
                    Content =
                        new StringContent("", Encoding.UTF8,
                            "application/xml")
                };

            var result = response.IsJson();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void
            IsJon_Returns_True_When_Response_Content_Headers_ContentType_MediaType_Contains_Json()
        {
            const bool expected = true;

            var response =
                new HttpResponseMessage
                {
                    Content =
                        new StringContent("", Encoding.UTF8,
                            "application/json")
                };

            var result = response.IsJson();
            Assert.AreEqual(expected, result);
        }
    }
}