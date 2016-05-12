using System;
using System.Collections.Generic;

namespace US.WordProcessor.Internal
{
   internal static class EnumerableExtensions
   {
      public static IEnumerable<TResult> SelectTuplewise<TSource, TResult>(this IEnumerable<TSource> source,
         Func<Optional<TSource>, TSource, Optional<TSource>, TResult> resultSelector)
      {
         var queue = new Queue<TSource>(source);

         var previous = Optional.Empty<TSource>();
         while (queue.Count > 0)
         {
            var current = queue.Dequeue();
            var next = Optional.Empty<TSource>();

            if (queue.Count > 0)
            {
               next = Optional.Of(queue.Peek());
            }

            yield return resultSelector(previous, current, next);

            previous = current;
         }
      }
   }
}
