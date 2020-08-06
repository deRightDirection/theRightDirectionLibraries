using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library
{
    public static partial class Extensions
    {
        public static string AggregateMessages(this Exception ex)
        {
            return ex.GetInnerExceptions()
                .Aggregate(
                    new StringBuilder(),
                    (sb, e) => sb.AppendLine(e.Message),
                    sb => sb.ToString());
        }

        public static IEnumerable<Exception> GetInnerExceptions(this Exception ex, int maxDepth = 5)
        {
            if (ex == null || maxDepth <= 0)
            {
                yield break;
            }

            yield return ex;

            if (ex is AggregateException ax)
            {
                foreach (var i in ax.InnerExceptions.SelectMany(ie => GetInnerExceptions(ie, maxDepth - 1)))
                    yield return i;
            }

            foreach (var i in GetInnerExceptions(ex.InnerException, maxDepth - 1))
                yield return i;
        }
    }
}