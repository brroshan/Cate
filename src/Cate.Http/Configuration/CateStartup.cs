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
                    if (configuration.OnStartAsync == null) return TaskCompleted;
                    return configuration.OnStartAsync(context);
                case EventType.Error:
                    if (configuration.OnErrorAsync == null) return TaskCompleted;
                    return configuration.OnErrorAsync(context);
                case EventType.Ended:
                    if (configuration.OnEndedAsync == null) return TaskCompleted;
                    return configuration.OnEndedAsync(context);
                default:
                    return TaskCompleted;
            }
        }

        private static Task<object> TaskCompleted => Task.FromResult<object>(null);
    }
}