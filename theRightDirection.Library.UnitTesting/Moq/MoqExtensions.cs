using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.Language.Flow;

namespace theRightDirection.Library.UnitTesting.Moq
{
    /// <summary>
    /// Extension methods for the Moq-mocking framework
    /// </summary>
    public static class MoqExtensions
    {
        /// <summary>
        /// Setup for each invocation a new result<para></para>
        /// mock.Setup(m => m.GetNumber()).ReturnsInOrder(5, 10, 3, 0);<para></para>
        /// The first invocation returns 5, the second 10, third 3 and the last one is zero
        /// </summary>
        public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup,params TResult[] results) where T : class
        {
            setup.Returns(new Queue<TResult>(results).Dequeue);
        }
    }
}