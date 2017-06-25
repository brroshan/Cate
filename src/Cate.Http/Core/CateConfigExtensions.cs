using System;
using System.Net;
using System.Xml;
using Cate.Http.Serializers;
using Cate.Http.Utils;

namespace Cate.Http.Core
{
    public static class CateConfigExtensions
    {
        public static ICateHttp WithBasicAuth(this ICateHttp cate, string username,
                                              string password)
        {
            cate.Client.SetBasicAuthHeader(username, password);
            return cate;
        }

        public static ICateHttp WithOAuth(this ICateHttp cate, string token)
        {
            cate.Client.SetOAuthHeader(token);
            return cate;
        }

        public static ICateHttp WithJsonSerializer<T>(this ICateHttp cate)
            where T : ISerializer, new()
        {
            cate.Configuration.JsonSerializer = new T();
            return cate;
        }

        public static ICateHttp WithXmlSerializer<T>(this ICateHttp cate)
            where T : ISerializer, new()
        {
            cate.Configuration.XmlSerializer = new T();
            return cate;
        }

        public static ICateHttp WithXmlWriterSettings(
            this ICateHttp cate, XmlWriterSettings settings)
        {
            cate.Configuration.XmlSerializer.Settings = settings;
            return cate;
        }

        public static ICateHttp WithBaseAddress(this ICateHttp cate, string uri)
        {
            cate.Configuration.BaseAddress = new Uri(uri);
            return cate;
        }

        public static ICateHttp WithTimeout(this ICateHttp cate, int seconds)
        {
            cate.Configuration.Timeout = TimeSpan.FromSeconds(seconds);
            return cate;
        }

        public static ICateHttp WithProxy(this ICateHttp cate, IWebProxy proxy)
        {
            cate.Configuration.Proxy = proxy;
            return cate;
        }

        public static ICateHttp UsePreAuthentication(this ICateHttp cate, bool preAuthenticate)
        {
            cate.Configuration.PreAuthenticate = preAuthenticate;
            return cate;
        }

        public static ICateHttp WithCredentials(this ICateHttp cate, ICredentials cc)
        {
            cate.Configuration.Credentials = cc;
            return cate;
        }
    }
}