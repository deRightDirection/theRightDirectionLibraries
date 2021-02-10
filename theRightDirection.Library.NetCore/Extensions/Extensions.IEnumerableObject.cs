using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public static partial class Extensions
    {
        public static int GetListHashCode(this IEnumerable<object> sequence)
        {
            if (!sequence.Any())
            {
                return 1;
            }
            return sequence.Where(x => x != null)
                .Select(item => item.GetHashCode())
                .Aggregate((total, nextCode) => total ^ nextCode);
        }
    }
}