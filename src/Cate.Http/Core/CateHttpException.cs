using System;

namespace Cate.Http.Core
{
    public class CateHttpException : Exception
    {
        public CateHttpException(CateHttpContext context, string message,
                                 Exception inner) : base(message, inner)
        {
            Context = context;
        }

        public CateHttpException(CateHttpContext context, Exception inner) : this(context,
            ToMessage(context, inner),
            inner)
        { }

        public CateHttpException(CateHttpContext context) : this(context,
            ToMessage(context, null),
            null)
        { }

        public CateHttpContext Context { get; }

        private static string ToMessage(CateHttpContext context, Exception inner)
        {
            return context.Response == null
                ? $"The request to {context.Uri.AbsoluteUri} ended without a response form the remote machine. {inner?.Message} {inner?.GetBaseException().Message}"
                : $"The request to {context.Uri.AbsoluteUri} ended with statuscode {(int)context.Response.StatusCode} {context.Response.ReasonPhrase}.";
        }
    }

    public class CateSerializerException : Exception
    {
        public CateSerializerException(CateHttpContext context,
                                       string message, Exception inner) : base(message,
            inner)
        {
            Context = context;
        }

        public CateHttpContext Context { get; }

        public CateSerializerException(CateHttpContext context, Type type,
                                       Exception inner) : this(context,
            $"Unable to deserialize the data into {type.Namespace}.{type.Name}.", inner)
        { }
    }
}