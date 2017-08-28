using System.Net;
using Cate.Http.Core;
using Cate.Http.Test;
using Cate.Http.Factories;
using Cate.Tests.Fakes;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Cate.Tests
{
    [TestFixture]
    public class HttpResponseMessageExtensionsTests
    {
        private CateHttp _cate;

        [SetUp]
        public void Init()
        {
            _cate = new CateHttp { Configuration = { Factory = new MockFactory() } };
        }

        [Test]
        public void ReceiveString_WhenCalled_Returns_Response_Json_As_String()
        {
            const string expected = "{\"Test\":\"data\"}";

            _cate.RespondWithJson(expected)
                 .AndStatusCode(HttpStatusCode.OK);

            var response = _cate.GetJson("http://fake.url.com")
                                .ReceiveString();

            Assert.AreEqual(expected, response);
        }

        [Test]
        public void ReceiveString_WhenCalled_Returns_Response_Xml_As_String()
        {
            const string expected = @"<FakeData>
                                         <Id>1</Id>
                                         <Data>Test</Data>
                                      </FakeData>";

            _cate.RespondWithXml(expected)
                 .AndStatusCode(HttpStatusCode.OK);

            var response = _cate.GetXml("http://fake.url.com")
                                .ReceiveString();

            Assert.AreEqual(expected, response);
        }

        [Test]
        public void ReceiveAs_WhenCalled_Returns_Response_Json_As_Typed_Object()
        {
            var expected = new FakeData
                           {
                               Id = 1,
                               Data = "Test"
                           };

            _cate.RespondWithJson("{\"Id\":\"1\",\"Data\":\"Test\"}")
                 .AndStatusCode(HttpStatusCode.OK);

            var response = _cate.GetJson("http://fake.url.com")
                                .ReceiveAs<FakeData>();

            Assert.AreEqual(expected, response);
        }

        [Test]
        public void ReceiveAs_WhenCalled_Returns_Response_Xml_As_Typed_Object()
        {
            var expected = new FakeData
                           {
                               Id = 1,
                               Data = "Test"
                           };

            _cate.RespondWithXml(@"<FakeData>
                                     <Id>1</Id>
                                     <Data>Test</Data>
                                   </FakeData>")
                 .AndStatusCode(HttpStatusCode.OK);

            var response = _cate.GetXml("http://fake.url.com")
                                .ReceiveAs<FakeData>();

            Assert.AreEqual(expected, response);
        }
    }
}