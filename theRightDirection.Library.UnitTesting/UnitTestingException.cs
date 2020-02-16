using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace theRightDirection.Library.UnitTesting
{
    public sealed class UnitTestingException : Exception
    {
        public UnitTestingException(string message)
            : base(message)
        {

        }
    }
}