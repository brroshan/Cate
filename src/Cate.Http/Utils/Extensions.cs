using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cate.Http.Utils
{
    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> action,
                                   Func<T, bool> continueWhen = null,
                                   Func<T, bool> breakWhen = null)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in source) {
                if (continueWhen != null && continueWhen(item)) continue;
                if (breakWhen != null && breakWhen(item)) break;
                action(item);
            }
        }

        public static T Clone<T>(this object source) where T : new()
        {
            if (source == null) return default(T);
            var target = new T();

            var sourceProps = source.GetType().GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);
            var targetProps = target.GetType().GetProperties(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            foreach (var sourceProp in sourceProps) {
                var targetProp = targetProps
                    .FirstOrDefault(tp => tp.Name.Equals(sourceProp.Name) &&
                                          tp.PropertyType.IsAssignableFrom(sourceProp
                                              .PropertyType));

                targetProp?.SetValue(target, sourceProp.GetValue(source, null));
            }

            return target;
        }
    }
}