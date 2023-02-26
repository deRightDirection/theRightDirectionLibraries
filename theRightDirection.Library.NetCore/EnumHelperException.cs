using System;

namespace theRightDirection
{
    public class EnumHelperException : Exception
    {
        public EnumHelperException(string errormessage) : base(errormessage)
        {
        }
    }
}