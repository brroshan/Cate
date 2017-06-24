using System;
using System.Threading.Tasks;
using Cate.Http.Core;

namespace Cate.Http.Configuration
{
    public static class CateStartup
    {
        public static Registry Registry { get; } = Registry.Instance;

        internal static Task Emit(EventType on, CateHttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var configuration = context.Configuration;

            switch (on) {
                case EventType.Start:
                    return configuration.OnStartAsync(context);
                case EventType.Error:
                    return configuration.OnErrorAsync(context);
                case EventType.Ended:
                    return configuration.OnEndedAsync(context);
                default:
                    return Task.FromResult<object>(null);
            }
        }
    }
}